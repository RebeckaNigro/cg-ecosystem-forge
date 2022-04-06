using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
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

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                #region Instituição

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

            //if (!await Validar(obj, resposta)) return resposta;

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

            //if (!await Validar(obj, resposta)) return resposta;

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
    }
}