import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SettingsComponent} from "../settings/settings.component";
import {UserWishlistComponent} from "../wishlist/user-wishlist/user-wishlist.component";
import {SearchComponent} from "../search/search.component";
import {AuthFormComponent} from "../auth/auth-form/auth-form.component";
import {authGuard} from "../auth/auth.guard";
import {NotFoundComponent} from "../common/not-found/not-found.component";

const routes: Routes = [
  { path: 'search', component: SearchComponent },
  { path: 'user/:id', component: UserWishlistComponent },
  { path: 'me', component: UserWishlistComponent, canActivate: [authGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [authGuard]  },
  { path: 'auth', component: AuthFormComponent },
  { path: 'auth/sign-in', component: AuthFormComponent },
  { path: 'auth/sign-up', component: AuthFormComponent },
  { path: '', redirectTo: "/search", pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RoutingModule { }
