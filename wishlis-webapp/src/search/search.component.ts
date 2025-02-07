import {Component, OnInit} from '@angular/core';
import {WishlistItemsService} from "../wishlist/wishlist-items.service";
import {InputGroupModule} from "primeng/inputgroup";
import {InputTextModule} from "primeng/inputtext";
import {ButtonDirective} from "primeng/button";
import {distinctUntilChanged, from, Observable, share, tap} from "rxjs";
import {User} from '../user/user';
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {UserPreviewComponent} from "../common/user-preview/user-preview.component";
import {UserService} from "../user/user.service";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    InputGroupModule,
    InputTextModule,
    ButtonDirective,
    NgForOf,
    UserPreviewComponent,
    FormsModule,
    AsyncPipe,
    NgIf
  ],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit {
  public searchResults$: Observable<User[]> = new Observable<User[]>();
  public results: User[] | null = null;
  public query: string;

  constructor(private userService: UserService) {
  }

  search(): void {
    if (!this.query?.trim()) {
      this.searchResults$ = from([[]]);
      return;
    }

    this.searchResults$ = this.userService.searchUsers(this.query)
      .pipe(
        // Store the results locally
        tap(results => this.results = results),
        // Share the subscription
        share()
      );
  }

  ngOnInit(): void {

  }


}
