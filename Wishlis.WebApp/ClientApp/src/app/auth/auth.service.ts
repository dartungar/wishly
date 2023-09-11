import { Injectable } from '@angular/core';
import {BehaviorSubject, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authorized = new BehaviorSubject<boolean>(true);
  public isAuthorized = this.authorized.asObservable();
  constructor() {
    this.authorized.next(false); // test
  }

  public fakeLogin() {
    this.authorized.next(true);
  }

  public fakeLogout() {
    this.authorized.next(false);
  }
}
