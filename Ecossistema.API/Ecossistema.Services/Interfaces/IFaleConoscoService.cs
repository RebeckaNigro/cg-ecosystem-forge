using Ecossistema.Services.Dto;

namespace Ecossistema.Services.Interfaces
{
    public interface IFaleConoscoService
    {
        Task<object> ObterFaleConosco();

        Task<object> ObterContatosSetor(int faleConoscoSetorId);
        Task<RespostaPadrao> Registrar(FaleConoscoDTO obj);
    }
}
