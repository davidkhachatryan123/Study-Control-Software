export class Admin {
  constructor(
    public id: number,
    public username: string,
    public email: string,
    public emailConfirmed: boolean,
    public phone: string,
    public role: string
  ) { }
}