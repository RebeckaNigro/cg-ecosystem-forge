import { defineStore } from 'pinia';
import { GeneralResponseHandler } from '../../utils/GeneralResponseHandler';
import { httpRequest } from '../../utils/http';
import { CustomTag } from './types';

const allTags: Array<CustomTag> = [];

export const useTagStore = defineStore('tagStore', {
  state: () => {
    return {
      response: new GeneralResponseHandler(0, 'none', 'no request made yet'),
      allTags,
    };
  },
  persist: false,
  actions: {
    async getTags() {
      try {
        const response = await httpRequest.get('/api/Tag/listarTags');
        if (response.data.codigo === 200) {
          this.response.putResponse(
            response.data.codigo,
            response.data.retorno,
            response.data.resposta
          );
          this.allTags = [];
          for (const tags of response.data.retorno) {
            this.allTags.push(tags);
          }
        }
      } catch (error) {
        console.error(error);
      }
    },
  },
  getters: {},
});
