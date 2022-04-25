import { ILoggedUser } from "./type";

export class LoggedUser implements ILoggedUser {
  email: string | null;
  token: string | null;
  id: string | null;

  constructor(email: string | null, token: string | null, id: string | null) {
    [ this.email, this.token, this.id ] = arguments
  }
}