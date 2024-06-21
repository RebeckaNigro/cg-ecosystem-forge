import { defineStore } from 'pinia';
import { GeneralResponseHandler } from '../../utils/GeneralResponseHandler';
import { httpRequest } from '../../utils/http';
import { IPerfil } from './types';

const perfil: IPerfil = {
  id: 0,
  nomeCompleto: '',
  cpf: '',
  dataNascimento: new Date(),
  telefone: '',
  cargo: '',
  instituicaoId: 0,
  uf: '',
  cidade: '',
  logradouro: '',
  bairro: '',
  numero: '',
  cep: '',
  email: '',
  password: '',
  confirmPassword: '',
};

const PERFIL_BASE_RESOURCE = '/api/autenticacao';

export const usePerfilStore = defineStore('perfilStore', {
  state: () => {
    return {
      perfil,
      perfilResponse: new GeneralResponseHandler(0, 'none', 'no request made yet'),
    };
  },
  persist: false,
  actions: {
    async postPerfil(newPerfil: IPerfil) {
      try {
        const response = await httpRequest.post(
          `${PERFIL_BASE_RESOURCE}/registrar-usuario-comum`,
          newPerfil
        );
        this.perfilResponse.putResponse(
          response.data.codigo,
          response.data.dado,
          response.data.resposta
        );
      } catch (error) {
        if (error instanceof TypeError) {
          this.perfilResponse.putError(222, error.message);
        } else {
          console.error(error);
        }
      }
    },
  },
});
