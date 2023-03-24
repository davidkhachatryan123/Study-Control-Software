import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Course } from '../../models';

import { NewDialogComponent } from './dialogs/new/new.component';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { TableOptions } from 'src/app/website/models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CoursesService } from './services';
import { TableResponseDto } from 'src/app/website/dto/usersResponseDto';
import { HttpErrorResponse } from '@angular/common/http';
import { Lecturer } from '../../../users/models';
import { LecturersService } from '../../../users/routing/lecturers/services/lecturers.service';

@Component({
  selector: 'app-dashboard-course',
  templateUrl: 'course.component.html'
})
export class CourseComponent implements OnInit {
  data: Course[] = [];
  resultsLength: number = 0;

  lecturers: Lecturer[] = [];

  private userListOptions: TableOptions;
  private newDialogRef: MatDialogRef<NewDialogComponent>;

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private coursesService: CoursesService,
    private lecturersService: LecturersService
  ) { }

  ngOnInit() {
    this.lecturersService.getAll(new TableOptions('id', 'asc', 0, 999))
    .subscribe((data: TableResponseDto<Lecturer>) => {
      this.lecturers = data.entities;
    });
  }

  onChangeCard(userListOptions: TableOptions) {
    this.userListOptions = userListOptions;
    this.getData();
  }

  getData() {
    this.coursesService.getAll(new TableOptions(
      this.userListOptions.sort,
      this.userListOptions.sortDirection,
      this.userListOptions.pageIndex,
      this.userListOptions.pageSize
    )).subscribe((data: TableResponseDto<Course>) => {
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
      data: { model: new Course(0, '', '') }
    });

    this.newDialogRef.componentInstance.title = "Create new Course";
    this.newDialogRef.componentInstance.submitBtnText = "Create";

    this.newDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.new(data);
    });
  }
  new(newCourse: any) {
    this.coursesService.create(newCourse.model)
    .subscribe({
      next: (data: Course) => {

        this._snackBar.open("Course created successful!", 'Ok', {
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
  openEditDialog(course: Course) {
    this.newDialogRef = this.dialog.open(NewDialogComponent, {
      width: '500px',
      data: { model: course }
    });

    this.newDialogRef.componentInstance.title = "Edit Course";
    this.newDialogRef.componentInstance.submitBtnText = "Edit";

    this.newDialogRef.componentInstance.onSubmit.subscribe((data: any) => {
      this.edit(data.id, data.model);
    });
  }
  edit(id: number, course: Course) {
    this.coursesService.edit(id, course)
    .subscribe((data: Course) => {
  
      this._snackBar.open("New course created!", 'Ok', {
        duration: 10000,
      });
  
      this.newDialogRef.close();
      this.getData();
    });
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
    this.coursesService.delete(id)
    .subscribe(() => {
      this._snackBar.open("Course deleted!", 'Ok', {
        duration: 10000,
      });

      this.getData();
    });
  }
}