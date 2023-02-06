import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { Course } from '../../../../models';

@Component({
  selector: 'app-dashboard-course-new',
  templateUrl: 'new.component.html'
})
export class NewDialogComponent {
  @Input() dialogTitle: string;
  @Input() submitBtnText: string;

  @Output() onSubmit = new EventEmitter<Course>();

  newFormGroup: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<NewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

    this.newFormGroup = new FormGroup({
      "title": new FormControl(data.model.title, [
        Validators.required, Validators.minLength(1), Validators.maxLength(64)
      ]),
      "description": new FormControl(data.model.description, [
        Validators.required, Validators.minLength(1), Validators.maxLength(256)
      ])
    });
  }

  onSubmitEvent() {
    if(this.newFormGroup.valid) {
      this.onSubmit.emit(new Course(
        this.data.model.id,
        this.newFormGroup.controls['title'].value,
        this.newFormGroup.controls['description'].value
      ));
    }
  }
}