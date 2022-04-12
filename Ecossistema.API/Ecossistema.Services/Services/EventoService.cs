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
        private readonly IEnderecoService _enderecoService;

        public EventoService(IUnitOfWork unitOfWork, IEnderecoService enderecoService)
        {
            _unitOfWork = unitOfWork;
            _enderecoService = enderecoService;
        }

        public async Task<RespostaPadrao> Incluir(EventoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                #region Endereço

                if (dado.EnderecoId == null)
                {
                    var endereco = dado.Endereco;

                    dado.EnderecoId = await _enderecoService.Vincular(endereco, usuarioId, resposta);

                    if (dado.EnderecoId == 0) return resposta;
                }

                #endregion

                #region Evento

                var obj = new Evento((int)dado.InstituicaoId,
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

        public async Task<RespostaPadrao> Editar(EventoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                #region Endereço

                if (dado.EnderecoId == null)
                {
                    var endereco = dado.Endereco;

                    dado.EnderecoId = await _enderecoService.Vincular(endereco, usuarioId, resposta);

                    if (dado.EnderecoId == 0) return resposta;
                }

                #endregion

                var objAlt = await _unitOfWork.Eventos.FindAsync(x => x.Id == dado.Id, new[] { "Aprovacoes" });

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

                    objAlt.InstituicaoId = (int)dado.InstituicaoId;
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
                    objAlt.AprovacaoId = aprovacaoId;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Eventos.Update(objAlt);

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
                var objAlt = await _unitOfWork.Eventos.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });

                if (objAlt != null)
                {
                    #region Aprovação

                    if (objAlt.Aprovacoes.Any())
                    {
                        _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());
                        _unitOfWork.Complete();
                    }

                    #endregion

                    _unitOfWork.Eventos.Delete(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;

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

        public async Task<RespostaPadrao> ListarUltimas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var result = query.Select(x => new
            {
                id = x.Id,
                titulo = x.Titulo,
                dataInicio = x.DataInicio,
                dataTermino = x.DataTermino,
                local = x.Local
            })
            .Distinct()
            .OrderByDescending(x => x.dataInicio)
            .Take(3)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var result = query.Select(x => new
            {
                id = x.Id,
                titulo = x.Titulo,
                dataInicio = x.DataInicio,
                dataTermino = x.DataTermino,
                local = x.Local
            })
            .Distinct()
            .OrderByDescending(x => x.dataInicio)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> Detalhes(int id)
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Id == id, new[] { "Instituicao", "TipoEvento", "Aprovacao", "Endereco" });

            var result = query.Select(x => new
            {
                id = x.Id,
                instituicaoId = x.InstituicaoId,
                instituicao = x.Instituicao != null ? x.Instituicao.Descricao : null,
                tipoEventoId = x.TipoEventoId,
                tipoEvento = x.TipoEvento != null ? x.Descricao : null,
                titulo = x.Titulo,
                descricao = x.Descricao,
                dataInicio = x.DataInicio,
                dataTermino = x.DataTermino,
                local = x.Local,
                enderecoId = x.EnderecoId,
                endereco = x.Endereco != null
                    ? new
                    {
                        cep = x.Endereco.Cep,
                        logradouro = x.Endereco.Logradouro,
                        numero = x.Endereco.Numero,
                        complemento = x.Endereco.Complemento,
                        pontoReferencia = x.Endereco.PontoReferencia,
                        bairro = x.Endereco.Bairro,
                        cidade = x.Endereco.Cidade,
                        uf = x.Endereco.Uf
                    }
                    : null,
                linkExterno = x.LinkExterno,
                exibirMaps = x.ExibirMaps,
                responsavel = x.Responsavel,
                aprovado = x.Aprovacao
            })
            .Distinct();

            if (result.Any()) resposta.Retorno = result.FirstOrDefault();
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarInstituicaoIdValida(dado, resposta)
            || !await ValidarInstituicaoIdCadastrada(dado, resposta)
            || !ValidarTipoEventoIdValida(dado, resposta)
            || !await ValidarTipoEventoIdCadastrada(dado, resposta)
            || !ValidarTituloInformada(dado, resposta)
            || !ValidarTituloTamanho(dado, resposta)
            || !ValidarDescricaoInformada(dado, resposta)
            || !ValidarDescricaoTamanho(dado, resposta)
            || !ValidarDataInicioInformada(dado, resposta)
            || !ValidarDataInicioValida(dado, resposta)
            || !ValidarDataTerminoInformada(dado, resposta)
            || !ValidarDataTerminoValida(dado, resposta)
            || !ValidarPeriodoValido(dado, resposta)
            || !ValidarLocalInformada(dado, resposta)
            || !ValidarLocalTamanho(dado, resposta)
            || (dado.EnderecoId != null
                && (!ValidarEnderecoIdValido(dado, resposta)
                    || !await ValidarEnderecoIdCadastrada(dado, resposta)))
            || !ValidarLinkExternoInformada(dado, resposta)
            || !ValidarLinkExternoTamanho(dado, resposta)
            || !ValidarExibirMapsInformada(dado, resposta)
            || !ValidarResponsavelInformada(dado, resposta)
            || !ValidarResponsavelTamanho(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEditar(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarInstituicaoIdValida(dado, resposta)
               || !await ValidarInstituicaoIdCadastrada(dado, resposta)
               || !ValidarTipoEventoIdValida(dado, resposta)
               || !await ValidarTipoEventoIdCadastrada(dado, resposta)
               || !ValidarTituloInformada(dado, resposta)
               || !ValidarTituloTamanho(dado, resposta)
               || !ValidarDescricaoInformada(dado, resposta)
               || !ValidarDescricaoTamanho(dado, resposta)
               || !ValidarDataInicioInformada(dado, resposta)
               || !ValidarDataInicioValida(dado, resposta)
               || !ValidarDataTerminoInformada(dado, resposta)
               || !ValidarDataTerminoValida(dado, resposta)
               || !ValidarPeriodoValido(dado, resposta)
               || !ValidarLocalInformada(dado, resposta)
               || !ValidarLocalTamanho(dado, resposta)
               || (dado.EnderecoId != null
                    && (!ValidarEnderecoIdValido(dado, resposta)
                        || !await ValidarEnderecoIdCadastrada(dado, resposta)))
               || !ValidarLinkExternoInformada(dado, resposta)
               || !ValidarLinkExternoTamanho(dado, resposta)
               || !ValidarExibirMapsInformada(dado, resposta)
               || !ValidarResponsavelInformada(dado, resposta)
               || !ValidarResponsavelTamanho(dado, resposta))
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
            if (!ValidacaoUtil.ValidarInteiroValido(dado.InstituicaoId))
            {
                resposta.SetCampoInvalido("InstituicaoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.Instituicoes.FindAllAsync(x => x.Id == (int)dado.InstituicaoId
                                                                      && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para a instituição informada!");
                return false;
            }
            return true;
        }

        private bool ValidarTipoEventoIdValida(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(dado.TipoEventoId))
            {
                resposta.SetCampoInvalido("TipoEventoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoEventoIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.TiposEventos.FindAllAsync(x => x.Id == (int)dado.TipoEventoId
                                                                      && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para o tipo de evento informado!");
                return false;
            }
            return true;
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

        private bool ValidarDataInicioInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (dado.DataInicio == null)
            {
                resposta.SetCampoVazio("DataInicio");
                return false;
            }
            return true;
        }

        private bool ValidarDataInicioValida(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarData(dado.DataInicio))
            {
                resposta.SetCampoInvalido("DataInicio");
                return false;
            }
            return true;
        }

        private bool ValidarDataTerminoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (dado.DataTermino == null)
            {
                resposta.SetCampoVazio("DataTermino");
                return false;
            }
            return true;
        }

        private bool ValidarDataTerminoValida(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarData(dado.DataTermino))
            {
                resposta.SetCampoInvalido("DataTermino");
                return false;
            }
            return true;
        }

        private bool ValidarPeriodoValido(EventoDto dado, RespostaPadrao resposta)
        {
            if ((DateTime)dado.DataInicio > (DateTime)dado.DataInicio)
            {
                resposta.SetCampoInvalido("DataTermino", "A data de término deve ser posterior a data de início!");
                return false;
            }
            return true;
        }

        private bool ValidarLocalInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Local))
            {
                resposta.SetCampoVazio("Local");
                return false;
            }
            return true;
        }

        private bool ValidarLocalTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Local, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Local", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarEnderecoIdValido(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(dado.EnderecoId))
            {
                resposta.SetCampoInvalido("EnderecoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEnderecoIdCadastrada(EventoDto dado, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.Enderecos.FindAllAsync(x => x.Id == (int)dado.EnderecoId
                                                                      && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Não existe cadastro para o endereço informado!");
                return false;
            }
            return true;
        }

        private bool ValidarLinkExternoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.LinkExterno))
            {
                resposta.SetCampoVazio("LinkExterno");
                return false;
            }
            return true;
        }

        private bool ValidarLinkExternoTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.LinkExterno, tamanhoCampo))
            {
                resposta.SetCampoInvalido("LinkExterno", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarExibirMapsInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (dado.ExibirMaps == null)
            {
                resposta.SetCampoVazio("ExibirMaps");
                return false;
            }
            return true;
        }

        private bool ValidarResponsavelInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Responsavel))
            {
                resposta.SetCampoVazio("Responsavel");
                return false;
            }
            return true;
        }

        private bool ValidarResponsavelTamanho(EventoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Responsavel, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Responsavel", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        #endregion
    }
}