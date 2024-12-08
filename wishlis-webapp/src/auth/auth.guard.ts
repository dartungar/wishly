import { CanActivateFn } from '@angular/router';
import {AuthService} from "./auth.service";
import {inject} from "@angular/core";
import {map, take} from "rxjs";

export const authGuard: CanActivateFn = (route, state) => {
  return inject(AuthService).isAuthenticated$.pipe(take(1),
    map(isAuthenticated => {
      return isAuthenticated;
    }));
};
