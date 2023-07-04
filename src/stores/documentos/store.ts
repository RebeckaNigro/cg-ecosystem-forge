import { defineStore } from "pinia"
import { IDocumento, IDocumentoSimplificado } from "../documentos/types"
import { getLastContent, httpRequest } from "../../utils/http"
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler"

const lastDocs: Array<IDocumentoSimplificado> = []
const allDocs: Array<IDocumentoSimplificado> = []
const allUserDocs: Array<IDocumentoSimplificado> = []
const allUserLastDocs: Array<IDocumentoSimplificado> = []
const researches: Array<IDocumentoSimplificado> = []
export const useDocumentStore = defineStore("documentStore", {
	state: () => {
		return {
			response: new GeneralResponseHandler(0, "none", "no request made yet"),
			lastDocs,
			allDocs,
			allUserDocs,
			allUserLastDocs,
			researches
		}
	},
	persist: false,
	actions: {
		async postDocument(novoDocumento: IDocumento) {
			try {
				const formData = new FormData()
				formData.append("nome", novoDocumento.nome)
				formData.append("descricao", novoDocumento.descricao)

				formData.append(
					"documentoAreaId",
					novoDocumento.documentoAreaid.toString()
				)
				formData.append("instituicaoId", novoDocumento.instituicaoId.toString())
				formData.append("data", novoDocumento.data.toString())
				if (novoDocumento.tags.length > 0) {
					novoDocumento.tags.forEach((tag, index) => {
						formData.append(`tags[${index}].descricao`, tag.descricao)
					})
				}
				formData.append("arquivo", novoDocumento.arquivo)
				const response = await httpRequest.post(
					"/api/documento/incluir",
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

		async getLastDocs() {
			const response = await getLastContent("documento")
			if (response.data.codigo === 200) {
				this.lastDocs = []
				for (const news of response.data.retorno) {
					this.lastDocs.push(news)
				}
			}
		},

		async getUserDocs() {
			try {
				const response = await httpRequest.get(
					"/api/documento/listarPorUsuarioId"
				)
				if (response.data.codigo === 200) {
					this.response.putResponse(
						response.data.codigo,
						response.data.retorno,
						response.data.resposta
					)
					this.allUserDocs = []
					for (const docs of response.data.retorno) {
						this.allUserDocs.push(docs)
					}
				}
			} catch (error) {
				console.error(error)
			}
		},

		async getUserLastDocs() {
			try {
				const response = await httpRequest.get(
					"/api/documento/listarUltimosPorUsuarioId"
				)
				if (response.data.codigo === 200) {
					this.response.putResponse(
						response.data.codigo,
						response.data.retorno,
						response.data.resposta
					)
					this.allUserLastDocs = []
					for (const docs of response.data.retorno) {
						this.allUserLastDocs.push(docs)
					}
				}
			} catch (error) {
				console.error(error)
			}
		},

		async getAllDocs(page: number) {
			try {
				const response = await httpRequest.get(`api/documento/listarTodos?paginacao=${page.toString()}`)
				if (response.data.codigo === 200) {
					this.response.putResponse(
						response.data.codigo,
						response.data.retorno,
						response.data.resposta
					)
					this.allDocs = []
					this.allDocs.concat(response.data.retorno)

					
				}else{
					this.response.putError(response.data.codigo, response.data.resposta)
					
					
				}
			} catch (error) {
				console.error(error)
			}
		},

		async deleteDoc(documentId: number) {
			try {
				const response = await httpRequest.delete(
					`/api/documento/excluir?id=${documentId}`
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
					this.response.putError(661, "Erro ao remover documento.")
					console.error(error)
				}
			}
		},

		async putDoc(documentoEdicao: IDocumento) {
			try {
				const formData = new FormData()
				formData.append("Id", documentoEdicao.id.toString())
				formData.append("nome", documentoEdicao.nome)
				formData.append("descricao", documentoEdicao.descricao)
				
				formData.append(
					"documentoAreaId",
					documentoEdicao.documentoAreaid.toString()
				)
				formData.append(
					"instituicaoId",
					documentoEdicao.instituicaoId.toString()
				)
				formData.append("data", documentoEdicao.data)
				if (documentoEdicao.tags.length > 0) {
					documentoEdicao.tags.forEach((tag, index) => {
						formData.append(`tags[${index}].descricao`, tag.descricao)
					})
				}
				formData.append("arquivo", documentoEdicao.arquivo)
				const res = await httpRequest.put("/api/documento/editar", formData, {
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
		async getDocDetailsById(documentId: number) {
			try {
				const response = await httpRequest.get(
					`/api/documento/detalhes?id=${documentId}`
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
		},
		async getResearches(page: number){
			await this.getAllDocs(page)
			this.researches = this.allDocs.filter((doc: IDocumentoSimplificado) => {
				return doc.documentoArea.toLowerCase() === 'pesquisa' ? true : false
				
			})

		}

	},

	getters: {}
})
