import { NgModule} from '@angular/core';
import {AmplifyAuthenticatorModule } from '@aws-amplify/ui-angular';
import {BrowserModule} from "@angular/platform-browser";
import {AppComponent} from "./app.component";
import {CUSTOM_ELEMENTS_SCHEMA} from "@angular/core";
import {AuthModule} from "../auth/auth.module";
import {RouterOutlet} from "@angular/router";
import {NavbarComponent} from "../common/navbar/navbar.component";
import {RoutingModule} from "./routing.module";
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import {wishlistApiInterceptor} from "../wishlist/wishlist-api.interceptor";
import {provideAnimations} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AmplifyAuthenticatorModule, AuthModule, RoutingModule, RouterOutlet, NavbarComponent],
  providers: [provideHttpClient(withInterceptors([wishlistApiInterceptor])), provideAnimations()],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
