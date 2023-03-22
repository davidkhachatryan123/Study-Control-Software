import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, map } from 'rxjs';

import { appRoutes } from 'src/app/website/consts';
import { StorageService } from 'src/app/website/services';

@Injectable()
export class AuthGuard implements CanActivate {
  private routers: typeof appRoutes = appRoutes;

  constructor(
    private router: Router,
    private storageService: StorageService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if(this.storageService.isLoggedIn())
      return true;
    else
      this.router.navigate([this.routers.AUTH]);
  }
}