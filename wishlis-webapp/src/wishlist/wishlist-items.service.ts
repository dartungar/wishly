import { Injectable } from '@angular/core';
import {Observable, of} from "rxjs";
import {WishlistItem} from "./wishlistItem";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class WishlistItemsService {

  constructor(private http: HttpClient) { }

  public getItemsForUser(userId: string): Observable<WishlistItem[]> {
    return this.http.get<WishlistItem[]>('/api/persons/1/items');
  }

}
