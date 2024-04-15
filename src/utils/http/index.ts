import axios, {
  AxiosInstance,
  AxiosRequestConfig,
  AxiosRequestHeaders
} from "axios"
import router from "./../../router"
import { useUserStore } from "../../stores/user/store"
export const baseURL = "https://hom-api.ecossistemadeinovacaocg.com.br"

// TODO: provide axios instance throught provide/inject
// TODO: enhance headers when api routes gets private
export const httpRequest: AxiosInstance = axios.create({
  baseURL: baseURL,
  headers: {
    "Content-type": "application/json",
	"Access-Control-Allow-Origin": "*"
  }
})

httpRequest.interceptors.request.use(
  (config: any) => {
    const userStore = useUserStore()
    if (userStore.loggedUser.token)
      config.headers.Authorization = `Bearer ${userStore.loggedUser.token}`
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

httpRequest.interceptors.response.use(
  (response: any) => {
    const userStore = useUserStore()

    if (response.data.codigo == 440 || response.status == 440)
      userStore.disconnected = true
    return response
  },
  (error) => {
    const userStore = useUserStore()

    if (error.response.status === 440) {
      userStore.logout()
      userStore.disconnected = true

      router.push({ name: "Login" })
    }

    return error
  }
)

export const getFromCEP = async (cep: string) => {
  try {
    const response = await (
      await fetch(`https://brasilapi.com.br/api/cep/v2/${cep}`)
    ).json()
    return response
  } catch (error) {
    return error
  }
}

export const getLastContent = async (contentType: string) => {
	let response = null
	if(contentType === 'evento' || contentType === 'documento'){
		response = await httpRequest.get(`/api/${contentType}/listarUltimos`)
	}else{
		response = await httpRequest.get(`/api/${contentType}/listarUltimas`)
	}
  return response
}
