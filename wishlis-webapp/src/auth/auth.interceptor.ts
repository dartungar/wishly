import { HttpInterceptorFn } from '@angular/common/http';
import { fetchAuthSession } from "aws-amplify/auth";


// TODO: pass auth token to API calls via fetchAuthSession()
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};
