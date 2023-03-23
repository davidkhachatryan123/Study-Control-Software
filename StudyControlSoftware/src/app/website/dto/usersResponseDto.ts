import { Admin } from "../routing/dashboard/routing/users/models";

export class UsersResponseDto<T> {
  entities: T[];
  totalCount: number;
}