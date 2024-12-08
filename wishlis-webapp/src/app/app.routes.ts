import { Routes } from '@angular/router';
import {SearchComponent} from "../search/search.component";
import {UserWishlistComponent} from "../wishlist/user-wishlist/user-wishlist.component";
import {authGuard} from "../auth/auth.guard";
import {SettingsComponent} from "../settings/settings.component";
import {AuthFormComponent} from "../auth/auth-form/auth-form.component";
import {NotFoundComponent} from "../common/not-found/not-found.component";
import {FavoriteUsersComponent} from "../favorite-users/favorite-users.component";

export const routes: Routes = [
  {
    path: 'users',
    children: [
      {
        path: ':userId',
        component: UserWishlistComponent
      }
    ]
  },
  { path: 'search', component: SearchComponent },
  { path: 'favorite-users', component: FavoriteUsersComponent },
  { path: 'settings', component: SettingsComponent, canActivate: [authGuard] },
  { path: 'auth', component: AuthFormComponent },
  { path: 'auth/sign-in', component: AuthFormComponent },
  { path: 'auth/sign-up', component: AuthFormComponent },
  //{ path: '', redirectTo: 'search', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];
