import { Component, Input, OnInit, Output } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { Course, Faculty } from 'src/app/website/routing/dashboard/routing/education/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { AddCourseDialogComponent } from '../../dialogs';
import { FacultyService } from '../../services';

@Component({
  selector: 'app-dashboard-add-course-list',
  templateUrl: 'add-course-list.component.html'
})
export class AddCourseListComponent implements OnInit {
  @Input() facultyIndex: number;
  @Input() allCourses: Course[] = [];

  private addCourseDialogRef: MatDialogRef<AddCourseDialogComponent>;

  addedCourses: Course[] = [];

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private facultyService: FacultyService
  ) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.facultyService.getCourses(this.facultyIndex)
    .subscribe((data: Array<Course>) => {
      this.addedCourses = data;
    });
  }

  addCourse() {
    this.openAddCourseDialog();
  }
  openAddCourseDialog() {
    this.addCourseDialogRef = this.dialog.open(AddCourseDialogComponent, {
      width: '500px',
      data: {
        allCourses: this.allCourses
      }
    });

    this.addCourseDialogRef.componentInstance.onSubmit.subscribe((courses: Course[]) => {
      this.add(courses);
    });
  }
  add(courses: Course[]) {
    this.facultyService.addCourses(this.facultyIndex, courses.map(c => c.id))
    .subscribe(() => {
      this._snackBar.open("Course(s) added successful!", 'Ok', {
        duration: 10000,
      });

      this.addCourseDialogRef.close();
      this.getData();
    });
  }

  deleteCourse(course: Course) {
    this.openDeleteDialog(course);
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
    this.facultyService.deleteCourse(this.facultyIndex, id)
    .subscribe(() => {
      this._snackBar.open("Deleted successful!", 'Ok', {
        duration: 10000,
      });

      this.addCourseDialogRef?.close();
      this.getData();
    });
  }
}