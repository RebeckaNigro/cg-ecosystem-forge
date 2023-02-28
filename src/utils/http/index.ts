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

httpRequest.interceptors.request.use((config: any) => {
  const userStore = useUserStore();
  if (userStore.loggedUser.token) config.headers.Authorization = `Bearer ${userStore.loggedUser.token}`
  return config
}, (error) => {
  return Promise.reject(error)
})

httpRequest.interceptors.response.use((response: any) => {
	const userStore = useUserStore();
    if(response.data.codigo == 403) userStore.disconnected = true
	return response
}, (error)=> {
	console.log(error);
    return error
})

export const getFromCEP = async (cep: string) => {
  try {
    const response = await (await fetch(`https://brasilapi.com.br/api/cep/v2/${cep}`)).json()
    return response
  } catch (error) {
    return error
  }
}

export const getLastContent = async (contentType: string) => {
  const response = await httpRequest.get(`/api/${contentType}/listarUltimas`)
  return response
}