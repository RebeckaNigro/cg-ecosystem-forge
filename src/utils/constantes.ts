import { InjectionKey } from "vue";
import { AxiosInstance } from "axios";

export const CONSTANTES = {
  defaultUrl: 'https://www.integra.sesims.com.br' // FIXME: when possible, change to ecossistema api url
}

// origem: 5 -> evento; 4 -> noticia
export const buildImageSrc = (id: number, fileName: string, origem: number): string=> {
	return `http://dev-api.ecossistemadeinovacaocg.com.br/api/documento/downloadDocumento?id=${id}&nome=${fileName}&origem=${origem}`
}