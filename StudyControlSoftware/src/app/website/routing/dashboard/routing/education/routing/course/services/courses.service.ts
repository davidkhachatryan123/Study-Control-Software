import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { TableResponseDto } from "src/app/website/dto/usersResponseDto";
import { environment } from "src/environments/environment";
import { Lecturer } from "../../../../users/models";
import { Course } from "../../../models";
import { ManageEducationBaseService } from "../../../services/education.service";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoursesService extends ManageEducationBaseService<TableResponseDto<Course>, Course> {
  private api: string = "courses";

  constructor(
    http: HttpClient
  ) {
    super(http, "courses");

    this.api = environment.config.apiUrl + this.api + "/";
  }

  getLecturer(id: number): Observable<Lecturer> {
    return this.http.get<Lecturer>(this.api + id + "/getLecturer");
  }

  setLecturer(id: number, lecturerId: string): Observable<Lecturer> {
    const params = new HttpParams()
    .set('lecturerId', lecturerId);

    return this.http.post<Lecturer>(
      this.api + id + "/setLecturer",
      null,
      { params: params });
  }

  deleteLecturer(id: number) {
    return this.http.delete(this.api + id + "/deleteLecturer/");
  }
}