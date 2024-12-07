import {Component, Input, OnInit} from '@angular/core';
import {UserService} from "../../user/user.service";
import {Button} from "primeng/button";
import {NotificationService} from "../../common/notification.service";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-favorite-button',
  standalone: true,
  imports: [
    Button,
    NgIf
  ],
  templateUrl: './favorite-button.component.html',
  styleUrl: './favorite-button.component.css'
})
export class FavoriteButtonComponent implements OnInit {
  @Input() isFavorite: boolean;
  @Input() userId: string;
  currentUserId: string | undefined;

  constructor(private userService: UserService, private notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.userService.authenticatedUser$.subscribe(user => {
      this.currentUserId = user?.id;
    })
  }

  toggleFavorite() {
    if (!this.currentUserId)
      return;
    if (this.isFavorite) {
      this.userService.removeUserFromFavorites(this.currentUserId, this.userId).subscribe(_ => {
        this.notificationService.showInfo("Removed from favorites", "Removed user from favorites.");
      });
    } else {
      this.userService.addUserToFavorites(this.currentUserId, this.userId).subscribe(_ => {
        this.notificationService.showSuccess("Added to favorites", "Added user to favorites.");
      });
    }
  }
}
