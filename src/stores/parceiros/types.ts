export interface PartnerInterface {
  parceiroId: string
  parceiroNome: string
  primeiroParagrafo: string
  subTitulo: string
  segundoParagrafo: string
  coverPath: string
  logoPath: string
  emailContato: string
  telefone: string
  website: string
  endereco: string
  instaLink: string
  linkedinLink: string
  wppLink: string
}

export interface IPartnerSimplificado{
	logo: string | null
	id: number,
	nome: string
}

export interface IPartnerSeccionado{
	tipoInstituicao: string
	parceiros: IPartnerSimplificado[]
}