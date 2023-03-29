import { IEvento, EnderecoExistente, IUltimoEvento, Evento } from "./types"
import { defineStore } from "pinia"
import { httpRequest, getLastContent } from "../../utils/http"
import { validateEventoInput } from "../../utils/eventos/validation"
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler"

const enderecosExistentes: Array<EnderecoExistente> = []
const ultimosEventos: Array<IUltimoEvento> = []
const eventos: Array<IUltimoEvento> = []
const eventosUsuarioLogado: Array<IUltimoEvento> = []
export const useEventoStore = defineStore("eventoStore", {
  state: () => {
    return {
      enderecosExistentes,
      ultimosEventos,
      response: new GeneralResponseHandler(0, "none", "no request made yet"),
      eventos,
      eventosUsuarioLogado,
      loadRascunho: false
    }
  },
  persist: true,
  actions: {
    async postEvent(novoEvento: IEvento) {
      try {
        const formData = new FormData()
        formData.append("evento", JSON.stringify(novoEvento))

        novoEvento.tags.forEach((tag, index) => {
          formData.append(`tags[${index}].descricao`, tag.descricao)
        })

        formData.append("arquivo", novoEvento.arquivo)

        const response = await httpRequest.post(
          "/api/evento/incluir",
          formData,
          {
            headers: { "Content-Type": "multipart/form-data" }
          }
        )

        this.response.putResponse(
          response.data.codigo,
          response.data.dado,
          response.data.resposta
        )
      } catch (error) {
        if (error instanceof TypeError) {
          this.response.putError(222, error.message)
        } else {
          this.response.putError(661, "Entre em contato com a Startup do SESI")
          console.error(error)
        }
      }

      return false
    },

    async getUserEvents() {
      try {
        const response = await httpRequest.get("/api/evento/listarPorUsuarioId")
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          )
          this.eventosUsuarioLogado = []
          for (const event of response.data.retorno) {
            this.eventosUsuarioLogado.push(event)
          }
        }
      } catch (error) {
        console.error(error)
      }
    },

    async getAddresses(tipoEvendoId: string) {
      try {
        const response = await httpRequest.get(
          `/api/evento/listarEnderecos?tipoEnderecoId=${tipoEvendoId}`
        )
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
          } else if (response.data.codigo === 404) this.enderecosExistentes = []
        }
      } catch (error) {}
    },

    async getLastEvents() {
      const response = await getLastContent("evento")
      if (response.data.codigo === 200) {
        this.ultimosEventos = []
        for (const lastEvent of response.data.retorno) {
          this.ultimosEventos.push(lastEvent)
        }
      }
    },

    async getAllEvents() {
      try {
        const response = await httpRequest.get("/api/evento/listarTodas")
        if (response.data.codigo === 200) {
          this.eventos = []

          //console.log(this.eventos);
          for (const evento of response.data.retorno) {
            this.eventos.push(evento)
          }
        }
      } catch (error) {
        console.error(error)
      }
    },

    async deleteEvent(eventId: number) {
      try {
        const response = await httpRequest.delete(
          `/api/evento/excluir?id=${eventId}`
        )
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.dado,
            response.data.resposta
          )
        }
      } catch (error) {
        if (error instanceof TypeError) {
          this.response.putError(222, error.message)
        } else {
          this.response.putError(661, "Erro ao remover evento.")
          console.error(error)
        }
      }
    },

    async putEvent(eventoEdicao: IEvento) {
      try {
        const formData = new FormData()
        formData.append("evento", JSON.stringify(eventoEdicao))

        eventoEdicao.tags.forEach((tag, index) => {
          formData.append(`tags[${index}].descricao`, tag.descricao)
        })

        formData.append("arquivo", eventoEdicao.arquivo)
        const res = await httpRequest.put("/api/evento/editar", formData, {
          headers: { "Content-Type": "multipart/form-data" }
        })
        this.response.putResponse(
          res.data.codigo,
          res.data.dado,
          res.data.resposta
        )
      } catch (error) {
        if (error instanceof TypeError) {
          this.response.putError(222, error.message)
        } else {
          this.response.putError(661, "Entre em contato com a Startup do SESI")
          console.error(error)
        }
      }
    },

    async getEventById(eventoId: number) {
      try {
        const response = await httpRequest.get(
          `/api/evento/detalhes?Id=${eventoId}`
        )
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.dado,
            response.data.resposta
          )
        }
      } catch (error) {
        console.error(error)
      }
    }
  }
})
