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

        public async Task<RespostaPadrao> Registrar(FaleConoscoDTO obj)
        {
            RespostaPadrao resposta = new RespostaPadrao();
            //validações
            if (!await Validar(obj, resposta)) return resposta;

            try
            {
                if (!await GravarMensagem(obj))
                {
                    resposta.SetErroInterno("Erro ao gravar a solicitação.");
                    return resposta;
                }

                await EnviarEmailFaleConoscoSolicitado(obj);

                if (await EnviarEmailFaleConoscoSolicitante(obj))
                {
                    resposta.SetMensagem("Sua mensagem foi registrada com sucesso!");
                    return resposta;
                }
                else
                {
                    resposta.SetMensagem("Sua mensagem foi registrada, mas ocorreu um problema no envio do e-mail");
                    return resposta;
                }
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
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
                FaleConoscoSetorId = (int)obj.SetorId,
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
                var mensagem = new Mensagem(new List<string> { "startupsuporte@sesims.com.br" });
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

        private async Task<bool> Validar(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidarNome(obj, resposta)) return false;
            if (!ValidarEmailCorporativo(obj, resposta)) return false;
            if (!ValidarTelefone(obj, resposta)) return false;
            if (!ValidarEmpresa(obj, resposta)) return false;
            if (!ValidarCargo(obj, resposta)) return false;
            if (!await ValidarSetor(obj, resposta)) return false;
            if (!ValidarMensagem(obj, resposta)) return false;

            return true;
        }

        private bool ValidarNome(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(obj.Nome))
            {
                resposta.SetCampoVazio("nome");
                return false;
            }
            return true;
        }

        private bool ValidarEmailCorporativo(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidarStringEmailCorporativo(obj, resposta)) return false;
            if (!ValidarTamanhoEmailCorporativo(obj, resposta)) return false;
            if (!ValidarEmailCorporativoValido(obj, resposta)) return false;

            return true;
        }

        #region Validações Internas Email
        private bool ValidarStringEmailCorporativo(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(obj.EmailCorporativo))
            {
                resposta.SetCampoVazio("emailCorporativo");
                return false;
            }
            return true;
        }

        private bool ValidarTamanhoEmailCorporativo(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            var tamanhoCampo = 250;
            if (!ValidacaoUtil.ValidarTamanhoString(obj.EmailCorporativo, tamanhoCampo))
            {
                resposta.SetCampoInvalido("emailCorporativo", "O tamanho do e-mail não pode ser mais de 250 caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarEmailCorporativoValido(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidaEMail(obj.EmailCorporativo))
            {
                resposta.SetCampoInvalido("emailCorporativo", "O e-mail não está em um formato válido.");
                return false;
            }
            return true;
        }
        #endregion

        private bool ValidarTelefone(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidarStringTelefone(obj, resposta)) return false;
            if (!ValidarTamanhoTelefone(obj, resposta)) return false;

            return true;
        }

        #region Validações Internas Telefone
        private bool ValidarStringTelefone(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(obj.Telefone))
            {
                resposta.SetCampoVazio("telefone");
                return false;
            }
            return true;
        }

        private bool ValidarTamanhoTelefone(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            var tamanhoCampo = 12;
            if (!ValidacaoUtil.ValidarTamanhoString(obj.Telefone, tamanhoCampo))
            {
                resposta.SetCampoInvalido("telefone", "O número de telefone não pode conter mais do que 12 caracteres");
                return false;
            }
            return true;
        }
        #endregion

        private bool ValidarEmpresa(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(obj.Empresa))
            {
                resposta.SetCampoVazio("empresa");
                return false;
            }
            return true;
        }

        private bool ValidarCargo(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(obj.Cargo))
            {
                resposta.SetCampoVazio("cargo");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarSetor(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidarSetorInteiroValido(obj, resposta)) return false;
            if (! await ValidarSetorExistente(obj, resposta)) return false;

            return true;
        }

        #region Validações Internas Setor
        private bool ValidarSetorInteiroValido(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(obj.SetorId))
            {
                resposta.SetCampoInvalido("setor", "O setor não é válido.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarSetorExistente(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            var setor = await _unitOfWork.FaleConoscoSetores.GetByIdAsync((int)obj.SetorId);
            if (setor == null)
            {
                resposta.SetNaoEncontrado("O setor selecionado não existe.");
                return false;
            }
            return true;
        }
        #endregion

        private bool ValidarMensagem(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidarStringMensagem(obj, resposta)) return false;
            if (!ValidarTamanhoMensagem(obj, resposta)) return false;

            return true;
        }

        #region Validações Internas Mensagem
        private bool ValidarStringMensagem(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(obj.Mensagem))
            {
                resposta.SetCampoVazio("mensagem");
                return false;
            }
            return true;
        }

        private bool ValidarTamanhoMensagem(FaleConoscoDTO obj, RespostaPadrao resposta)
        {
            var tamanhoCampo = 800;
            if (!ValidacaoUtil.ValidarTamanhoString(obj.Mensagem, tamanhoCampo))
            {
                resposta.SetCampoInvalido("mensagem", "A mensagem não pode ser maior do que 800 caracteres.");
                return false;
            }
            return true;
        }
        #endregion

        #endregion
    }
}
