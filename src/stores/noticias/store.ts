import { defineStore } from "pinia"
import { INoticia, INoticiaSimplificada, Noticia, Tag } from "./types"
import { getLastContent, httpRequest } from "../../utils/http"
import { validateNoticiaInput } from "../../utils/noticias/validation"
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler"

const lastNews: Array<INoticiaSimplificada> = []
const allNews: Array<INoticiaSimplificada> = []
const allUserNews: Array<INoticiaSimplificada> = []
const allTags: Array<Tag> = []
export const useNoticiaStore = defineStore("noticiaStore", {
  state: () => {
    return {
      response: new GeneralResponseHandler(0, "none", "no request made yet"),
      lastNews,
      allNews,
      allUserNews,
      allTags
    }
  },
  persist: true,
  actions: {
    async postNews(novaNoticia: INoticia) {
      // TODO salvar capa da noticia no banco
      try {
        const formData = new FormData()
        formData.append("titulo", novaNoticia.titulo)
        formData.append("descricao", novaNoticia.descricao)
        formData.append("subTitulo", novaNoticia.subTitulo)
        formData.append("dataPublicacao", novaNoticia.dataPublicacao.toString())

        novaNoticia.tags.forEach((tag, index) => {
          formData.append(`tags[${index}].descricao`, tag.descricao)
        })

        formData.append("arquivo", novaNoticia.arquivo)

        const response = await httpRequest.post(
          "/api/noticia/incluir",
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

    async getLastNews() {
      const response = await getLastContent("noticia")
      if (response.data.codigo === 200) {
        this.lastNews = []
        for (const news of response.data.retorno) {
          this.lastNews.push(news)
        }
      }
    },

    async getUserNews() {
      try {
        const response = await httpRequest.get(
          "/api/noticia/listarPorUsuarioId"
        )
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          )
          this.allUserNews = []
          for (const news of response.data.retorno) {
            this.allUserNews.push(news)
          }
        }
      } catch (error) {
        console.error(error)
      }
    },

    async getTags() {
      try {
        const response = await httpRequest.get("/api/Tag/listarTags")
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          )
          this.allTags = []
          for (const tags of response.data.retorno) {
            this.allTags.push(tags)
          }
        }
      } catch (error) {
        console.error(error)
      }
    },

    async getAllNews() {
      try {
        const response = await httpRequest.get("api/noticia/listarTodas")

        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          )
          this.allNews = []
          for (const news of response.data.retorno) {
            this.allNews.push(news)
          }
        }
      } catch (error) {
        console.error(error)
      }
    },

    async deleteNews(noticiaId: number) {
      try {
        const response = await httpRequest.delete(
          `/api/noticia/excluir?id=${noticiaId}`
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
          this.response.putError(661, "Erro ao remover notícia.")
          console.error(error)
        }
      }
    },

    async putNews(noticia: INoticia) {
      // TODO editar capa da noticia
      try {
        const noticiaInput = validateNoticiaInput({ ...noticia })
        if (noticiaInput instanceof Noticia) {
          const res = await httpRequest.put("/api/noticia/editar", noticia)
          this.response.putResponse(
            res.data.codigo,
            res.data.dado,
            res.data.resposta
          )
        } else {
          this.response.putError(223, noticiaInput.message)
        }
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

    async getNewsById(noticiaId: number) {
      try {
        const response = await httpRequest.get(
          `/api/noticia/detalhes?Id=${noticiaId}`
        )
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          )
        } else {
          console.log(response.data.codigo)
        }
      } catch (error) {
        console.error(error)
      }
    }
  },
  getters: {}
})
