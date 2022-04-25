import { ILoggedUser } from "./type";

export class LoggedUser implements ILoggedUser {
  username: string | null;
  token: string | null;
  id: string | null;

  constructor(username: string | null, token: string | null, id: string | null) {
    [ this.username, this.token, this.id ] = arguments
  }
}