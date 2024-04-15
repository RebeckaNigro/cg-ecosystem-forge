import { defineStore } from "pinia";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";
import { baseURL, httpRequest } from "../../utils/http";
import { LoggedUser } from "../user/model";
import axios from "axios";
import headers from "../../utils/headers";

export const useRecuperacaoSenhaStore = defineStore("recuperacaoSenhaStore", {
  state: () => {
    return {
      response: new GeneralResponseHandler(0, "none", "no request made yet"),
      info: {
        otp: "",
        email: "",
        novaSenha: "",
      },
    };
  },
  persist: false,
  actions: {
    async enviarEmailComCodigo(email: string) {
      let data = JSON.stringify(email);

      const response = await axios.request({
        method: "post",
        maxBodyLength: Infinity,
        url: baseURL + "/api/Autenticacao/gerarCodigoRefinirSenha",
        headers: {
          "Content-Type": "application/json",
        },
        data: data,
      });

      this.response.putResponse(
        response.data.codigo,
        response.data.retorno,
        response.data.resposta
      );
    },
    async redefinirSenha(novaSenha: string, confirmaSenha: string) {
      this.info.novaSenha = novaSenha;
      let data = JSON.stringify({ ...this.info, confirmaSenha: confirmaSenha });
      console.log(data);

      const response = await axios.request({
        method: "post",
        maxBodyLength: Infinity,
        url: baseURL + "/api/Autenticacao/refinir-senha",
        headers: {
          "Content-Type": "application/json",
        },
        data: data,
      });
      this.response.putResponse(
        response.data.codigo,
        response.data.retorno,
        response.data.resposta
      );
    },
  },
  getters: {},
});
