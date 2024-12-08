import {Component, OnDestroy, OnInit} from '@angular/core';
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
import {Subject, forkJoin, takeUntil, take} from 'rxjs';

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
export class UserWishlistComponent implements OnInit, OnDestroy {
  public user: User | undefined = undefined;
  public authenticatedUser: User | undefined;
  public isFavorite: boolean;
  public items: WishlistItem[] = [];
  private destroy$ = new Subject<void>();

  constructor(
    private wishlistItemService: WishlistItemsService,
    private route: ActivatedRoute,
    private notificationService: NotificationService,
    private userService: UserService
  ) {

  }

  ngOnInit() {
    const userId = this.route.snapshot.paramMap.get('userId');

    if (!userId) {
      this.notificationService.showError("Error", "Invalid user id.");
      return;
    }

    // Get authentication state first
    this.userService.authenticatedUser$.pipe(
      take(1),
      takeUntil(this.destroy$)
    ).subscribe(authenticatedUser => {
      this.authenticatedUser = authenticatedUser;

      if (!authenticatedUser && userId === "me") {
        this.notificationService.showWarning(
          "Please sign in",
          "To view your wishlist, please sign in or sign up."
        );
        return;
      }

      // Handle authenticated user's own wishlist
      if (authenticatedUser && (authenticatedUser.id === userId || userId === "me")) {
        this.user = authenticatedUser;
        this.loadItemsForUser(authenticatedUser.id);
        return;
      }

      // Handle other user's wishlist
      this.loadOtherUserData(userId);
    });
  }

  private loadItemsForUser(userId: string) {
    this.wishlistItemService.getItemsForUser(userId).pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: (items) => {
        this.items = items;
      },
      error: (error) => {
        console.error('Error loading items:', error);
        this.notificationService.showError(
          "Error",
          "Failed to load wishlist items"
        );
      }
    });
  }

  private loadOtherUserData(userId: string) {
    forkJoin({
      user: this.userService.getUser(userId),
      items: this.wishlistItemService.getItemsForUser(userId)
    }).pipe(
      takeUntil(this.destroy$),
      take(1)
    ).subscribe({
      next: ({ user, items }) => {
        if (!user) {
          this.notificationService.showError(
            "Error",
            "User not found"
          );
          return;
        }

        this.user = user;
        this.items = items;

        // Only subscribe to favorite users if this is not the authenticated user's wishlist
        if (this.user.id !== this.authenticatedUser?.id) {
          this.userService.favoriteUsers$.pipe(
            takeUntil(this.destroy$)
          ).subscribe(favoriteUsers => {
            this.isFavorite = favoriteUsers?.some(u => u.id === this.user!.id) ?? false;
          });
        }
      },
      error: (error) => {
        console.error('Error loading user data:', error);
        this.notificationService.showError(
          "Error",
          "Failed to load user data"
        );
      }
    });
  }

  isWishlistOwnedByCurrentUser(): boolean {
    return !!this.authenticatedUser &&
      !!this.user &&
      this.user.id === this.authenticatedUser.id;
  }

  addItem(): void {
    if (this.authenticatedUser) {
      this.items = [
        ...this.items,
        createDefaultWishlistItem(
          this.authenticatedUser.id,
          this.authenticatedUser.currencyCode
        )
      ];
    }
  }

  removeItem(itemToDelete: WishlistItem): void {
    this.items = this.items.filter(item => item.id !== itemToDelete.id);
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
