import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AdminDto } from "src/app/website/dto/adminDto";
import { UserDto } from "src/app/website/dto/userDto";
import { UsersResponseDto } from "src/app/website/dto/usersResponseDto";
import { Admin, Lecturer } from "../../../models";
import { ManageUsersService } from "../../../services/users.service";

@Injectable({
  providedIn: "root"
})
export class LecturersService
extends ManageUsersService<UsersResponseDto<Lecturer>, UserDto, Lecturer> {
  constructor(
    http: HttpClient
  ) {
    super(http, "manageLecturers");
  }
}