import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { AppUser } from '../routing/auth/models';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() { }
  
  clean(): void {
    window.localStorage.clear();
  }
  
  public saveToken(token: string) {
    window.localStorage.removeItem(environment.sessionStorageConfig.TOKEN_KEY);
    window.localStorage.setItem(
      environment.sessionStorageConfig.TOKEN_KEY,
      token);
  }

  public getToken(): string {
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
    if (user) {
      return true;
    }

    return false;
  }
}