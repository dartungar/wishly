import { NgModule} from '@angular/core';
import {AmplifyAuthenticatorModule } from '@aws-amplify/ui-angular';
import {BrowserModule} from "@angular/platform-browser";
import {AppComponent} from "./app.component";
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/core";
import {AuthModule} from "../auth/auth.module";

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AmplifyAuthenticatorModule, AuthModule],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
