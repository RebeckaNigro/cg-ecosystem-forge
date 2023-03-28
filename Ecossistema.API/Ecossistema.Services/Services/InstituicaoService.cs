using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
using Ecossistema.Util.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class InstituicaoService : IInstituicaoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnderecoService _enderecoService;

        public InstituicaoService(IUnitOfWork unitOfWork,
            IEnderecoService enderecoService)
        {
            _unitOfWork = unitOfWork;
            _enderecoService = enderecoService;
        }

        public async Task<RespostaPadrao> Incluir(InstituicaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                #region Instituição

                var obj = new Instituicao(dado.RazaoSocial,
                                          dado.Cnpj,
                                          dado.Responsavel,
                                          (int)dado.InstituicaoAreaId,
                                          (int)dado.InstituicaoClassificacaoId,
                                          (int)dado.TipoInstituicaoId,
                                          dado.Descricao,
                                          dado.Missao,
                                          dado.Visao,
                                          dado.Valores,
                                          usuarioId,
                                          DateTime.Now);

                await _unitOfWork.Instituicoes.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion

                #region Aprovação

                var aprovacao = await _unitOfWork.Aprovacoes.FindAsync(x => x.InstituicaoId == obj.Id);

                if (aprovacao != null)
                {
                    obj.AprovacaoId = aprovacao.Id;

                    _unitOfWork.Instituicoes.Update(obj);
                }

                resposta.Retorno = _unitOfWork.Complete() > 0;

                #endregion

                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Editar(InstituicaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Instituicoes.FindAsync(x => x.Id == dado.Id, new[] { "Aprovacoes" });

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacaoId = objAlt.AprovacaoId;

                    if (objAlt.Aprovacao.SituacaoAprovacaoId != ESituacaoAprovacao.Pendente.Int32Val())
                    {
                        var aprovacao = new Aprovacao(EOrigem.Parceiro, usuarioId, dataAtual, objAlt.Id);

                        await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                        _unitOfWork.Complete();

                        aprovacaoId = aprovacao.Id;
                    }

                    #endregion

                    objAlt.RazaoSocial = dado.RazaoSocial;
                    objAlt.Cnpj = dado.Cnpj;
                    objAlt.Responsável = dado.Responsavel;
                    objAlt.InstituicaoAreaId = (int)dado.InstituicaoAreaId;
                    objAlt.InstituicaoClassificacaoId = (int)dado.InstituicaoClassificacaoId;
                    objAlt.TipoInstituicaoId = (int)dado.TipoInstituicaoId;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.Missao = dado.Missao;
                    objAlt.Visao = dado.Visao;
                    objAlt.Valores = dado.Valores;
                    objAlt.AprovacaoId = aprovacaoId;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Instituicoes.Update(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;

                    resposta.SetMensagem("Dados gravados com sucesso!");
                }
                else resposta.SetErroInterno();
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Excluir(int id)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarExcluir(id, resposta)) return resposta;

            try
            {
                var objAlt = await _unitOfWork.Instituicoes.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });

                if (objAlt != null)
                {
                    #region Aprovação

                    if (objAlt.Aprovacoes.Any())
                    {
                        _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());
                        _unitOfWork.Complete();
                    }

                    #endregion

                    _unitOfWork.Instituicoes.Delete(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;

                    resposta.SetMensagem("Dados excluídos com sucesso!");
                }
                else resposta.SetErroInterno();
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> VincularEndereco(EnderecoDto dado, int instituicaoId, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            try
            {
                var dataAtual = DateTime.Now;

                #region Endereço

                var enderecoId = await _enderecoService.Vincular(dado, dataAtual, usuarioId, resposta);

                if (enderecoId == 0) return resposta;

                #endregion

                #region Instituição / Endereço

                InstituicaoEndereco? obj = null;

                var query = await _unitOfWork.InstituicoesEnderecos.FindAllAsync(x => x.InstituicaoId == instituicaoId
                                                                                  && x.EnderecoId == enderecoId
                                                                                  && x.Ativo);

                if (query.Any())
                {
                    obj = query.First();
                    Recursos.Auditoria(obj, usuarioId, dataAtual);
                    _unitOfWork.InstituicoesEnderecos.Update(obj);
                }
                else
                {
                    obj = new InstituicaoEndereco(instituicaoId,
                                          enderecoId,
                                          (int)dado.TipoEnderecoId,
                                          usuarioId,
                                          dataAtual);

                    await _unitOfWork.InstituicoesEnderecos.AddAsync(obj);
                }


                resposta.Retorno = _unitOfWork.Complete() > 0;

                #endregion

                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarRazaoSocialInformada(dado, resposta)
                 || !ValidarRazaoSocialTamanho(dado, resposta)
                 || !ValidarCnpjInformado(dado, resposta)
                 || !ValidarCnpjTamanho(dado, resposta)
                 || !ValidarCnpjValido(dado, resposta)
                 || !ValidarResponsavelInformado(dado, resposta)
                 || !ValidarResponsavelTamanho(dado, resposta)
                 || !ValidarInstituicaoAreaIdValida(dado, resposta)
                 || !await ValidarInstituicaoAreaIdCadastrada(dado, resposta)
                 || !ValidarInstituicaoClassificacaoIdValida(dado, resposta)
                 || !await ValidarInstituicaoClassificacaoIdCadastrado(dado, resposta)
                 || !ValidarDescricaoInformada(dado, resposta)
                 || !ValidarDescricaoTamanho(dado, resposta)
                 || !ValidarMissaoInformada(dado, resposta)
                 || !ValidarMissaoTamanho(dado, resposta)
                 || !ValidarVisaoInformada(dado, resposta)
                 || !ValidarVisaoTamanho(dado, resposta)
                 || !ValidarValoresInformada(dado, resposta)
                 || !ValidarValoresTamanho(dado, resposta)
                 || !ValidarTipoInstituicaoIdValida(dado, resposta)
                 || !await ValidarTipoInstituicaoIdCadastrada(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEditar(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(dado, resposta)
                || !ValidarIdValido(dado, resposta)
                || !await ValidarIdCadastrado(dado, resposta)
                || !ValidarRazaoSocialInformada(dado, resposta)
                || !ValidarRazaoSocialTamanho(dado, resposta)
                || !ValidarCnpjInformado(dado, resposta)
                || !ValidarCnpjTamanho(dado, resposta)
                || !ValidarCnpjValido(dado, resposta)
                || !ValidarResponsavelInformado(dado, resposta)
                || !ValidarResponsavelTamanho(dado, resposta)
                || !ValidarInstituicaoAreaIdValida(dado, resposta)
                || !await ValidarInstituicaoAreaIdCadastrada(dado, resposta)
                || !ValidarInstituicaoClassificacaoIdValida(dado, resposta)
                || !await ValidarInstituicaoClassificacaoIdCadastrado(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarMissaoInformada(dado, resposta)
                || !ValidarMissaoTamanho(dado, resposta)
                || !ValidarVisaoInformada(dado, resposta)
                || !ValidarVisaoTamanho(dado, resposta)
                || !ValidarValoresInformada(dado, resposta)
                || !ValidarValoresTamanho(dado, resposta)
                || !ValidarTipoInstituicaoIdValida(dado, resposta)
                || !await ValidarTipoInstituicaoIdCadastrada(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarExcluir(int id, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(id, resposta)
                || !ValidarIdValido(id, resposta)
                || !await ValidarIdCadastrado(id, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private bool ValidarIdInformado(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return ValidarIdInformado(dado.Id, resposta);
        }

        private bool ValidarIdInformado(int? id, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiro(id))
            {
                resposta.SetCampoVazio("Id");
                return false;
            }
            return true;
        }

        private bool ValidarIdValido(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return ValidarIdValido(dado.Id, resposta);
        }

        private bool ValidarIdValido(int? id, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(id))
            {
                resposta.SetCampoInvalido("Id");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarIdCadastrado(int? id, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.Instituicoes.FindAllAsync(x => x.Id == (int)id
                                                                      && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarIdCadastrado(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return await ValidarIdCadastrado(dado.Id, resposta);
        }

        private bool ValidarRazaoSocialInformada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.RazaoSocial))
            {
                resposta.SetCampoVazio("RazaoSocial");
                return false;
            }
            return true;
        }

        private bool ValidarRazaoSocialTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.RazaoSocial, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Nome", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarCnpjInformado(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Cnpj))
            {
                resposta.SetCampoVazio("Cnpj");
                return false;
            }
            return true;
        }

        private bool ValidarCnpjTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 14;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Cnpj, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Cnpj", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarCnpjValido(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidaCnpj(dado.Cnpj))
            {
                resposta.SetCampoInvalido("Cnpj");
                return false;
            }
            return true;
        }

        private bool ValidarResponsavelInformado(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Responsavel))
            {
                resposta.SetCampoVazio("Responsavel");
                return false;
            }
            return true;
        }

        private bool ValidarResponsavelTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Responsavel, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Responsavel", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarInstituicaoAreaIdValida(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return ValidarInstituicaoAreaIdValida(dado.InstituicaoAreaId, resposta);
        }

        private bool ValidarInstituicaoAreaIdValida(int? instituicaoAreaId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(instituicaoAreaId))
            {
                resposta.SetCampoInvalido("InstituicaoAreaId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoAreaIdCadastrada(int? instituicaoAreaId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.InstituicoesAreas.FindAllAsync(x => x.Id == (int)instituicaoAreaId
                                                                && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para a área informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoAreaIdCadastrada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return await ValidarInstituicaoAreaIdCadastrada(dado.InstituicaoAreaId, resposta);
        }

        private bool ValidarInstituicaoClassificacaoIdValida(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return ValidarInstituicaoClassificacaoIdValido(dado.InstituicaoClassificacaoId, resposta);
        }

        private bool ValidarInstituicaoClassificacaoIdValido(int? instituicaoClassificacaoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(instituicaoClassificacaoId))
            {
                resposta.SetCampoInvalido("InstituicaoClassificacaoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoClassificacaoIdCadastrado(int? instituicaoClassificacaoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.InstituicoesClassificacoes.FindAllAsync(x => x.Id == (int)instituicaoClassificacaoId
                                                                        && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para a classificação informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoClassificacaoIdCadastrado(InstituicaoDto dado, RespostaPadrao resposta)
        {

            return await ValidarInstituicaoClassificacaoIdCadastrado(dado.InstituicaoClassificacaoId, resposta);
        }

        private bool ValidarDescricaoInformada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Descricao))
            {
                resposta.SetCampoVazio("Descricao");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Descricao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Descricao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarMissaoInformada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Missao))
            {
                resposta.SetCampoVazio("Missao");
                return false;
            }
            return true;
        }

        private bool ValidarMissaoTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Missao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Missao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarVisaoInformada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Visao))
            {
                resposta.SetCampoVazio("Visao");
                return false;
            }
            return true;
        }

        private bool ValidarVisaoTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Visao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Visao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarValoresInformada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Valores))
            {
                resposta.SetCampoVazio("Valores");
                return false;
            }
            return true;
        }

        private bool ValidarValoresTamanho(InstituicaoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Valores, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Valores", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarTipoInstituicaoIdValida(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return ValidarTipoInstituicaoIdValida(dado.TipoInstituicaoId, resposta);
        }

        private bool ValidarTipoInstituicaoIdValida(int? tipoInstituicaoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(tipoInstituicaoId))
            {
                resposta.SetCampoInvalido("TipoInstituicaoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoInstituicaoIdCadastrada(int? tipoInstituicaoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.TiposInstituicoes.FindAllAsync(x => x.Id == (int)tipoInstituicaoId
                                                                         && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para o tipo de instituição informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoInstituicaoIdCadastrada(InstituicaoDto dado, RespostaPadrao resposta)
        {
            return await ValidarTipoInstituicaoIdCadastrada(dado.TipoInstituicaoId, resposta);
        }

        public async Task<RespostaPadrao> BuscarInstituicoes()
        {
            var respostaPadrao = new RespostaPadrao();
            var query = await _unitOfWork.Instituicoes.FindAllAsync(x => x.Ativo);
            var result = query.Select(x => new
            {
                x.Id,
                x.RazaoSocial,
                x.Cnpj
            }).Distinct()
              .OrderBy(x => x.RazaoSocial)
              .ToList();
            respostaPadrao.Retorno = result;
            return respostaPadrao;
        }

        #endregion
    }
}