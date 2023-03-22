export class AuthResponseDto {
  public constructor(
    public isAuthSuccessful: boolean,
    public token: string,
    public errorMessage: string,
    public email: string,
    public role: string
  ) {}
}