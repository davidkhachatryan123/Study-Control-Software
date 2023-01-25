import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { ValidationService } from 'src/app/website/routing/auth/services';

import { NewUser } from '../../models';
import { roles } from 'src/app/website/routing/auth/models';

@Component({
  selector: 'app-dashboard-new-user-dialog',
  templateUrl: 'new-user.component.html',
  styleUrls: [ 'new-user.component.css' ]
})

export class NewUserDialogComponent {
  newUserForm: FormGroup;
  roles: typeof roles = roles;

  @Input() title: string = "";
  @Input() submitBtnText: string = "";

  @Output() onSubmit = new EventEmitter<NewUser>();

  constructor(
    public dialogRef: MatDialogRef<NewUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private validation: ValidationService
  ) {
    this.newUserForm = new FormGroup({
      "username": new FormControl(data.user.username, [
        Validators.required, Validators.minLength(5), Validators.maxLength(16),
        Validators.pattern('(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$')
      ]),
      "password": new FormControl('', [
        Validators.required, Validators.minLength(8), Validators.maxLength(64),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&=#])[A-Za-z0-9@$!%*?&=#]+$')
      ]),
      "confirmPassword": new FormControl('', [
        Validators.required, Validators.minLength(8), Validators.maxLength(64),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&=#])[A-Za-z0-9@$!%*?&=#]+$')
      ]),
      "email": new FormControl(data.user.email, [
        Validators.required, Validators.email
      ]),
      "phoneNumber": new FormControl(data.user.phoneNumber),
      "role": new FormControl(data.user.role, [
        Validators.required
      ])
    }, /*{
      validators: this.validation.MatchPassword("password", "confirmPassword")
    }*/);
  }

  onSubmitEvent() {
    if(this.newUserForm.valid) {
      this.onSubmit.emit(new NewUser(
        this.data.user.id,
        this.newUserForm.controls['username'].value,
        this.newUserForm.controls['password'].value,
        this.newUserForm.controls['email'].value,
        this.newUserForm.controls['phoneNumber'].value,
        this.newUserForm.controls['role'].value
      ));
    }
  }
}