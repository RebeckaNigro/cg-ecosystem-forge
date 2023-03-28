using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string RepositorioArquivo = "RepositoryFiles";
        private readonly string Documents = "Documents";
        private readonly string NoticiasImagens = "NoticiasImagens";
        private readonly string EventosImagens = "EventosImagens";

        public ArquivoService(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> Vincular(EOrigem origem, int id, List<IFormFile>? arquivos, int usuarioId, DateTime dataAtual, RespostaPadrao resposta)
        {
            if (arquivos == null || arquivos.Count == 0) return true;

            try
            {
                var ids = new List<int>();

                foreach (var arquivo in arquivos)
                {
                    var item = new Arquivo(origem, arquivo.FileName, usuarioId, dataAtual, id);

                    await _unitOfWork.Arquivos.AddAsync(item);

                    if (_unitOfWork.Complete() > 0 && SalvarArquivo(item.Id, arquivo, origem)) ids.Add(item.Id);
                    else
                    {
                        ExcluirArquivos(ids);
                        resposta.SetErroInterno("Erro na gravação dos arquivos!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<RespostaPadrao> Atualizar(ArquivoDto arquivoDto, EOrigem origem, int usuarioId)
        {
            RespostaPadrao resposta = new RespostaPadrao();
            try
            {
                var dataAtual = DateTime.Now;
                var arquivoOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.ArquivoId == arquivoDto.Id);
                var arquivo = await _unitOfWork.Arquivos.FindAsync(x => x.Id == arquivoOrigem.ArquivoId);
                
                if (arquivo == null)
                {
                    resposta.SetNaoEncontrado(arquivoDto.NomeOriginal);
                    return resposta;
                }
                arquivo.Extensao = arquivoDto.NomeOriginal.Substring(arquivoDto.NomeOriginal.LastIndexOf(".") + 1, arquivoDto.NomeOriginal.Length - arquivoDto.NomeOriginal.LastIndexOf(".") - 1);
                arquivo.Extensao = arquivo.Extensao.ToLower();
                arquivo.NomeOriginal = arquivoDto.NomeOriginal.Replace("." + arquivo.Extensao, "");
                arquivoOrigem.Titulo =  arquivoDto.NomeOriginal.Replace("." + arquivo.Extensao, "");
                Recursos.Auditoria(arquivo, usuarioId, dataAtual);
                Recursos.Auditoria(arquivoOrigem, usuarioId, dataAtual);
                _unitOfWork.Arquivos.Update(arquivo);
                _unitOfWork.ArquivosOrigens.Update(arquivoOrigem);
                _unitOfWork.Complete();
                resposta.SetMensagem("Dados gravados com sucesso");
            }
            catch(Exception ex)
            {
                resposta.SetBadRequest(ex.Message);
            }
            return resposta;
        }  

        public async Task<List<ArquivoDto>> ObterArquivos(EOrigem origem, int id, RespostaPadrao resposta)
        {
            int? instituicaoId = null;
            int? noticiaId = null;
            int? eventoId = null;
            int? documentoId = null;
            int? paginaId = null;
            var dir = RepositorioArquivo;
            switch (origem)
            {
                case EOrigem.Parceiro:
                    instituicaoId = id;
                    break;
                case EOrigem.Documento:
                    dir = Documents;
                    documentoId = id;
                    break;
                case EOrigem.Noticia:
                    dir = NoticiasImagens;
                    noticiaId = id;
                    break;
                case EOrigem.Evento:
                    dir = EventosImagens;
                    eventoId = id;
                    break;
                case EOrigem.Pagina:
                    paginaId = id;
                    break;
                default:
                    break;
            }

            var includes = new[] { "Arquivo" };

            var query = await _unitOfWork.ArquivosOrigens.FindAllAsync(x => x.Ativo
                                                                        && x.InstituicaoId == instituicaoId
                                                                        && x.NoticiaId == noticiaId
                                                                        && x.EventoId == eventoId
                                                                        && x.DocumentoId == documentoId
                                                                        && x.PaginaId == paginaId, includes);


            var result = query.Select(x => new ArquivoDto
            {
                Id = x.ArquivoId,
                NomeOriginal = x.Arquivo.NomeOriginal,
                Extensao = x.Arquivo.Extensao,
                Arquivo = null
            }).ToList();

            /*if (origem.Equals("Evento"))
            {
                result[result.Count - 1].Arquivo = ObterArquivoBinario(result[result.Count - 1].Id);
                return result;
            }*/
            foreach (var item in result)
            {
                item.Arquivo = ObterArquivoBinario(item.Id, dir);
            }

            return result;
        }

        public bool SalvarArquivo(int id, IFormFile file, EOrigem origem)
        {
            try
            {
                if (file == null) return false;
                //var mimeType = file.ContentType;
                var convertOrigem = origem.ToString();

                var fileExtension = file.FileName.Split('.').Last();
                if (convertOrigem.Equals("Noticia"))
                {
                    using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, NoticiasImagens, id.ToString() + "." + fileExtension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                else
                if (convertOrigem.Equals("Documento"))
                {
                    using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, Documents, id.ToString() + "." + fileExtension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                else
                if (convertOrigem.Equals("Evento"))
                {
                    using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, EventosImagens, id.ToString() + "." + fileExtension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                else
                {
                    using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, id.ToString() + "." + fileExtension), FileMode.Create))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                }
                
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ExcluirArquivos(List<int> arquivos)
        {
            return true;
        }
        public async Task<RespostaPadrao> ExcluirArquivo(int id, string tipo)
        {
            RespostaPadrao resposta = new RespostaPadrao();
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo);
                var idArquivo = 0;
                var idArquivoOrigem = 0;
                if (tipo == "documento")
                {
                    path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, Documents);
                    var buscaOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.DocumentoId == id);
                    var busca = await _unitOfWork.Arquivos.FindAsync(x => x.Id == buscaOrigem.ArquivoId);
                    idArquivoOrigem = buscaOrigem.Id;
                    idArquivo = busca.Id;
                }
                else if(tipo == "evento")
                {
                    path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, EventosImagens);
                    var buscaOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.EventoId == id);
                    var busca = await _unitOfWork.Arquivos.FindAsync(x => x.Id == buscaOrigem.ArquivoId);
                    idArquivoOrigem = buscaOrigem.Id;
                    idArquivo = busca.Id;
                }
                else if (tipo == "noticia")
                {
                    path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, NoticiasImagens);
                    var buscaOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.NoticiaId == id);
                    var busca = await _unitOfWork.Arquivos.FindAsync(x => x.Id == buscaOrigem.ArquivoId);
                    idArquivoOrigem = buscaOrigem.Id;
                    idArquivo = busca.Id;
                }
                string result = Directory.GetFiles(path, Path.GetFileName(idArquivo.ToString()) + ".*").FirstOrDefault();
                if (result == null)
                {
                    resposta.SetNaoEncontrado("Arquivo não existe");
                    return resposta;
                }
                File.Delete(result);
                var origem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.Id == idArquivoOrigem);
                var arquivo = await _unitOfWork.Arquivos.FindAsync(x => x.Id == idArquivo);
                _unitOfWork.ArquivosOrigens.Delete(origem);
                _unitOfWork.Arquivos.Delete(arquivo);
                _unitOfWork.Complete();
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
                return resposta;
            }
        }


        public async Task<RespostaPadrao> ExcluirDoDiretorio(int id, string tipo)
        {
            RespostaPadrao resposta = new RespostaPadrao();
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo);
                var idArquivo = 0;
                if (tipo == "documento")
                {
                    path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, Documents);
                    var origem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.DocumentoId == id);
                    var busca = await _unitOfWork.Arquivos.FindAsync(x => x.Id == origem.ArquivoId);
                    idArquivo = busca.Id;
                }
                else if (tipo == "evento")
                {
                    path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, EventosImagens);
                    var origem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.EventoId == id);
                    var busca = await _unitOfWork.Arquivos.FindAsync(x => x.Id == origem.ArquivoId);
                    idArquivo = busca.Id;
                }
                else if (tipo == "noticia")
                {
                    path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, NoticiasImagens);
                    var origem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.EventoId == id);
                    var busca = await _unitOfWork.Arquivos.FindAsync(x => x.Id == origem.ArquivoId);
                    idArquivo = busca.Id;
                }
                string result = Directory.GetFiles(path, Path.GetFileName(idArquivo.ToString()) + ".*").FirstOrDefault();
                if (result == null)
                {
                    resposta.SetNaoEncontrado("Arquivo não existe");
                    return resposta;
                }
                File.Delete(result);
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
                return resposta;
            }
        }


        private byte[] ObterArquivoBinario(int id, string dir)
        {
            byte[] arquivoBin = null;

            //var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, Path.GetFileName(id.ToString()));
            var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo);
            if(dir != RepositorioArquivo)
            {
                path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, dir);
            }
            string result = Directory.GetFiles(path, Path.GetFileName(id.ToString()) + ".*").FirstOrDefault();
            if(result == null)
                return null;
            using (var fs = File.OpenRead(result))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                fs.Flush();
                arquivoBin = ms.ToArray();
            }

            return arquivoBin;
        }

        public async Task<FileContentResult> DownloadArquivo(int id, string nome, EOrigem origem)
        {
            
            var arquivoId = await EncontraArquivoId(id, origem);
            if (arquivoId == 0) {
                //resposta.SetNaoEncontrado("Documento não existe");
                return null;
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo);
            if (origem == EOrigem.Documento)
            {
                path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, Documents);
            }
            string dir = Directory.GetFiles(path, Path.GetFileName(arquivoId.ToString()) + ".*").FirstOrDefault();
            if(dir == null)
            {
                return null;
            }
            string fileExtension = dir.Split('.').Last();
            var data = System.IO.File.ReadAllBytes(dir);
            var result = new FileContentResult(data, "applicatiom/octet-stream")
            {
                FileDownloadName = nome + "." + fileExtension
            };
            return result;
        }

        public async Task<int> EncontraArquivoId(int id, EOrigem origem)
        {
            var buscaOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.EventoId == id);
            switch (origem)
            {
                case EOrigem.Documento:
                    buscaOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.DocumentoId == id);
                    break;
                case EOrigem.Evento:
                    buscaOrigem = await _unitOfWork.ArquivosOrigens.FindAsync(x => x.EventoId == id);
                    break;
                default:
                    break;
            }
            if (buscaOrigem == null)
            {
                return 0;
            }
            var buscaArquivo = await _unitOfWork.Arquivos.FindAsync(x => x.Id == buscaOrigem.ArquivoId);
            return buscaArquivo.Id;
        }
    }
}
