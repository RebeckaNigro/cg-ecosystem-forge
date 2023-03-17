import { Modal } from "bootstrap"
import { defineStore } from "pinia"
export const useModalStore = defineStore("modalStore", {
  state: () => {
    return {
      modalInstance: {},
      visible: false,
      options: {
        message: "",
        status: "info"
      }
    }
  },
  actions: {
    showErrorModal(message: string) {
      this.options = {
        message: message,
        status: "error"
      }
      this.visible = true

      this.showModal()
    },

    showInfoModal(message: string) {
      this.options = {
        message: message,
        status: "info"
      }
      this.visible = true

      this.showModal()
    },

    showSuccessModal(message: string) {
      this.options = {
        message: message,
        status: "success"
      }
      this.visible = true

      this.showModal()
    },

    showWarningModal(message: string) {
      this.options = {
        message: message,
        status: "warning"
      }
      this.visible = true

      this.showModal()
    },

    showModal() {
      this.modalInstance.show()
    },

    closeModal() {
      this.modalInstance.toggle()
    }
  },
  persist: false
})
