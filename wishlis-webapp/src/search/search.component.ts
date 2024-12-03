import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist/wishlist-items.service";
import {InputGroupModule} from "primeng/inputgroup";
import {InputTextModule} from "primeng/inputtext";
import {ButtonDirective} from "primeng/button";
import {from, Observable} from "rxjs";
import {User} from '../user/user';
import {SearchResultComponent} from "./search-result/search-result.component";
import {AsyncPipe, NgForOf} from "@angular/common";

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    InputGroupModule,
    InputTextModule,
    ButtonDirective,
    SearchResultComponent,
    NgForOf,
    AsyncPipe
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
        id: "12345",
        name: "Test Search Result",
        currencyCode: "USD",
        birthday: new Date(),
        isProfileSearchable: true
      }];
  }

}
