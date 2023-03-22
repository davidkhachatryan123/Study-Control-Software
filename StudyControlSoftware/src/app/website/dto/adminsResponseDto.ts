import { Admin } from "../routing/dashboard/routing/users/models";

export class AdminsResponseDto {
  entities: Admin[];
  totalCount: number;
}