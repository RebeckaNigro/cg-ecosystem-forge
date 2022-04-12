using Ecossistema.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    internal interface IUsuarioService
    {
        Task<RespostaPadrao> Incluir(UsuarioCriacaoDto dado, int usuarioId);
        Task<RespostaPadrao> Editar(UsuarioCriacaoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
    }
}
