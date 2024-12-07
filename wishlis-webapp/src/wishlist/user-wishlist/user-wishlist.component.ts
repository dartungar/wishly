import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {createDefaultWishlistItem, WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {DataViewModule} from 'primeng/dataview';
import {Button} from "primeng/button";
import {User} from "../../user/user";
import {NotificationService} from "../../common/notification.service";
import {UserService} from "../../user/user.service";
import {FavoriteButtonComponent} from "../../favorite-users/favorite-button/favorite-button.component";

@Component({
  selector: 'app-user-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgForOf,
    DataViewModule,
    Button,
    FavoriteButtonComponent,
    NgIf
  ],
  templateUrl: './user-wishlist.component.html',
  styleUrls: ['./user-wishlist.component.css']
})
export class UserWishlistComponent implements OnInit {
  public user: User | undefined = undefined;
  public authenticatedUser: User | undefined;
  public isFavorite: boolean;

  items: WishlistItem[] = [];

  constructor(private wishlistItemService: WishlistItemsService,
              private route: ActivatedRoute,
              private notificationService: NotificationService,
              private userService: UserService) {
  }

  ngOnInit() {
    const userId = this.route.snapshot.paramMap.get('userId');
    console.log("opening wishlist for userId", userId);

    if (!userId) {
      this.notificationService.showError("Error", "Invalid user id.")
      return;
    }


    this.userService.authenticatedUser$.subscribe(user => {
      if (!user && userId === "me") {
        this.notificationService.showWarning("Please sign in", "To view your wishlist, please sign in or sign up.")
        return;
      }

      if (user) {
        if (user.id === userId || userId === "me") {
          this.authenticatedUser = user;
          this.user = this.authenticatedUser;
          this.subscribeToUserItems(user!.id);
          return;
        }
      }

      this.subscribeToUserInfo(userId);
      this.subscribeToUserItems(userId);
    });

  }

  private subscribeToUserItems(userId: string) {
    this.wishlistItemService.getItemsForUser(userId).subscribe(items => {
      this.items = items;
    });
  }

  private subscribeToUserInfo(userId: string) {
    this.userService.getUser(userId).subscribe(user => {
      if (!user) {
        this.notificationService.showError("Error", "There was a problem getting user's information. Please try to refresh the page.")
      }
      this.user = user!;
      if (this.user?.id === this.authenticatedUser?.id) {
        return;
      }
      this.userService.favoriteUsers$.subscribe(favoriteUsers => {
        this.isFavorite = (favoriteUsers && favoriteUsers?.map(u => u.id).includes(this.user!.id)) ?? false;
      })
    });
  }

  isWishlistOwnedByCurrentUser(): boolean {
    if (!this.authenticatedUser)
      return false;
    if (!this.user)
      return false;
    return this.user.id === this.authenticatedUser.id;
  }

  addItem(): void {
    if (this.authenticatedUser) {
      this.items = this.items.concat(createDefaultWishlistItem(this.authenticatedUser.id, this.authenticatedUser.currencyCode));
    }
  }

  removeItem(itemToDelete: WishlistItem): void {
    this.items = this.items.filter(item => item.id !== itemToDelete.id);
  }
}
