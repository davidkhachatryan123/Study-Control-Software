import { Component, Output, EventEmitter, } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { AuthService } from '../../../../services';
import { User } from '../../../../models';
import { ResponseModel } from 'src/app/website/models';
import { AuthResponseDto, LoginDto } from 'src/app/website/dto';
import { catchError, combineLatest } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'login-user',
  templateUrl: 'user.component.html',
  styleUrls: [ 'user.component.css' ]
})

export class UserComponent {
  @Output() loginForm: FormGroup;
  @Output() nextEvent = new EventEmitter();

  constructor(
    private authService: AuthService,
    private _snackBar: MatSnackBar,
    private _router: Router,
    private _route: ActivatedRoute,
    ) {

    this.loginForm = new FormGroup({
        "username": new FormControl('', [
          Validators.required, Validators.minLength(5), Validators.maxLength(16),
          Validators.pattern('(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$')
        ]),
        "password": new FormControl('', [
          Validators.required, Validators.minLength(8), Validators.maxLength(64),
          Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&=#])[A-Za-z0-9@$!%*?&=#]+$')
        ])
    });
  }

  submit(){
    if(this.loginForm.valid) {

      this.authService.login(new LoginDto(
        this.loginForm.controls['username'].value,
        this.loginForm.controls['password'].value
      ))
      .subscribe({
        next: () => {

          this._router.navigate([], {
            relativeTo: this._route,
            queryParams: {
              username: this.loginForm.controls['username'].value
            },
            queryParamsHandling: 'merge',
            skipLocationChange: false
          });

          this.nextEvent.emit();
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