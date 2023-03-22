import { UserSkel } from "./user-skel";

export class Student extends UserSkel {
  constructor(
    public id: string,
    public surname: string,
    public lastname: string,
    public username: string = '',
    public password: string = '',
    public email: string = '',
    public emailConfirmed: boolean = false,
    public phoneNumber: string = '',
  ) {
    super(id, username, password, email, emailConfirmed, phoneNumber);
  }

  getFullName(): string {
    return this.surname + ' ' + this.lastname;
  }
}