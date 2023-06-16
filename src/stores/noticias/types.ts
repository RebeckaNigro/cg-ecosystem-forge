import { CustomTag } from "../tag/types"
export interface INoticia {
  id: number
  titulo: string
  tags: CustomTag[]
  descricao: string
  subTitulo: string
  dataPublicacao: string
  arquivo: File
}

export class Noticia implements INoticia {
  id!: number
  titulo!: string
  tags!: CustomTag[]
  descricao!: string
  subTitulo!: string
  dataPublicacao!: string
  arquivo!: File

  constructor(
    id: number,
    titulo: string,
    tags: CustomTag[],
    descricao: string,
    subTitulo: string,
    dataPublicacao: string,
    arquivo: File
  ) {
    this.id = id
    this.titulo = titulo
    this.tags = tags
    this.descricao = descricao
    this.subTitulo = subTitulo
    this.dataPublicacao = dataPublicacao
    this.arquivo = arquivo
  }
}

export class NoticiaSimplificada implements INoticiaSimplificada {
  id!: number
  titulo!: string
  tags!: CustomTag[]
  dataPublicacao!: string
  dataOperacao!: string
  arquivo: string
  nomeUsuario: string

  constructor(
    id: number,
    titulo: string,
    tags: CustomTag[],
    dataOperacao: string,
    dataPublicacao: string,
    arquivo: string,
	nomeUsuario: string
  ) {
    this.id = id
    this.titulo = titulo
    this.tags = tags
    this.dataOperacao = dataOperacao
    this.dataPublicacao = dataPublicacao
    this.arquivo = arquivo
	this.nomeUsuario = nomeUsuario
  }
}

export class NoticiaRascunho {
  noticia: Noticia

  constructor(noticia: Noticia) {
    this.noticia = noticia
  }
}

export interface INoticiaSimplificada {
  id: number
  titulo: string
  tags: CustomTag[]
  dataPublicacao: string
  dataOperacao: string
  arquivo: string
  nomeUsuario: string
}
