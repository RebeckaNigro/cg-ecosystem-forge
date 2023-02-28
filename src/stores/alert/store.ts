import { defineStore } from "pinia"

export const useAlertStore = defineStore("alertStore", {
  state: () => {
    return {
      visible: false,
      options: {
        message: "",
        type: "",
        dispensable: false,
        timeout: false
      }
    }
  },
  actions: {
    showErrorMessage(message: string) {
      this.options = {
        message: message,
        type: "error",
        dispensable: true,
        timeout: false
      }

      this.visible = true
    }
  },
  persist: false
})
