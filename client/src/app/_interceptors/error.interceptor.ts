import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  const handleError = (error: any) => {
    const { status, error: errorData } = error;

    switch (status) {
      case 400:
        if (errorData.errors) {
          const modalStateErrors = Object.values(errorData.errors).flat();
          throw modalStateErrors;
        }
        toastr.error(errorData, status);
        break;
      case 401:
        toastr.error('Unauthorised', status);
        break;
      case 404:
        router.navigateByUrl('/not-found');
        break;
      case 500:
        const navigationExtras: NavigationExtras = {state: { error: errorData }}
        router.navigateByUrl('/server-error', navigationExtras );
        break;
      default:
        toastr.error('Something unexpected went wrong');
        break;
    }

    throw error;
  };

  return next(req).pipe(catchError(handleError));
};
