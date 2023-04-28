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
        Task<RespostaPadrao> Incluir(EventoArquivosDto dado, string token);
        Task<RespostaPadrao> Editar(EventoArquivosDto dado, string token);
        Task<RespostaPadrao> Excluir(int id, string idLogin);
        //Task<RespostaPadrao> ListarUltimas();
        Task<RespostaPadrao> ListarEventos(string listagem, string id);
        Task<RespostaPadrao> Detalhes(int id);
        Task<RespostaPadrao> ListarTiposEventos();
        Task<RespostaPadrao> ListarEnderecos(int instituicaoId, int tipoEnderecoId);
        Task<RespostaPadrao> ListarTiposEnderecos();
        
    }
}

