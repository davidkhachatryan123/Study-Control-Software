import { AfterContentInit, Component, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Course } from 'src/app/website/routing/dashboard/routing/education/models';
import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { Lecturer } from '../../../../../users/models';
import { SetLecturerDialogComponent } from '../../dialogs';

@Component({
  selector: 'app-dashboard-add-lecturer',
  templateUrl: 'add-lecturer.component.html'
})
export class AddLecturerComponent implements AfterContentInit {
  @Input() courseIndex: number;

  lecturer: Lecturer | undefined/* = new Lecturer(1, 'Դավիթ', 'Խաչատրյան')*/;

  private setLecturerDialogRef: MatDialogRef<SetLecturerDialogComponent>;

  constructor(
    public dialog: MatDialog
  ) { }

  ngAfterContentInit() {
    console.log(this.courseIndex);
  }

  setLecturer() {
    this.openSetLecturerDialog();
  }
  openSetLecturerDialog() {
    this.setLecturerDialogRef = this.dialog.open(SetLecturerDialogComponent, {
      width: '500px'
    });

    this.setLecturerDialogRef.componentInstance.onSubmit.subscribe((lecturer: Lecturer) => {
      this.set(lecturer);
    });
  }
  set(lecturer: Lecturer) {
    console.log(lecturer.id);
    /*this.usersManagmentService.deleteAdminUser(id)
    .subscribe((data: ResponseModel) => {
      this._snackBar.open(data.message, 'Ok', {
        duration: 10000,
      });

      if(data.statusCode == '200')
        this.getUsers();
    });*/
  }

  removeLecturer() {
    this.openDeleteDialog();
  }
  openDeleteDialog() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: this.lecturer.surname + ' ' + this.lecturer.lastname, isDelete: 'false' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.delete();
    });
  }
  delete() {
    console.log('Yes remove!');
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