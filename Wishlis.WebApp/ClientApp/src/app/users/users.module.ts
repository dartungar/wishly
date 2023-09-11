import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsComponent } from './settings/settings.component';
import { FavoriteUsersComponent } from './favorite-users/favorite-users.component';
import { MyListComponent } from './my-list/my-list.component';



@NgModule({
  declarations: [
    SettingsComponent,
    FavoriteUsersComponent,
    MyListComponent
  ],
  imports: [
    CommonModule
  ]
})
export class UsersModule { }
