import { defineStore } from 'pinia';

export const useCookiesStore = defineStore('cookiesStore', {
  state: () => {
    return {
      choseCookies: false,
    };
  },
  persist: true,
});
