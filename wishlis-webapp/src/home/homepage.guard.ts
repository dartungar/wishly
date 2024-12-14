import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "../auth/auth.service";
import {map, take} from "rxjs";

export const homepageGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.isAuthenticated$.pipe(take(1), map(isAuthenticated => {
    if (isAuthenticated) {
      router.navigate(["/users/me"]);
      return false;
    }
    return true;
  }))
};
