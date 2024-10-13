import { Routes } from '@angular/router';
import {SearchComponent} from "../search/search.component";
import {UserWishlistComponent} from "../wishlist/user-wishlist/user-wishlist.component";
import {authGuard} from "../auth/auth.guard";
import {SettingsComponent} from "../settings/settings.component";
import {AuthFormComponent} from "../auth/auth-form/auth-form.component";
import {NotFoundComponent} from "../common/not-found/not-found.component";

export const routes: Routes = [
  { path: 'search', component: SearchComponent },
  { path: 'users/:id', component: UserWishlistComponent },
  { path: 'me', component: UserWishlistComponent, canActivate: [authGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [authGuard]  },
  { path: 'auth', component: AuthFormComponent },
  { path: 'auth/sign-in', component: AuthFormComponent },
  { path: 'auth/sign-up', component: AuthFormComponent },
  { path: '', redirectTo: "/search", pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];
