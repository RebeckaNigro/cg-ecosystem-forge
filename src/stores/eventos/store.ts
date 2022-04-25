import { IEvento, EnderecoExistente, IUltimoEvento } from "./types";
import { defineStore } from "pinia";
import { httpRequest, getLastContent } from "../../utils/http";

const novoEvento: IEvento = {
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
const enderecosExistentes: Array<EnderecoExistente> = []
const ultimosEventos: Array<IUltimoEvento> = []
export const useEventoStore = defineStore('eventoStore', {
  state: () => {
    return {
      novoEvento, // TODO: remove novoEvento
      enderecosExistentes,
      ultimosEventos
    }
  },
  persist: true,
  actions: {
    async putEvent(newEvent: IEvento, media?: HTMLInputElement) {

      const formData = new FormData()
      formData.append("evento", JSON.stringify(newEvent))

      // TODO: find out how to sent file as string (create another index.html to find out)
      if (media?.files) formData.append("arquivos", media.files[0])
      
      try {
        const response = await httpRequest.post('/api/evento/incluir', formData, {
          headers: {
            "Content-type": "multipart/form-data",
          }
        })
      } catch (error) {
        
      }
    },
    async getAddresses(tipoEvendoId: string) {
      try {
        const response = await httpRequest.get(`/api/evento/listarEnderecos?tipoEnderecoId=${tipoEvendoId}`)
        if (response) {
          if (response.data.codigo === 200) {
            const extractExistingAddress = (data: any) => {
                const address: EnderecoExistente = { ...data }
                return address
            }
            this.enderecosExistentes = []
            for (const addrr of response.data.retorno) {
              this.enderecosExistentes.push(extractExistingAddress(addrr))
            }
          }
          else if (response.data.codigo === 404) this.enderecosExistentes = []
        }
      } catch (error) {
        
      }
    },
    async getLastEvents() {
      const response = await getLastContent('evento')
      if (response.data.codigo === 200) {
        this.ultimosEventos = []
        for (const lastEvent of response.data.retorno) {
          this.ultimosEventos.push(lastEvent)
        }
      }
    }
  }
})