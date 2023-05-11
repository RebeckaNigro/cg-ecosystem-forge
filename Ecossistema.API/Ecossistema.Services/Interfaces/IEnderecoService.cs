using Ecossistema.Services.Dto;

namespace Ecossistema.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<RespostaPadrao> Incluir(EnderecoDto dado, int usuarioId);
        Task<RespostaPadrao> Editar(EnderecoDto dado, int usuarioId);
        Task<RespostaPadrao> Excluir(int id);
        Task<int> Vincular(EnderecoDto dado, DateTime dataAtual, int usuarioId, RespostaPadrao resposta);
    }
}

