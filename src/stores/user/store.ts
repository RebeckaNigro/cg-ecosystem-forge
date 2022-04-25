import { defineStore } from "pinia";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";
import { httpRequest } from "../../utils/http";
import { LoggedUser } from "./model";

export const useUserStore = defineStore('userStore', {
  state: () => {
    return {
      userResponse: new GeneralResponseHandler(0, 'none', 'no request made yet'),
      loggedUser: new LoggedUser(null, null, null)
    }
  },
  persist: true,
  actions: {
    async login(username: string, password: string) {
      const body = {
        username: username,
        password: password
      }
      try {
        const response = await httpRequest.post('/api/autenticacao/login', body)
        if (response.status === 200) {
          this.userResponse.putResponse(response.data.codigo, response.data.retorno, response.data.resposta);
          if (response.data.retorno) [ this.loggedUser ] = [ response.data.retorno ]
        }
        else this.userResponse.putError(response.status, response.statusText); 
      } catch (error) {
        if (error instanceof Error) this.userResponse.putError(400, error.message) 
        console.error(error)
      }
    }
  },
  getters: {
    getUserId: (state) => state.loggedUser.id
  }
})