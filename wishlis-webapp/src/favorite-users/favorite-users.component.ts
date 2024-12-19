import {Component, OnInit} from '@angular/core';
import {User} from "../user/user";
import {UserPreviewComponent} from "../common/user-preview/user-preview.component";
import {UserService} from "../user/user.service";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-favorite-users',
  standalone: true,
    imports: [
        UserPreviewComponent,
        NgForOf,
        NgIf
    ],
  templateUrl: './favorite-users.component.html',
  styleUrl: './favorite-users.component.css'
})
export class FavoriteUsersComponent implements OnInit {
  public favoriteUsers: User[];

  constructor(public userService: UserService) {
  }

  ngOnInit(): void {
    this.userService.favoriteUsers$.subscribe(users => {
      this.favoriteUsers = users ?? [];
    });
  }

}
