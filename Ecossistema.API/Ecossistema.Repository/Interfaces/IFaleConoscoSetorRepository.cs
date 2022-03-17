using Ecossistema.Domain.Entities;

namespace Ecossistema.Data.Interfaces
{
    public interface IFaleConoscoSetorRepository
    {
        Task<object> ObterTodosGenerico();
    }
}
