import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { delay, Subscription, of } from 'rxjs';

import { environment } from 'src/environments/environment';
import { AppUser } from '../routing/auth/models';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  tokenSubscription = new Subscription()
  timeout;

  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router) { }
  
  clean(): void {
    window.localStorage.clear();
  }
  
  public saveToken(token: string) {
    this.timeout = this.jwtHelper.getTokenExpirationDate(token).valueOf() - new Date().valueOf();
    this.expirationCounter(this.timeout);

    window.localStorage.removeItem(environment.sessionStorageConfig.TOKEN_KEY);
    window.localStorage.setItem(
      environment.sessionStorageConfig.TOKEN_KEY,
      token);
  }
  expirationCounter(timeout) {
    this.tokenSubscription.unsubscribe();
    this.tokenSubscription = of(null).pipe(delay(timeout)).subscribe((expired) => {
      console.log('EXPIRED!!');

      this.clean();
      this.router.navigate(["/auth/login"]);
    });
  }

  public static getToken(): string {
    return window.localStorage.getItem(environment.sessionStorageConfig.TOKEN_KEY);
  }

  public saveUser(user: AppUser): void {
    window.localStorage.removeItem(environment.sessionStorageConfig.USER_KEY);
    window.localStorage.setItem(
      environment.sessionStorageConfig.USER_KEY,
      JSON.stringify(user));
  }

  public getUser(): AppUser {
    const user = window.localStorage.getItem(environment.sessionStorageConfig.USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return null;
  }

  public isLoggedIn(): boolean {
    const user = window.localStorage.getItem(environment.sessionStorageConfig.USER_KEY);
    const token = window.localStorage.getItem(environment.sessionStorageConfig.TOKEN_KEY);

    if (user && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }

    this.clean();
    return false;
  }
}