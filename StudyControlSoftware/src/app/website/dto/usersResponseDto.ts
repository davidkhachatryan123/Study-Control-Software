import { Admin } from "../routing/dashboard/routing/users/models";

export class TableResponseDto<T> {
  entities: T[];
  totalCount: number;
}