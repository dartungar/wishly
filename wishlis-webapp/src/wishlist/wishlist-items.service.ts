import { Injectable } from '@angular/core';
import {catchError, Observable, of} from "rxjs";
import {WishlistItem} from "./wishlistItem";
import {HttpClient, HttpHeaders} from "@angular/common/http";

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

  public deleteItem(userId: string, itemId: string): Observable<void> {
    return this.http.delete<void>(`/api/WishlistItems/${userId}/${itemId}`);
  }

  public updateAllItemsCurrency(userId: string, newCurrencyCode: string): Observable<void | Error> {
    return this.http.put<void>(`/api/WishlistItems/${userId}/update-currency`, JSON.stringify(newCurrencyCode), {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(
      catchError(error => {
        throw new Error(error);
      })
    );
  }

}
