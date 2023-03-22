import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { map, Observable } from 'rxjs';

import { AppUser, User, TwoFA } from '../models';
import { ResponseModel } from 'src/app/website/models';
import { LoginDto } from 'src/app/website/dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = "account";

  constructor(
    private http: HttpClient) {
    this.apiUrl = environment.config.apiUrl + this.apiUrl;
  }

  login(loginDto: LoginDto) {
    return this.http.post(this.apiUrl + "/login", loginDto);
  }

  twoFA(twoFA: TwoFA) {
    return this.http.post(this.apiUrl + "/2fa", twoFA);
  }
}