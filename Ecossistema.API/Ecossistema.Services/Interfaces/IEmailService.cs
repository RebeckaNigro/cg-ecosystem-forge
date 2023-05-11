using Ecossistema.Services.Dto;

namespace Ecossistema.Services.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmail(Mensagem mensagem);
    }
}
