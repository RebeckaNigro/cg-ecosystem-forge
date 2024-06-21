export interface faleConoscoInterface {
  nome: string;
  emailCorporativo: string;
  telefone: string;
  empresa: string;
  cargo: string;
  setorId: number;
  mensagem: string;
}

export class faleConosco implements faleConoscoInterface {
  nome!: string;
  emailCorporativo!: string;
  telefone!: string;
  empresa!: string;
  cargo!: string;
  setorId!: number;
  mensagem!: string;

  constructor(
    nome: string,
    emailCorporativo: string,
    telefone: string,
    empresa: string,
    cargo: string,
    setorId: number,
    mensagem: string
  ) {
    [
      this.nome,
      this.emailCorporativo,
      this.telefone,
      this.empresa,
      this.cargo,
      this.setorId,
      this.mensagem,
    ] = arguments;
  }
}

export interface SetorInterface {
  id: number;
  descricao: string;
}
