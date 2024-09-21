import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-user-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgForOf
  ],
  templateUrl: './user-wishlist.component.html',
  styleUrl: './user-wishlist.component.css'
})
export class UserWishlistComponent implements OnInit {
  items: WishlistItem[] = [];

  constructor(private wishlistItemService: WishlistItemsService) {
  }

  ngOnInit() {
    this.wishlistItemService.getItemsForUser("123").subscribe((items: WishlistItem[]) => {
      this.items = items;
    })
  }
}
