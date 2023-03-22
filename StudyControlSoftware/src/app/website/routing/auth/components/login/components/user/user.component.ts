import { Component, Output, EventEmitter, } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { AuthService } from '../../../../services';
import { User } from '../../../../models';
import { ResponseModel } from 'src/app/website/models';
import { LoginDto } from 'src/app/website/dto';
import { catchError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'login-user',
  templateUrl: 'user.component.html',
  styleUrls: [ 'user.component.css' ]
})

export class UserComponent {
  @Output() loginForm: FormGroup;
  @Output() nextEvent = new EventEmitter<boolean>();

  constructor(
    private authService: AuthService,
    private _snackBar: MatSnackBar,
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
        next: (data: string) => {
          this.nextEvent.emit();
        },
        error: (error: HttpErrorResponse) => {
          this._snackBar.open(error.error, 'Ok', {
            duration: 10000,
          });
        }
      });
    }
  }
}