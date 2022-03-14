using Ecossistema.API.Models.DTO;
using Ecossistema.API.Util;

namespace Ecossistema.API.Services
{
    public class FaleConoscoService : IFaleConoscoService
    {
        private readonly IEmailService _emailService;
        public FaleConoscoService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<string> Registrar(FaleConoscoDTO obj)
        {
            //validações
            if (!Validar(obj)) return "Erro de validação";

            if (!await GravarMensagem(obj))
                return "Erro ao registrar a mensagem!";

            await EnviarEmailFaleConoscoSolicitado(obj);

            if (await EnviarEmailFaleConoscoSolicitante(obj))
                return "Sua mensagem foi registrada com sucesso!";
            else
                return "Sua mensagem foi registrada, mas ocorreu um problema no envio do e-mail";

        }

        private Task<bool> GravarMensagem(FaleConoscoDTO obj)
        {
            return Task.FromResult(true);
        }

        private async Task<bool> EnviarEmailFaleConoscoSolicitante(FaleConoscoDTO obj)
        {
            try
            {
                var mensagem = new Mensagem(new List<string> { obj.EmailCorporativo });
                mensagem.SetFaleConoscoSolicitante(obj, 123);
                await _emailService.EnviarEmail(mensagem);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> EnviarEmailFaleConoscoSolicitado(FaleConoscoDTO obj)
        {
            try
            {
                var mensagem = new Mensagem(new List<string> { "victor.gimenez@sesims.com.br" });
                mensagem.SetFaleConoscoSolicitado(obj, 123);
                await _emailService.EnviarEmail(mensagem);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        #region Validações

        private bool Validar(FaleConoscoDTO obj)
        {
            if(!ValidarNome(obj)) return false;
            if(!ValidarEmailCorporativo(obj)) return false;
            if(!ValidarTelefone(obj)) return false;
            if(!ValidarEmpresa(obj)) return false;
            if(!ValidarCargo(obj)) return false;
            if(!ValidarSetor(obj)) return false;
            if(!ValidarMensagem(obj)) return false;

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
            if(!ValidarStringEmailCorporativo(obj)) return false;
            if(!ValidarTamanhoEmailCorporativo(obj)) return false;
            if(!ValidarEmailCorporativoValido(obj)) return false;

            return true;
        }

        #region Validações Internas Email
        private bool ValidarStringEmailCorporativo(FaleConoscoDTO obj)
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
        #endregion

        private bool ValidarTelefone(FaleConoscoDTO obj)
        {
            if (!ValidarStringTelefone(obj)) return false;
            if (!ValidarTamanhoTelefone(obj)) return false;

            return true;
        }

        #region Validações Internas Telefone
        private bool ValidarStringTelefone(FaleConoscoDTO obj)
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
        #endregion

        private bool ValidarEmpresa(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarString(obj.Empresa))
            {
                return false;
            }
            return true;
        }

        private bool ValidarCargo(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarString(obj.Cargo))
            {
                return false;
            }
            return true;
        }

        private bool ValidarSetor(FaleConoscoDTO obj)
        {
            if (!ValidarSetorInteiroValido(obj)) return false;
            if (!ValidarSetorExistente(obj)) return false;

            return true;
        }

        #region Validações Internas Setor
        private bool ValidarSetorInteiroValido(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(obj.Setor))
            {
                return false;
            }
            return true;
        }

        private bool ValidarSetorExistente(FaleConoscoDTO obj)
        {
            //TODO colocar validação no banco e os awaits
            if (false)
            {
                return false;
            }
            return true;
        }
        #endregion

        private bool ValidarMensagem(FaleConoscoDTO obj)
        {
            if (!ValidarStringMensagem(obj)) return false;
            if (!ValidarTamanhoMensagem(obj)) return false;

            return true;
        }

        #region Validações Internas Mensagem
        private bool ValidarStringMensagem(FaleConoscoDTO obj)
        {
            if (!ValidacaoUtil.ValidarString(obj.Mensagem))
            {
                return false;
            }
            return true;
        }

        private bool ValidarTamanhoMensagem(FaleConoscoDTO obj)
        {
            var tamanhoCampo = 800;
            if (!ValidacaoUtil.ValidarTamanhoString(obj.Mensagem, tamanhoCampo))
            {
                return false;
            }
            return true;
        }
        #endregion

        #endregion

    }
}
