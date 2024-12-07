import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist/wishlist-items.service";
import {InputGroupModule} from "primeng/inputgroup";
import {InputTextModule} from "primeng/inputtext";
import {ButtonDirective} from "primeng/button";
import {from, Observable} from "rxjs";
import {User} from '../user/user';
import {AsyncPipe, NgForOf} from "@angular/common";
import {UserPreviewComponent} from "../common/user-preview/user-preview.component";

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    InputGroupModule,
    InputTextModule,
    ButtonDirective,
    NgForOf,
    UserPreviewComponent
  ],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit {
  public searchResults: User[];

  constructor() {
  }

  ngOnInit(): void {
    // TODO: subscribe to search service
    this.searchResults =
      [{
        id: "0408e448-a0d1-702a-9676-95c81e77f176",
        name: "gmail user",
        currencyCode: "USD",
        birthday: new Date(),
        isProfileSearchable: true
      },
        {
          id: "d4d824c8-e011-70a6-f141-92558d66fd25",
          name: "outlook user",
          currencyCode: "USD",
          birthday: new Date(),
          isProfileSearchable: true
        }];
  }

}
