import { Instituicao } from "./types"
import { defineStore } from "pinia"
import { httpRequest } from "../../utils/http"
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler"

const PERFIL_BASE_RESOURCE = "/api/instituicao"

const instituicao: Instituicao = {
  id: -1,
  razaoSocial: "",
  cnpj: "",
  responsavel: "",
  instituicaoAreaId: "",
  instituicaoClassificacaoId: "",
  descricao: "",
  missao: "",
  visao: "",
  valores: "",
  tipoInstituicaoId: ""
}

const allInstituicoes: Array<Instituicao> = []
export const useInstituicaoStore = defineStore("instituicaoStore", {
  state: () => {
    return {
      instituicao,
      allInstituicoes,
      response: new GeneralResponseHandler(0, "none", "no request made yet")
    }
  },
  persist: true,

  actions: {
    async getInstituicoes() {
      try {
        const response = await httpRequest.get(
          `${PERFIL_BASE_RESOURCE}/buscarInstituicoes`
        )
        this.allInstituicoes = response.data.retorno
        this.response.putResponse(
          response.data.codigo,
          response.data.retorno,
          response.data.resposta
        )
      } catch (error) {
        console.error(error)
        this.response.putError(222, error.message)
      }
    }
  }
})
