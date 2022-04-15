using Ecossistema.Services.Dto;
using Ecossistema.Util.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IAprovacaoService
    {
        Task<RespostaPadrao> Incluir(AprovacaoDto dado, int usuarioId);
        Task<RespostaPadrao> Editar(AprovacaoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
        Task<bool> Vincular(EOrigem origem, int id, RespostaPadrao resposta);
    }
}