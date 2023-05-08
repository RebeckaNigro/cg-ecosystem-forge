using Ecossistema.Data;
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
        private readonly UrlStringsDto _urlStrings;

        public DocumentoService(IUnitOfWork unitOfWork, IArquivoService arquivoService, ITagService tagService, UrlStringsDto urlStrings)
        {
            _unitOfWork = unitOfWork;
            _arquivoService = arquivoService;
            _tagService = tagService;   
            _urlStrings = urlStrings;   
        }

        public async Task<RespostaPadrao> Incluir(DocumentoDto dado, IFormFile doc, string usuarioId)
        {
            var resposta = new RespostaPadrao();
            var extensao = System.IO.Path.GetExtension(doc.FileName);
            if (extensao != ".pdf")
            {
                resposta.SetBadRequest("Tipo de documento não válido, por favor, insira um do tipo pdf");
                return resposta;
            }

            if (!await ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == usuarioId);

                #region Instituição

                var obj = new Documento(dado.Nome,
                                          dado.Descricao,
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

        public async Task<RespostaPadrao> Editar(DocumentoDto dado, IFormFile doc, string usuarioLoginId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;
            var extensao = System.IO.Path.GetExtension(doc.FileName);
            if (extensao != ".pdf")
            {
                resposta.SetBadRequest("Tipo de documento não válido, por favor, insira um do tipo pdf");
                return resposta;
            }

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Documentos.FindAsync(x => x.Id == dado.Id, new[] { "Aprovacoes" });
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == usuarioLoginId);

                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacaoId = objAlt.AprovacaoId;

                    if (objAlt.Aprovacao.SituacaoAprovacaoId != ESituacaoAprovacao.Pendente.Int32Val())
                    {
                        var aprovacao = new Aprovacao(EOrigem.Documento, usuario.Id, dataAtual, objAlt.Id);

                        await _unitOfWork.Aprovacoes.AddAsync(aprovacao);

                        _unitOfWork.Complete();

                        aprovacaoId = aprovacao.Id;
                    }

                    #endregion

                    objAlt.Nome = (string)dado.Nome;
                    objAlt.Descricao = dado.Descricao;
                    objAlt.DocumentoAreaId = (int)dado.DocumentoAreaId;
                    objAlt.InstituicaoId = (int)dado.InstituicaoId;
                    objAlt.Data = (DateTime)dado.Data;
                    objAlt.AprovacaoId = aprovacaoId;
                    Recursos.Auditoria(objAlt, usuario.Id, dataAtual);

                    _unitOfWork.Documentos.Update(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;
                    List<IFormFile> arquivo = new List<IFormFile>();
                    arquivo.Add(doc);

                    var encontra = await _arquivoService.EncontraArquivoId(dado.Id.Value, EOrigem.Documento);
                    if (encontra == 0)
                    {
                        await _arquivoService.Vincular(EOrigem.Documento, objAlt.Id, arquivo, usuario.Id, dataAtual, resposta);
                    }
                    else
                    {
                        IFormFile documento = arquivo[arquivo.Count - 1];
                        ArquivoDto arquivoDto = new ArquivoDto();
                        arquivoDto.Id = encontra;
                        arquivoDto.NomeOriginal = doc.FileName;
                        arquivoDto.Extensao = documento.ContentType;
                        await _arquivoService.ExcluirDoDiretorio(objAlt.Id, "documento");
                        _arquivoService.SalvarArquivo(encontra, documento, EOrigem.Documento);
                        resposta = await _arquivoService.Atualizar(arquivoDto, EOrigem.Documento, usuario.Id);
                    }

                    if (dado.Tags != null)
                    {
                        foreach (var x in dado.Tags)
                        {
                            var buscaTag = await _unitOfWork.Tags.FindAsync(y => y.Descricao == x.Descricao);
                            if (buscaTag == null)
                            {
                                var cadastro = new RespostaPadrao();
                                cadastro = await _tagService.CadastrarTag(x, usuario.Id);
                                var tagItem = new TagItem(EOrigem.Documento, (int)cadastro.Retorno, usuario.Id, DateTime.Now, objAlt.Id);
                                await _unitOfWork.TagsItens.AddAsync(tagItem);
                                _unitOfWork.Complete();
                            }
                            else
                            {
                                var buscaTagItem = await _unitOfWork.TagsItens.FindAsync(y => y.TagId == buscaTag.Id && y.DocumentoId == dado.Id);
                                if (buscaTagItem == null)
                                {
                                    var tagItem = new TagItem(EOrigem.Documento, buscaTag.Id, usuario.Id, DateTime.Now, dado.Id);
                                    _unitOfWork.TagsItens.Add(tagItem);
                                    _unitOfWork.Complete();
                                }
                            }

                        }
                    }

                    var buscaTagsItens = await _unitOfWork.TagsItens.FindAllAsync(x => x.DocumentoId == dado.Id);
                    foreach (var x in buscaTagsItens)
                    {
                        var tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                        bool encontrou = false;
                        if (dado.Tags == null)
                        {
                            _unitOfWork.TagsItens.Delete(x);
                            _unitOfWork.Complete();
                        }
                        else
                        {
                            if (dado.Tags != null)
                            {
                                foreach (var z in dado.Tags)
                                {
                                    if (z.Descricao == tag.Descricao)
                                        encontrou = true;
                                }
                            }
                            if (encontrou == false)
                            {
                                _unitOfWork.TagsItens.Delete(x);
                                _unitOfWork.Complete();
                            }
                        }

                    }

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

        public async Task<RespostaPadrao> Excluir(int id, string idLogin)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarExcluir(id, resposta)) return resposta;

            try
            {
                var objAlt = await _unitOfWork.Documentos.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });
                var tagItem = await _unitOfWork.TagsItens.FindAllAsync(x => x.DocumentoId == id);
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);

                if (usuario.Id != objAlt.UsuarioCriacaoId)
                {
                    resposta.SetChamadaInvalida("Você não tem permissão para excluir documento criada por outro usuário!");
                    return resposta;
                }

                if (objAlt != null)
                {
                    #region Aprovação

                    if (objAlt.Aprovacoes.Any())
                    {
                        _unitOfWork.Aprovacoes.DeleteRange(objAlt.Aprovacoes.ToList());
                        _unitOfWork.Complete();
                    }

                    #endregion
                    if (tagItem != null)
                    {
                        foreach (var x in tagItem)
                        {
                            _unitOfWork.TagsItens.Delete(x);
                            _unitOfWork.Complete();
                        }
                    }
                    await _arquivoService.ExcluirArquivo(objAlt.Id, "documento");

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

        public async Task<RespostaPadrao> ListarUltimosPorUsuarioId(string idLogin)
        {
            var resposta = new RespostaPadrao();
            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
                var query = await _unitOfWork.Documentos.FindAllAsync(x => x.UsuarioCriacaoId == usuario.Id && x.Ativo && x.Aprovado);

                var result = (await BuscarNomeUsuarioEtags(query)).Select(x => new
                {
                    id = x.Id,
                    nome = x.Nome,
                    descricao = x.Descricao,
                    ultimaOperacao = x.DataOperacao,
                    autor = x.NomeUsuario,
                    tags = x.Tags,
                    data = x.Data
                })
                .Distinct()
                .OrderByDescending(x => x.ultimaOperacao)
                .Take(3)
                .ToList();

                resposta.Retorno = result;
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }
           

            return resposta;
        }


        public async Task<RespostaPadrao> ListarUltimos()
        {
            var resposta = new RespostaPadrao();
            try
            {
                var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Ativo && x.Aprovado);

                var result = (await BuscarNomeUsuarioEtags(query)).Select(x => new
                {
                    id = x.Id,
                    nome = x.Nome,
                    descricao = x.Descricao,
                    ultimaOperacao = x.DataOperacao,
                    autor = x.NomeUsuario,
                    tags = x.Tags,
                    data = x.Data
                })
                .Distinct()
                .OrderByDescending(x => x.ultimaOperacao)
                .Take(3)
                .ToList();

                resposta.Retorno = result;
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }


            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodos(int paginacao)
        {
            var resposta = new RespostaPadrao();
            try
            {
                var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

                var fim = paginacao * 6;
                var inicio = fim - 6;

                var result = (await BuscarNomeUsuarioEtags(query)).Select(x => new
                {
                    id = x.Id,
                    nome = x.Nome,
                    descricao = x.Descricao,
                    ultimaOperacao = x.DataOperacao,
                    autor = x.NomeUsuario,
                    tags = x.Tags,
                    data = x.Data
                })
                .Distinct()
                .Skip(inicio)
                .Take(6)
                .OrderByDescending(x => x.data)
                .ToList();

                resposta.Retorno = result;
                if (result.Count == 0)
                {
                    resposta.SetNaoEncontrado("Nenhum evento encontrado");
                }

                return resposta;
            }
            catch(Exception ex)
            {
                resposta.SetBadRequest(ex.Message);
                return resposta;
            }

        }

        public async Task<RespostaPadrao> ListarPorUsuarioId(string idLogin)
        {
            var resposta = new RespostaPadrao();

            var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
            var query = await _unitOfWork.Documentos.FindAllAsync(x => x.UsuarioCriacaoId == usuario.Id && x.Ativo && x.Aprovado);

            var result = (await BuscarNomeUsuarioEtags(query)).Select(x => new
            {
                id = x.Id,
                nome = x.Nome,
                descricao = x.Descricao,
                documentoArea = x.DocumentoArea,
                ultimaOperacao = x.DataOperacao,
                autor = x.NomeUsuario,
                tags = x.Tags,
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

            var query = await _unitOfWork.Documentos.FindAllAsync(x => x.Id == id, new[] { "Aprovacao" , "DocumentoArea", "Instituicao" });
            var doc = query.FirstOrDefault();
            var nome = doc.Nome.Replace(" ", "%20");
            var download = _urlStrings.ApiUrl + "documento/downloadDocumento?id="+doc.Id+"&nome="+nome+"&origem=3";
            var tagItems = await _unitOfWork.TagsItens.FindAllAsync(x => x.DocumentoId == id);
            List<TagDto> tags = new List<TagDto>();
            foreach(var x in tagItems)
            {
                tags.Add(new TagDto());
                var tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                tags[tags.Count - 1].Descricao = tag.Descricao;
            }
            var result = query.Select(x => new
            {
                download,
                id = x.Id,
                nome = x.Nome,
                descricao = x.Descricao,
                documentoAreaid = x.DocumentoAreaId,
                documentoArea = x.DocumentoArea != null ? x.DocumentoArea.Descricao : null,
                instituicaoId = x.InstituicaoId,
                instituicao = x.Instituicao != null ? x.Instituicao.RazaoSocial : null,
                data = x.Data,
                tags,
                x.Aprovado
            })
            .Distinct();

            if (result.Any()) resposta.Retorno = result.FirstOrDefault();
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        /*public async Task<RespostaPadrao> ListarTiposDocumentos()
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
        }*/

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

        public async Task<List<DocumentoDto>> BuscarNomeUsuarioEtags(IEnumerable<Documento> query)
        {
            List<DocumentoDto> documentos = new List<DocumentoDto>();
            List<Tag> tags = new List<Tag>();
            RespostaPadrao resposta = new RespostaPadrao();
            foreach (var item in query)
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.Id == item.UsuarioCriacaoId);
                var pessoa = _unitOfWork.Pessoas.FindAll(x => x.Id == usuario.PessoaId)
                                .Select(x => new { x.NomeCompleto })
                                .FirstOrDefault();

                var tagsItens = _unitOfWork.TagsItens.FindAll(x => x.DocumentoId == item.Id);
                var documentoArea = await _unitOfWork.DocumentosAreas.FindAsync(x => x.Id == item.DocumentoAreaId);

                DocumentoDto documento = new DocumentoDto();
                documento.Id = item.Id;
                documento.Nome = item.Nome;
                documento.Descricao = item.Descricao;
                documento.DocumentoArea = documentoArea.Descricao;
                documento.Data = item.Data;
                documento.DataOperacao = item.DataOperacao;
                documento.NomeUsuario = pessoa.NomeCompleto;
                documento.Tags = new List<TagDto>();
                foreach (var x in tagsItens)
                {
                    Tag tag = new Tag();
                    TagDto tagDto = new TagDto();
                    tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                    tagDto.Descricao = tag.Descricao;
                    documento.Tags.Add(tagDto);
                }
                /*var temp = await _arquivoService.ObterArquivos(EOrigem.Noticia, item.Id, resposta);
                if (temp.Count != 0)
                {
                    noticia.Arquivo = temp[temp.Count - 1].Arquivo;
                }*/
                documentos.Add(documento);
            }
            return documentos;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(DocumentoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarNomeInformado(dado, resposta)
                || !ValidarNomeTamanho(dado, resposta)
                || !ValidarDescricaoInformada(dado, resposta)
                || !ValidarDescricaoTamanho(dado, resposta)
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
