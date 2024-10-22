import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {createDefaultWishlistItem, WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {AsyncPipe, NgForOf} from "@angular/common";
import {UserService} from "../../user/user.service";
import {ActivatedRoute} from "@angular/router";
import {OrderListModule} from "primeng/orderlist";
import {Button} from "primeng/button";

@Component({
  selector: 'app-user-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgForOf,
    AsyncPipe,
    OrderListModule,
    Button
  ],
  templateUrl: './user-wishlist.component.html',
  styleUrl: './user-wishlist.component.css'
})
export class UserWishlistComponent implements OnInit {
  private userId: string | null = null;
  public authenticatedUserId: string | null = null;

  items: WishlistItem[] = [];

  constructor(private wishlistItemService: WishlistItemsService, private userService: UserService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.userId = this.route.snapshot.paramMap.get('userId');
    this.authenticatedUserId = this.userService.getAuthenticatedUserId();
    if (this.userId === "me") {
      this.userId = this.authenticatedUserId;
    }
    this.wishlistItemService.getItemsForUser(this.userId!).subscribe(items => {
      this.items = items;
    });
  }

  addItem(): void {
    if (this.authenticatedUserId !== null) {
      this.items = this.items.concat(createDefaultWishlistItem(this.authenticatedUserId));
    }
  }

  removeItem(itemId: string): void {
    this.items = this.items.filter(item => item.id !== itemId);
  }
}
