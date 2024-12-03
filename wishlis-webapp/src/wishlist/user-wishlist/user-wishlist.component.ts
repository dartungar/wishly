import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {createDefaultWishlistItem, WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {NgForOf} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {DataViewModule} from 'primeng/dataview';
import {Button} from "primeng/button";
import {AuthService} from "../../auth/auth.service";
import {User} from "../../user/user";

@Component({
  selector: 'app-user-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgForOf,
    DataViewModule,
    Button
  ],
  templateUrl: './user-wishlist.component.html',
  styleUrls: ['./user-wishlist.component.css']
})
export class UserWishlistComponent implements OnInit {
  private userId: string | null = null;
  public authenticatedUser: User | undefined;

  items: WishlistItem[] = [];

  constructor(private wishlistItemService: WishlistItemsService, private authService: AuthService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.userId = this.route.snapshot.paramMap.get('userId');

    if (!this.userId) {
      // TODO: show error
      return;
    }

    if (this.userId === "me") {
      this.authService.authenticatedUser$.subscribe(user => {
        if (!user) {
          // TODO: show error or prevent to get to /me for unauthenticated users
          return;
        }
        this.authenticatedUser = user;
        this.subscribeToUserItems(user!.id);
      });
    } else {
      this.subscribeToUserItems(this.userId);
    }
  }

  private subscribeToUserItems(userId: string) {
    this.wishlistItemService.getItemsForUser(userId).subscribe(items => {
      this.items = items;
    });
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
