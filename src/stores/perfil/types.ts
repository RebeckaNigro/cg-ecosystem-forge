export interface IPerfil {
  id: number;
  nomeCompleto: string;
  cpf: string;
  dataNascimento: Date;
  telefone: string;
  cargo: string;
  instituicaoId: number;
  uf: string;
  cidade: string;
  logradouro: string;
  bairro: string;
  numero: string;
  cep: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export class Perfil implements IPerfil {
  id!: number;
  nomeCompleto!: string;
  cpf!: string;
  dataNascimento!: Date;
  telefone!: string;
  cargo!: string;
  instituicaoId!: number;
  uf!: string;
  cidade!: string;
  logradouro!: string;
  bairro!: string;
  numero!: string;
  cep!: string;
  email!: string;
  password!: string;
  confirmPassword!: string;

  constructor(
    id: number,
    nomeCompleto: string,
    cpf: string,
    dataNascimento: Date,
    telefone: string,
    cargo: string,
    intituicaoId: number,
    uf: string,
    cidade: string,
    logradouro: string,
    bairro: string,
    numero: string,
    cep: string,
    email: string,
    password: string,
    confirmPassword: string
  ) {
    [
      this.id,
      this.nomeCompleto,
      this.cpf,
      this.dataNascimento,
      this.telefone,
      this.cargo,
      this.instituicaoId,
      this.uf,
      this.cidade,
      this.logradouro,
      this.bairro,
      this.numero,
      this.cep,
      this.email,
      this.password,
      this.confirmPassword,
    ] = arguments;
  }
}
