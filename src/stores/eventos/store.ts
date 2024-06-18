//@ts-nocheck
import {
  IEvento,
  EnderecoExistente,
  IUltimoEvento,
  Evento,
  IEventoSimplificado,
  IOrganizador,
} from "./types";
import { defineStore } from "pinia";
import { httpRequest, getLastContent } from "../../utils/http";
import { validateEventoInput } from "../../utils/eventos/validation";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";

const enderecosExistentes: Array<EnderecoExistente> = [];
const ultimosEventos: Array<IEventoSimplificado> = [];
const eventos: Array<IUltimoEvento> = [];
const eventosUsuarioLogado: Array<IEventoSimplificado> = [];
const ultimosEventosUsuarioLogado: Array<IUltimoEvento> = [];
const organizadores: IOrganizador[] = [];
const eventosFiltrados: Array<IEventoSimplificado> = [];

export const useEventoStore = defineStore("eventoStore", {
  state: () => {
    return {
      enderecosExistentes,
      ultimosEventos,
      response: new GeneralResponseHandler(0, "none", "no request made yet"),
      eventos,
      eventosUsuarioLogado,
      ultimosEventosUsuarioLogado,
      loadRascunho: false,
      organizadores,
      eventosFiltrados,
      filterPage: 1,
    };
  },
  persist: false,
  actions: {
    async postEvent(novoEvento: IEvento) {
      const data = {
        ...novoEvento,
        endereco: {
          ...novoEvento.endereco,
          cep: novoEvento.endereco.cep.split("-").join(""),
        },
      };

      try {
        const formData = new FormData();
        formData.append("evento", JSON.stringify(data));

        novoEvento.tags.forEach((tag, index) => {
          formData.append(`tags[${index}].descricao`, tag.descricao);
        });

        formData.append("arquivo", novoEvento.arquivo);

        const response = await httpRequest.post(
          "/api/evento/incluir",
          formData,
          {
            headers: { "Content-Type": "multipart/form-data" },
          }
        );

        this.response.putResponse(
          response.data.codigo,
          response.data.dado,
          response.data.resposta
        );
      } catch (error) {
        if (error instanceof TypeError) {
          this.response.putError(222, error.message);
        } else {
          this.response.putError(661, "Entre em contato com a Startup do SESI");
          console.error(error);
        }
      }

      return false;
    },

    async getUserEvents(page: number) {
      try {
        const response = await httpRequest.get(
          `/api/evento/listarPorUsuarioId?paginacao=${page}`
        );
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          );
          this.eventosUsuarioLogado = [];
          for (const event of response.data.retorno) {
            this.eventosUsuarioLogado.push(event);
          }
        }
      } catch (error) {
        console.error(error);
      }
    },

    async getAddresses(tipoEvendoId: string) {
      try {
        const response = await httpRequest.get(
          `/api/evento/listarEnderecos?tipoEnderecoId=${tipoEvendoId}`
        );
        if (response) {
          if (response.data.codigo === 200) {
            const extractExistingAddress = (data: any) => {
              const address: EnderecoExistente = { ...data };
              return address;
            };
            this.enderecosExistentes = [];
            for (const addrr of response.data.retorno) {
              this.enderecosExistentes.push(extractExistingAddress(addrr));
            }
          } else if (response.data.codigo === 404)
            this.enderecosExistentes = [];
        }
      } catch (error) {}
    },

    async getLastEvents() {
      const response = await getLastContent("evento");
      if (response.data.codigo === 200) {
        this.ultimosEventos = [];
        for (const lastEvent of response.data.retorno) {
          this.ultimosEventos.push(lastEvent);
        }
      }
    },

    async getUserLastEvents() {
      try {
        const response = await httpRequest.get(
          "/api/evento/listarUltimosPorUsuarioId"
        );
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          );
          this.ultimosEventosUsuarioLogado = [];
          for (const event of response.data.retorno) {
            this.ultimosEventosUsuarioLogado.push(event);
          }
        } else {
          this.response.putError(response.data.codigo, response.data.resposta);
        }
      } catch (error) {
        console.error(error);
      }
    },

    async getAllEvents(page: number) {
      try {
        const response = await httpRequest.get(
          `/api/evento/listarTodos?paginacao=${page.toString()}`
        );
        if (response.data.codigo === 200) {
          if (page === 1) {
            this.eventos = response.data.retorno;
          } else {
            this.eventos = this.eventos.concat(response.data.retorno);
          }
        }
      } catch (error) {
        console.error(error);
      }
    },

    async deleteEvent(eventId: number) {
      try {
        const response = await httpRequest.delete(
          `/api/evento/excluir?id=${eventId}`
        );
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.dado,
            response.data.resposta
          );
        }
      } catch (error) {
        if (error instanceof TypeError) {
          this.response.putError(222, error.message);
        } else {
          this.response.putError(661, "Erro ao remover evento.");
          console.error(error);
        }
      }
    },

    async putEvent(eventoEdicao: IEvento) {
      try {
        let data;
        if (eventoEdicao.endereco) {
          data = {
            ...eventoEdicao,
            endereco: {
              ...eventoEdicao.endereco,
              cep: (eventoEdicao.endereco.cep = eventoEdicao.endereco?.cep
                .split("-")
                .join("")),
            },
          };
        } else {
          data = { ...eventoEdicao };
        }
        const formData = new FormData();
        formData.append("evento", JSON.stringify(data));

        if (eventoEdicao.tags.length > 0) {
          eventoEdicao.tags.forEach((tag, index) => {
            formData.append(`tags[${index}].descricao`, tag.descricao);
          });
        }

        formData.append("arquivo", eventoEdicao.arquivos[0].arquivo);

        const res = await httpRequest.put("/api/evento/editar", formData, {
          headers: { "Content-Type": "multipart/form-data" },
        });
        this.response.putResponse(
          res.data.codigo,
          res.data.dado,
          res.data.resposta
        );
      } catch (error) {
        if (error instanceof TypeError) {
          this.response.putError(222, error.message);
        } else {
          this.response.putError(661, "Entre em contato com a Startup do SESI");
          console.error(error);
        }
      }
    },

    async getEventById(eventoId: number) {
      try {
        const response = await httpRequest.get(
          `/api/evento/detalhes?Id=${eventoId}`
        );
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          );
        }
      } catch (error) {
        console.error(error);
      }
    },
    async getOrganizersList() {
      try {
        const response = await httpRequest.get(
          "api/evento/listarOrganizadores"
        );

        if (response.data.codigo === 200) {
          this.organizadores = [];
          response.data.retorno.forEach((organizador: IOrganizador) => {
            this.organizadores.push(organizador);
          });
        }
      } catch (error) {
        console.error(error);
      }
    },
    async filterEvents(page: number, organizador: string) {
      try {
        const response = await httpRequest.get(
          `/api/evento/listarTodos?paginacao=${page}&organizador=${organizador}`
        );
        if (response.data.codigo === 200) {
          this.eventosFiltrados = [];
          response.data.retorno.forEach((evento: IEventoSimplificado) => {
            this.eventosFiltrados.push(evento);
          });
        }
      } catch (error) {
        console.error(error);
      }
    },
  },
});
