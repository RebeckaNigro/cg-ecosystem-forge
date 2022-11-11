import { defineStore } from "pinia";
import { INoticia, IUltimaNoticia, Noticia } from "./types";
import { getLastContent, httpRequest } from "../../utils/http";
import { validateNoticiaInput } from "../../utils/noticias/validation";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";

const lastNews: Array<IUltimaNoticia> = []
const allNews: Array<IUltimaNoticia> = []
export const useNoticiaStore = defineStore('noticiaStore', {
  state: () => {
    return {
	  novaNoticiaResponse: new GeneralResponseHandler(0, 'none', 'no request made yet'),
	  lastNews,
	  allNews
    }
  },
  persist: true,
  actions: {
    async putNews(novaNoticia: INoticia) {
	  // TODO salvar capa da noticia no banco
      try {
		const noticiaInput = validateNoticiaInput({...novaNoticia})

		if(noticiaInput instanceof Noticia){
			const response = await httpRequest.post('/api/noticia/incluir', novaNoticia)
			this.novaNoticiaResponse.putResponse(response.data.codigo, response.data.dado, response.data.resposta)
		}else{
			this.novaNoticiaResponse.putError(223, noticiaInput.message)
		}
        
      } catch (error) {
		if(error instanceof TypeError){
			this.novaNoticiaResponse.putError(222, error.message)
		}else{
			this.novaNoticiaResponse.putError(661, 'Entre em contato com a Startup do SESI')
			console.error(error)
		}
        
      }

	  return false
    },

	async getLastNews(){
		const response = await getLastContent("noticia")
		if(response.data.codigo === 200){
			this.lastNews = []
			for(const news of response.data.retorno){
				this.lastNews.push(news)
			}
		}
	},

	async getAllNews(){
		try{
			const response = await httpRequest.get('api/noticia/listarTodas')
			if(response.data.codigo === 200){
				this.novaNoticiaResponse.putResponse(response.data.codigo, response.data.retorno, response.data.resposta)
				this.allNews = []
				for(const news of response.data.retorno){
					this.allNews.push(news)
				}
			}
		}catch(error){
			console.error(error);
			
		}
	}
  },
  getters: {
  }
});