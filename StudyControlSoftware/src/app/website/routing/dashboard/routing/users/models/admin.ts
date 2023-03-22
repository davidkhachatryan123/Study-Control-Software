import { UserSkel } from "./user-skel";

export class Admin extends UserSkel {
  constructor(
    public id: string,
    public username: string,
    public password: string,
    public email: string,
    public emailConfirmed: boolean,
    public phoneNumber: string
  ) {
    super(id, username, password, email, emailConfirmed, phoneNumber);
  }
}