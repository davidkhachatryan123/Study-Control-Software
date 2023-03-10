import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, map } from 'rxjs';

import { AuthService } from '../services';
import { appRoutes } from 'src/app/website/consts';

@Injectable()
export class AuthGuard implements CanActivate {
  private routers: typeof appRoutes = appRoutes;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return null/*this.authService.isLoggedIn().pipe(
      map((data: boolean) => {
        if(!data) this.router.navigate([this.routers.SETUP]);

        return data;
      }
    ))*/;
  }
}