import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

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

  private addCourseDialogRef: MatDialogRef<AddCourseDialogComponent>;

  addedCourses: Course[] = [];

  constructor(
    public dialog: MatDialog,
    private facultyService: FacultyService
  ) { }

  ngOnInit() {
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
      width: '500px'
    });

    this.addCourseDialogRef.componentInstance.onSubmit.subscribe((courses: Course[]) => {
      this.add(courses);
    });
  }
  add(courses: Course[]) {
    console.log(courses.map(a => a.id));
    /*this.usersManagmentService.deleteAdminUser(id)
    .subscribe((data: ResponseModel) => {
      this._snackBar.open(data.message, 'Ok', {
        duration: 10000,
      });

      if(data.statusCode == '200')
        this.getUsers();
    });*/
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