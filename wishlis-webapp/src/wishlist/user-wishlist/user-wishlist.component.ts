import {Component, Input, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist-items.service";
import {WishlistItem} from "../wishlistItem";
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {AsyncPipe, NgForOf} from "@angular/common";
import {Observable} from "rxjs";
import {UserService} from "../../user/user.service";
import {ActivatedRoute} from "@angular/router";

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
  private userId: string | null = null;

  items: Observable<WishlistItem[]>;

  constructor(private wishlistItemService: WishlistItemsService, private userService: UserService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.userId = this.route.snapshot.paramMap.get('userId');
    console.log("userId:",this.userId);
    if (this.userId === "me") {
      this.userId = this.userService.getAuthenticatedUserId();
    }

    this.items = this.wishlistItemService.getItemsForUser(this.userId!);
  }
}
