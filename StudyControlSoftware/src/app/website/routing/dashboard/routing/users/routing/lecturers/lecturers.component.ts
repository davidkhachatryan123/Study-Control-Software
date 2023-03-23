import { Component } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TableOptions } from 'src/app/website/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';

import { Lecturer } from '../../models';
import { NewDialogComponent } from './dialogs';
import { AuthService } from 'src/app/website/routing/auth/services';
import { LecturersService } from './services/lecturers.service';
import { UsersResponseDto } from 'src/app/website/dto/usersResponseDto';
import { HttpErrorResponse } from '@angular/common/http';
import { UserDto } from 'src/app/website/dto/userDto';

@Component({
  selector: 'app-dashboard-admins',
  templateUrl: 'lecturers.component.html'
})
export class LecturersComponent {
  data: Lecturer[] = [];
  resultsLength: number = 0;

  private userListOptions: TableOptions;
  private createDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private lecturersService: LecturersService,
    private authService: AuthService
  ) { }

  onChangeUserCard(userListOptions: TableOptions) {
    this.userListOptions = userListOptions;
    this.getUsers();
  }

  getUsers() {
    this.lecturersService.getAll(new TableOptions(
      this.userListOptions.sort,
      this.userListOptions.sortDirection,
      this.userListOptions.pageIndex,
      this.userListOptions.pageSize,
    )
    ).subscribe((data: UsersResponseDto<Lecturer>) => {
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
      data: { user: new Lecturer('', '', '', '', '', '', false, '') }
    });

    this.createDialogRef.componentInstance.title = "Create new Lecturer";
    this.createDialogRef.componentInstance.submitBtnText = "Create";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newLecturer: any) {
    this.lecturersService.create(newLecturer.user)
    .subscribe({
      next: (data: Lecturer) => {

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
    this.lecturersService.delete(id)
    .subscribe(() => {
      this._snackBar.open("User deleted!", 'Ok', {
        duration: 10000,
      });

      this.getUsers();
    });
  }

  onEdit($event: Lecturer) {
    this.openEditDialog($event);
  }
  openEditDialog(lecturer: Lecturer) {
    this.createDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { user: lecturer }
    });

    this.createDialogRef.componentInstance.title = "Edit Lecturer";
    this.createDialogRef.componentInstance.submitBtnText = "Edit";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.edit(data.id, data.user);
    });
  }
  edit(id: string, editedLecturer: UserDto) {
    this.lecturersService.edit(id, editedLecturer)
    .subscribe((data: Lecturer) => {
  
      this._snackBar.open(data.username + " user changed!", 'Ok', {
        duration: 10000,
      });
  
      this.createDialogRef.close();
      this.getUsers();
    });
  }

  onConfirmEmail($event: string) {
    console.log($event);

    this.authService.sendConfirmEmail($event)
    .subscribe((data: any) => {
    
        this._snackBar.open("Message sended!", 'Ok', {
          duration: 10000,
        });
    });
  }
}