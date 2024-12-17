import {Component, OnDestroy, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {createDefaultWishlistItem, WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {NgForOf, NgIf} from "@angular/common";
import {ActivatedRoute, Router} from "@angular/router";
import {DataViewModule} from 'primeng/dataview';
import {Button} from "primeng/button";
import {User} from "../../user/user";
import {NotificationService} from "../../common/notification.service";
import {UserService} from "../../user/user.service";
import {FavoriteButtonComponent} from "../../favorite-users/favorite-button/favorite-button.component";
import {Subject, forkJoin, takeUntil, take, filter} from 'rxjs';
import { ProgressBarModule } from 'primeng/progressbar';


@Component({
  selector: 'app-user-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgForOf,
    DataViewModule,
    Button,
    FavoriteButtonComponent,
    NgIf,
    ProgressBarModule
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
  isLoading: boolean = true;

  constructor(
    private wishlistItemService: WishlistItemsService,
    private route: ActivatedRoute,
    private router: Router,
    private notificationService: NotificationService,
    private userService: UserService
  ) {  }

  ngOnInit() {
    this.route.paramMap.pipe(
      takeUntil(this.destroy$)
    ).subscribe(params => {
      const userId = params.get('userId');

      if (!userId) {
        this.notificationService.showError("Error", "Invalid user id.");
        return;
      }

      this.loadUserData(userId);
    });
  }

  private loadUserData(userId: string) {
    this.isLoading = true;

    this.userService.authenticatedUser$.pipe(
      filter(user => user !== null),
      take(1),
      takeUntil(this.destroy$)
    ).subscribe({
      next: authenticatedUser => {
        this.authenticatedUser = authenticatedUser;

        if (!authenticatedUser && userId === "me") {
          this.notificationService.showWarning(
            "Please sign in",
            "To view your wishlist, please sign in or sign up."
          );
          this.router.navigate(["/"]);
          return;
        }

        if (authenticatedUser && (authenticatedUser.id === userId || userId === "me")) {
          this.user = authenticatedUser;
          this.loadItemsForUser(authenticatedUser.id);
          return;
        }

        this.loadOtherUserData(userId);
      },
      complete: () => this.isLoading = false
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
      },
      complete: () => {
        this.isLoading = false;
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

  getWishlistTitle(): string {
    if (this.isWishlistOwnedByCurrentUser())
      return "Your wishlist";

    return `${this.user?.name}'s wishlist`;
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

  async share() {
    if (!navigator.share) {
      this.notificationService.showWarning("Sharing not supported", "Sharing is not supported by your browser, but you can just copy the URL and send it as is!")
    }

    await navigator.share({
      url: window.location.href,
    })
  }
}
