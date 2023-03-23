import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { TableResponseDto } from "src/app/website/dto/usersResponseDto";
import { Course } from "../../../models";
import { ManageEducationBaseService } from "../../../services/education.service";

@Injectable({
  providedIn: 'root'
})
export class CoursesService extends ManageEducationBaseService<TableResponseDto<Course>, Course> {
  constructor(
    http: HttpClient
  ) {
    super(http, "courses");
  }
}