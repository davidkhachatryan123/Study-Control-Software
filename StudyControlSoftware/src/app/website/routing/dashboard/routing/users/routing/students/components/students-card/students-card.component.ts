import { Component, EventEmitter, Input, Output, ViewChild, AfterViewInit } from '@angular/core';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';

import { TableOptions } from 'src/app/website/models';
import { tableDetailExpand } from 'src/app/website/shared/dashboard/animations';
import { Student } from '../../../../models/student';

@Component({
  selector: 'app-dashboard-students-card',
  templateUrl: 'students-card.component.html',
  styleUrls: [ 'students-card.component.css' ],
  animations: [
    tableDetailExpand
  ],
})
export class StudentsCardComponent implements AfterViewInit {

  @Input() data: Student[] = [];
  @Input() resultsLength: number = 0;

  @Output() onChange = new EventEmitter<TableOptions>();
  @Output() onDelete = new EventEmitter<any>();
  @Output() onEdit = new EventEmitter<Student>();
  @Output() onConfirmEmail = new EventEmitter<string>();

  @ViewChild(MatTable) table: MatTable<Student>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  private userListOptions: TableOptions = new TableOptions('', '', 0, 0);

  columnsToDisplay: string[] = ['fullName', 'username', 'email', 'emailConfirmed', 'phoneNumber', 'actions'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];

  expandedElement: Student | null;

  ngAfterViewInit() {
    this.onChangeEvent();

    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0;
      this.onChangeEvent();
    });

    this.paginator.page.subscribe(() => {
      this.onChangeEvent();
    });
  }
  
  onChangeEvent() {
    this.userListOptions.sort = this.sort.active;
    this.userListOptions.sortDirection = this.sort.direction;
    this.userListOptions.pageIndex = this.paginator.pageIndex;
    this.userListOptions.pageSize = this.paginator.pageSize;

    this.onChange.emit(this.userListOptions);
  }

  onDeleteEvent(id: number, username: string) {
    this.onDelete.emit({id, username});
  }

  onEditEvent(element: Student) {
    this.onEdit.emit(element);
  }

  onConfirmEmailEvent(email: string) {
    this.onConfirmEmail.emit(email);
  }
}