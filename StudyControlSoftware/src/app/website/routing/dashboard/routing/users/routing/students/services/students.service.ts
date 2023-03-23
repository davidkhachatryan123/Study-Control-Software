import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserDto } from "src/app/website/dto/userDto";
import { TableResponseDto } from "src/app/website/dto/usersResponseDto";
import {  } from "../../../models";
import { Student } from "../../../models/student";
import { ManageUsersService } from "../../../services/users.service";

@Injectable({
  providedIn: "root"
})
export class StudentsService
extends ManageUsersService<TableResponseDto<Student>, UserDto, Student> {
  constructor(
    http: HttpClient
  ) {
    super(http, "manageStudents");
  }
}