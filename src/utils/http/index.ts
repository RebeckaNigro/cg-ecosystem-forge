import axios, { AxiosInstance, AxiosRequestConfig, AxiosRequestHeaders } from "axios";
import { useUserStore } from "../../stores/user/store";

// TODO: provide axios instance throught provide/inject
// TODO: enhance headers when api routes gets private
export const httpRequest: AxiosInstance = axios.create({
  baseURL: "http://dev-api.ecossistemadeinovacaocg.com.br",
  headers: {
    "Content-type": "application/json",
  },
});

// const userStore = useUserStore();

// httpRequest.interceptors.request.use((config: any) => {
//   if (userStore.loggedUser.token) config.headers.Authorization = `Bearer ${userStore.loggedUser.token}`
//   return config
// }, (error) => {
//   return Promise.reject(error)
// })