import { AfterContentInit, Component, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Course } from 'src/app/website/routing/dashboard/routing/education/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { AddCourseDialogComponent } from '../../dialogs';

@Component({
  selector: 'app-dashboard-add-course-list',
  templateUrl: 'add-course-list.component.html'
})
export class AddCourseListComponent implements AfterContentInit {
  @Input() facultyIndex: number;

  private addCourseDialogRef: MatDialogRef<AddCourseDialogComponent>;

  addedCourses: Course[] = [
    new Course(1, 'C# ծրագրավորում', 'Սովորում ենք գրել ծրագրեր օգտագործելով C# ծրագրավորման լեզուն'),
    new Course(2, 'C++ ծրագրավորում', 'Սովորում ենք գրել ծրագրեր օգտագործելով C++ ծրագրավորման լեզուն'),
    new Course(3, 'ASP.NET Core', 'Սովորում ենք գրել ծրագրեր օգտագործելով ASP.NET Core framework-ը')
  ];

  constructor(
    public dialog: MatDialog
  ) { }

  ngAfterContentInit() {
    console.log(this.facultyIndex);
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