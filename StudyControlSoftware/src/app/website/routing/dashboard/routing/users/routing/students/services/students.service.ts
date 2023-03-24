import { HttpClient, HttpParams } from "@angular/common/http";
import { FactoryProvider, Injectable } from "@angular/core";
import { UserDto } from "src/app/website/dto/userDto";
import { TableResponseDto } from "src/app/website/dto/usersResponseDto";
import { environment } from "src/environments/environment";
import { Student } from "../../../models/student";
import { ManageUsersService } from "../../../services/users.service";
import { Observable } from 'rxjs';
import { Faculty } from "../../../../education/models";

@Injectable({
  providedIn: "root"
})
export class StudentsService
extends ManageUsersService<TableResponseDto<Student>, UserDto, Student> {
  private api: string = "manageStudents";

  constructor(
    public http: HttpClient
  ) {
    super(http, "manageStudents");

    this.api = environment.config.apiUrl + this.api + "/";
  }

  getFaculty(id: string): Observable<Faculty> {
    return this.http.get<Faculty>(this.api + id + "/getFaculty");
  }

  setFaculty(id: string, facultyId: number): Observable<Faculty> {
    const params = new HttpParams()
    .set('facultyId', facultyId);

    return this.http.post<Faculty>(
      this.api + id + "/setFaculty",
      null,
      { params: params });
  }

  deleteFaculty(id: string) {
    return this.http.delete(this.api + id + "/deleteFaculty/");
  }
}