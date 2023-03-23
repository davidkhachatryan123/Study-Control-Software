import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { TableOptions } from 'src/app/website/models';

export class ManageEducationBaseService<TResponse, TDto> {
  private apiUrl: string;

  constructor(public http: HttpClient, api: string) {

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

  create(dto: TDto) {
    return this.http.post<TDto>(
      this.apiUrl,
      dto);
  }

  edit(id: number, dto: TDto) {
    return this.http.put<TDto>(
      this.apiUrl + '/' + id,
      dto);
  }

  delete(id: number) {
    const params = new HttpParams()
    .set('id', id);

    return this.http.delete(
      this.apiUrl,
      { params: params });
  }
}