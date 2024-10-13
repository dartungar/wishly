import { Component } from '@angular/core';
import {WishlistItemsService} from "../wishlist/wishlist-items.service";
import {Observable} from "rxjs";
import {WishlistItem} from "../wishlist/wishlistItem";

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {

  constructor(private itemService: WishlistItemsService) {
  }

  getItemsForUser(id: string)  {
    this.itemService.getItemsForUser(id).subscribe(x => console.log(x));
  }
}
