import { UserSkel } from "./user-skel";

export class Lecturer extends UserSkel {
  constructor(
    public id: string,
    public firstName: string,
    public lastName: string,
    public username: string = '',
    public password: string = '',
    public email: string = '',
    public emailConfirmed: boolean = false,
    public phoneNumber: string = '',
  ) {
    super(id, username, password, email, emailConfirmed, phoneNumber);
  }

  public get fullName() {
    return this.firstName + ' ' + this.lastName;
  }
}