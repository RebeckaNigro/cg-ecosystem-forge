using Ecossistema.API.Models.DTO;

namespace Ecossistema.API.Services
{
    public interface IEmailService
    {
        Task EnviarEmail(Mensagem mensagem);
    }
}
