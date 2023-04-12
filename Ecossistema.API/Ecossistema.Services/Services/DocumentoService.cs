using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
using Ecossistema.Util.Validacao;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;
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
        private readonly IArquivoService _arquivoService;
        private readonly ITagService _tagService;

        public DocumentoService(IUnitOfWork unitOfWork, IArquivoService arquivoService, ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _arquivoService = arquivoService;
            _tagService = tagService;   

        }

        public async Task<RespostaPadrao> Incluir(DocumentoDto dado, IFormFile doc, string usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == usuarioId);

                #region Instituição

                var obj = new Documento(dado.Nome,
                                          dado.Descricao,
                                          (int)dado.TipoDocumentoId,
                                          (int)dado.DocumentoAreaId,
                                          (int)dado.InstituicaoId,
                                          (DateTime)dado.Data,
                                          usuario.Id,
                                          DateTime.Now);

                var documento = await _unitOfWork.Documentos.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion
                List<IFormFile> arquivo = new List<IFormFile>();
                arquivo.Add(doc);

                if (!await _arquivoService.Vincular(EOrigem.Documento, obj.Id, arquivo, usuario.Id, DateTime.Now, resposta))
                {
                    return resposta;
                }

                #region Aprovação

                var aprovacao = await _unitOfWork.Aprovacoes.FindAsync(x => x.DocumentoId == obj.Id);

                if (aprovacao != null)
                {
                    obj.AprovacaoId = aprovacao.Id;

                    _unitOfWork.Documentos.Update(obj);
                }

                resposta.Retorno = _unitOfWork.Complete() > 0;

                #endregion

                TagDto anterior = new TagDto();
                if (dado.Tags != null)
                {
                    foreach (var x in dado.Tags)
                    {
                        RespostaPadrao cadastro = new RespostaPadrao();
                        cadastro = await _tagService.CadastrarTag(x, usuario.Id);
                        x.Descricao = x.Descricao.ToLower();
                        if (x.Descricao != anterior.Descricao)
                        {
                            if (cadastro.Retorno != null)
                            {
                                var tagItem = new TagItem(EOrigem.Documento, (int)cadastro.Retorno, usuario.Id, DateTime.Now, documento.Id);
                                await _unitOfWork.TagsItens.AddAsync(tagItem);
                                _unitOfWork.Complete();
                            }
                        }
                        anterior = x;
                    }
                }

                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Editar(DocumentoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Documentos.FindAsync(x => x.Id == dado.Id, new[] { "Aprovacoes" });

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacaoId = objAlt.AprovacaoId;

                    if (objAlt.Aprovacao.SituacaoAprovacaoId != ESituacaoAprovacao.Pendente.Int32Val())
                    {
                        var aprovacao = new Aprovacao(EOrigem.Documento, usuarioId, dataAtual, objAlt.Id);

                        await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                        _unitOfWork.Complete();

                        aprovacaoId = aprovacao.Id;
                    }

                    #endregion

                    objAlt.Nome = (string)dado.Nome;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.TipoDocumentoId = (int)dado.TipoDocumentoId;
                    objAlt.DocumentoAreaId = (int)dado.DocumentoAreaId;
                    objAlt.InstituicaoId = (int)dado.InstituicaoId;
                    objAlt.Data = (DateTime)dado.Data;
                    objAlt.AprovacaoId = aprovacaoId;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Documentos.Update(objAlt);

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
                var objAlt = await _unitOfWork.Documentos.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });

                if (objAlt != null)
                {
                    #region Aprovação

                    if (objAlt.Aprovacoes.Any())
                    {
                        _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());
                        _unitOfWork.Complete();
                    }

                    #endregion

                    _unitOfWork.Documentos.Delete(objAlt);

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

            var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Ativo
                                                                   && x.Aprovado);

            var result = query.Select(x => new
            {
                id = x.Id,
                nome = x.Nome,
                data = x.Data
            })
            .Distinct()
            .OrderByDescending(x => x.data)
            .Take(3)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);
            var user = await _unitOfWork.Usuarios.GetAllAsync();

            
            var result = query.Select(x => new
            {
                id = x.Id,
                nome = x.Nome,
                descricao = x.Descricao,
                data = x.Data
            })
            .Distinct()
            .OrderByDescending(x => x.data)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> Detalhes(int id)
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Id == id, new[] { "Aprovacao", "TipoDocumento", "DocumentoArea", "Instituicao" });

            var result = query.Select(x => new
            {
                id = x.Id,
                nome = x.Nome,
                descricao = x.Descricao,
                tipoDocumentoId = x.TipoDocumentoId,
                tipoDocumento = x.TipoDocumento != null ? x.TipoDocumento.Descricao : null,
                documentoAreaid = x.DocumentoAreaId,
                documentoArea = x.DocumentoArea != null ? x.DocumentoArea.Descricao : null,
                instituicaoId = x.InstituicaoId,
                instituicao = x.Instituicao != null ? x.Instituicao.Descricao : null,
                data = x.Data,
                x.Aprovado
            })
            .Distinct();

            if (result.Any()) resposta.Retorno = result.FirstOrDefault();
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        public async Task<RespostaPadrao> ListarTiposDocumentos()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.TiposDocumentos.FindAllAsync(x => x.Ativo);

            var result = query.Select(x => new
            {
                tipoDocumentoId = x.Id,
                tipoDocumento = x.Descricao
            })
            .Distinct()
            .OrderBy(x => x.tipoDocumento)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> ListarDocumentosAreas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.DocumentosAreas.FindAllAsync(x => x.Ativo);

            var result = query.Select(x => new
            {
                documentoAreaId = x.Id,
                documentoArea = x.Descricao
            })
            .Distinct()
            .OrderBy(x => x.documentoArea)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarNomeInformado(dado, resposta)
                || !ValidarNomeTamanho(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarTipoDocumentoIdValido(dado, resposta)
                || !await ValidarTipoDocumentoIdCadastrado(dado, resposta)
                || !ValidarDocumentoAreaIdValido(dado, resposta)
                || !await ValidarDocumentoAreaIdCadastrado(dado, resposta)
                || !ValidarInstituicaoIdValida(dado, resposta)
                || !await ValidarInstituicaoIdCadastrada(dado, resposta)
                || !ValidarDataInformada(dado, resposta)
                || !ValidarDataValida(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEditar(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(dado, resposta)
                || !ValidarIdValido(dado, resposta)
                || !await ValidarIdCadastrado(dado, resposta)
                || !ValidarNomeInformado(dado, resposta)
                || !ValidarNomeTamanho(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
                || !ValidarTipoDocumentoIdValido(dado, resposta)
                || !await ValidarTipoDocumentoIdCadastrado(dado, resposta)
                || !ValidarDocumentoAreaIdValido(dado, resposta)
                || !await ValidarDocumentoAreaIdCadastrado(dado, resposta)
                || !ValidarInstituicaoIdValida(dado, resposta)
                || !await ValidarInstituicaoIdCadastrada(dado, resposta)
                || !ValidarDataInformada(dado, resposta)
                || !ValidarDataValida(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
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
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private bool ValidarIdInformado(DocumentoDto dado, RespostaPadrao resposta)
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

        private bool ValidarIdValido(DocumentoDto dado, RespostaPadrao resposta)
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
            var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Id == (int)id
                                                                && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarIdCadastrado(DocumentoDto dado, RespostaPadrao resposta)
        {
            return await ValidarIdCadastrado(dado.Id, resposta);
        }

        private bool ValidarNomeInformado(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Nome))
            {
                resposta.SetCampoVazio("Nome");
                return false;
            }
            return true;
        }

        private bool ValidarNomeTamanho(DocumentoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Nome, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Nome", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoInformada(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Descricao))
            {
                resposta.SetCampoVazio("Descrição");
                return false;
            }
            return true;
        }

        private bool ValidarDescricaoTamanho(DocumentoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Descricao, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Descrição", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarTipoDocumentoIdValido(DocumentoDto dado, RespostaPadrao resposta)
        {
            return ValidarTipoDocumentoIdValido(dado.TipoDocumentoId, resposta);
        }

        private bool ValidarTipoDocumentoIdValido(int? tipoDocumentoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(tipoDocumentoId))
            {
                resposta.SetCampoInvalido("TipoDocumentoId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoDocumentoIdCadastrado(int? tipoDocumentoId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.TiposDocumentos.FindAllAsync(x => x.Id == (int)tipoDocumentoId
                                                                        && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarTipoDocumentoIdCadastrado(DocumentoDto dado, RespostaPadrao resposta)
        {

            return await ValidarTipoDocumentoIdCadastrado(dado.TipoDocumentoId, resposta);
        }

        private bool ValidarDocumentoAreaIdValido(DocumentoDto dado, RespostaPadrao resposta)
        {
            return ValidarDocumentoAreaIdValido(dado.DocumentoAreaId, resposta);
        }

        private bool ValidarDocumentoAreaIdValido(int? documentoAreaId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(documentoAreaId))
            {
                resposta.SetCampoInvalido("DocumentoAreaId");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarDocumentoAreaIdCadastrado(int? documentoAreaId, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.DocumentosAreas.FindAllAsync(x => x.Id == (int)documentoAreaId
                                                                && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarDocumentoAreaIdCadastrado(DocumentoDto dado, RespostaPadrao resposta)
        {
            return await ValidarDocumentoAreaIdCadastrado(dado.DocumentoAreaId, resposta);
        }

        private bool ValidarInstituicaoIdValida(DocumentoDto dado, RespostaPadrao resposta)
        {
            return ValidarInstituicaoIdValida(dado.InstituicaoId, resposta);
        }

        private bool ValidarInstituicaoIdValida(int? instituicaoId, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(instituicaoId))
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
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarInstituicaoIdCadastrada(DocumentoDto dado, RespostaPadrao resposta)
        {
            return await ValidarInstituicaoIdCadastrada(dado.InstituicaoId, resposta);
        }

        private bool ValidarDataInformada(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (dado.Data == null)
            {
                resposta.SetCampoVazio("Data");
                return false;
            }
            return true;
        }

        private bool ValidarDataValida(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarData(dado.Data))
            {
                resposta.SetCampoInvalido("Data");
                return false;
            }
            return true;
        }

        #endregion

    }
}
