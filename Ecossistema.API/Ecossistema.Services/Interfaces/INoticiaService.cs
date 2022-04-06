using Ecossistema.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface INoticiaService
    {
        Task<RespostaPadrao> Incluir(NoticiaDto dado, int usuarioId);
        Task<RespostaPadrao> Editar(NoticiaDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
    }
}
