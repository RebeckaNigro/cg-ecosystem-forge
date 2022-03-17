using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class FaleConoscoService : IFaleConoscoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public FaleConoscoService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<object> ObterFaleConosco()
        {
            var query = await _unitOfWork.FaleConoscos.FindAllAsync(x => true, new[] { "FaleConoscoSetor" });

            var result = query.Select(x => new
            {
                id = x.Id,
                nome = x.Nome,
                email = x.Email,
                telefone = x.Telefone,
                empresa = x.Empresa,
                cargo = x.Cargo,
                faleconoscoSetorId = x.FaleConoscoSetorId,
                faleConoscoSetor = x.FaleConoscoSetor.Descricao,
                descricao = x.Descricao
            });

            return result;
        }

        public async Task<object> ObterContatosSetor(int faleConoscoSetorId)
        {
            var query = await _unitOfWork.FaleConoscoSetores.FindAllAsync(x => x.Id == faleConoscoSetorId, new[] { "FaleConoscoSetoresContatos" });

            var result = query.Select(x => new
            {
                id = x.Id,
                descricao = x.Descricao,
                contatos = x.FaleConoscoSetoresContatos.Select(y => new
                {
                    id = y.Id,
                    nome = y.Nome,
                    email = y.Email
                }).Distinct()                
            });

            return result;
        }

        //public async Task<bool> Atualizar(FaleConosco obj)
        //{
        //    var item = _unitOfWork.FaleConoscos.GetById(obj.Id);

        //    item.Nome = obj.Nome;

        //    _unitOfWork.FaleConoscos.Update(item);

        //    _unitOfWork.Complete();
        //}

        public async Task<string> Registrar(FaleConoscoDTO obj)
        {
            //validações
            if (!Validar(obj)) return "Erro de validação";

            try
            {
                if (!await GravarMensagem(obj))
                    return "Erro ao registrar a mensagem!";

                await EnviarEmailFaleConoscoSolicitado(obj);

                if (await EnviarEmailFaleConoscoSolicitante(obj))
                    return "Sua mensagem foi registrada com sucesso!";
                else
                    return "Sua mensagem foi registrada, mas ocorreu um problema no envio do e-mail";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private Task<bool> GravarMensagem(FaleConoscoDTO obj)
        {
            var faleConosco = new FaleConosco
            {
                Nome = obj.Nome,
                Email = obj.EmailCorporativo,
                Telefone = obj.Telefone,
                Empresa = obj.Empresa,
                Cargo = obj.Cargo,
                FaleConoscoSetorId = (int)obj.Setor,
                Descricao = obj.Mensagem,
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioCriacaoId = 1,
                NaturezaOperacao = "I",
                DataOperacao = DateTime.Now,
                UsuarioOperacaoId = 1
            };

            _unitOfWork.FaleConoscos.AddAsync(faleConosco);
            _unitOfWork.Complete();

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
            if (!ValidarNome(obj)) return false;
            if (!ValidarEmailCorporativo(obj)) return false;
            if (!ValidarTelefone(obj)) return false;
            if (!ValidarEmpresa(obj)) return false;
            if (!ValidarCargo(obj)) return false;
            if (!ValidarSetor(obj)) return false;
            if (!ValidarMensagem(obj)) return false;

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
            if (!ValidarStringEmailCorporativo(obj)) return false;
            if (!ValidarTamanhoEmailCorporativo(obj)) return false;
            if (!ValidarEmailCorporativoValido(obj)) return false;

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
