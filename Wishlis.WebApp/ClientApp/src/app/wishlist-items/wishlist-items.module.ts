import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemsListComponent } from './items-list/items-list.component';
import { WishlistItemComponent } from './wishlist-item/wishlist-item.component';



@NgModule({
    declarations: [
        ItemsListComponent,
        WishlistItemComponent
    ],
    exports: [
        WishlistItemComponent
    ],
    imports: [
        CommonModule
    ]
})
export class WishlistItemsModule { }
