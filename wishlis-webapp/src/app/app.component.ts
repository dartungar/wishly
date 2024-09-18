import { Component } from '@angular/core';
import {AmplifyAuthenticatorModule} from "@aws-amplify/ui-angular";
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

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    AmplifyAuthenticatorModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

}
