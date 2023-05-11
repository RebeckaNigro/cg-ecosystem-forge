using Ecossistema.Services.Dto;

namespace Ecossistema.Services.Interfaces
{
    public interface IFaleConoscoSetorService
    {
        Task<RespostaPadrao> ObterTodosFaleConoscoSetor();
    }
}
