import { AfterViewInit, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { TableOptions } from 'src/app/website/models';

import { Course } from 'src/app/website/routing/dashboard/routing/education/models';
import { tableDetailExpand } from 'src/app/website/shared/dashboard/animations';
import { Lecturer } from '../../../../../users/models';

@Component({
  selector: 'app-dashboard-course-card',
  templateUrl: 'course-card.component.html',
  styleUrls: [ 'course-card.component.css' ],
  animations: [
    tableDetailExpand
  ],
})
export class CourseCardComponent implements AfterViewInit {
  @Input() data: Course[] = [];
  @Input() resultsLength: number = 0;

  @Input() lecturers: Lecturer[] = [];


  @Output() onChange = new EventEmitter<TableOptions>();

  @Output() onEdit = new EventEmitter<Course>;
  @Output() onDelete = new EventEmitter<Course>;
  

  @ViewChild(MatTable) table: MatTable<Course>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  private userListOptions: TableOptions = new TableOptions('', '', 0, 0);

  columnsToDisplay: string[] = [ 'title', 'description', 'actions' ];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];

  expandedElement: Course | null;

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

  onEditEvent(element: Course) {
    this.onEdit.emit(element);
  }

  onDeleteEvent(element: Course) {
    this.onDelete.emit(element);
  }
}