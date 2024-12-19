import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
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
import { CheckboxModule } from 'primeng/checkbox';
import {User} from "../../user/user";
import {NotificationService} from "../../common/notification.service"; // Import PrimeNG CheckboxModule



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
  @Input() authenticatedUser: User | undefined;
  @Input() item: WishlistItem;
  @Output() deleteEvent = new EventEmitter<void>();
  @ViewChild('titleInput') titleInput!: ElementRef;
  editing: boolean = false;
  belongsToAuthenticatedUser = false;

  constructor(private itemsService: WishlistItemsService, private notificationService: NotificationService) {
  }

  ngOnInit(): void {
        this.belongsToAuthenticatedUser = (this.authenticatedUser?.id === this.item.userId);
        this.item ??= createDefaultWishlistItem(this.authenticatedUser!.id, this.authenticatedUser?.currencyCode);
    }

  toggleEdit(): void {
    this.editing = !this.editing;
    if (this.editing) {
      // Delay focus to ensure the view is fully updated
      setTimeout(() => this.titleInput.nativeElement.focus(), 0);
    }
  }

  save(): void {
    this.toggleEdit();
    this.itemsService.saveItem(this.item).subscribe(_ => this.notificationService.showSuccess("Item saved", "The item has been saved."));
  }

  delete(): void {
    this.itemsService.deleteItem(this.item.userId, this.item.id).subscribe(_ => this.notificationService.showInfo("Item deleted", "The item has been deleted."));
    this.deleteEvent.emit();
  }
}
