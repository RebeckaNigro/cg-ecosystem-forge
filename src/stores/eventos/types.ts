import { Instituicao } from "./../instituicao/types"
import { CustomTag } from "../tag/types"
export interface IEvento {
  id: number
  instituicaoId: number
  tags: CustomTag[]
  tipoEventoId: TipoEvento
  titulo: string
  descricao: string
  dataInicio: string
  dataTermino: string
  local: string
  enderecoId: number | null
  endereco: INovoEndereco | null
  linkExterno: string
  exibirMaps: boolean
  responsavel: string
  arquivos: Array<IFile>
}

export class Evento implements IEvento {
  id!: number
  instituicaoId!: number
  tags: CustomTag[]
  tipoEventoId!: TipoEvento
  titulo!: string
  descricao!: string
  dataInicio!: string
  dataTermino!: string
  local!: string
  enderecoId!: number | null
  endereco!: INovoEndereco | null
  linkExterno!: string
  exibirMaps!: boolean
  responsavel!: string
  arquivos!: Array<IFile>

  constructor(
    id: number,
    instituicaoId: number,
    tags: CustomTag[],
    tipoEventoId: TipoEvento,
    titulo: string,
    descricao: string,
    dataInicio: string,
    dataTermino: string,
    local: string,
    enderecoId: number | null,
    endereco: INovoEndereco | null,
    linkExterno: string,
    exibirMaps: boolean,
    responsavel: string,
    arquivos: File
  ) {
    ;[
      this.instituicaoId,
      this.tags,
      this.tipoEventoId,
      this.titulo,
      this.descricao,
      this.dataInicio,
      this.dataTermino,
      this.local,
      this.enderecoId,
      this.endereco,
      this.linkExterno,
      this.exibirMaps,
      this.responsavel,
      this.arquivos
    ] = arguments
  }
}

export enum TipoEvento {
  Presencial = 1,
  Online = 2,
  Palestra = 3,
  Workshop = 4
}

export interface INovoEndereco {
  id: number | null
  cep: string
  logradouro: string
  numero: string
  complemento: string
  pontoReferencia: string
  bairro: string
  cidade: string
  uf: string
}

export class NovoEndereco implements INovoEndereco {
  id: number | null
  cep: string
  logradouro: string
  numero: string
  complemento: string
  pontoReferencia: string
  bairro: string
  cidade: string
  uf: string

  constructor(
    id: number | null,
    cep: string,
    logradouro: string,
    numero: string,
    complemento: string,
    pontoReferencia: string,
    bairro: string,
    cidade: string,
    uf: string
  ) {
    ;[
      this.id,
      this.cep,
      this.logradouro,
      this.numero,
      this.complemento,
      this.pontoReferencia,
      this.bairro,
      this.cidade,
      this.uf
    ] = arguments
  }
}

export interface EnderecoExistente {
  cep: string
  enderecoId: number
  tipoEndereco: string
  logradouro: string
  numero: string
  complemento: string
  pontoReferencia: string
  bairro: string
  cidade: string
  uf: string
}

export interface IEventoSimplificado {
  id: number
  titulo: string
  tags: CustomTag[]
  dataInicio: string
  dataTermino: string
  local: string
  arquivo: string | null
  ultimaAtualizacao: string
}

export class EventoSimplificado implements IEventoSimplificado {
  id: number
  titulo: string
  tags: CustomTag[]
  dataInicio: string
  dataTermino: string
  local: string
  arquivo: string | null
  ultimaAtualizacao: string = ""

  constructor(
    id: number,
    tags: CustomTag[],
    titulo: string,
    dataInicio: string,
    dataTermino: string,
    local: string,
    arquivo: File
  ) {
    ;[
      this.id,
      this.tags,
      this.titulo,
      this.dataInicio,
      this.dataTermino,
      this.local,
      this.arquivo
    ] = arguments
  }
}

export interface IFile {
  id: number
  arquivo: string
  extensao: string
  nomeOriginal: string
}

export class EventoRascunho {
  evento: Evento
  instituicao: Instituicao
  horaInicio: string
  horaTermino: string

  constructor(
    evento: Evento,
    instituicao: Instituicao,
    horaInicio: string,
    horaTermino: string
  ) {
    this.evento = evento
    this.horaInicio = horaInicio
    this.horaTermino = horaTermino
    this.instituicao = instituicao
  }
}

export interface IOrganizador{
	responsavel: string
}