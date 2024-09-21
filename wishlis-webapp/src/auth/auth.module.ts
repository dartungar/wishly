import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AmplifyAuthenticatorModule} from '@aws-amplify/ui-angular';
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/core";
import {Amplify} from "aws-amplify";
import {AuthFormComponent} from "./auth-form/auth-form.component";

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
                    scopes: ['openid','email','phone','profile','aws.cognito.signin.user.admin'],
                    redirectSignIn: ["https://localhost"],
                    redirectSignOut: ["https://localhost"],
                    responseType: 'token',
                    providers: ["Google", "Facebook"]
                }
            },
            signUpVerificationMethod: undefined,
            userAttributes: {
                email: { required: true },
                email_verified: {required: true},
                birthdate: { required: true },
                sub: {required: true},
            },
            // allowGuestAccess: true,
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
    declarations: [AuthFormComponent],
    imports: [
        CommonModule,
        AmplifyAuthenticatorModule,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    exports: [
        AuthFormComponent,
    ]
})
export class AuthModule {
}
