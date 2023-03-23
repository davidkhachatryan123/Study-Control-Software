import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { Faculty } from '../../../../models';

@Component({
  selector: 'app-dashboard-faculty-new',
  templateUrl: 'new.component.html'
})
export class NewDialogComponent {
  @Input() title: string;
  @Input() submitBtnText: string;

  @Output() onSubmit = new EventEmitter<any>();

  newFormGroup: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<NewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

    this.newFormGroup = new FormGroup({
      "faculty": new FormControl(data.model.name, [
        Validators.required, Validators.minLength(1), Validators.maxLength(100)
      ])
    });
  }

  onSubmitEvent() {
    if(this.newFormGroup.valid) {
      this.onSubmit.emit({
        id: this.data.model.id,
        model: new Faculty(
          this.data.model.id,
          this.newFormGroup.controls['faculty'].value
        )
      });
    }
  }
}