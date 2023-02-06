import { Component } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TableOptions } from 'src/app/website/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';

import { Admin } from '../../models';
import { NewDialogComponent } from './dialogs';
import { roles } from 'src/app/website/routing/auth/models';

@Component({
  selector: 'app-dashboard-admins',
  templateUrl: 'admins.component.html'
})
export class AdminsComponent {
  data: Admin[] = [
    new Admin(1, 'david', '', 'davidkhachatryan359@gmail.com', true, '+37441214803'),
    new Admin(2, 'hayk', '', 'haykkhachatryan359@gmail.com', false, '+37494214803')
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
      data: { user: new Admin(-1, '', '', '', false, '') }
    });

    this.createDialogRef.componentInstance.title = "Create new Admin";
    this.createDialogRef.componentInstance.submitBtnText = "Create";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newAdmin: Admin) {
    console.log(newAdmin);

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

  onEdit($event: Admin) {
    this.openEditDialog($event);
  }
  openEditDialog(admin: Admin) {
    this.createDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { user: admin }
    });

    this.createDialogRef.componentInstance.title = "Edit User";
    this.createDialogRef.componentInstance.submitBtnText = "Edit";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: Admin) => {
      this.edit(data);
    });
  }
  edit(editedUser: Admin) {
    console.log(editedUser);

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