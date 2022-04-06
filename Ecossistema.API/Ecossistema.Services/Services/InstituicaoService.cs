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
    public class InstituicaoService : IInstituicaoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstituicaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> Incluir(InstituicaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                #region Instituição

                var obj = new Instituicao(dado.RazaoSocial,
                                          dado.Cnpj,
                                          dado.Responsável,
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

        public async Task<RespostaPadrao> Editar(InstituicaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            //if (!await Validar(obj, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Instituicoes.FindAsync(x => x.Id == dado.Id);

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacao = new Aprovacao(EOrigem.Parceiro, usuarioId, dataAtual, objAlt.Id);

                    await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                    _unitOfWork.Complete();

                    #endregion

                    objAlt.RazaoSocial = dado.RazaoSocial;
                    objAlt.Cnpj = dado.Cnpj;
                    objAlt.Responsável = dado.Responsável;
                    objAlt.InstituicaoAreaId = (int)dado.InstituicaoAreaId;
                    objAlt.InstituicaoClassificacaoId = (int)dado.InstituicaoClassificacaoId;
                    objAlt.TipoInstituicaoId = (int)dado.TipoInstituicaoId;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.Missao = dado.Missao;
                    objAlt.Visao = dado.Visao;
                    objAlt.Valores = dado.Valores;
                    objAlt.AprovacaoId = aprovacao.Id;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Instituicoes.Update(objAlt);

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
                var objAlt = await _unitOfWork.Instituicoes.FindAsync(x => x.Id == id);

                if (objAlt != null)
                {
                    if (objAlt.Aprovacoes.Any()) _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());

                    _unitOfWork.Instituicoes.Delete(objAlt);

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