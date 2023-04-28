using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
using Ecossistema.Util.Validacao;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class EventoService : IEventoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAprovacaoService _aprovacaoService;
        private readonly IArquivoService _arquivoService;
        private readonly IEnderecoService _enderecoService;
        private readonly ITagService _tagService;

        public EventoService(IUnitOfWork unitOfWork,
            IEnderecoService enderecoService,
            IArquivoService arquivoService,
            IAprovacaoService aprovacaoService,
            ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _aprovacaoService = aprovacaoService;
            _arquivoService = arquivoService;
            _enderecoService = enderecoService;
            _tagService = tagService;
        }

        public async Task<RespostaPadrao> Incluir(EventoArquivosDto item, string token)
        {
            var resposta = new RespostaPadrao();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var aspNetId = jwtSecurityToken.Payload.Values.FirstOrDefault().ToString();
            //var lalala = jwtSecurityToken.Payload.Values.ElementAt(3).ToJson;
            var objAlt = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == aspNetId);
            var usuarioId = objAlt.Id;
            var dado = ConverterEvento(item);

            if (!await ValidarIncluir(dado, item.Arquivo, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                #region Endereço

                if (dado.EnderecoId == null)
                {
                    if (dado.Endereco != null)
                    {
                        dado.EnderecoId = await _enderecoService.Vincular(dado.Endereco, dataAtual, usuarioId, resposta);
                    }

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
                                          dataAtual);

                var evento = await _unitOfWork.Eventos.AddAsync(obj);

                _unitOfWork.Complete();

                #endregion

                if (!await _arquivoService.Vincular(EOrigem.Evento, obj.Id, item.Arquivo, usuarioId, dataAtual, resposta)
                    || !await _aprovacaoService.Vincular(EOrigem.Evento, obj.Id, resposta))
                {
                    return resposta;
                }

                resposta.Retorno = true;
                TagDto anterior = new TagDto();
                if (item.Tags != null)
                {
                    foreach (var x in item.Tags)
                    {
                        RespostaPadrao cadastro = new RespostaPadrao();
                        cadastro = await _tagService.CadastrarTag(x, usuarioId);
                        x.Descricao = x.Descricao.ToLower();
                        if (x.Descricao != anterior.Descricao)
                        {
                            if (cadastro.Retorno != null)
                            {
                                var tagItem = new TagItem(EOrigem.Evento, (int)cadastro.Retorno, usuarioId, DateTime.Now, evento.Id);
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

        public async Task<RespostaPadrao> Editar(EventoArquivosDto item, string token)
        {
            var resposta = new RespostaPadrao();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var aspNetId = jwtSecurityToken.Payload.Values.FirstOrDefault().ToString();
            //var perfis = jwtSecurityToken.Payload.Values.ElementAt(3).ToString();
            var buscaUsuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == aspNetId);
            var usuarioId = buscaUsuario.Id;
            var dado = ConverterEvento(item);
            int idEvento = (int)dado.Id;

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                #region Endereço

                if (dado.EnderecoId == null)
                {
                    var endereco = dado.Endereco;

                    dado.EnderecoId = await _enderecoService.Vincular(endereco, dataAtual, usuarioId, resposta);

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
                    if (item.Arquivo.Count > 0)
                    {
                        var encontra = await _arquivoService.EncontraArquivoId(dado.Id.Value, EOrigem.Evento);
                        if (encontra == 0)
                        {
                            await _arquivoService.Vincular(EOrigem.Evento, idEvento, item.Arquivo, usuarioId, dataAtual, resposta);
                        }
                        else
                        {
                            IFormFile arquivo = item.Arquivo[item.Arquivo.Count - 1];
                            ArquivoDto arquivoDto = new ArquivoDto();
                            arquivoDto.Id = encontra;
                            arquivoDto.NomeOriginal = arquivo.FileName;
                            arquivoDto.Extensao = arquivo.ContentType;
                            await _arquivoService.ExcluirDoDiretorio(idEvento, "evento");
                            _arquivoService.SalvarArquivo(encontra, arquivo, EOrigem.Evento);
                            resposta = await _arquivoService.Atualizar(arquivoDto, EOrigem.Evento, usuarioId);
                        }

                    }

                    if (item.Tags != null)
                    {
                        foreach (var x in item.Tags)
                        {
                            var buscaTag = await _unitOfWork.Tags.FindAsync(y => y.Descricao == x.Descricao);
                            if (buscaTag == null)
                            {
                                var cadastro = new RespostaPadrao();
                                cadastro = await _tagService.CadastrarTag(x, usuarioId);
                                var tagItem = new TagItem(EOrigem.Evento, (int)cadastro.Retorno, usuarioId, DateTime.Now, objAlt.Id);
                                await _unitOfWork.TagsItens.AddAsync(tagItem);
                                _unitOfWork.Complete();
                            }
                            if (buscaTag != null)
                            {
                                var buscaTagItem = await _unitOfWork.TagsItens.FindAsync(y => y.TagId == buscaTag.Id && y.EventoId == dado.Id);
                                if (buscaTagItem == null)
                                {
                                    var tagItem = new TagItem(EOrigem.Evento, buscaTag.Id, usuarioId, DateTime.Now, dado.Id);
                                    _unitOfWork.TagsItens.Add(tagItem);
                                    _unitOfWork.Complete();
                                }
                            }

                        }
                    }

                    var buscaTagsItens = await _unitOfWork.TagsItens.FindAllAsync(x => x.EventoId == dado.Id);
                    foreach (var x in buscaTagsItens)
                    {
                        var tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                        bool encontrou = false;
                        if (item.Tags == null)
                        {
                            _unitOfWork.TagsItens.Delete(x);
                            _unitOfWork.Complete();
                        }
                        else
                        {
                            if (item.Tags != null)
                            {
                                foreach (var z in item.Tags)
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
                var objAlt = await _unitOfWork.Eventos.FindAsync(x => x.Id == id, new[] { "Aprovacoes" });
                var tagItem = await _unitOfWork.TagsItens.FindAllAsync(x => x.EventoId == id);
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);

                if (usuario.Id != objAlt.UsuarioCriacaoId)
                {
                    resposta.SetChamadaInvalida("Você não tem permissão para excluir evento criada por outro usuário!");
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
                    await _arquivoService.ExcluirArquivo(id, "evento");
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

        /*public async Task<RespostaPadrao> ListarUltimas()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);
            List<EventoGetImagenDto> evento = new List<EventoGetImagenDto>();
            evento.Add(new EventoGetImagenDto());
            foreach (var item in query)
            {
                evento[evento.Count - 1].Id = item.Id;
                evento[evento.Count - 1].Titulo = item.Titulo;
                evento[evento.Count - 1].DataInicio = item.DataInicio;
                evento[evento.Count - 1].DataTermino = item.DataTermino;
                evento[evento.Count - 1].Local = item.Local;
                var temp = await _arquivoService.ObterArquivos(EOrigem.Evento, item.Id, resposta);
                evento[evento.Count - 1].DataOperacao = item.DataOperacao;
                if (temp.Count > 0)
                    evento[evento.Count - 1].Arquivo = temp[temp.Count - 1].Arquivo;
                else
                    evento[evento.Count - 1].Arquivo = null;
                evento.Add(new EventoGetImagenDto());
            }
            var result = evento.Select(x => new
            {
                id = x.Id,
                titulo = x.Titulo,
                dataInicio = x.DataInicio,
                dataTermino = x.DataTermino,
                local = x.Local,
                arquivo = x.Arquivo,
                utimaAtualizacao = x.DataOperacao

            })
            .Distinct()
            .OrderByDescending(x => x.dataInicio)
            .Take(3)
            .ToList();
            resposta.Retorno = result;
            return resposta;
        }*/

        public async Task<RespostaPadrao> ListarEventos(string listagem, string idLogin)
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var id = 0;

            if (idLogin != null)
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == idLogin);
                id = usuario.Id;
            }

            List<EventoGetImagenDto> evento = new List<EventoGetImagenDto>();
            //evento.Add(new EventoGetImagenDto());
            foreach (var item in query)
            {
                //arquivoDto = await _arquivoService.ObterArquivos(EOrigem.Evento, item.Id, resposta);
                //var temp = arquivoDto[cont].Arquivo;
                //item.Arquivo = temp;
                evento.Add(new EventoGetImagenDto());
                evento[evento.Count - 1].Id = item.Id;
                evento[evento.Count - 1].Titulo = item.Titulo;
                evento[evento.Count - 1].DataInicio = item.DataInicio;
                evento[evento.Count - 1].DataTermino = item.DataTermino;
                evento[evento.Count - 1].Local = item.Local;
                evento[evento.Count - 1].UsuarioId = item.UsuarioCriacaoId;
                var temp = await _arquivoService.ObterArquivos(EOrigem.Evento, item.Id, resposta);
                evento[evento.Count - 1].DataOperacao = item.DataOperacao;
                evento[evento.Count - 1].Tags = new List<TagDto>();
                var tagsItens = _unitOfWork.TagsItens.FindAll(x => x.EventoId == item.Id);
                if(tagsItens != null)
                {
                    foreach (var x in tagsItens)
                    {
                        Tag tag = new Tag();
                        TagDto tagDto = new TagDto();
                        tag = await _unitOfWork.Tags.FindAsync(y => y.Id == x.TagId);
                        tagDto.Descricao = tag.Descricao;
                        evento[evento.Count - 1].Tags.Add(tagDto);
                    }
                }
                
                if (temp.Count > 0)
                {
                    evento[evento.Count - 1].Arquivo = temp[temp.Count - 1].Arquivo;
                    if (evento[evento.Count - 1].Arquivo != null)
                    {
                        var aux = await _arquivoService.DownloadArquivo(item.Id, item.Titulo, EOrigem.Evento);
                        if (aux != null)
                            evento[evento.Count - 1].LinkImagem = aux.ToString();
                    }

                    //
                }
                else
                    evento[evento.Count - 1].Arquivo = null;
            }
            if (listagem == "todos")
            {
                var result = evento.Select(x => new
                {
                    id = x.Id,
                    titulo = x.Titulo,
                    dataInicio = x.DataInicio,
                    dataTermino = x.DataTermino,
                    local = x.Local,
                    arquivo = x.Arquivo,
                    link = x.LinkImagem,
                    tags = x.Tags,
                    ultimaAtualizacao = x.DataOperacao

                })
            .Distinct()
            .OrderByDescending(x => x.id)
            .ToList();
                resposta.Retorno = result;
            }
            else if (listagem == "ultimos")
            {
                var result = evento.Select(x => new
                {
                    id = x.Id,
                    titulo = x.Titulo,
                    dataInicio = x.DataInicio,
                    dataTermino = x.DataTermino,
                    local = x.Local,
                    arquivo = x.Arquivo,
                    tags = x.Tags,
                    ultimaAtualizacao = x.DataOperacao

                })
            .Distinct()
            .OrderByDescending(x => x.dataInicio)
            .Take(3)
            .ToList();
                resposta.Retorno = result;
            }
            else if (listagem == "ultimosPorUsuarioId")
            {
                var result = evento.Select(x => new
                {
                    id = x.Id,
                    titulo = x.Titulo,
                    dataInicio = x.DataInicio,
                    dataTermino = x.DataTermino,
                    local = x.Local,
                    arquivo = x.Arquivo,
                    tags = x.Tags,
                    usuarioId = x.UsuarioId,
                    ultimaAtualizacao = x.DataOperacao

                }).Where(x => x.usuarioId == id)
            .Distinct()
            .OrderByDescending(x => x.dataInicio)
            .Take(3)
            .ToList();
                resposta.Retorno = result;
            }
            else if (listagem == "id" && id != 0)
            {
                var result = evento.Select(x => new
                {
                    id = x.Id,
                    titulo = x.Titulo,
                    dataInicio = x.DataInicio,
                    dataTermino = x.DataTermino,
                    local = x.Local,
                    arquivo = x.Arquivo,
                    tags = x.Tags,
                    usuarioId = x.UsuarioId,
                    ultimaAtualizacao = x.DataOperacao

                }).Where(x => x.usuarioId == id).ToList()
            .Distinct()
            .OrderByDescending(x => x.dataInicio)
            .ToList();
                if (result.Count == 0)
                    resposta.SetNaoEncontrado("Não existe evento cadastrado referente ao id informado");
                resposta.Retorno = result;
            }
            return resposta;
        }

        public async Task<RespostaPadrao> Detalhes(int id)
        {
            var resposta = new RespostaPadrao();

            try
            {
                var arquivos = await _arquivoService.ObterArquivos(EOrigem.Evento, id, resposta);

                var includes = new[] { "Instituicao", "TipoEvento", "Aprovacao", "Endereco", "TagsItens.Tag" };

                var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Id == id, includes);
                var evento = query.FirstOrDefault();
                var tagsItens = _unitOfWork.TagsItens.FindAll(x => x.EventoId == evento.Id);
                var result = query.Select(x => new
                {
                    id = x.Id,
                    instituicaoId = x.InstituicaoId,
                    instituicao = x.Instituicao != null ? x.Instituicao.RazaoSocial : null,
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
                    aprovado = x.Aprovado,
                    arquivos = arquivos,
                    tags = x.TagsItens.Select(ti => new { id = ti.Tag.Id, descricao = ti.Tag.Descricao }).ToList()
                })
                .Distinct();


                if (result.Any()) resposta.Retorno = result.FirstOrDefault();
                else resposta.SetNaoEncontrado("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                resposta.SetBadRequest(ex.Message);
            }


            return resposta;
        }

        public async Task<RespostaPadrao> ListarTiposEventos()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.TiposEventos.FindAllAsync(x => x.Ativo);

            var result = query.Select(x => new
            {
                tipoEventoId = x.Id,
                tipoEvento = x.Descricao
            })
            .Distinct()
            .OrderBy(x => x.tipoEvento);

            if (result.Any()) resposta.Retorno = result;
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        public async Task<RespostaPadrao> ListarEnderecos(int instituicaoId, int tipoEnderecoId)
        {
            var resposta = new RespostaPadrao();

            var includes = new[] { "Endereco" };

            var query = await _unitOfWork.InstituicoesEnderecos.FindAllAsync(x => x.InstituicaoId == instituicaoId
                                                                              && x.TipoEnderecoId == tipoEnderecoId
                                                                              && x.Ativo, includes);

            var result = query.Select(x => new
            {
                enderecoId = x.EnderecoId,
                cep = x.Endereco.Cep,
                logradouro = x.Endereco.Logradouro,
                numero = x.Endereco.Numero,
                complemento = x.Endereco.Complemento,
                pontoReferencia = x.Endereco.PontoReferencia,
                bairro = x.Endereco.Bairro,
                cidade = x.Endereco.Cidade,
                uf = x.Endereco.Uf,
            })
            .Distinct()
            .OrderBy(x => x.logradouro);

            if (result.Any()) resposta.Retorno = result;
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        public async Task<RespostaPadrao> ListarTiposEnderecos()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.TiposEnderecos.FindAllAsync(x => x.Ativo);

            var queryDict = query.Distinct()
                                 .OrderBy(x => x.Descricao)
                                 .ToDictionary(x => x.Id, x => x.Descricao);

            queryDict.Add(0, "Novo Endereço");

            var result = queryDict.Select(x => new
            {
                tipoEnderecoId = x.Key,
                tipoEndereco = x.Value
            });

            if (result.Any()) resposta.Retorno = result;
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }

        private EventoDto? ConverterEvento(EventoArquivosDto obj)
        {
            var evento = JsonConvert.DeserializeObject<EventoDto>(obj.Evento);

            return evento != null ? evento : null;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(EventoDto dado, List<IFormFile>? arquivos, RespostaPadrao resposta)
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
            //|| !ValidarLocalInformada(dado, resposta)
            || !ValidarLocalTamanho(dado, resposta)
            || (dado.EnderecoId != null
                && (!ValidarEnderecoIdValido(dado, resposta)
                    || !await ValidarEnderecoIdCadastrada(dado, resposta)))
            //|| !ValidarLinkExternoInformada(dado, resposta)
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
               //|| !ValidarLocalInformada(dado, resposta)
               || !ValidarLocalTamanho(dado, resposta)
               || (dado.EnderecoId != null
                    && (!ValidarEnderecoIdValido(dado, resposta)
                        || !await ValidarEnderecoIdCadastrada(dado, resposta)))
               //|| !ValidarLinkExternoInformada(dado, resposta)
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
            var query = await _unitOfWork.Eventos.FindAllAsync(x => x.Id == (int)id
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

        /*private bool ValidarLinkExternoInformada(EventoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.LinkExterno))
            {
                resposta.SetCampoVazio("LinkExterno");
                return false;
            }
            return true;
        }*/

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