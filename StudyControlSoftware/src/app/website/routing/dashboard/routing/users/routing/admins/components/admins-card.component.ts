import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';

import { TableOptions } from 'src/app/website/models';
import { AppUser } from 'src/app/website/routing/auth/models';
import { Admin } from '../../../models';

@Component({
  selector: 'app-dashboard-admins-card',
  templateUrl: 'admins-card.component.html'
})

export class AdminsCardComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['username', 'email', 'emailConfirmed', 'phone', 'actions'];

  @Input() data: Admin[] = [];
  @Input() resultsLength: number = 0;

  @Output() onChange = new EventEmitter<TableOptions>();
  @Output() onDelete = new EventEmitter<any>();
  @Output() onEdit = new EventEmitter<Admin>();
  @Output() onConfirmEmail = new EventEmitter<string>();

  @ViewChild(MatTable) table: MatTable<Admin>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  private userListOptions: TableOptions = new TableOptions('', '', 0, 0);

  appUser: AppUser = new AppUser('', '', '', '');

  constructor(
    /*private authService: AuthService*/
  ) { }

  ngOnInit() {
    //this.authService.getUser().subscribe(data => this.appUser = data);
  }

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

  onEditEvent(element: Admin) {
    this.onEdit.emit(element);
  }

  onConfirmEmailEvent(email: string) {
    this.onConfirmEmail.emit(email);
  }
}