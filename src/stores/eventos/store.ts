import { IEvento } from "./types";
import { defineStore } from "pinia";
import { httpRequest } from "../../utils/http";

const novoEvento: IEvento = {
  id: 1, // deschumbar quando retorno da api mudar
  instituicaoId: 0,
  tipoEventoId: 0,
  titulo: "",
  descricao: "",
  dataInicio: "",
  dataTermino: "",
  local: "",
  enderecoId: 0,
  endereco: null,
  linkExterno: "",
  exibirMaps: false,
  responsavel: ""
}
export const useEventoStore = defineStore('eventoStore', {
  state: () => {
    return {
      novoEvento
    }
  },
  persist: true,
  actions: {
    async putEvent() {
      try {        
        const response = await httpRequest.post('/api/evento/incluir', this.novoEvento)
        console.log(response)
      } catch (error) {
        
      }
    }
  }
})