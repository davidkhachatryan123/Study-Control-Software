import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { Admin } from '../../../models';
import { AdminDto } from 'src/app/website/dto/adminDto';
import { Observable } from 'rxjs';
import { TableOptions } from 'src/app/website/models';
import { AdminsResponseDto } from 'src/app/website/dto/adminsResponseDto';

@Injectable({
  providedIn: 'root'
})
export class ManageAdminsService {
  private apiUrl = "manageAdmins";

  constructor(
    private http: HttpClient) {
    this.apiUrl = environment.config.apiUrl + this.apiUrl;
  }

  getAll(options: TableOptions): Observable<AdminsResponseDto> {
    const params = new HttpParams()
    .set('sort', options.sort)
    .set('orderDirection', options.sortDirection)
    .set('page', options.pageIndex + 1)
    .set('pageSize', options.pageSize);

    return this.http.get<AdminsResponseDto>(this.apiUrl, { params: params });
  }

  create(admin: AdminDto) {
    return this.http.post<Admin>(this.apiUrl, admin);
  }

  edit(id: string, admin: AdminDto) {
    return this.http.put<Admin>(this.apiUrl + '/' + id, admin);
  }

  delete(id: string) {
    const params = new HttpParams()
    .set('id', id);

    return this.http.delete(this.apiUrl, { params: params });
  }
}