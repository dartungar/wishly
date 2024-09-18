import { NgModule} from '@angular/core';
import {AmplifyAuthenticatorModule, AuthenticatorComponent} from '@aws-amplify/ui-angular';
import {BrowserModule} from "@angular/platform-browser";
import {AppComponent} from "./app.component";
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/compiler";

@NgModule({
  declarations: [],
  imports: [BrowserModule, AmplifyAuthenticatorModule, AppComponent],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
