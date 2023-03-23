import { Component } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TableOptions } from 'src/app/website/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';

import { Admin } from '../../models';
import { NewDialogComponent } from './dialogs';
import { HttpErrorResponse } from '@angular/common/http';
import { AdminDto } from 'src/app/website/dto/adminDto';
import { AuthService } from 'src/app/website/routing/auth/services';
import { AdminsService } from './services/admins.service';
import { TableResponseDto } from 'src/app/website/dto/usersResponseDto';

@Component({
  selector: 'app-dashboard-admins',
  templateUrl: 'admins.component.html'
})
export class AdminsComponent {
  data: Admin[] = [];
  resultsLength: number = 0;

  private userListOptions: TableOptions;
  private createDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private adminsService: AdminsService,
    private authService: AuthService
  ) { }

  onChangeUserCard(userListOptions: TableOptions) {
    this.userListOptions = userListOptions;
    this.getUsers();
  }

  getUsers() {
    this.adminsService.getAll(new TableOptions(
      this.userListOptions.sort,
      this.userListOptions.sortDirection,
      this.userListOptions.pageIndex,
      this.userListOptions.pageSize
    )).subscribe((data: TableResponseDto<Admin>) => {
      this.data = data.entities;
      this.resultsLength = data.totalCount;
    });
  }

  onNew() {
    this.openNewDialog();
  }
  openNewDialog() {
    this.createDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { user: new AdminDto('', '', '', '', '', '') }
    });

    this.createDialogRef.componentInstance.title = "Create new Admin";
    this.createDialogRef.componentInstance.submitBtnText = "Create";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newAdmin: any) {
    this.adminsService.create(newAdmin.user)
    .subscribe({
      next: (data: Admin) => {

        this._snackBar.open("User created successful!", 'Ok', {
          duration: 10000,
        });

        this.createDialogRef.close();
        this.getUsers();
      },
      error: (error: HttpErrorResponse) => {
        this._snackBar.open("Validating error!", 'Ok', {
          duration: 10000,
        });
      }
    });
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
    this.adminsService.delete(id)
    .subscribe(() => {
      this._snackBar.open("User deleted!", 'Ok', {
        duration: 10000,
      });

      this.getUsers();
    });
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

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.edit(data.id, data.user);
    });
  }
  edit(id: string, editedUser: AdminDto) {
    this.adminsService.edit(id, editedUser)
    .subscribe((data: Admin) => {
  
      this._snackBar.open(data.username + " user changed!", 'Ok', {
        duration: 10000,
      });
  
      this.createDialogRef.close();
      this.getUsers();
    });
  }

  onConfirmEmail($event: string) {
    this.authService.sendConfirmEmail($event)
    .subscribe((data: any) => {
    
        this._snackBar.open("Message sended!", 'Ok', {
          duration: 10000,
        });
    });
  }
}