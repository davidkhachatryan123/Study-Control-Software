import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { NewDialogComponent } from './dialogs/new/new.component';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';

import { Course, Faculty } from '../../models';
import { TableOptions } from 'src/app/website/models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FacultyService } from './services';
import { TableResponseDto } from 'src/app/website/dto/usersResponseDto';
import { HttpErrorResponse } from '@angular/common/http';
import { CoursesService } from '../course/services';

@Component({
  selector: 'app-dashboard-faculty',
  templateUrl: 'faculty.component.html'
})
export class FacultyComponent implements OnInit {
  data: Faculty[] = [];
  resultsLength: number = 0;

  allCourses: Course[] = [];

  private userListOptions: TableOptions;
  private newDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private facultyService: FacultyService,
    private coursesService: CoursesService
  ) { }

  ngOnInit() {
    this.coursesService.getAll(new TableOptions('id', 'asc', 0, 999))
    .subscribe((data: TableResponseDto<Course>) => {
      this.allCourses = data.entities;
    });
  }

  onChangeCard(userListOptions: TableOptions) {
    this.userListOptions = userListOptions;
    this.getData();
  }

  getData() {
    this.facultyService.getAll(new TableOptions(
      this.userListOptions.sort,
      this.userListOptions.sortDirection,
      this.userListOptions.pageIndex,
      this.userListOptions.pageSize
    )).subscribe((data: TableResponseDto<Faculty>) => {
      this.data = data.entities;
      this.resultsLength = data.totalCount;
    });
  }

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
  new(newFaculty: any) {
    this.facultyService.create(newFaculty.model)
    .subscribe({
      next: (data: Faculty) => {

        this._snackBar.open("Faculty created successful!", 'Ok', {
          duration: 10000,
        });

        this.newDialogRef.close();
        this.getData();
      },
      error: (error: HttpErrorResponse) => {
        this._snackBar.open("Validating error!", 'Ok', {
          duration: 10000,
        });
      }
    });
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

    this.newDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.edit(data.id, data.model);
    });
  }
  edit(id: number, faculty: Faculty) {
    this.facultyService.edit(id, faculty)
    .subscribe((data: Faculty) => {
  
      this._snackBar.open("Faculty are edited!", 'Ok', {
        duration: 10000,
      });
  
      this.newDialogRef.close();
      this.getData();
    });
  }

  onDelete($event: any) {
    this.openDeleteDialog($event);
  }
  openDeleteDialog(faculty: Faculty) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: faculty.name, isDelete: 'false' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.delete(faculty.id);
    });
  }
  delete(id: number) {
    this.facultyService.delete(id)
    .subscribe(() => {
      this._snackBar.open("Faculty deleted!", 'Ok', {
        duration: 10000,
      });

      this.getData();
    });
  }
}