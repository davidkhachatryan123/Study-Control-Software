import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AdminDto } from "src/app/website/dto/adminDto";
import { TableResponseDto } from "src/app/website/dto/usersResponseDto";
import { Admin } from "../../../models";
import { ManageUsersService } from "../../../services/users.service";

@Injectable({
  providedIn: "root"
})
export class AdminsService
extends ManageUsersService<TableResponseDto<Admin>, AdminDto, Admin> {
  constructor(
    http: HttpClient
  ) {
    super(http, "manageAdmins");
  }
}