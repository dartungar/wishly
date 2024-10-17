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
    return this.http.get<WishlistItem[]>(`/api/users/${userId}/items`);
  }

  public saveItem(item: WishlistItem): Observable<WishlistItem> {
    return this.http.put<WishlistItem>(`/api/WishlistItems/${item.id}`, item);
  }

}
