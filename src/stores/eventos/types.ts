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