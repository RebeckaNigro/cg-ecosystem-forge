using Ecossistema.Services.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IDocumentoService
    {
        Task<RespostaPadrao> Incluir(DocumentoDto dado, IFormFile arquivo, string usuarioId);
        Task<RespostaPadrao> Editar(DocumentoDto dado, IFormFile arquivo, string usuarioId);
        Task<RespostaPadrao> Excluir(int id, string idLogin);
        Task<RespostaPadrao> ListarUltimos();
        Task<RespostaPadrao> ListarUltimosPorUsuarioId(string idLogin);
        Task<RespostaPadrao> ListarPorUsuarioId(string idLogin);
        Task<RespostaPadrao> ListarTodos();
        Task<RespostaPadrao> Detalhes(int id);
        //Task<RespostaPadrao> ListarTiposDocumentos();
        Task<RespostaPadrao> ListarDocumentosAreas();
    }
}