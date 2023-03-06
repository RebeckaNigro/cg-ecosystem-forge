using Ecossistema.Services.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface INoticiaService
    {
        Task<RespostaPadrao> Incluir(NoticiaDto dado, IFormFile arquivo, int usuarioId);
        Task<RespostaPadrao> Editar(NoticiaDto dado, IFormFile arquivo, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
        Task<RespostaPadrao> ListarUltimas();
        Task<RespostaPadrao> ListarTodas();
        Task<RespostaPadrao> ListarPorUsuarioId(string idLogin);
        Task<RespostaPadrao> Detalhes(int id);
    }
}
