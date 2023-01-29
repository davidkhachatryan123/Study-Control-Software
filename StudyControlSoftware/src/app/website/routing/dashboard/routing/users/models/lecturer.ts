export class Lecturer {
  constructor(
    public id: number,
    public surname: string,
    public lastname: string,
  ) { }

  getFullName(): string {
    return this.surname + ' ' + this.lastname;
  }
}