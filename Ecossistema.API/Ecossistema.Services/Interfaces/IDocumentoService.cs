using Ecossistema.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IDocumentoService
    {
        Task<RespostaPadrao> Incluir(DocumentoDto dado, int usuarioId);
        Task<RespostaPadrao> Editar(DocumentoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
        Task<RespostaPadrao> ListarUltimas();
        Task<RespostaPadrao> ListarTodas();
        Task<RespostaPadrao> Detalhes(int id);
    }
}
