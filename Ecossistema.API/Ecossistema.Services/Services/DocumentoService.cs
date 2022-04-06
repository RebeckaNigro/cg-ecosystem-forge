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
    public class DocumentoService : IDocumentoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> Incluir(DocumentoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                #region Instituição

                var obj = new Documento(dado.Nome,
                                          dado.Descricao,
                                          (int)dado.TipoDocumentoId,
                                          (int)dado.DocumentoAreaId,
                                          (int)dado.InstituicaoId,
                                          (DateTime)dado.Data,
                                          usuarioId,
                                          DateTime.Now);

                await _unitOfWork.Documentos.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion

                #region Aprovação

                var aprovacao = await _unitOfWork.Aprovacoes.FindAsync(x => x.DocumentoId == obj.Id);

                if (aprovacao != null)
                {
                    obj.AprovacaoId = aprovacao.Id;

                    _unitOfWork.Documentos.Update(obj);
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

        public async Task<RespostaPadrao> Editar(DocumentoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Documentos.FindAsync(x => x.Id == dado.Id);

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacao = new Aprovacao(EOrigem.Documento, usuarioId, dataAtual, objAlt.Id);

                    await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                    _unitOfWork.Complete();

                    #endregion

                    objAlt.Nome = (string)dado.Nome;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.TipoDocumentoId = (int)dado.TipoDocumentoId;
                    objAlt.DocumentoAreaId = (int)dado.DocumentoAreaId;
                    objAlt.InstituicaoId = (int)dado.InstituicaoId;
                    objAlt.Data = (DateTime)dado.Data;
                    objAlt.AprovacaoId = aprovacao.Id;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Documentos.Update(objAlt);

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
                var objAlt = await _unitOfWork.Documentos.FindAsync(x => x.Id == id);

                if (objAlt != null)
                {
                    if (objAlt.Aprovacoes.Any()) _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());

                    _unitOfWork.Documentos.Delete(objAlt);

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
