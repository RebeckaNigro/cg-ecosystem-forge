
// origem: 5 -> evento; 4 -> noticia
export const buildImageSrc = (id: number, fileName: string, origem: number): string=> {
	return `http://dev-api.ecossistemadeinovacaocg.com.br/api/documento/downloadDocumento?id=${id}&nome=${fileName}&origem=${origem}`
}