import { defineStore } from "pinia"
export const useModalStore = defineStore("modalStore", {
  state: () => {
    return {
      modalInstance: {},
      visible: false,
      loading: false,
      options: {
        message: "",
        status: ""
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
      this.loading = false
      this.showModal()
    },

    showInfoModal(message: string) {
      this.options = {
        message: message,
        status: "info"
      }
      this.visible = true
      this.loading = false
      this.showModal()
    },

    showSuccessModal(message: string) {
      this.options = {
        message: message,
        status: "success"
      }
      this.visible = true
      this.loading = false
      this.showModal()
    },

    showWarningModal(message: string) {
      this.options = {
        message: message,
        status: "warning"
      }
      this.visible = true
      this.loading = false
      this.showModal()
    },

    showLoadingModal() {
      this.loading = true
      this.visible = true

      this.showModal()
    },

    showModal() {
	  //@ts-ignore
      this.modalInstance.show()
    },

    closeModal() {
      //@ts-ignore
		this.modalInstance.hide()
    }
  },
  persist: false
})
