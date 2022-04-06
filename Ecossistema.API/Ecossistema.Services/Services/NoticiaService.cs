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
    public class NoticiaService : INoticiaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoticiaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> Incluir(NoticiaDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                #region Instituição

                var obj = new Noticia(dado.Titulo,
                                      dado.Descricao,
                                      dado.SubTitulo,
                                      dado.DataPublicacao,
                                      usuarioId,
                                      DateTime.Now);

                await _unitOfWork.Noticias.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion

                #region Aprovação

                var aprovacao = await _unitOfWork.Aprovacoes.FindAsync(x => x.NoticiaId == obj.Id);

                if (aprovacao != null)
                {
                    obj.AprovacaoId = aprovacao.Id;

                    _unitOfWork.Noticias.Update(obj);
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

        public async Task<RespostaPadrao> Editar(NoticiaDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Noticias.FindAsync(x => x.Id == dado.Id);

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacao = new Aprovacao(EOrigem.Parceiro, usuarioId, dataAtual, objAlt.Id);

                    await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                    _unitOfWork.Complete();

                    #endregion

                    objAlt.Titulo = dado.Titulo;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.SubTitulo = dado.SubTitulo;
                    objAlt.DataPublicacao = dado.DataPublicacao;
                    objAlt.AprovacaoId = aprovacao.Id;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Noticias.Update(objAlt);

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
                var objAlt = await _unitOfWork.Noticias.FindAsync(x => x.Id == id);

                if (objAlt != null)
                {
                    if (objAlt.Aprovacoes.Any()) _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());

                    _unitOfWork.Noticias.Delete(objAlt);

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
