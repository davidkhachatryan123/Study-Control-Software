export class UserDto {
  constructor(
    public id: string,
    public firstName: string,
    public lastName: string,
    public username: string,
    public password: string,
    public passwordConfirm: string,
    public email: string,
    public phoneNumber: string
  ) { }
}