import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { roles } from 'src/app/website/routing/auth/models';
import { Student } from '../../../../models/student';

@Component({
  selector: 'app-dashboard-student-new',
  templateUrl: 'new.component.html'
})

export class NewDialogComponent {
  newUserForm: FormGroup;
  roles: typeof roles = roles;

  @Input() title: string = "";
  @Input() submitBtnText: string = "";

  @Output() onSubmit = new EventEmitter<Student>();

  constructor(
    public dialogRef: MatDialogRef<NewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.newUserForm = new FormGroup({
      "surname": new FormControl(data.user.surname, [
        Validators.required, Validators.minLength(2), Validators.maxLength(255)
      ]),
      "lastname": new FormControl(data.user.lastname, [
        Validators.required, Validators.minLength(2), Validators.maxLength(255)
      ]),
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
      "phoneNumber": new FormControl(data.user.phoneNumber)
    }, /*{
      validators: this.validation.MatchPassword("password", "confirmPassword")
    }*/);
  }

  onSubmitEvent() {
    if(this.newUserForm.valid) {
      this.onSubmit.emit(new Student(
        this.data.user.id,
        this.newUserForm.controls['surname'].value,
        this.newUserForm.controls['lastname'].value,
        this.newUserForm.controls['username'].value,
        this.newUserForm.controls['password'].value,
        this.newUserForm.controls['email'].value,
        false,
        this.newUserForm.controls['phoneNumber'].value
      ));
    }
  }
}