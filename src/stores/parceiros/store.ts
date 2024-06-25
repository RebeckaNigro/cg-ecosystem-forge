import { defineStore } from 'pinia';
import type { PartnerInterface } from './types';

export const useParceirosStore = defineStore('parceirosStore', {
  state: () => {
    return {
      parceiros: new Array<PartnerInterface>(),
    };
  },
  persist: true,
  actions: {
    setPartner(p: PartnerInterface) {
      this.parceiros.push(p);
    },
  },
  getters: {
    getPartner: (state) => {
      return (id: string) => state.parceiros.find((p) => p.parceiroId === id);
    },
  },
});
