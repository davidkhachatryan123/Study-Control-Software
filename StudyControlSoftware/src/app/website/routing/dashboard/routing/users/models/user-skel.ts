export abstract class UserSkel {
  constructor(
    public id: string,
    public username: string,
    public password: string,
    public email: string,
    public emailConfirmed: boolean,
    public phoneNumber: string
  ) { }
}