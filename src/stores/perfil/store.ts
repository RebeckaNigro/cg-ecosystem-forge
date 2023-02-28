import {IPerfil, Perfil} from "./types";
import {defineStore} from "pinia";
import {httpRequest} from "../../utils/http";
import {GeneralResponseHandler} from "../../utils/GeneralResponseHandler";

const perfil: IPerfil = {
    id: 0,
    nomeCompleto: "",
    cpf: "",
    dataNascimento: new Date(),
    telefone: "",
    cargo: "",
    intituicaoId: 0,
    uf: "",
    cidade: "",
    logradouro: "",
    bairro: "",
    numero: "",
    cep: "",
    email: "",
    password: "",
    confirmPassword: ""
}

const PERFIL_BASE_HOST = '/api/autenticacao'

export const usePerfilStore = defineStore('perfilStore', {
    state: () => {
        return {
            perfil,
            perfilResponse: new GeneralResponseHandler(0, 'none', 'no request made yet'),
        }
    },
    persist: true,
    actions: {
        async postPerfil(newPerfil: IPerfil) {
            try {
//                const evento = validateEventoInput(newEvent)
//                if(evento instanceof Evento){

                const response = await httpRequest.post(`${PERFIL_BASE_HOST}/registrar-usuario-comum`, newPerfil)
                this.perfilResponse.putResponse(response.data.codigo, response.data.dado, response.data.resposta)

//                }else{
//                    this.eventResponse.putError(223, evento.message)
//                }
            } catch (error) {
                if (error instanceof TypeError) {
                    this.perfilResponse.putError(222, error.message)
                } else {
                    console.error(error);
                }
            }
        },
    }
})