using Ecossistema.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IInstituicaoService
    {
        Task<RespostaPadrao> Incluir(InstituicaoDto dado, string idLogin);
        Task<RespostaPadrao> Editar(InstituicaoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
        Task<RespostaPadrao> VincularEndereco(EnderecoDto dado, int instituicaoId, int usuarioId);
        Task<RespostaPadrao> BuscarInstituicoes();
        Task<RespostaPadrao> BuscarParceirosPorArea(int idArea);
    }
}
