import { Component, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

import { AuthService } from '../../../../services/auth.service';
import { ResponseModel } from 'src/app/website/models';
import { appRoutes } from 'src/app/website/consts';
import { AuthResponseDto, TwoFADto } from 'src/app/website/dto';
import { HttpErrorResponse } from '@angular/common/http';
import { StorageService } from 'src/app/website/services';
import { AppUser } from '../../../../models';

@Component({
  selector: 'login-2fa',
  templateUrl: '2fa.component.html',
  styleUrls: [ '2fa.component.css' ]
})
export class TwoFAComponent {
  @Output() twoFAForm: FormGroup;

  private routers: typeof appRoutes = appRoutes;

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute
    ) {
    this.twoFAForm = new FormGroup({
        "token": new FormControl('', [
          Validators.required, Validators.minLength(6), Validators.maxLength(6),
          Validators.pattern('[0-9]+$')
        ])
    });
  }

  submit(){
    if(this.twoFAForm.valid) {

      let username = this.route.snapshot.queryParams['username'];

      this.authService.twoFA(new TwoFADto(
        username,
        this.twoFAForm.controls['token'].value
      )).subscribe({
        next: (data: AuthResponseDto) => {
          if (data.isAuthSuccessful) {
            this.storageService.saveUser(new AppUser(username, data.email, data.role));
            this.storageService.saveToken(data.token);

            this.router.navigate([this.routers.DASHBOARD])
          }
        },
        error: (error: HttpErrorResponse) => {
          this._snackBar.open(error.error.errorMessage, 'Ok', {
            duration: 10000,
          });
        }
      });
    }
  }
}