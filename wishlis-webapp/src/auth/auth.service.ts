import {Injectable} from '@angular/core';
import {AuthenticatorService} from "@aws-amplify/ui-angular";
import {Hub} from "@aws-amplify/core";
import {fetchAuthSession, signOut} from "aws-amplify/auth";
import {Router} from "@angular/router";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authenticated = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.authenticated.asObservable();
  public userName: string | undefined = undefined;
  public userToken: string | undefined = undefined;

  constructor(private authenticator: AuthenticatorService, private router: Router) {
    this.subscribeToAmplifyEvents();
  }

  public async signOut() {
    console.log("signing out...");
    this.authenticator.signOut();

    this.authenticated.next(false);
    this.userName = undefined;
  }

  public async tryGetUserFromCognitoAuthenticatorCookies() {
    await this.setAuthenticatedUserInfo();
  }

  private async setAuthenticatedUserInfo() {
    let session = await fetchAuthSession();
    if (!this.authenticator.user)
      return;
    this.authenticated.next(true);
    this.userName = this.authenticator.username;

    this.userToken = session.tokens?.accessToken.toString()
  }

  public getAuthenticatedUserId(): string | null {
    //console.log(this.isAuthenticated);
    if (this.authenticated.value) {
      //console.log(this.authenticator.user);
      return this.authenticator.user.userId;
    }
    return null;
  }

  private subscribeToAmplifyEvents() {
    Hub.listen('auth', async ({payload}) => {
      switch (payload.event) {
        case 'signedIn':
          console.log('user have been signedIn successfully.');
          await this.setAuthenticatedUserInfo();
          // TODO: this should be unnecessary
          await fetchAuthSession();
          this.authenticated.next(true);
          await this.router.navigate(["/"]);
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
