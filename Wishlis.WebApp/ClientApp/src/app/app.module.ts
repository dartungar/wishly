import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './shared/home/home.component';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import {MyListComponent} from "./users/my-list/my-list.component";
import {SettingsComponent} from "./users/settings/settings.component";
import {LoginComponent} from "./auth/login/login.component";
import {FavoriteUsersComponent} from "./users/favorite-users/favorite-users.component";
import {AuthGuard} from "./auth/auth.guard";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavBarComponent,
    NotFoundComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'my-list', component: MyListComponent, canActivate: [AuthGuard] },
      { path: 'favorite-users', component: FavoriteUsersComponent, canActivate: [AuthGuard] },
      { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
      { path: 'sign-out', component: LoginComponent, canActivate: [AuthGuard] },
      { path: 'sign-in', component: LoginComponent },
      { path: 'sign-up', component: LoginComponent },
      { path: '**', component: NotFoundComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
