import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {createDefaultWishlistItem, WishlistItem} from "../wishlistItem";
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {WishlistItemsService} from "../wishlist-items.service";
import {UserService} from "../../user/user.service";
import {CardModule} from "primeng/card";
import {InputTextModule} from "primeng/inputtext";
import {InputNumberModule} from "primeng/inputnumber";
import {Button} from "primeng/button";
import {FloatLabelModule} from "primeng/floatlabel";
import {InputGroupModule} from "primeng/inputgroup";
import {InputGroupAddonModule} from "primeng/inputgroupaddon";
import { CommonModule } from '@angular/common';
import { CheckboxModule } from 'primeng/checkbox'; // Import PrimeNG CheckboxModule



@Component({
  selector: 'app-wishlist-item',
  standalone: true,
  imports: [
    NgIf,
    FormsModule,
    CardModule,
    InputTextModule,
    InputNumberModule,
    Button,
    FloatLabelModule,
    InputGroupModule,
    InputGroupAddonModule,
    CommonModule,
    CheckboxModule
  ],
  templateUrl: './wishlist-item.component.html',
  styleUrl: './wishlist-item.component.css'
})
export class WishlistItemComponent implements OnInit {
  @Input() authenticatedUserId: string | null;
  @Input() item: WishlistItem;
  @Output() deleteEvent = new EventEmitter<void>();
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
    this.itemsService.saveItem(this.item).subscribe();
  }

  delete(): void {
    this.itemsService.deleteItem(this.item.id).subscribe();
    this.deleteEvent.emit();
  }
}
