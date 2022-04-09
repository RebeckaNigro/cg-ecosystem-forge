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
    public class EventoService : IEventoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> Incluir(EventoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                #region Instituição

                var obj = new Evento((int)dado.EventoId,
                                    (int)dado.TipoEventoId,
                                    dado.Titulo,
                                    dado.Descricao,
                                    (DateTime)dado.DataInicio,
                                    (DateTime)dado.DataTermino,
                                    dado.Local,
                                    dado.EnderecoId,
                                    dado.LinkExterno,
                                    (bool)dado.ExibirMaps,
                                    dado.Responsavel,
                                    usuarioId,
                                    DateTime.Now);

                await _unitOfWork.Eventos.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion

                #region Aprovação

                var aprovacao = await _unitOfWork.Aprovacoes.FindAsync(x => x.EventoId == obj.Id);

                if (aprovacao != null)
                {
                    obj.AprovacaoId = aprovacao.Id;

                    _unitOfWork.Eventos.Update(obj);
                }

                _unitOfWork.Complete();

                #endregion

                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Editar(EventoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Eventos.FindAsync(x => x.Id == dado.Id);

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacao = new Aprovacao(EOrigem.Parceiro, usuarioId, dataAtual, objAlt.Id);

                    await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                    _unitOfWork.Complete();

                    #endregion

                    objAlt.EventoId = (int)dado.EventoId;
                    objAlt.TipoEventoId = (int)dado.TipoEventoId;
                    objAlt.Titulo = (string)dado.Titulo;
                    objAlt.Descricao = (string)dado.Descricao;
                    objAlt.DataInicio = (DateTime)dado.DataInicio;
                    objAlt.DataTermino = (DateTime)dado.DataTermino;
                    objAlt.Local = dado.Local;
                    objAlt.EnderecoId = dado.EnderecoId;
                    objAlt.LinkExterno = dado.LinkExterno;
                    objAlt.ExibirMaps = (bool)dado.ExibirMaps;
                    objAlt.Responsavel = (string)dado.Responsavel;
                    objAlt.AprovacaoId = aprovacao.Id;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Eventos.Update(objAlt);

                    _unitOfWork.Complete();

                    resposta.SetMensagem("Dados gravados com sucesso!");
                }
                else resposta.SetErroInterno();
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Excluir(int id)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarExcluir(dado, resposta)) return resposta;

            try
            {
                var objAlt = await _unitOfWork.Eventos.FindAsync(x => x.Id == id);

                if (objAlt != null)
                {
                    if (objAlt.Aprovacoes.Any()) _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());

                    _unitOfWork.Eventos.Delete(objAlt);

                    _unitOfWork.Complete();

                    resposta.SetMensagem("Dados excluídos com sucesso!");
                }
                else resposta.SetErroInterno();
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarTituloInformada(dado, resposta)
                 || !ValidarTituloTamanho(dado, resposta)
                 || !ValidarCnpjInformado(dado, resposta)
                 || !ValidarCnpjTamanho(dado, resposta)
                 || !ValidarCnpjValido(dado, resposta)
                 || !ValidarDescricaoInformada(dado, resposta)
                 || !ValidarDescricaoTamanho(dado, resposta)
                 || !ValidarEventoAreaIdValida(dado, resposta)
                 || !await ValidarEventoAreaIdCadastrada(dado, resposta)
                 || !ValidarEventoClassificacaoIdValida(dado, resposta)
                 || !await ValidarEventoClassificacaoIdCadastrado(dado, resposta)
                 || !ValidarDescricaoInformada(dado, resposta)
                 || !ValidarDescricaoTamanho(dado, resposta)
                 || !ValidarMissaoInformada(dado, resposta)
                 || !ValidarMissaoTamanho(dado, resposta)
                 || !ValidarVisaoInformada(dado, resposta)
                 || !ValidarVisaoTamanho(dado, resposta)
                 || !ValidarValoresInformada(dado, resposta)
                 || !ValidarValoresTamanho(dado, resposta)
                 || !ValidarTipoEventoIdValida(dado, resposta)
                 || !await ValidarTipoEventoIdCadastrada(dado, resposta))
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEditar(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(dado, resposta)
                || !ValidarIdValido(dado, resposta)
                || !await ValidarIdCadastrado(dado, resposta)
                || !ValidarTituloInformada(dado, resposta)
                || !ValidarTituloTamanho(dado, resposta)
                || !ValidarCnpjInformado(dado, resposta)
                || !ValidarCnpjTamanho(dado, resposta)
                || !ValidarCnpjValido(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarEventoAreaIdValida(dado, resposta)
                || !await ValidarEventoAreaIdCadastrada(dado, resposta)
                || !ValidarEventoClassificacaoIdValida(dado, resposta)
                || !await ValidarEventoClassificacaoIdCadastrado(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarMissaoInformada(dado, resposta)
                || !ValidarMissaoTamanho(dado, resposta)
                || !ValidarVisaoInformada(dado, resposta)
                || !ValidarVisaoTamanho(dado, resposta)
                || !ValidarValoresInformada(dado, resposta)
                || !ValidarValoresTamanho(dado, resposta)
                || !ValidarTipoEventoIdValida(dado, resposta)
                || !await ValidarTipoEventoIdCadastrada(dado, resposta))
            {
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
                return false;
            }
            return true;
        }

        private bool ValidarIdInformado(EventoDto dado, RespostaPadrao resposta)
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

        private bool ValidarIdValido(EventoDto dado, RespostaPadrao resposta)
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

        private async Task<bool> ValidarIdCadastrado(EventoDto dado, RespostaPadrao resposta)
        {
            return await ValidarIdCadastrado(dado.Id, resposta);
        }

        private bool ValidarInstituicaoIdValida(EventoDto dado, RespostaPadrao resposta)
        {
            return ValidarEventoAreaIdValida(dado.InstituicaoId, resposta);
        }

        private bool ValidarInstituicaoIdValida(int? EventoAreaId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(EventoAreaId))
            {
                resposta.SetCampoInvalido("InstituicaoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoIdCadastrada(int? instituicaoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.Instituicoes.FindAllAsync(x => x.Id == (int)instituicaoId
                                                                      && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para a instituição informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            return await ValidarInstituicaoIdCadastrada(dado.InstituicaoId, resposta);
        }

        private bool ValidarTipoEventoIdValida(EventoDto dado, RespostaPadrao resposta)
        {
            return ValidarEventoAreaIdValida(dado.TipoEventoId, resposta);
        }

        private bool ValidarTipoEventoIdValida(int? tipoEventoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(tipoEventoId))
            {
                resposta.SetCampoInvalido("TipoEventoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoEventoIdCadastrada(int? tipoEventoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.TiposEventos.FindAllAsync(x => x.Id == (int)tipoEventoId
                                                                      && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para o tipo de evento informado!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoEventoIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            return await ValidarTipoEventoIdCadastrada(dado.TipoEventoId, resposta);
        }

        private bool ValidarTituloInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Titulo))
            {
                resposta.SetCampoVazio("Titulo");
                return false;
            }
            return true;
        }

        private bool ValidarTituloTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Titulo, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Nome", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Descricao))
            {
                resposta.SetCampoVazio("Descricao");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Descricao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Descricao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }


        //Data Ínicio

        private bool ValidarEventoAreaIdValida(EventoDto dado, RespostaPadrao resposta)
        {
            return ValidarEventoAreaIdValida(dado.EventoAreaId, resposta);
        }

        private bool ValidarEventoAreaIdValida(int? EventoAreaId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(EventoAreaId))
            {
                resposta.SetCampoInvalido("EventoAreaId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEventoAreaIdCadastrada(int? EventoAreaId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.InstituicoesAreas.FindAllAsync(x => x.Id == (int)EventoAreaId
                                                                && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para a área informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEventoAreaIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            return await ValidarEventoAreaIdCadastrada(dado.EventoAreaId, resposta);
        }

        private bool ValidarEventoClassificacaoIdValida(EventoDto dado, RespostaPadrao resposta)
        {
            return ValidarEventoClassificacaoIdValido(dado.EventoClassificacaoId, resposta);
        }

        private bool ValidarEventoClassificacaoIdValido(int? EventoClassificacaoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(EventoClassificacaoId))
            {
                resposta.SetCampoInvalido("EventoClassificacaoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEventoClassificacaoIdCadastrado(int? EventoClassificacaoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.InstituicoesClassificacoes.FindAllAsync(x => x.Id == (int)EventoClassificacaoId
                                                                        && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para a classificação informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEventoClassificacaoIdCadastrado(EventoDto dado, RespostaPadrao resposta)
        {

            return await ValidarEventoClassificacaoIdCadastrado(dado.EventoClassificacaoId, resposta);
        }

        private bool ValidarDescricaoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Descricao))
            {
                resposta.SetCampoVazio("Descricao");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Descricao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Descricao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarMissaoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Missao))
            {
                resposta.SetCampoVazio("Missao");
                return false;
            }
            return true;
        }

        private bool ValidarMissaoTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Missao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Missao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarVisaoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Visao))
            {
                resposta.SetCampoVazio("Visao");
                return false;
            }
            return true;
        }

        private bool ValidarVisaoTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Visao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Visao", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarValoresInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Valores))
            {
                resposta.SetCampoVazio("Valores");
                return false;
            }
            return true;
        }

        private bool ValidarValoresTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Valores, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Valores", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarTipoEventoIdValida(EventoDto dado, RespostaPadrao resposta)
        {
            return ValidarTipoEventoIdValida(dado.TipoEventoId, resposta);
        }

        private bool ValidarTipoEventoIdValida(int? tipoEventoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(tipoEventoId))
            {
                resposta.SetCampoInvalido("TipoEventoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoEventoIdCadastrada(int? tipoEventoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.TiposInstituicoes.FindAllAsync(x => x.Id == (int)tipoEventoId
                                                                         && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para o tipo de instituição informada!");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoEventoIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            return await ValidarTipoEventoIdCadastrada(dado.TipoEventoId, resposta);
        }

        #endregion
    }
}