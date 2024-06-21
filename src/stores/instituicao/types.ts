export interface IInstituicao {
  id: number;
  razaoSocial: string;
  cnpj: string;
  responsavel: string;
  instituicaoAreaId: string;
  instituicaoClassificacaoId: string;
  descricao: string;
  missao: string;
  visao: string;
  valores: string;
  tipoInstituicaoId: string;
}

export class Instituicao implements IInstituicao {
  id: number;
  razaoSocial: string;
  cnpj: string;
  responsavel: string;
  instituicaoAreaId: string;
  instituicaoClassificacaoId: string;
  descricao: string;
  missao: string;
  visao: string;
  valores: string;
  tipoInstituicaoId: string;

  constructor(
    id: number,
    razaoSocial: string,
    cnpj: string,
    responsavel: string,
    instituicaoAreaId: string,
    instituicaoClassificacaoId: string,
    descricao: string,
    missao: string,
    visao: string,
    valores: string,
    tipoInstituicaoId: string
  ) {
    [
      this.id,
      this.razaoSocial,
      this.cnpj,
      this.responsavel,
      this.instituicaoAreaId,
      this.instituicaoClassificacaoId,
      this.descricao,
      this.missao,
      this.visao,
      this.valores,
      this.tipoInstituicaoId,
    ] = arguments;
  }
}
