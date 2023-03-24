import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { Faculty } from '../../../../../education/models';
import { SetFacultyDialogComponent } from '../../dialogs/set-faculty/set-faulty-dialog.component';
import { StudentsService } from '../../services/students.service';

@Component({
  selector: 'app-dashboard-set-faculty',
  templateUrl: 'set-faculty.component.html'
})
export class SetFacultyComponent implements OnInit {
  @Input() studentIndex: string;
  @Input() allFaculties: Faculty[] = [];

  faculty: Faculty | null;

  private setFacultyDialogRef: MatDialogRef<SetFacultyDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private studentsService: StudentsService
  ) { }

  ngOnInit() {
    this.getData();
  }
  
  getData() {
    this.studentsService.getFaculty(this.studentIndex)
    .subscribe((data: Faculty) => {
      this.faculty = data;
    });
  }

  setFaculty() {
    this.openSetFacultyDialog();
  }
  openSetFacultyDialog() {
    this.setFacultyDialogRef = this.dialog.open(SetFacultyDialogComponent, {
      width: '500px',
      data: {
        allFaculties: this.allFaculties
      }
    });

    this.setFacultyDialogRef.componentInstance.onSubmit.subscribe((faculty: Faculty) => {
      this.set(faculty);
    });
  }
  set(faculty: Faculty) {
    this.studentsService.setFaculty(this.studentIndex, faculty.id)
    .subscribe((data: Faculty) => {
      this._snackBar.open("Faculty set successful!", 'Ok', {
        duration: 10000,
      });

      this.setFacultyDialogRef.close();
      this.getData();
    });
  }

  removeFaculty() {
    this.openDeleteDialog();
  }
  openDeleteDialog() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: this.faculty.name, isDelete: 'false' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.delete();
    });
  }
  delete() {
    this.studentsService.deleteFaculty(this.studentIndex)
    .subscribe(() => {
      this._snackBar.open("Faculty deleted successful!", 'Ok', {
        duration: 10000,
      });

      this.setFacultyDialogRef?.close();
      this.getData();
    });
  }
}