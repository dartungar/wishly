import {Injectable} from '@angular/core';
import {AuthenticatorService} from "@aws-amplify/ui-angular";
import {Hub} from "@aws-amplify/core";
import {fetchAuthSession, getCurrentUser} from "aws-amplify/auth";
import {Router} from "@angular/router";
import {BehaviorSubject, catchError, EMPTY, take} from "rxjs";
import {UserService} from "../user/user.service";
import {createDefaultUser, User} from "../user/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authenticated = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.authenticated.asObservable();
  private authenticatedUser = new BehaviorSubject<User | undefined>(undefined);
  public authenticatedUser$ = this.authenticatedUser.asObservable();
  public userToken: string | undefined = undefined;

  constructor(private authenticator: AuthenticatorService, private router: Router, private userService: UserService) {
    this.subscribeToAmplifyEvents();
  }

  public async signOut() {
    console.log("signing out...");
    this.authenticator.signOut();

    this.authenticated.next(false);
    this.authenticatedUser.next(undefined);
  }

  public async tryGetUserFromCognitoAuthenticatorCookies() {
    await this.setAuthenticatedUserInfo();
    await this.onSignIn();
  }

  private async onSignIn(): Promise<void> {
    try {
      // Wait for auth session
      // Wait a bit for Amplify to fully process the sign-in
      //await new Promise(resolve => setTimeout(resolve, 500));
      const currentUserFromAuth = await getCurrentUser();
      const userId = currentUserFromAuth.userId;

      if (!userId) {
        console.error("User ID not set after sign in!");
        return;
      }

      // Use firstValueFrom to handle the observable in an async way
      // @ts-ignore
      this.userService.getUser(userId).pipe(
        take(1),
        catchError(error => {
          console.error('Error fetching user:', error);
          return EMPTY;
        })
      ).subscribe(user => {
        if (user) {
          // User exists
          this.authenticatedUser.next(user);
          this.router.navigate(["/"]); // Don't use await in subscribe
        } else {
          // User doesn't exist, create new user
          const newUser = createDefaultUser(
            userId,
            null, // TODO: pass from authenticator data
            null
          );

          this.userService.createUser(newUser).pipe(
            take(1),
            catchError(error => {
              console.error('Error saving user:', error);
              return EMPTY;
            })
          ).subscribe(user => {
            this.authenticatedUser.next(user);
            this.router.navigate(["/settings"]); // Don't use await in subscribe
          });
        }

        this.setAuthenticatedUserInfo(); // Don't use await in subscribe
      });
    } catch (error) {
      console.error('Error during sign in:', error);
    }
  }

  private async setAuthenticatedUserInfo() {
    let session = await fetchAuthSession();
    if (!this.authenticator.user)
      return;
    this.authenticated.next(true);

    this.userToken = session.tokens?.accessToken.toString()
  }

  public getAuthenticatedUserId(): string | null {
    return this.authenticator?.user?.userId;
  }

  private subscribeToAmplifyEvents() {
    Hub.listen('auth', async ({payload}) => {
      switch (payload.event) {
        case 'signedIn':
          console.log('user have been signedIn successfully.');
          await this.onSignIn();
          this.authenticated.next(true);
          break;
        case 'signedOut':
          console.log('user have been signedOut successfully.');
          this.authenticated.next(false);
          await this.router.navigate(["/"]);
          break;
        case 'tokenRefresh':
          console.log('auth tokens have been refreshed.');
          await this.setAuthenticatedUserInfo();
          break;
        case 'tokenRefresh_failure':
          console.log('failure while refreshing auth tokens.');
          break;
        case 'signInWithRedirect':
          console.log('signInWithRedirect API has successfully been resolved.');
          break;
        case 'signInWithRedirect_failure':
          console.log('failure while trying to resolve signInWithRedirect API.');
          break;
        case 'customOAuthState':
          //logger.info('custom state returned from CognitoHosted UI');
          break;
      }
    });
  }
}
