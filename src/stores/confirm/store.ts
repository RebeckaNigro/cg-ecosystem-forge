//@ts-nocheck
import { Modal } from "bootstrap"
import { defineStore } from "pinia"
export const useConfirmStore = defineStore("confirmStore", {
  state: () => {
    return {
      confirmInstance: {},
      visible: false,
      options: {
        message: "",
        parameter: {}
      }
    }
  },
  actions: {
    showConfirmModal(message: string, parameter: any) {
      this.options = {
        message: message,
        parameter: parameter
      }
      this.visible = true
      this.showConfirm()
    },

    showConfirm() {
      this.confirmInstance.show()
    },

    closeConfirm() {
      this.confirmInstance.toggle()
    },
	setConfirmInstance(confirmInstance: Modal){
		this.confirmInstance = confirmInstance
	}
  },
  persist: false
})
