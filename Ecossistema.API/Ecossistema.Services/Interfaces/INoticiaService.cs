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
        Task<RespostaPadrao> Incluir(NoticiaDto dado, IFormFile arquivo, string idLogin);
        Task<RespostaPadrao> Editar(NoticiaDto dado, IFormFile arquivo, string idLogin);
        Task<RespostaPadrao> Excluir(int id, string idLogin);
        Task<RespostaPadrao> ListarUltimasPorUsuarioId(string idLogin);
        Task<RespostaPadrao> ListarUltimas();
        Task<RespostaPadrao> ListarTodas(int paginacao, int? autorId);
        Task<RespostaPadrao> ListarPorUsuarioId(string idLogin, int paginacao);
        Task<RespostaPadrao> Detalhes(int id);
        Task<RespostaPadrao> ListarAutores();
    }
}
