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

        public async Task<bool> Vincular(EOrigem origem, int id, List<IFormFile> arquivos, int usuarioId, DateTime dataAtual, RespostaPadrao resposta)
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

        private bool SalvarArquivo(int id, IFormFile file)
        {
            try
            {
                if (file == null) return false;
                using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, RepositorioArquivo, id.ToString()+".ecs"), FileMode.Create))
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
    }
}
