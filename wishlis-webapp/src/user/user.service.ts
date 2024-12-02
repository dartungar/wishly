import { Injectable } from '@angular/core';
import {AuthService} from "../auth/auth.service";
import {HttpClient} from "@angular/common/http";
import {WishlistItem} from "../wishlist/wishlistItem";
import {Observable} from "rxjs";
import {User} from "./user";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public getUser(userId: string): Observable<User> | null {
    return this.http.get<User>(`/api/users/${userId}`);
  }

  public updateUser(user: User): Observable<User> {
    return this.http.put<User>(`/api/users/${user.id}`, user);
  }

  public createUser(user: User): Observable<User> {
    return this.http.post<User>(`/api/users`, user);
  }
}
