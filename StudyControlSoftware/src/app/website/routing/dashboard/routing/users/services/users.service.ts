import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { TableOptions } from 'src/app/website/models';

export class ManageUsersService<TResponse, TDto, T> {
  private apiUrl: string;

  constructor(
    private http: HttpClient,
    api: string) {

    this.apiUrl = environment.config.apiUrl + api;
  }

  getAll(options: TableOptions): Observable<TResponse> {
    const params = new HttpParams()
    .set('sort', options.sort)
    .set('orderDirection', options.sortDirection)
    .set('page', options.pageIndex + 1)
    .set('pageSize', options.pageSize);

    return this.http.get<TResponse>(
      this.apiUrl,
      { params: params });
  }

  create(user: TDto) {
    console.log(user);
    return this.http.post<T>(
      this.apiUrl,
      user);
  }

  edit(id: string, user: TDto) {
    return this.http.put<T>(
      this.apiUrl + '/' + id,
      user);
  }

  delete(id: string) {
    const params = new HttpParams()
    .set('id', id);

    return this.http.delete(
      this.apiUrl,
      { params: params });
  }
}