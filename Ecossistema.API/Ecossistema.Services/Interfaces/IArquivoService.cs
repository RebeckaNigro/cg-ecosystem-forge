using Ecossistema.Services.Dto;
using Ecossistema.Util.Const;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IArquivoService
    {
        Task<bool> Vincular(EOrigem origem, int id, List<IFormFile>? arquivos, int usuarioId, DateTime dataAtual, RespostaPadrao resposta);
        Task<List<ArquivoDto>> ObterArquivos(EOrigem origem, int id, RespostaPadrao resposta);
    }
}