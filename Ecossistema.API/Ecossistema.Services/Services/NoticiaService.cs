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

            if (!ValidarIncluir(dado, resposta)) return resposta;

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

        public async Task<RespostaPadrao> Editar(NoticiaDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Noticias.FindAsync(x => x.Id == dado.Id, new[] { "Aprovacao" });

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacaoId = objAlt.AprovacaoId;

                    if (objAlt.Aprovacao.SituacaoAprovacaoId != ESituacaoAprovacao.Pendente.Int32Val())
                    {
                        var aprovacao = new Aprovacao(EOrigem.Noticia, usuarioId, dataAtual, objAlt.Id);

                        await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                        _unitOfWork.Complete();

                        aprovacaoId = aprovacao.Id;
                    }

                    #endregion

                    objAlt.Titulo = dado.Titulo;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.SubTitulo = dado.SubTitulo;
                    objAlt.DataPublicacao = dado.DataPublicacao;
                    objAlt.AprovacaoId = aprovacaoId;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Noticias.Update(objAlt);

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
                var objAlt = await _unitOfWork.Noticias.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });

                if (objAlt != null)
                {
                    #region Aprovação

                    if (objAlt.Aprovacoes.Any())
                    {
                        _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());
                        _unitOfWork.Complete();
                    }

                    #endregion

                    _unitOfWork.Noticias.Delete(objAlt);

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

        public async Task<RespostaPadrao> ListarUltimas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var result = query.Select(x => new
            {
                x.Id,
                x.Titulo,
                x.DataPublicacao
            })
            .Distinct()
            .OrderByDescending(x => x.DataPublicacao)
            .Take(3)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var result = query.Select(x => new
            {
                x.Id,
                x.Titulo,
                x.DataPublicacao
            })
            .Distinct()
            .OrderByDescending(x => x.DataPublicacao)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> Detalhes(int id)
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.Id == id, new[] { "Aprovacao" });

            var result = query.Select(x => new
            {
                x.Id,
                x.Titulo,
                x.Descricao,
                x.SubTitulo,
                x.DataPublicacao,
                x.Aprovado
            })
            .Distinct();

            if (result.Any()) resposta.Retorno = result.FirstOrDefault();
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        #region Validações

        private bool ValidarIncluir(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (!ValidarTituloInformado(dado, resposta)
                || !ValidarTituloTamanho(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarSubTituloInformada(dado, resposta)
                || !ValidarSubTituloTamanho(dado, resposta)
                || !ValidarDataPublicacaoInformada(dado, resposta)
                || !ValidarDataPublicacaoValida(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEditar(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(dado, resposta)
                || !ValidarIdValido(dado, resposta)
                || !await ValidarNoticiaCadastrada(dado, resposta)
                || !ValidarTituloInformado(dado, resposta)
                || !ValidarTituloTamanho(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarSubTituloInformada(dado, resposta)
                || !ValidarSubTituloTamanho(dado, resposta)
                || !ValidarDataPublicacaoInformada(dado, resposta)
                || !ValidarDataPublicacaoValida(dado, resposta))
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
                || !await ValidarNoticiaCadastrada(id, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private bool ValidarIdInformado(NoticiaDto dado, RespostaPadrao resposta)
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

        private bool ValidarIdValido(NoticiaDto dado, RespostaPadrao resposta)
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

        private async Task<bool> ValidarNoticiaCadastrada(int? id, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.Id == (int)id
                                                                && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarNoticiaCadastrada(NoticiaDto dado, RespostaPadrao resposta)
        {
            return await ValidarNoticiaCadastrada(dado.Id, resposta);
        }

        private bool ValidarTituloInformado(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Titulo))
            {
                resposta.SetCampoVazio("Título");
                return false;
            }
            return true;
        }

        private bool ValidarTituloTamanho(NoticiaDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Titulo, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Título", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoInformada(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Descricao))
            {
                resposta.SetCampoVazio("Descrição");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoTamanho(NoticiaDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Titulo, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Descrição", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarSubTituloInformada(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.SubTitulo))
            {
                resposta.SetCampoVazio("SubTitulo");
                return false;
            }
            return true;
        }

        private bool ValidarSubTituloTamanho(NoticiaDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 300;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.SubTitulo, tamanhoCampo))
            {
                resposta.SetCampoInvalido("SubTitulo", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarDataPublicacaoInformada(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (dado.DataPublicacao == null)
            {
                resposta.SetCampoVazio("Data da Publicação");
                return false;
            }
            return true;
        }

        private bool ValidarDataPublicacaoValida(NoticiaDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarData(dado.DataPublicacao))
            {
                resposta.SetCampoInvalido("Data da Publicação");
                return false;
            }
            return true;
        }

        #endregion
    }
}