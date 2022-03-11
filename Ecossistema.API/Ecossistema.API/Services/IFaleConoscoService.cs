using Ecossistema.API.Models.DTO;

namespace Ecossistema.API.Services
{
    public interface IFaleConoscoService
    {
        Task<string> Registrar(FaleConoscoDTO obj);
    }
}
