import { Component, AfterViewInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';

import { TableOptions } from 'src/app/website/models';
import { Lecturer } from '../../../models';

@Component({
  selector: 'app-dashboard-lecturers-card',
  templateUrl: 'lecturers-card.component.html'
})

export class LecturersCardComponent implements AfterViewInit {
  displayedColumns: string[] = ['fullName', 'username', 'email', 'emailConfirmed', 'phoneNumber', 'actions'];

  @Input() data: Lecturer[] = [];
  @Input() resultsLength: number = 0;

  @Output() onChange = new EventEmitter<TableOptions>();
  @Output() onDelete = new EventEmitter<any>();
  @Output() onEdit = new EventEmitter<Lecturer>();
  @Output() onConfirmEmail = new EventEmitter<string>();

  @ViewChild(MatTable) table: MatTable<Lecturer>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  private userListOptions: TableOptions = new TableOptions('', '', 0, 0);

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

  onEditEvent(element: Lecturer) {
    this.onEdit.emit(element);
  }

  onConfirmEmailEvent(email: string) {
    this.onConfirmEmail.emit(email);
  }
}