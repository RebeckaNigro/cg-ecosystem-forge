import { CustomTag } from '../tag/types';
export interface INoticia {
  id: number;
  titulo: string;
  tags: CustomTag[];
  descricao: string;
  subTitulo: string;
  dataPublicacao: string;
  arquivo: File;
  autor: string;
}

export class Noticia implements INoticia {
  id!: number;
  titulo!: string;
  tags!: CustomTag[];
  descricao!: string;
  subTitulo!: string;
  dataPublicacao!: string;
  arquivo!: File;
  autor: string;

  constructor(
    id: number,
    titulo: string,
    tags: CustomTag[],
    descricao: string,
    subTitulo: string,
    dataPublicacao: string,
    arquivo: File,
    autor: string
  ) {
    this.id = id;
    this.titulo = titulo;
    this.tags = tags;
    this.descricao = descricao;
    this.subTitulo = subTitulo;
    this.dataPublicacao = dataPublicacao;
    this.arquivo = arquivo;
    this.autor = autor;
  }
}

export class NoticiaSimplificada implements INoticiaSimplificada {
  id!: number;
  titulo!: string;
  tags!: CustomTag[];
  dataPublicacao!: string;
  dataOperacao!: string;
  arquivo: string;
  nomeUsuario: string;

  constructor(
    id: number,
    titulo: string,
    tags: CustomTag[],
    dataOperacao: string,
    dataPublicacao: string,
    arquivo: string,
    nomeUsuario: string
  ) {
    this.id = id;
    this.titulo = titulo;
    this.tags = tags;
    this.dataOperacao = dataOperacao;
    this.dataPublicacao = dataPublicacao;
    this.arquivo = arquivo;
    this.nomeUsuario = nomeUsuario;
  }
}

export class NoticiaRascunho {
  noticia: Noticia;

  constructor(noticia: Noticia) {
    this.noticia = noticia;
  }
}

export interface INoticiaSimplificada {
  id: number;
  titulo: string;
  tags: CustomTag[];
  dataPublicacao: string;
  dataOperacao: string;
  arquivo: string;
  nomeUsuario: string;
}

export interface IAutor {
  id: number;
  nomeCompleto: string;
}
