using Ecossistema.API.Models.DTO;
using Ecossistema.API.Util;

namespace Ecossistema.API.Services
{
    public class FaleConoscoService : IFaleConoscoService
    {
        public async Task<string> Registrar(FaleConoscoDTO obj)
        {
            //validações
            Validar(obj);


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

        private bool Validar(FaleConoscoDTO obj)
        {
            if (obj == null) return false;
            if(!ValidarNome(obj)) return false;
            if(!ValidarEmailCorporativo(obj)) return false;
            if(!ValidarTamanhoEmailCorporativo(obj)) return false;
            if(!ValidarEmailCorporativoValido(obj)) return false;
            if(!ValidarNome(obj)) return false;
            if(!ValidarNome(obj)) return false;

            return true;
        }

        private bool ValidarNome(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarString(obj.Nome))
            {
                return false;
            }
            return true;
        }

        private bool ValidarEmailCorporativo(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarString(obj.EmailCorporativo))
            {
                return false;
            }
            return true;
        }

        private bool ValidarTamanhoEmailCorporativo(FaleConoscoDTO obj)
        {
            var tamanhoCampo = 250;
            if (!ValidacaoUtil.ValidarTamanhoString(obj.EmailCorporativo, tamanhoCampo))
            {
                return false;
            }
            return true;
        }

        private bool ValidarEmailCorporativoValido(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidaEMail(obj.EmailCorporativo))
            {
                return false;
            }
            return true;
        }

        private bool ValidarTelefone(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarString(obj.Telefone))
            {
                return false;
            }
            return true;
        }

        private bool ValidarTamanhoTelefone(FaleConoscoDTO obj)
        {
            var tamanhoCampo = 15;
            if (!ValidacaoUtil.ValidarTamanhoString(obj.Telefone, tamanhoCampo))
            {
                return false;
            }
            return true;
        }

    }
}
