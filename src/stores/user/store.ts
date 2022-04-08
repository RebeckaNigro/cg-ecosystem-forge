import { defineStore } from "pinia";
import { ILoggedUser } from "./type";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";
import { httpRequest } from "../../utils/http";

const loggedUser: ILoggedUser = {
  email: null,
  token: null,
}

export const useUserStore = defineStore('userStore', {
  state: () => {
    return {
      userResponse: new GeneralResponseHandler(0, 'none', 'no request made yet'),
      loggedUser,
    }
  },
  actions: {
    async login(email: string, password: string) {
      const body = {
        email: email,
        password: password
      }
      try {
        const response = await httpRequest.post('/api/autenticacao/login', body)
        if (response.status === 200) {
          this.userResponse.putResponse(response.status, '', response.statusText);
          this.loggedUser.email = email
          this.loggedUser.token = response.data.token
        }
        else this.userResponse.putError(response.status, response.statusText); 
      } catch (error) {
        if (error instanceof Error) this.userResponse.putError(400, error.message) 
        console.error(error)
      }
    }
  }
})