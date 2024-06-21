import { CustomTag } from '../tag/types';
export interface IDocumento {
  id: number;
  nome: string;
  descricao: string;
  tags: CustomTag[];
  tipoDocumentoId: number;
  documentoArea: string;
  documentoAreaid: number;
  instituicaoId: number;
  instituicao: string;
  data: string;
  download: string;
  aprovado: boolean;
  autor: string;
  arquivo: File;
}

export class Documento implements IDocumento {
  id: number;
  nome: string;
  descricao: string;
  tags: CustomTag[];
  tipoDocumentoId: number;
  documentoArea: string;
  documentoAreaid: number;
  instituicaoId: number;
  instituicao: string;
  data: string;
  download: string;
  arquivo: File;
  aprovado: boolean;
  autor: string;

  constructor(
    id: number,
    nome: string,
    descricao: string,
    tags: CustomTag[],
    tipoDocumentoId: number,
    documentoAreaid: number,
    documentoArea: string,
    instituicaoId: number,
    instituicao: string,
    data: string,
    download: string,
    aprovado: boolean,
    autor: string,
    arquivo: File
  ) {
    this.id = id;
    this.nome = nome;
    this.descricao = descricao;
    this.tags = tags;
    this.tipoDocumentoId = tipoDocumentoId;
    this.documentoAreaid = documentoAreaid;
    this.instituicaoId = instituicaoId;
    this.documentoAreaid = documentoAreaid;
    this.data = data;
    this.download = download;
    this.documentoArea = documentoArea;
    this.instituicao = instituicao;
    this.aprovado = aprovado;
    this.autor = autor;
    this.arquivo = arquivo;
  }
}
export interface IDocumentoSimplificado {
  id: number;
  nome: string;
  descricao: string;
  documentoArea: string;
  ultimaOperacao: string;
  autor: string;
  tags: CustomTag[];
  data: string;
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
