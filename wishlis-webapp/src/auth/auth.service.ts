import { Injectable } from '@angular/core';
import {AuthenticatorService} from "@aws-amplify/ui-angular";
import {Hub} from "@aws-amplify/core";
import {fetchAuthSession} from "aws-amplify/auth";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuthenticated = false;
  public userName: string | undefined = undefined;
  public userToken: string | undefined = undefined;

  constructor(private authenticator: AuthenticatorService) {
    this.subscribeToAmplifyEvents();
  }

  public signOut() {
    this.authenticator.signOut();
  }

  private subscribeToAmplifyEvents() {
    Hub.listen('auth', async ({ payload }) => {
      switch (payload.event) {
        case 'signedIn':
          console.log('user have been signedIn successfully.');
          this.isAuthenticated = true;
          this.userName = this.authenticator.username;

          let session = await fetchAuthSession();
          this.userToken = session.tokens?.accessToken.toString()
          break;
        case 'signedOut':
          console.log('user have been signedOut successfully.');
          this.isAuthenticated = false;
          this.userName = undefined;
          break;
        case 'tokenRefresh':
          console.log('auth tokens have been refreshed.');
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
