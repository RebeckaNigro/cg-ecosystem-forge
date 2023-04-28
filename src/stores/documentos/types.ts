import { CustomTag } from "../tag/types"
export interface IDocumento {
  id: number
  nome: string
  descricao: string
  tags: CustomTag[]
  tipoDocumentoId: number
  documentoAreaid: number
  instituicaoId: number
  data: string
  arquivo: File
}

export class Documento implements IDocumento {
  id: number
  nome: string
  descricao: string
  tags: CustomTag[]
  tipoDocumentoId: number
  documentoAreaid: number
  instituicaoId: number
  data: string
  arquivo: File

  constructor(
    id: number,
    nome: string,
    descricao: string,
    tags: CustomTag[],
    tipoDocumentoId: number,
    documentoAreaid: number,
    instituicaoId: number,
    data: string,
    arquivo: File
  ) {
    this.id = id
    this.nome = nome
    this.descricao = descricao
    this.tags = tags
    this.tipoDocumentoId = tipoDocumentoId
    this.documentoAreaid = documentoAreaid
    this.instituicaoId = instituicaoId
    this.documentoAreaid = documentoAreaid
    this.data = data
    this.arquivo = arquivo
  }
}
export interface IDocumentoSimplificado {
  id: number
  nome: string
  tags: CustomTag[]
  data: string
  arquivo: string
}

// export class NoticiaSimplificada implements INoticiaSimplificada {
//   id!: number
//   titulo!: string
//   tags!: CustomTag[]
//   dataPublicacao!: string
//   arquivo: string

//   constructor(
//     id: number,
//     titulo: string,
//     tags: CustomTag[],
//     dataPublicacao: string,
//     arquivo: string
//   ) {
//     this.id = id
//     this.titulo = titulo
//     this.tags = tags
//     this.dataPublicacao = dataPublicacao
//     this.arquivo = arquivo
//   }
// }
