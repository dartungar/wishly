import { Injectable } from '@angular/core';
import {AuthService} from "../auth/auth.service";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private authService: AuthService) { }

  getAuthenticatedUserId(): string | null {
    return this.authService.getAuthenticatedUserId();
  }
}
