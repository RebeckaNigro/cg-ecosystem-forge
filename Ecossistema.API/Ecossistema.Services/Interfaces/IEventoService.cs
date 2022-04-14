using Ecossistema.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IEventoService
    {
        Task<RespostaPadrao> Incluir(EventoDto dado, int usuarioId);
        Task<RespostaPadrao> Editar(EventoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
        Task<RespostaPadrao> ListarUltimas();
        Task<RespostaPadrao> ListarTodas();
        Task<RespostaPadrao> Detalhes(int id);
        Task<RespostaPadrao> ListarTiposEventos();
        Task<RespostaPadrao> ListarEnderecos(int instituicaoId, int tipoEnderecoId);
        Task<RespostaPadrao> ListarTiposEnderecos();
        
    }
}

