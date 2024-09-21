import { Injectable } from '@angular/core';
import {Observable, of} from "rxjs";
import {WishlistItem} from "./wishlistItem";

@Injectable({
  providedIn: 'root'
})
export class WishlistItemsService {

  constructor() { }

  public getItemsForUser(userId: string): Observable<WishlistItem[]> {
    return of([
      {
        name: "test",
        userId: "123",
        url: "https://google.com"
      }
    ])
  }

}
