import { NgModule} from '@angular/core';
import {AmplifyAuthenticatorModule } from '@aws-amplify/ui-angular';
import {BrowserModule} from "@angular/platform-browser";
import {AppComponent} from "./app.component";
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/core";
import {AuthModule} from "../auth/auth.module";
import {RouterOutlet} from "@angular/router";
import {NavbarComponent} from "../navbar/navbar.component";
import {RoutingModule} from "./routing.module";

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AmplifyAuthenticatorModule, AuthModule, RoutingModule, RouterOutlet, NavbarComponent],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
