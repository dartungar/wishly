import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BehaviorSubject, catchError, Observable, of, switchMap, tap} from "rxjs";
import {User} from "./user";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private authenticatedUser = new BehaviorSubject<User | undefined>(undefined);
  public authenticatedUser$ = this.authenticatedUser.asObservable();
  private favoriteUsers = new BehaviorSubject<User[] | undefined>(undefined);
  public favoriteUsers$: Observable<User[] | undefined> = this.favoriteUsers.asObservable();

  constructor(private http: HttpClient) {
  }

  public clearAuthenticatedUser(): void {
    this.authenticatedUser.next(undefined);
  }

  public setAuthenticatedUser(user: User): void {
    this.authenticatedUser.next(user);
    this.getFavoriteUsers(user.id).subscribe(users => {
      this.favoriteUsers.next(users);
    })
  }

  public getUser(userId: string): Observable<User | undefined> {
    return this.http.get<User>(`/api/users/${userId}`).pipe(
      catchError(error => {
        console.error('Error fetching user:', error);
        return of(undefined);
      })
    );
  }

  public updateUser(user: User): Observable<void> {
    return this.http.put<void>(`/api/users/${user.id}`, user);
  }

  public createUser(user: User): Observable<User> {
    return this.http.post<User>(`/api/users`, user);
  }

  public getFavoriteUsers(currentUserId: string): Observable<User[]> {
    return this.http.get<User[]>(`/api/users/${currentUserId}/favorite-users`);
  }

  public addUserToFavorites(currentUserId: string, userToAddId: string): Observable<User[]> {
    return this.http.post<void>(`/api/users/${currentUserId}/favorite-users`, JSON.stringify(userToAddId), {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(switchMap(_ => this.getFavoriteUsers(currentUserId).pipe(tap(users => this.favoriteUsers.next(users)))
      )
    );
  }

  public removeUserFromFavorites(currentUserId: string, userToRemoveId: string): Observable<void> {
    return this.http.delete<void>(`/api/users/${currentUserId}/favorite-users/${userToRemoveId}`).pipe(
      tap(() => {
        const currentFavorites = this.favoriteUsers.getValue();
        if (currentFavorites) {
          const updatedFavorites = currentFavorites.filter(user => user.id !== userToRemoveId);
          this.favoriteUsers.next(updatedFavorites);
        }
      }
    ));
  }
}
