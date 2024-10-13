import {Component, Input, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {AsyncPipe, NgForOf} from "@angular/common";
import {Observable} from "rxjs";

@Component({
  selector: 'app-user-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgForOf,
    AsyncPipe
  ],
  templateUrl: './user-wishlist.component.html',
  styleUrl: './user-wishlist.component.css'
})
export class UserWishlistComponent implements OnInit {
  @Input() userId: string | undefined;

  items: Observable<WishlistItem[]>;

  constructor(private wishlistItemService: WishlistItemsService) {
  }

  ngOnInit() {
    this.items = this.wishlistItemService.getItemsForUser(this.userId!);
  }
}
