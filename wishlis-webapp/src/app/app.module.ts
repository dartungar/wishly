import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import {AmplifyAuthenticatorModule, AuthenticatorComponent} from '@aws-amplify/ui-angular';
import {Amplify} from "aws-amplify";
import {BrowserModule} from "@angular/platform-browser";
import {AppComponent} from "./app.component";
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/compiler";


Amplify.configure({
  Auth: {
    Cognito: {
      userPoolId: "us-east-1_pDllIs8gd",
      userPoolClientId: "7hlufeo473990p368hkh48pv1t",
      identityPoolId: "<your-cognito-identity-pool-id>",
      loginWith: {
        oauth: {
          domain: "https://wishlis.auth.us-east-1.amazoncognito.com",
          scopes: [],
          redirectSignIn: ["https://localhost"],
          redirectSignOut: ["https://localhost"],
          responseType: 'token',
          providers: ["Google", "Facebook"]
        }
      },
      signUpVerificationMethod: "code",
      userAttributes: {
        email: {
          required: true,
        },
      },
      allowGuestAccess: true,
      passwordFormat: {
        minLength: 8,
        requireLowercase: true,
        requireUppercase: true,
        requireNumbers: true,
        requireSpecialCharacters: true,
      },
    },
  },
});

@NgModule({
  declarations: [],
  imports: [BrowserModule, AmplifyAuthenticatorModule, AppComponent],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
