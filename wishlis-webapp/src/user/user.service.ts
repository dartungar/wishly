import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BehaviorSubject, catchError, Observable, of, switchMap, tap} from "rxjs";
import {User} from "./user";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private authenticatedUser = new BehaviorSubject<User | undefined | null>(null);
  public authenticatedUser$ = this.authenticatedUser.asObservable();
  private favoriteUsers = new BehaviorSubject<User[] | undefined>(undefined);
  public favoriteUsers$: Observable<User[] | undefined> = this.favoriteUsers.asObservable();

  constructor(private http: HttpClient) {
  }

  public clearAuthenticatedUser(): void {
    this.authenticatedUser.next(undefined);
    this.favoriteUsers.next(undefined);
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
    return this.http.put<void>(`/api/users/${user.id}`, user).pipe(
      tap(() => {
        if (user.id === this.authenticatedUser.getValue()?.id) {
          this.authenticatedUser.next(user);
        }
      }),
      catchError(error => {
        console.error('Error updating user:', error);
        return of(undefined);
      })
    );
  }

  public createUser(user: User): Observable<User> {
    return this.http.post<User>(`/api/users`, user);
  }

  public getFavoriteUsers(currentUserId: string): Observable<User[]> {
    return this.http.get<User[]>(`/api/users/${currentUserId}/favorite-users`);
  }

  public searchUsers(query: string): Observable<User[]> {
    if (!query?.trim()) {
      return of([]);
    }

    const encodedQuery = encodeURIComponent(query.trim());
    return this.http.get<User[]>(`/api/users?q=${encodedQuery}`).pipe(
      catchError(error => {
        console.error('Error searching users:', error);
        return of([]);
      })
    );
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
