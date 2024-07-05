import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  let user: any = JSON.parse(sessionStorage.getItem('user') || '{}');
  if (user && Object.keys(user).length > 0) { return true; }

  const baseUrl: string = location.origin;
  window.location.href = `${baseUrl}`;

  return false;
};


