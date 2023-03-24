import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserDto } from 'src/app/website/dto/userDto';
import { TableResponseDto } from 'src/app/website/dto/usersResponseDto';
import { TableOptions } from 'src/app/website/models';
import { AuthService } from 'src/app/website/routing/auth/services';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { Faculty } from '../../../education/models';
import { FacultyService } from '../../../education/routing/faculty/services';

import { Student } from '../../models/student';
import { NewDialogComponent } from './dialogs';
import { StudentsService } from './services/students.service';

@Component({
  selector: 'app-dashboard-students',
  templateUrl: 'students.component.html'
})
export class StudentsComponent implements OnInit {
  data: Student[] = [];
  resultsLength: number = 0;

  allFaculties: Faculty[] = [];

  private userListOptions: TableOptions;
  private createDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private studentsService: StudentsService,
    private authService: AuthService,
    private facultyService: FacultyService
  ) { }

  ngOnInit() {
    this.facultyService.getAll(new TableOptions('id', 'asc', 0, 999))
    .subscribe((data: TableResponseDto<Faculty>) => {
      this.allFaculties = data.entities;
    });
  }

  onChangeUserCard(userListOptions: TableOptions) {
    this.userListOptions = userListOptions;
    this.getUsers();
  }

  getUsers() {
    this.studentsService.getAll(new TableOptions(
      this.userListOptions.sort,
      this.userListOptions.sortDirection,
      this.userListOptions.pageIndex,
      this.userListOptions.pageSize,
    )
    ).subscribe((data: TableResponseDto<Student>) => {
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
      data: { user: new Student('', '', '', '', '', '', false, '') }
    });

    this.createDialogRef.componentInstance.title = "Create new Student";
    this.createDialogRef.componentInstance.submitBtnText = "Create";

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newStudent: any) {
    this.studentsService.create(newStudent.user)
    .subscribe({
      next: (data: Student) => {

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
    this.studentsService.delete(id)
    .subscribe(() => {
      this._snackBar.open("User deleted!", 'Ok', {
        duration: 10000,
      });

      this.getUsers();
    });
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

    this.createDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.edit(data.id, data.user);
    });
  }
  edit(id: string, editedStudent: UserDto) {
    this.studentsService.edit(id, editedStudent)
    .subscribe((data: Student) => {
  
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