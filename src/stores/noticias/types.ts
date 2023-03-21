export interface INoticia {
  id: number
  titulo: string
  tags: NewsTag[]
  descricao: string
  subTitulo: string
  dataPublicacao: string
  arquivo: File
}

export class Noticia implements INoticia {
  id!: number
  titulo!: string
  tags!: NewsTag[]
  descricao!: string
  subTitulo!: string
  dataPublicacao!: string
  arquivo!: File

  constructor(
    id: number,
    titulo: string,
    tags: NewsTag[],
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
  tags!: NewsTag[]
  dataPublicacao!: string
  arquivo: string

  constructor(
    id: number,
    titulo: string,
    tags: NewsTag[],
    dataPublicacao: string,
    arquivo: string
  ) {
    this.id = id
    this.titulo = titulo
    this.tags = tags
    this.dataPublicacao = dataPublicacao
    this.arquivo = arquivo
  }
}

export class NoticiaRascunho {
  noticia: Noticia
  termosDeUso: boolean

  constructor(noticia: Noticia, termosDeUso: boolean) {
    this.noticia = noticia
    this.termosDeUso = termosDeUso
  }
}

export interface INoticiaSimplificada {
  id: number
  titulo: string
  tags: NewsTag[]
  dataPublicacao: string
  arquivo: string
}

export class NewsTag {
  id: number
  descricao: string

  constructor(descricao: string) {
    this.id = -1
    this.descricao = descricao
  }
}
