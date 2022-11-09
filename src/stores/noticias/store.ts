import { defineStore } from "pinia";
import { Noticia } from "./types";
import { httpRequest } from "../../utils/http";
import { validateNoticiaInput } from "../../utils/noticias/validation";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";

export const useNoticiaStore = defineStore('noticiaStore', {
  state: () => {
    return {
      novaNoticia: new Noticia("","","","",""),
	  novaNoticiaResponse: new GeneralResponseHandler(0, 'none', 'no request made yet')
    }
  },
  persist: true,
  actions: {
    async putNews() {
      try {
		const noticiaInput = validateNoticiaInput({id: "", titulo: this.novaNoticia.titulo, descricao: this.novaNoticia.descricao, subTitulo: this.novaNoticia.subTitulo, dataPublicacao: this.novaNoticia.dataPublicacao})

		if(noticiaInput instanceof Noticia){
			const response = await httpRequest.post('/api/noticia/incluir', this.novaNoticia)
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
    }
  },
  getters: {
  }
});