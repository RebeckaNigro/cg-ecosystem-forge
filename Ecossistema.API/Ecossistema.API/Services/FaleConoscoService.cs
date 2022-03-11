using Ecossistema.API.Models.DTO;

namespace Ecossistema.API.Services
{
    public class FaleConoscoService : IFaleConoscoService
    {
        public async Task<string> Registrar(FaleConoscoDTO obj)
        {
            //validações


            if (!await GravarMensagem(obj))
                return "Erro ao registrar a mensagem!";

            if (await EnviarEmailFaleConosco(obj))
                return "Sua mensagem foi registrada com sucesso!";
            else
                return "Sua mensagem foi registrada, mas ocorreu um problema no envio do e-mail";

        }

        private Task<bool> GravarMensagem(FaleConoscoDTO obj)
        {
            return Task.FromResult(true);
        }

        private Task<bool> EnviarEmailFaleConosco(FaleConoscoDTO obj)
        {
            return Task.FromResult(false);
        }
    }
}
