export class NewUser {
  public constructor(
    public id: number,
    public username: string,
    public password: string,
    public email: string,
    public phoneNumber: string,
    public role: string
  ) { }
}