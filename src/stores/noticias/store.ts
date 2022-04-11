import { defineStore } from "pinia";
import { Noticia } from "./types";
import { httpRequest } from "../../utils/http";

export const useNoticiaStore = defineStore('noticiaStore', {
  state: () => {
    return {
      novaNoticia: new Noticia()
    }
  },
  persist: true,
  actions: {
    async putNews() {
      try {
        const response = await httpRequest.post('/api/noticia/incluir', this.novaNoticia)
      } catch (error) {
        console.error(error)
      }
    }
  },
  getters: {
  }
});