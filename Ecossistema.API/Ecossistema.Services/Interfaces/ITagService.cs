using Ecossistema.Services.Dto;

namespace Ecossistema.Services.Interfaces
{
    public interface ITagService
    {
        Task<RespostaPadrao> CadastrarTag(TagDto tag, int usuarioId);
        Task<RespostaPadrao> ListarTodas();
    }
}
