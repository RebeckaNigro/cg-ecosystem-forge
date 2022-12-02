export interface IEvento {
  id: number
  instituicaoId: number
  tipoEventoId: number
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
  imagem: string | null
}

export class Evento implements IEvento {
	id!: number
	instituicaoId!: number
	tipoEventoId!: number
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
	imagem!: string

	constructor(
		id: number,
		instituicaoId: number,
		tipoEventoId: number,
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
		imagem: string | null
		){
			
		[this.instituicaoId, this.tipoEventoId, this.titulo, this.descricao, this.dataInicio, this.dataTermino, this.local, this.enderecoId, this.endereco, this.linkExterno, this.exibirMaps,this.responsavel, this.imagem] = arguments
		}
}

export interface INovoEndereco {
  cep: string
  logradouro: string
  numero: string
  complemento: string
  pontoReferencia: string
  bairro: string
  cidade: string
  uf: string
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

export interface IUltimoEvento {
  id: number,
  titulo: string,
  dataInicio: string,
  dataTermino: string,
  local: string,
  arquivo: string | null,
  ultimaAtualizacao: string
}