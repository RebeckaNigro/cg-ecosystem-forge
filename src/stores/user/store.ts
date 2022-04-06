import { defineStore } from "pinia";
import { UserToAuth } from "./type";

const userToAuth: UserToAuth = {
  email: '',
  password: ''
}

export const useUserStore = defineStore('userStore', {
  state: () => {
    return {
      userToAuth
    }
  }
})