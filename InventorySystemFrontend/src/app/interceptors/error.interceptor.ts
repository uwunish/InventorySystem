import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        // Token expired or invalid — log out and redirect to login
        authService.logout();
        router.navigate(['/login']);
      }

      if (error.status === 0) {
        // Network error — server is not reachable
        console.error('Cannot reach the server. Is the API running?');
      }

      // Re-throw so individual components can handle specific errors
      return throwError(() => error);
    })
  );
};
