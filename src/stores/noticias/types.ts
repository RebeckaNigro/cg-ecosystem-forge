export interface INoticia {
	id: number
	titulo: string
	descricao: string
	subTitulo: string
	dataPublicacao: string
}

export class Noticia implements INoticia {
	id!: number
	titulo!: string
	descricao!: string
	subTitulo!: string
	dataPublicacao!: string

	constructor(id: number, titulo: string, descricao: string, subTitulo: string, dataPublicacao: string) {
		[this.id, this.titulo, this.descricao, this.subTitulo, this.dataPublicacao] = arguments;
	}
}

export interface IUltimaNoticia{
	id: number
	titulo: string
	dataPublicacao: string
}