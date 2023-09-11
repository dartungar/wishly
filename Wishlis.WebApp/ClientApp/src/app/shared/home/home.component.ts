import { Component } from '@angular/core';
import {WishlistItemsService} from "../../wishlist-items/wishlist-items.service";
import {Observable} from "rxjs";
import {WishlistItem} from "../../wishlist-items/wishlistItem";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  url: string;
  items: Observable<WishlistItem[]>;
  seedCompleted = false;
  constructor(private service: WishlistItemsService) {
    this.url = "trololo";
    this.items = service.getAll();
  }

  seed() {
    //this.service.
  }
}
