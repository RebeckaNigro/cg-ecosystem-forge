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
    },

    showWarningMessage(message: string) {
      this.options = {
        message: message,
        type: "warning",
        dispensable: true,
        timeout: false
      }
      this.visible = true
    },

    showSuccessMessage(message: string) {
      this.options = {
        message: message,
        type: "success",
        dispensable: true,
        timeout: false
      }
      this.visible = true
    },

    showInfoMessage(message: string) {
      this.options = {
        message: message,
        type: "warning",
        dispensable: true,
        timeout: false
      }
      this.visible = true
    },

    showTimeoutErrorMessage(message: string) {
      this.options = {
        message: message,
        type: "error",
        dispensable: false,
        timeout: true
      }
      this.visible = true
    },

    showTimeoutWarningMessage(message: string) {
      this.options = {
        message: message,
        type: "warning",
        dispensable: false,
        timeout: true
      }
      this.visible = true
    },

    showTimeoutSuccessMessage(message: string) {
      this.options = {
        message: message,
        type: "success",
        dispensable: false,
        timeout: true
      }
      this.visible = true
    },

    showTimeoutInfoMessage(message: string) {
      this.options = {
        message: message,
        type: "warning",
        dispensable: false,
        timeout: true
      }
      this.visible = true
    }
  },
  persist: false
})
