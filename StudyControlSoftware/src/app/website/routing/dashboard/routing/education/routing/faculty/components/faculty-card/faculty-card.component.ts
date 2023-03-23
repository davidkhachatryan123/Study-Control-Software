import { Component, EventEmitter, Output, AfterViewInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { TableOptions } from 'src/app/website/models';

import { tableDetailExpand } from 'src/app/website/shared/dashboard/animations';

import { Course, Faculty } from '../../../../models';

@Component({
  selector: 'app-dashboard-faculity-card',
  templateUrl: 'faculty-card.component.html',
  styleUrls: [ 'faculty-card.component.css' ],
  animations: [
    tableDetailExpand
  ],
})
export class FaculityCardComponent implements AfterViewInit {
  @Input() data: Faculty[] = [];
  @Input() resultsLength: number = 0;

  @Input() allCourses: Course[] = [];


  @Output() onChange = new EventEmitter<TableOptions>();

  @Output() onEdit = new EventEmitter<Faculty>;
  @Output() onDelete = new EventEmitter<Faculty>;

  
  @ViewChild(MatTable) table: MatTable<Faculty>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  private userListOptions: TableOptions = new TableOptions('', '', 0, 0);

  columnsToDisplay: string[] = [ 'name', 'actions' ];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];

  expandedElement: Faculty | null;

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

  onEditEvent(element: Faculty) {
    this.onEdit.emit(element);
  }

  onDeleteEvent(element: Faculty) {
    this.onDelete.emit(element);
  }
}