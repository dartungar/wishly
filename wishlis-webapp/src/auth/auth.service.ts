import {Injectable} from '@angular/core';
import {AuthenticatorService} from "@aws-amplify/ui-angular";
import {Hub} from "@aws-amplify/core";
import {fetchAuthSession, getCurrentUser} from "aws-amplify/auth";
import {Router} from "@angular/router";
import {BehaviorSubject, catchError, EMPTY, take} from "rxjs";
import {UserService} from "../user/user.service";
import {createDefaultUser} from "../user/user";
import {NotificationService} from "../common/notification.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authenticated = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.authenticated.asObservable();
  public userToken: string | undefined = undefined;
  private isAuthenticating: boolean;

  constructor(private authenticator: AuthenticatorService,
              private router: Router,
              private userService: UserService,
              private notificationService: NotificationService) {
    this.subscribeToAmplifyEvents();
  }

  public async signOut() {
    console.log("signing out...");
    this.authenticator.signOut();

    this.authenticated.next(false);
    this.userService.clearAuthenticatedUser();
    this.notificationService.showInfo("Signed out", "You have been signed out.");
  }

  async tryGetUserFromCognitoAuthenticatorCookies() {
    if (this.isAuthenticating) {
      return;
    }

    try {
      this.isAuthenticating = true;
      await this.trySetAuthSessionFromCookies();

      if (this.authenticated.value) {
        await this.fetchAndSetAuthenticatedUser();
      } else {
        this.userService.clearAuthenticatedUser();
      }
    } finally {
      this.isAuthenticating = false;
    }
  }

  private async fetchAndSetAuthenticatedUser(): Promise<void> {
    try {
      // Wait for auth session
      // Wait a bit for Amplify to fully process the sign-in
      await new Promise(resolve => setTimeout(resolve, 1000));

      const currentUserFromAuth = await getCurrentUser();
      const userId = currentUserFromAuth.userId;

      if (!userId) {
        this.notificationService.showError(
          "Could not get user data",
          "There was an error while trying to get user data. Please try to reload the page or sign out and sign in again"
        );
        return;
      }

      this.userService.getUser(userId).pipe(
        take(1),
        catchError(error => {
          this.notificationService.showError(
            "Could not get user data",
            "There was an error while trying to get user data from the server"
          );
          return EMPTY;
        })
      ).subscribe(user => {
        if (user) {
          this.userService.setAuthenticatedUser(user);
          this.router.navigate(["/users/me"]);
          this.notificationService.showSuccess("Sign in successful", "Welcome back!");
        } else {
          // User doesn't exist, create new user

          const newUser = createDefaultUser(
            userId,
            null,
            null
          );

          this.userService.createUser(newUser).pipe(
            take(1),
            catchError(error => {
              console.error('Error creating user:', error);
              this.notificationService.showError(
                "Sign-up error",
                "There was an error while trying to create a new user. Please try to sign out and sign in again."
              );
              return EMPTY;
            })
          ).subscribe(user => {
            this.userService.setAuthenticatedUser(user);
            this.router.navigate(["/settings"], { queryParams: { isNewUser: true } });
            this.notificationService.showSuccess("Welcome to Wishlist!", "Fill out your name and birthday to get started.");
          });
        }
      });
    } catch (error) {
      this.notificationService.showError(
        "Error",
        "There was an error while fetching your data. Please try reloading the page or sign in again"
      );
    }
  }

  private async trySetAuthSessionFromCookies(): Promise<void> {
    let session = await fetchAuthSession();

    if (!session || !session.tokens || !session.tokens.accessToken) {
      this.authenticated.next(false);
      return;
    }

    this.authenticated.next(true);
    this.userToken = session.tokens?.accessToken.toString();
  }

  private subscribeToAmplifyEvents() {
    Hub.listen('auth', async ({payload}) => {
      switch (payload.event) {
        case 'signedIn':
          console.log('user have been signedIn successfully.');
          await this.tryGetUserFromCognitoAuthenticatorCookies();
          break;
        case 'signedOut':
          console.log('user have been signedOut successfully.');
          this.authenticated.next(false);
          await this.router.navigate(["/"]);
          break;
        case 'tokenRefresh':
          console.log('auth tokens have been refreshed.');
          await this.trySetAuthSessionFromCookies();
          break;
        case 'tokenRefresh_failure':
          console.log('failure while refreshing auth tokens.');
          break;
        case 'signInWithRedirect':
          console.log('signInWithRedirect API has successfully been resolved.');
          break;
        case 'signInWithRedirect_failure':
          this.notificationService.showError("Authentication error", "Failed to authenticate via social provider.");
          break;
        case 'customOAuthState':
          //logger.info('custom state returned from CognitoHosted UI');
          break;
      }
    });
  }
}
