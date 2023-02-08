import { Component } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TableOptions } from 'src/app/website/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';

import { Student } from '../../models/student';
import { NewDialogComponent } from '../lecturers/dialogs';

@Component({
  selector: 'app-dashboard-students',
  templateUrl: 'students.component.html'
})
export class StudentsComponent {
  data: Student[] = [
    new Student(1, 'David', 'Khachatryan', 'david', '', 'davidkhachatryan359@gmail.com', true, '+37441214803'),
    new Student(2, 'Hayk', 'Khachatryan', 'hayk', '', 'haykkhachatryan359@gmail.com', false, '+37494214803')
  ];
  resultsLength: number = 0;

  private userListOptions: TableOptions;
  private createDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    /*private usersManagmentService: UsersManagmentService,*/
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
  ) { }

  onChangeUserCard(userListOptions: TableOptions) {
    this.userListOptions = userListOptions;
    this.getUsers();
  }

  getUsers() {
    /*this.usersManagmentService.getAdminUsers(new TableOptions(
      this.userListOptions.sort,
      this.userListOptions.sortDirection,
      this.userListOptions.pageIndex,
      this.userListOptions.pageSize,
    )
    ).subscribe(data => {
      this.data = data.users;
      this.resultsLength = data.totalCount;
    });*/
  }

  onNew() {
    this.openNewDialog();
  }
  openNewDialog() {
    this.createDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { user: new Student(-1, '', '', '', '', '', false, '') }
    });

    this.createDialogRef.componentInstance.title = "Create new Student";
    this.createDialogRef.componentInstance.submitBtnText = "Create";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newStudent: Student) {
    console.log(newStudent);

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

  onDelete($event: any) {
    this.openDeleteDialog($event.id, $event.username);
  }
  openDeleteDialog(id: string, username: string) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: username, isDelete: 'false' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.delete(id);
    });
  }
  delete(id: string) {
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

  onEdit($event: Student) {
    this.openEditDialog($event);
  }
  openEditDialog(student: Student) {
    this.createDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { user: student }
    });

    this.createDialogRef.componentInstance.title = "Edit User";
    this.createDialogRef.componentInstance.submitBtnText = "Edit";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: Student) => {
      this.edit(data);
    });
  }
  edit(editedStudent: Student) {
    console.log(editedStudent);

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

  onConfirmEmail($event: string) {
    console.log($event);

    /*this.usersManagmentService.sendConfirmEmail($event).subscribe(
      (data: ResponseModel) => {
    
        this._snackBar.open(data.message, 'Ok', {
          duration: 10000,
        });
    
        if(data.statusCode == '200')
          this.getUsers();
      });*/
  }
}