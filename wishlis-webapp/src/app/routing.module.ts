import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SettingsComponent} from "../settings/settings.component";
import {UserWishlistComponent} from "../wishlist/user-wishlist/user-wishlist.component";
import {SearchComponent} from "../search/search.component";
import {AuthFormComponent} from "../auth/auth-form/auth-form.component";
import {authGuard} from "../auth/auth.guard";
import {NotFoundComponent} from "../common/not-found/not-found.component";
import {routes} from "./app.routes";

@NgModule({
  imports: [RouterModule.forRoot(routes, {bindToComponentInputs: true})],
  exports: [RouterModule]
})
export class RoutingModule { }
