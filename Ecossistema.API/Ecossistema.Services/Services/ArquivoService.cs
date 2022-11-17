using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util.Const;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string RepositorioArquivo = "RepositoryFiles";

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

                    if (_unitOfWork.Complete() > 0 && SalvarArquivo(item.Id, arquivo)) ids.Add(item.Id);
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

        public async Task<List<ArquivoDto>> ObterArquivos(EOrigem origem, int id, RespostaPadrao resposta)
        {
            int? instituicaoId = null;
            int? noticiaId = null;
            int? eventoId = null;
            int? documentoId = null;
            int? paginaId = null;

            switch (origem)
            {
                case EOrigem.Parceiro:
                    instituicaoId = id;
                    break;
                case EOrigem.Documento:
                    documentoId = id;
                    break;
                case EOrigem.Noticia:
                    noticiaId = id;
                    break;
                case EOrigem.Evento:
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
                Id = x.Id,
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
                item.Arquivo = ObterArquivoBinario(item.Id);
            }

            return result;
        }

        private bool SalvarArquivo(int id, IFormFile file)
        {
            try
            {
                if (file == null) return false;
                var mimeType = file.ContentType;
                var fileExtension = mimeType.Split('/').Last();
                using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, id.ToString() + "." + fileExtension), FileMode.Create))
                {
                    file.CopyTo(fs);
                    fs.Flush();
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

        private byte[] ObterArquivoBinario(int id)
        {
            byte[] arquivoBin = null;

            //var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, Path.GetFileName(id.ToString()));
            var path = Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo);
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
    }
}
