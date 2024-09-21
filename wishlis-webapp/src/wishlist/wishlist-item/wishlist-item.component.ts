import {Component, Input} from '@angular/core';
import {WishlistItem} from "../wishlistItem";

@Component({
  selector: 'app-wishlist-item',
  standalone: true,
  imports: [],
  templateUrl: './wishlist-item.component.html',
  styleUrl: './wishlist-item.component.css'
})
export class WishlistItemComponent {
  @Input() item: WishlistItem | undefined;
}
