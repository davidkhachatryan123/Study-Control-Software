import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { AuthResponseDto, LoginDto, TwoFADto } from 'src/app/website/dto';

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
    return this.http.post<AuthResponseDto>(this.apiUrl + "/login", loginDto);
  }

  twoFA(twoFADto: TwoFADto) {
    return this.http.post<AuthResponseDto>(this.apiUrl + "/2fa", twoFADto);
  }

  sendConfirmEmail(email: string) {
    const params = new HttpParams()
    .set('email', email);

    return this.http.post(this.apiUrl + "/SendConfirmEmailMessage", null, { params: params });
  }
}