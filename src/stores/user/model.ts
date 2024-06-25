import { type ILoggedUser } from './type';

export class LoggedUser implements ILoggedUser {
  userName: string | null;
  token: string | null;
  id: string | null;

  constructor(userName: string | null, token: string | null, id: string | null) {
    [this.userName, this.token, this.id] = arguments;
  }
}
