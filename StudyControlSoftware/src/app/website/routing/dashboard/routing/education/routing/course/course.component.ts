import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Course } from '../../models';

import { NewDialogComponent } from './dialogs/new/new.component';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';

@Component({
  selector: 'app-dashboard-course',
  templateUrl: 'course.component.html'
})
export class CourseComponent {
  private newDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog
  ) { }

  onNew() {
    this.openNewDialog();
  }
  openNewDialog() {
    this.newDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { model: new Course(0, '', '') }
    });

    this.newDialogRef.componentInstance.dialogTitle = "Create new Course";
    this.newDialogRef.componentInstance.submitBtnText = "Create";

    this.newDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newCourse: Course) {
    console.log(newCourse);
    /*this.usersManagmentService.createAdminUser(newUser).subscribe(
    (data: ResponseModel) => {

      this._snackBar.open(data.message, 'Ok', {
        duration: 10000,
      });

      if(data.statusCode == '200') {
        this.createDialogRef.close();
        this.getUsers();
      }
    });*/
  }

  onEdit($event: any) {
    this.openEditDialog($event);
  }
  openEditDialog(course: Course) {
    this.newDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { model: course }
    });

    this.newDialogRef.componentInstance.dialogTitle = "Edit Course";
    this.newDialogRef.componentInstance.submitBtnText = "Edit";

    this.newDialogRef.componentInstance.onSubmit.subscribe((course: Course) => {
      this.update(course);
    });
  }
  update(course: Course) {
    console.log(course);

    /*this.usersManagmentService.updateAdminUser(newUser).subscribe(
    (data: ResponseModel) => {
  
      this._snackBar.open(data.message, 'Ok', {
        duration: 10000,
      });
  
      if(data.statusCode == '200') {
        this.createDialogRef.close();
        this.getUsers();
      }
    });*/
  }

  onDelete($event: any) {
    this.openDeleteDialog($event);
  }
  openDeleteDialog(course: Course) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: course.title, isDelete: 'false' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.delete(course.id);
    });
  }
  delete(id: number) {
    console.log(id);
    /*this.usersManagmentService.deleteAdminUser(id)
    .subscribe((data: ResponseModel) => {
      this._snackBar.open(data.message, 'Ok', {
        duration: 10000,
      });

      if(data.statusCode == '200')
        this.getUsers();
    });*/
  }
}