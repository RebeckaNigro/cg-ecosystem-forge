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
    public class AprovacaoService : IAprovacaoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AprovacaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> Incluir(AprovacaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                int? id = null;

                switch ((EOrigem)dado.OrigemId)
                {
                    case EOrigem.Parceiro:
                        if (dado.InstituicaoId.HasValue) id = dado.UsuarioId;
                        break;
                    case EOrigem.Documento:
                        if (dado.DocumentoId.HasValue) id = dado.DocumentoId;
                        break;
                    case EOrigem.Noticia:
                        if (dado.NoticiaId.HasValue) id = dado.NoticiaId;
                        break;
                    case EOrigem.Evento:
                        if (dado.EventoId.HasValue) id = dado.EventoId;
                        break;
                    default:
                        if (dado.UsuarioId.HasValue) id = dado.UsuarioId;
                        break;
                }

                var obj = new Aprovacao((EOrigem)dado.OrigemId,
                                          usuarioId,
                                          DateTime.Now,
                                          id);

                await _unitOfWork.Aprovacoes.AddAsync(obj);

                resposta.Retorno = _unitOfWork.Complete() > 0;

                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Editar(AprovacaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Aprovacoes.FindAsync(x => x.Id == dado.Id);

                if (objAlt != null)
                {
                    objAlt.OrigemId = (int)dado.OrigemId;
                    objAlt.DataAprovacao = dado.DataAprovacao;
                    objAlt.UsuarioAprovacaoId = dado.UsuarioAprovacaoId;
                    objAlt.Motivo = dado.Motivo;
                    objAlt.InstituicaoId = dado.InstituicaoId;
                    objAlt.UsuarioId = dado.UsuarioId;
                    objAlt.EventoId = dado.EventoId;
                    objAlt.NoticiaId = dado.NoticiaId;
                    objAlt.DocumentoId = dado.DocumentoId;
                    objAlt.SituacaoAprovacaoId = (int)dado.SituacaoAprovacaoId;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Aprovacoes.Update(objAlt);

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

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                var objAlt = await _unitOfWork.Aprovacoes.FindAsync(x => x.Id == id);

                if (objAlt != null)
                {
                    //if (objAlt.Aprovacoes.Any()) _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());

                    _unitOfWork.Aprovacoes.Delete(objAlt);

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
    }
}
