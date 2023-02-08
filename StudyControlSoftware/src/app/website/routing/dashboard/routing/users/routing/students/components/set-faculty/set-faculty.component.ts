import { AfterContentInit, Component, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { DeleteDialogComponent } from 'src/app/website/shared/dashboard/dialogs';
import { Faculty } from '../../../../../education/models';
import { SetFacultyDialogComponent } from '../../dialogs/set-faculty/set-faulty-dialog.component';

@Component({
  selector: 'app-dashboard-set-faculty',
  templateUrl: 'set-faculty.component.html'
})
export class SetFacultyComponent implements AfterContentInit {
  @Input() studentIndex: number;

  faculty: Faculty | undefined/* = new Lecturer(1, 'Դավիթ', 'Խաչատրյան')*/;

  private setFacultyDialogRef: MatDialogRef<SetFacultyDialogComponent>;

  constructor(
    public dialog: MatDialog
  ) { }

  ngAfterContentInit() {
    console.log(this.studentIndex);
  }

  setFaculty() {
    this.openSetFacultyDialog();
  }
  openSetFacultyDialog() {
    this.setFacultyDialogRef = this.dialog.open(SetFacultyDialogComponent, {
      width: '500px'
    });

    this.setFacultyDialogRef.componentInstance.onSubmit.subscribe((faculty: Faculty) => {
      this.set(faculty);
    });
  }
  set(faculty: Faculty) {
    console.log(faculty.id);
    /*this.usersManagmentService.deleteAdminUser(id)
    .subscribe((data: ResponseModel) => {
      this._snackBar.open(data.message, 'Ok', {
        duration: 10000,
      });

      if(data.statusCode == '200')
        this.getUsers();
    });*/
  }

  removeFaculty() {
    this.openDeleteDialog();
  }
  openDeleteDialog() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: { value: this.faculty.name, isDelete: 'false' }
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