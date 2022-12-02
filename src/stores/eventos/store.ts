import { IEvento, EnderecoExistente, IUltimoEvento, Evento } from "./types";
import { defineStore } from "pinia";
import { httpRequest, getLastContent } from "../../utils/http";
import { validateEventoInput } from "../../utils/eventos/validation";
import { GeneralResponseHandler } from "../../utils/GeneralResponseHandler";

const novoEvento: IEvento = {
  id: 0,
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
  responsavel: "",
  imagem: null
}
const enderecosExistentes: Array<EnderecoExistente> = []
const ultimosEventos: Array<IUltimoEvento> = []
const eventos: Array<IUltimoEvento> = []
export const useEventoStore = defineStore('eventoStore', {
  state: () => {
    return {
      novoEvento, // TODO: remove novoEvento
      enderecosExistentes,
      ultimosEventos,
	  eventResponse: new GeneralResponseHandler(0, 'none', 'no request made yet'),
	  eventos
    }
  },
  persist: true,
  actions: {
    async postEvent(newEvent: IEvento, media?: HTMLInputElement) {
		try {
		  const evento = validateEventoInput(newEvent)
		  if(evento instanceof Evento){
			  const formData = new FormData()
			  formData.append("evento", JSON.stringify(newEvent))

			  // TODO: find out how to sent file as string (create another index.html to find out)
			  if (media?.files) formData.append("arquivos", media.files[0])
			  
			  const response = await httpRequest.post('/api/evento/incluir', formData, {
				  headers: {
				  "Content-type": "multipart/form-data",
				  }
			  })

			  this.eventResponse.putResponse(response.data.codigo, response.data.dado, response.data.resposta)
		  }else{
			  this.eventResponse.putError(223, evento.message)
		  } 
		}catch (error) {
		  if(error instanceof TypeError){
			  this.eventResponse.putError(222, error.message)
		  }else{
			console.error(error);
			
		  }

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
    },
	async getAllEvents(){
		try{
			const response = await httpRequest.get('/api/evento/listarTodas')
			if(response.data.codigo === 200){
				this.eventos = []
			
				//console.log(this.eventos);
				for(const evento of response.data.retorno){	
					this.eventos.push(evento)
				}
			}
		}catch(error){
			console.error(error);
			
		}

	},

	async deleteEvent(eventId: number){
		try{
			const response = await httpRequest.delete(`/api/evento/excluir?id=${eventId}`)
			if(response.data.codigo === 200){
				console.log('deu bom');
			}
		}catch(error){
			console.error(error);
			
		}
	},

	async putEvent(evento: IEvento, media: HTMLInputElement){
		try {
			const eventoValidado = validateEventoInput(evento)
			if(eventoValidado instanceof Evento){
				const formData = new FormData()
				formData.append("evento", JSON.stringify(evento))
  
				// TODO: find out how to sent file as string (create another index.html to find out)
				if (media?.files) formData.append("arquivos", media.files[0])
				
				const res = await httpRequest.put('/api/evento/editar', formData, {
					headers: {
					"Content-type": "multipart/form-data",
					}
				})
  
				this.eventResponse.putResponse(res.data.codigo, res.data.dado, res.data.resposta)
			}else{
				this.eventResponse.putError(223, eventoValidado.message)
			} 
		  }catch (error) {
			if(error instanceof TypeError){
				this.eventResponse.putError(222, error.message)
			}else{
			  console.error(error);
			  
			}
  
		}
	},
	async getEventById(eventoId: number){
		try{
			const response = await httpRequest.get(`/api/evento/detalhes?Id=${eventoId}`)
			if(response.data.codigo === 200){
				this.eventResponse.putResponse(response.data.codigo, response.data.retorno, response.data.resposta)
			}
		}catch(error){
			console.error(error);
			
		}
	}
  }
})