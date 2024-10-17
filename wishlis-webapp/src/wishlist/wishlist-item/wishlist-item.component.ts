import {Component, Input, OnInit} from '@angular/core';
import {createDefaultWishlistItem, WishlistItem} from "../wishlistItem";
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {WishlistItemsService} from "../wishlist-items.service";
import {UserService} from "../../user/user.service";

@Component({
  selector: 'app-wishlist-item',
  standalone: true,
  imports: [
    NgIf,
    FormsModule
  ],
  templateUrl: './wishlist-item.component.html',
  styleUrl: './wishlist-item.component.css'
})
export class WishlistItemComponent implements OnInit {
  @Input() authenticatedUserId: string | null;
  @Input() item: WishlistItem;
  editing: boolean = false;
  belongsToAuthenticatedUser = false;

  constructor(private itemsService: WishlistItemsService) {
  }

  ngOnInit(): void {
        this.belongsToAuthenticatedUser = (this.authenticatedUserId === this.item.userId);
        this.item ??= createDefaultWishlistItem(this.authenticatedUserId ?? "");
    }

  toggleEdit(): void {
    console.log("toggling edit from: ", this.editing);
    this.editing = !this.editing;
    console.log("toggled edit to: ", this.editing);
  }

  save(): void {
    this.toggleEdit();
    this.itemsService.saveItem(this.item).subscribe(x => {
      console.log("Item saved", x);
    })
    // TODO: save via items service
  }
}
