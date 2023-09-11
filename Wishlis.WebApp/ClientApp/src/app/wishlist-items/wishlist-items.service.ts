import { Injectable } from '@angular/core';
import {WishlistApiClient} from "../shared/wishlist-api-client.service";
import {Observable} from "rxjs";
import {WishlistItem} from "./wishlistItem";

@Injectable({
  providedIn: 'root'
})
export class WishlistItemsService {

  private routeName = "/items";
  constructor(private _api: WishlistApiClient) { }

  public getAll(): Observable<WishlistItem[]> {
    return this._api.getAll<WishlistItem>(this.routeName);
  }

  public get(id: number): Observable<WishlistItem> {
    return this._api.get<WishlistItem>(this.routeName, id);
  }

  public update(entity: WishlistItem): Observable<any> {
    return this._api.update<WishlistItem>(this.routeName, entity);
  }

  public add(entity: WishlistItem): Observable<any> {
    return this._api.add<WishlistItem>(this.routeName, entity);
  }

  public delete(id: number): Observable<any> {
    return this._api.delete(this.routeName, id);
  }
}
