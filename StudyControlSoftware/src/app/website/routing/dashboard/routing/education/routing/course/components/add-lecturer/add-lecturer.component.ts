import { AfterContentInit, Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { Course } from 'src/app/website/routing/dashboard/routing/education/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { Lecturer } from '../../../../../users/models';
import { SetLecturerDialogComponent } from '../../dialogs';
import { CoursesService } from '../../services';

@Component({
  selector: 'app-dashboard-add-lecturer',
  templateUrl: 'add-lecturer.component.html'
})
export class AddLecturerComponent implements OnInit {
  @Input() courseIndex: number;
  @Input() allԼecturers: Lecturer[] = [];

  lecturer: Lecturer | undefined;

  private setLecturerDialogRef: MatDialogRef<SetLecturerDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private coursesService: CoursesService
  ) { }

  ngOnInit() {
    this.getData();
  }
  
  getData() {
    this.coursesService.getLecturer(this.courseIndex)
    .subscribe((data: Lecturer) => {
      this.lecturer = data;
    });
  }

  setLecturer() {
    this.openSetLecturerDialog();
  }
  openSetLecturerDialog() {
    this.setLecturerDialogRef = this.dialog.open(SetLecturerDialogComponent, {
      width: '500px',
      data: {
        allԼecturers: this.allԼecturers
      }
    });

    this.setLecturerDialogRef.componentInstance.onSubmit.subscribe((lecturer: Lecturer) => {
      this.set(lecturer);
    });
  }
  set(lecturer: Lecturer) {
    this.coursesService.setLecturer(this.courseIndex, lecturer.id)
    .subscribe((data: Lecturer) => {
      this._snackBar.open("Lecturer set successful!", 'Ok', {
        duration: 10000,
      });

      this.setLecturerDialogRef.close();
      this.getData();
    });
  }

  removeLecturer() {
    this.openDeleteDialog();
  }
  openDeleteDialog() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: this.lecturer.firstName + ' ' + this.lecturer.lastName, isDelete: 'false' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.delete();
    });
  }
  delete() {
    this.coursesService.deleteLecturer(this.courseIndex)
    .subscribe(() => {
      this._snackBar.open("Lecturer deleted successful!", 'Ok', {
        duration: 10000,
      });

      this.setLecturerDialogRef?.close();
      this.getData();
    });
  }
}