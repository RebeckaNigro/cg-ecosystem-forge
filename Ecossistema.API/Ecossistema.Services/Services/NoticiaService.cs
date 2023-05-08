using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
using Ecossistema.Util.Validacao;
using Microsoft.AspNetCore.Http;
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
        private readonly IArquivoService _arquivoService;
        private readonly ITagService _tagService;

        public NoticiaService(IUnitOfWork unitOfWork, IArquivoService arquivoService, ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _arquivoService = arquivoService;
            _tagService = tagService;
        }

        public async Task<RespostaPadrao> Incluir(NoticiaDto dado, IFormFile imagem, string idLogin)
        {
            var resposta = new RespostaPadrao();
            if(dado.Tags != null) dado.Tags = dado.Tags.OrderBy(x => x.Descricao).ToList();
            if (!ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                #region Instituição

                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
                var obj = new Noticia(dado.Titulo,
                                      dado.Descricao,
                                      dado.SubTitulo,
                                      dado.DataPublicacao,
                                      usuario.Id,
                                      DateTime.Now);

                var noticia = await _unitOfWork.Noticias.AddAsync(obj);
                await _unitOfWork.Noticias.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion

                List<IFormFile> arquivo = new List<IFormFile>();
                if(imagem != null)
                    arquivo.Add(imagem);

                if (!await _arquivoService.Vincular(EOrigem.Noticia, obj.Id, arquivo, usuario.Id, DateTime.Now, resposta))
                {
                    return resposta;
                }

                #region Aprovação

                var aprovacao = await _unitOfWork.Aprovacoes.FindAsync(x => x.NoticiaId == obj.Id);

                if (aprovacao != null)
                {
                    obj.AprovacaoId = aprovacao.Id;
                    /****** IMPORTANTE: Essa linha abaixo ficará aqui até os perfis de usuários responsáveis para aprovação estiver pronto***********/
                    obj.Aprovado = true; // Remover em breve
                    _unitOfWork.Noticias.Update(obj);
                }

                resposta.Retorno = _unitOfWork.Complete() > 0;

                #endregion

                TagDto anterior = new TagDto();
                if(dado.Tags != null)
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
                                var tagItem = new TagItem(EOrigem.Noticia, (int)cadastro.Retorno, usuario.Id, DateTime.Now, noticia.Id);
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

        public async Task<RespostaPadrao> Editar(NoticiaDto dado, IFormFile? imagem, string idLogin)
        {
            var resposta = new RespostaPadrao();

            
            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Noticias.FindAsync(x => x.Id == dado.Id, new[] { "Aprovacao" });
                if(objAlt.UsuarioCriacaoId != usuario.Id)
                {
                    resposta.SetChamadaInvalida("Você não tem permissão para editar notícia de outro ususário.");
                    return resposta;    
                }
                if (objAlt != null)
                {
                    #region Aprovação

                    var aprovacaoId = objAlt.AprovacaoId;

                    if (objAlt.Aprovacao.SituacaoAprovacaoId != ESituacaoAprovacao.Pendente.Int32Val())
                    {
                        var aprovacao = new Aprovacao(EOrigem.Noticia, usuario.Id, dataAtual, objAlt.Id);

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
                    Recursos.Auditoria(objAlt, usuario.Id, dataAtual);

                    _unitOfWork.Noticias.Update(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;
                    if(imagem == null)
                    {
                        await _arquivoService.ExcluirArquivo(objAlt.Id, "noticia");
                    }
                    if(imagem != null)
                    {
                        //var buscaImagem = _arquivoService.EncontraArquivoId(objAlt.Id, EOrigem.Noticia);
                        await _arquivoService.ExcluirArquivo(objAlt.Id, "noticia");
                        List<IFormFile> arquivo = new List<IFormFile>();
                        arquivo.Add(imagem);

                        if (!await _arquivoService.Vincular(EOrigem.Noticia, objAlt.Id, arquivo, usuario.Id, DateTime.Now, resposta))
                        {
                            return resposta;
                        }
                    }
                    if(dado.Tags != null)
                    {
                        foreach (var x in dado.Tags)
                        {
                            var buscaTag = await _unitOfWork.Tags.FindAsync(y => y.Descricao == x.Descricao);
                            if (buscaTag == null)
                            {
                                //resposta.SetBadRequest("Tags da noticia não modificadas. Tag não cadastrada, selecione uma tag válida.");
                                var cadastro = new RespostaPadrao();
                                cadastro = await _tagService.CadastrarTag(x, usuario.Id);
                                var tagItem = new TagItem(EOrigem.Noticia, (int)cadastro.Retorno, usuario.Id, DateTime.Now, objAlt.Id);
                                await _unitOfWork.TagsItens.AddAsync(tagItem);
                                _unitOfWork.Complete();
                            }
                            if(buscaTag != null)
                            {
                                var buscaTagItem = await _unitOfWork.TagsItens.FindAsync(y => y.TagId == buscaTag.Id && y.NoticiaId == dado.Id);
                                if (buscaTagItem == null)
                                {
                                    var tagItem = new TagItem(EOrigem.Noticia, buscaTag.Id, usuario.Id, DateTime.Now, dado.Id);
                                    _unitOfWork.TagsItens.Add(tagItem);
                                    _unitOfWork.Complete();
                                }
                            }
                            
                        }
                    }
                    
                    var buscaTagsItens = await _unitOfWork.TagsItens.FindAllAsync(x => x.NoticiaId == dado.Id);
                    foreach(var x in buscaTagsItens)
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
                var objAlt = await _unitOfWork.Noticias.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });
                var tagItem = await _unitOfWork.TagsItens.FindAllAsync(x => x.NoticiaId == objAlt.Id);
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);

                if(usuario.Id != objAlt.UsuarioCriacaoId)
                {
                    resposta.SetChamadaInvalida("Você não tem permissão para excluir notícia criada por outro usuário!");
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
                    await _arquivoService.ExcluirArquivo(objAlt.Id, "noticia");
                    foreach(var x in tagItem)
                    {
                        _unitOfWork.TagsItens.Delete(x);
                        _unitOfWork.Complete();
                    }
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

        public async Task<List<NoticiaListaDto>> BuscarTagsArquivos(IEnumerable<Noticia> query)
        {
            List<NoticiaListaDto> noticias = new List<NoticiaListaDto>();
            List<Tag> tags = new List<Tag>();
            RespostaPadrao resposta = new RespostaPadrao();
            foreach (var item in query)
            {
                var tagsItens = _unitOfWork.TagsItens.FindAll(x => x.NoticiaId == item.Id);

                NoticiaListaDto noticia = new NoticiaListaDto();
                noticia.Id = item.Id;
                noticia.Titulo = item.Titulo;
                noticia.DataPublicacao = item.DataPublicacao;
                noticia.DataOperacao = item.DataOperacao;
                noticia.Tags = new List<TagDto>();
                foreach (var x in tagsItens)
                {
                    Tag tag = new Tag();
                    TagDto tagDto = new TagDto();
                    tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                    tagDto.Descricao = tag.Descricao;
                    noticia.Tags.Add(tagDto);
                }
                var temp = await _arquivoService.ObterArquivos(EOrigem.Noticia, item.Id, resposta);
                if (temp.Count != 0)
                {
                    noticia.Arquivo = temp[temp.Count - 1].Arquivo;
                }
                noticias.Add(noticia);
            }
            return noticias;
        }

        public async Task<RespostaPadrao> ListarUltimas()
        {
            var resposta = new RespostaPadrao();


            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.Ativo && x.Aprovado);

            var result = (await BuscarTagsArquivos(query)).Select(x => new
            {
                x.Id,
                x.Titulo,
                x.DataPublicacao,
                x.DataOperacao,
                x.Tags,
                x.Arquivo
            })
            .Distinct()
            .OrderByDescending(x => x.DataPublicacao)
            .Take(3)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> ListarUltimasPorUsuarioId(string idLogin)
        {
            var resposta = new RespostaPadrao();

           
            var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.UsuarioCriacaoId == usuario.Id && x.Ativo && x.Aprovado);

            var result = (await BuscarTagsArquivos(query)).Select(x => new
            {
                x.Id,
                x.Titulo,
                x.DataPublicacao,
                x.DataOperacao,
                x.Tags,
                x.Arquivo
            })
            .Distinct()
            .OrderByDescending(x => x.DataPublicacao)
            .Take(3)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodas(int paginacao)
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Noticias.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var fim = paginacao * 6;
            var inicio = fim - 6;

            var result = (await BuscarTagsArquivos(query)).Select(x => new
            {
                x.Id,
                x.Titulo,
                x.DataPublicacao,
                x.DataOperacao,
                x.Tags,
                x.Arquivo
            })
            .Distinct()
            .Skip(inicio)
            .Take(6)
            .OrderByDescending(x => x.DataPublicacao)
            .ToList();

            resposta.Retorno = result;
            if (result.Count == 0)
            {
                resposta.SetNaoEncontrado("Nenhum evento encontrado");
            }

            return resposta;
        }

        public async Task<RespostaPadrao> ListarPorUsuarioId(string idLogin)
        {
            var resposta = new RespostaPadrao();
            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
                var query = await _unitOfWork.Noticias.FindAllAsync(x => x.UsuarioCriacaoId == usuario.Id && x.Ativo && x.Aprovado);
                var result = (await BuscarTagsArquivos(query)).Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    x.DataPublicacao,
                    x.DataOperacao,
                    x.Tags,
                    x.Arquivo
                })
                .Distinct()
                .OrderByDescending(x => x.DataPublicacao)
                .ToList();
       
                if (result == null)
                {
                    resposta.SetNaoEncontrado("Você não tem notícias cadastradas");
                    return resposta;
                }
                resposta.Retorno = result;

            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }
            return resposta;
        }

        public async Task<RespostaPadrao> Detalhes(int id)
        {
            var resposta = new RespostaPadrao();
            try
            {
                var item = await _unitOfWork.Noticias.FindAsync(x => x.Id == id, new[] { "Aprovacao" });
                if(item == null)
                {
                    resposta.SetNaoEncontrado("Nenhum registro encontrado!");
                    return resposta;
                }
                var tagsItens = _unitOfWork.TagsItens.FindAll(x => x.NoticiaId == item.Id);
                NoticiaDetalhesDto noticia = new NoticiaDetalhesDto();
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.Id == item.UsuarioCriacaoId);
                var pessoa = await _unitOfWork.Pessoas.FindAsync(x => x.Id == usuario.PessoaId);
                noticia.Id = item.Id;
                noticia.Autor = pessoa.NomeCompleto;
                noticia.Titulo = item.Titulo;
                noticia.Descricao = item.Descricao;
                noticia.SubTitulo = item.SubTitulo;
                noticia.DataPublicacao = item.DataPublicacao;
                noticia.Tags = new List<TagDto>();
                foreach (var x in tagsItens)
                {
                    Tag tag = new Tag();
                    TagDto tagDto = new TagDto();
                    tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                    tagDto.Descricao = tag.Descricao;
                    noticia.Tags.Add(tagDto);
                }
                var temp = await _arquivoService.ObterArquivos(EOrigem.Noticia, item.Id, resposta);
                if (temp.Count != 0)
                {
                    noticia.Arquivo = temp[temp.Count - 1].Arquivo;
                }

                var result = noticia;
                resposta.Retorno = result;
                
            }
            catch (Exception ex)
            {
                resposta.SetMensagem(ex.Message);
            }
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