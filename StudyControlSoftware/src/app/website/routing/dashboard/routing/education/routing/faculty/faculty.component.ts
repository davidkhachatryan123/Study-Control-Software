import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { NewDialogComponent } from './dialogs/new/new.component';
import { Faculty } from '../../models';

@Component({
  selector: 'app-dashboard-faculty',
  templateUrl: 'faculty.component.html'
})
export class FacultyComponent {
  private newDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog,
  ) { }

  onNew() {
    this.openNewDialog();
  }
  openNewDialog() {
    this.newDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { model: new Faculty(0, '') }
    });

    this.newDialogRef.componentInstance.title = "Create new Faculty";
    this.newDialogRef.componentInstance.submitBtnText = "Create";

    this.newDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newFaculty: Faculty) {
    console.log(newFaculty);
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
  openEditDialog(faculty: Faculty) {
    this.newDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { model: faculty }
    });

    this.newDialogRef.componentInstance.title = "Edit Faculty";
    this.newDialogRef.componentInstance.submitBtnText = "Edit";

    this.newDialogRef.componentInstance.onSubmit.subscribe((data: Faculty) => {
      this.update(data);
    });
  }
  update(faculty: Faculty) {
    console.log(faculty);

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
}