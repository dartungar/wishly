import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WishlistApiClient {
  constructor(private http: HttpClient, @Inject('API_URL') private baseUrl: string) {
  }

  public getAll<T>(routeName: string): Observable<T[]> {
    return this.http.get<T[]>(this.baseUrl+routeName);
  }

  public get<T>(routeName: string, id: number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl+routeName}/${id}`);
  }

  public update<T>(routeName: string, entity: T): Observable<any> {
    return this.http.put(this.baseUrl+routeName, entity);
  }

  public add<T>(routeName: string, entity: T): Observable<any> {
    return this.http.post(this.baseUrl+routeName, entity);
  }

  public delete(routeName: string, id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl+routeName}/${id}`);
  }
}
