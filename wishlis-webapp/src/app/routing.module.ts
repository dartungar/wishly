import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SettingsComponent} from "../settings/settings.component";
import {UserWishlistComponent} from "../wishlist/user-wishlist/user-wishlist.component";
import {SearchComponent} from "../search/search.component";
import {AuthFormComponent} from "../auth/auth-form/auth-form.component";



const routes: Routes = [
  { path: '', redirectTo: "/search", pathMatch: 'full' },
  { path: 'search', component: SearchComponent },
  { path: 'user/:id', component: UserWishlistComponent },
  { path: 'me', component: UserWishlistComponent },
  { path: 'settings', component: SettingsComponent },
  { path: 'auth', component: AuthFormComponent },
  { path: 'auth/sign-in', component: AuthFormComponent },
  { path: 'auth/sign-up', component: AuthFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RoutingModule { }
