import { HttpInterceptorFn } from '@angular/common/http';
import {inject} from "@angular/core";
import {AuthService} from "../auth/auth.service";

export const wishlistApiInterceptor: HttpInterceptorFn = (req, next) => {
  const token = inject(AuthService).userToken;
  if (token === undefined || token === null) {
    return next(req);
  }
  const newReq = req.clone({
    headers: req.headers.append('Authorization', `Bearer ${token}`),
  });
  return next(newReq);
};
