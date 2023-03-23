import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TableResponseDto } from 'src/app/website/dto/usersResponseDto';
import { TableOptions } from 'src/app/website/models';
import { environment } from 'src/environments/environment';
import { Course, Faculty } from '../../../models';
import { ManageEducationBaseService } from '../../../services/education.service';

@Injectable({
  providedIn: 'root'
})
export class FacultyService extends ManageEducationBaseService<TableResponseDto<Faculty>, Faculty> {
  private api: string = "faculties";

  constructor(
    public http: HttpClient
  ) {
    super(http, "faculties");

    this.api = environment.config.apiUrl + this.api + "/";
  }

  getCourses(id: number): Observable<Array<Course>> {
    return this.http.get<Array<Course>>(this.api + id + "/getCourses");
  }
}