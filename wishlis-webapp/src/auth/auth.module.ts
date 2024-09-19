import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthWrapperComponent } from './auth-wrapper/auth-wrapper.component';
import { AmplifyAuthenticatorModule } from '@aws-amplify/ui-angular';
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/core";
import {Amplify} from "aws-amplify";

Amplify.configure({
  Auth: {
    Cognito: {
      userPoolId: "us-east-1_pDllIs8gd",
      userPoolClientId: "3hsne5em3e5ga0nsvku1ba1b25",
      identityPoolId: "",
      loginWith: {
        email: true,
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
  declarations: [AuthWrapperComponent],
  imports: [
    CommonModule,
    AmplifyAuthenticatorModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    AuthWrapperComponent,
  ]
})
export class AuthModule { }
