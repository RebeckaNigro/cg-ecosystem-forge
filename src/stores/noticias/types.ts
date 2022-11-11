export interface INoticia {
	id: string
	titulo: string
	descricao: string
	subTitulo: string
	dataPublicacao: string
}

export class Noticia implements INoticia {
	id!: string
	titulo!: string
	descricao!: string
	subTitulo!: string
	dataPublicacao!: string

	constructor(id: string, titulo: string, descricao: string, subTitulo: string, dataPublicacao: string) {
		[this.id, this.titulo, this.descricao, this.subTitulo, this.dataPublicacao] = arguments;
	}
}

export interface IUltimaNoticia{
	id: number
	titulo: string
	dataPublicacao: string
}