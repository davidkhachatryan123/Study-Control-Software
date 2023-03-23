import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TableResponseDto } from 'src/app/website/dto/usersResponseDto';
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

  addCourses(id: number, newIDs: Array<number>) {
    return this.http.post<Array<Course>>(
      this.api + id + "/addCourse",
      newIDs);
  }

  deleteCourse(id: number, deleteId: number) {
    return this.http.delete(this.api + id + "/deleteCourse/" + deleteId);
  }
}