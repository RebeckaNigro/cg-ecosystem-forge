using Ecossistema.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IUsuarioService
    {
        //Task<RespostaPadrao> Cadastrar(UsuarioCriacaoDto dado, int usuarioId, string loginId);
        Task<RespostaPadrao> Cadastrar(string cargo, int usuarioId, string loginId);
        Task<RespostaPadrao> Editar(UsuarioCriacaoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
    }
}
