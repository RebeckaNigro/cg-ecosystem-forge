using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
using Ecossistema.Util.Const;
using Ecossistema.Util.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnderecoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> Incluir(EnderecoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarIncluir(dado, resposta)) return resposta;

            try
            {
                var obj = new Endereco(dado.Cep,
                                        dado.Logradouro,
                                        dado.Numero,
                                        dado.Complemento,
                                        dado.PontoReferencia,
                                        dado.Bairro,
                                        dado.Cidade,
                                        dado.Uf,
                                        usuarioId,
                                        DateTime.Now);

                await _unitOfWork.Enderecos.AddAsync(obj);

                resposta.Retorno = _unitOfWork.Complete() > 0;

                resposta.SetMensagem("Dados gravados com sucesso!");
                resposta.Resposta = obj.Id.ToString();
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Editar(EnderecoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarEditar(dado, resposta)) return resposta;

            try
            {
                var dataAtual = DateTime.Now;

                var objAlt = await _unitOfWork.Enderecos.FindAsync(x => x.Id == dado.Id);

                if (objAlt != null)
                {
                    objAlt.Cep = dado.Cep;
                    objAlt.Logradouro = dado.Logradouro;
                    objAlt.Numero = dado.Numero;
                    objAlt.Complemento = dado.Complemento;
                    objAlt.PontoReferencia = dado.PontoReferencia;
                    objAlt.Bairro = dado.Bairro;
                    objAlt.Cidade = dado.Cidade;
                    objAlt.Uf = dado.Uf;
                    Recursos.Auditoria(objAlt, usuarioId, dataAtual);

                    _unitOfWork.Enderecos.Update(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;

                    resposta.SetMensagem("Dados gravados com sucesso!");
                }
                else resposta.SetErroInterno();
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<RespostaPadrao> Excluir(int id)
        {
            var resposta = new RespostaPadrao();

            if (!await ValidarExcluir(id, resposta)) return resposta;

            try
            {
                var objAlt = await _unitOfWork.Enderecos.FindAsync(x => x.Id == id);

                if (objAlt != null)
                {
                    _unitOfWork.Enderecos.Delete(objAlt);

                    resposta.Retorno = _unitOfWork.Complete() > 0;

                    resposta.SetMensagem("Dados excluídos com sucesso!");
                }
                else resposta.SetErroInterno();
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;
        }

        public async Task<int> Vincular(EnderecoDto dado, DateTime dataAtual, int usuarioId, RespostaPadrao resposta)
        {
            if ((dado.Id == null || dado.Id <= 0))
            {
                if (!await ValidarIncluir(dado, resposta)) return 0;
            } 
            else if (!await ValidarEditar(dado, resposta)) return 0;

            try
            {
                Endereco? obj = null;

                if (dado.Id > 0)
                {
                    obj = await _unitOfWork.Enderecos.FindAsync(x => x.Id == (int)dado.Id);

                    obj.Cep = dado.Cep;
                    obj.Logradouro = dado.Logradouro;
                    obj.Numero = dado.Numero;
                    obj.Complemento = dado.Complemento;
                    obj.PontoReferencia = dado.PontoReferencia;
                    obj.Bairro = dado.Bairro;
                    obj.Cidade = dado.Cidade;
                    obj.Uf = dado.Uf;

                    Recursos.Auditoria(obj, usuarioId, dataAtual);

                    _unitOfWork.Enderecos.Update(obj);
                }
                else
                {
                    obj = new Endereco(dado.Cep,
                                         dado.Logradouro,
                                         dado.Numero,
                                         dado.Complemento,
                                         dado.PontoReferencia,
                                         dado.Bairro,
                                         dado.Cidade,
                                         dado.Uf,
                                         usuarioId,
                                         dataAtual);

                    await _unitOfWork.Enderecos.AddAsync(obj);
                }

                if (_unitOfWork.Complete() > 0) return obj.Id;
            }
            catch (Exception ex)
            {
                resposta.Retorno = false;
                resposta.SetErroInterno(ex.Message);
            }

            return 0;
        }

        #region Validações

        private async Task<bool> ValidarIncluir(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarCepInformado(dado, resposta)
                || !ValidarCepTamanho(dado, resposta)
                || !ValidarLogradouroInformada(dado, resposta)
                || !ValidarLogradouroTamanho(dado, resposta)
                || !ValidarNumeroInformada(dado, resposta)
                || !ValidarNumeroTamanho(dado, resposta)
                //|| !ValidarComplementoInformada(dado, resposta)
                || (dado.Complemento != null && !ValidarComplementoTamanho(dado, resposta))
                //|| !ValidarPontoReferenciaInformada(dado, resposta)
                || (dado.PontoReferencia != null && !ValidarPontoReferenciaTamanho(dado, resposta))
                || !ValidarBairroInformada(dado, resposta)
                || !ValidarBairroTamanho(dado, resposta)
                || !ValidarCidadeInformada(dado, resposta)
                || !ValidarCidadeTamanho(dado, resposta)
                || !ValidarUfInformada(dado, resposta)
                || !ValidarUfTamanho(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarEditar(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(dado, resposta)
                || !ValidarIdValido(dado, resposta)
                || !await ValidarIdCadastrado(dado, resposta)
                || !ValidarCepInformado(dado, resposta)
                || !ValidarCepTamanho(dado, resposta)
                || !ValidarLogradouroInformada(dado, resposta)
                || !ValidarLogradouroTamanho(dado, resposta)
                || !ValidarNumeroInformada(dado, resposta)
                || !ValidarNumeroTamanho(dado, resposta)
                //|| !ValidarComplementoInformada(dado, resposta)
                || (dado.Complemento != null && !ValidarComplementoTamanho(dado, resposta))
                //|| !ValidarPontoReferenciaInformada(dado, resposta)
                || (dado.PontoReferencia != null && !ValidarPontoReferenciaTamanho(dado, resposta))
                || !ValidarBairroInformada(dado, resposta)
                || !ValidarBairroTamanho(dado, resposta)
                || !ValidarCidadeInformada(dado, resposta)
                || !ValidarCidadeTamanho(dado, resposta)
                || !ValidarUfInformada(dado, resposta)
                || !ValidarUfTamanho(dado, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarExcluir(int id, RespostaPadrao resposta)
        {
            if (!ValidarIdInformado(id, resposta)
                || !ValidarIdValido(id, resposta)
                || !await ValidarIdCadastrado(id, resposta))
            {
                resposta.Retorno = false;
                return false;
            }
            return true;
        }

        private bool ValidarIdInformado(EnderecoDto dado, RespostaPadrao resposta)
        {
            return ValidarIdInformado(dado.Id, resposta);
        }

        private bool ValidarIdInformado(int? id, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiro(id))
            {
                resposta.SetCampoVazio("Id");
                return false;
            }
            return true;
        }

        private bool ValidarIdValido(EnderecoDto dado, RespostaPadrao resposta)
        {
            return ValidarIdValido(dado.Id, resposta);
        }

        private bool ValidarIdValido(int? id, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarInteiroValido(id))
            {
                resposta.SetCampoInvalido("Id");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarIdCadastrado(int? id, RespostaPadrao resposta)
        {
            var query = await _unitOfWork.Enderecos.FindAllAsync(x => x.Id == (int)id
                                                                && x.Ativo);

            if (!query.Any())
            {
                resposta.SetNaoEncontrado("Registro não encontrado.");
                return false;
            }
            return true;
        }

        private async Task<bool> ValidarIdCadastrado(EnderecoDto dado, RespostaPadrao resposta)
        {
            return await ValidarIdCadastrado(dado.Id, resposta);
        }

        private bool ValidarCepInformado(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Cep))
            {
                resposta.SetCampoVazio("Cep");
                return false;
            }
            return true;
        }

        private bool ValidarCepTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Cep, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Cep", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarLogradouroInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Logradouro))
            {
                resposta.SetCampoVazio("Logradouro");
                return false;
            }
            return true;
        }

        private bool ValidarLogradouroTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 8000;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Logradouro, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Logradouro", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarNumeroInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Numero))
            {
                resposta.SetCampoVazio("Numero");
                return false;
            }
            return true;
        }

        private bool ValidarNumeroTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 30;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Numero, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Numero", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarComplementoInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Complemento))
            {
                resposta.SetCampoVazio("Complemento");
                return false;
            }
            return true;
        }

        private bool ValidarComplementoTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Complemento, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Complemento", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarPontoReferenciaInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.PontoReferencia))
            {
                resposta.SetCampoVazio("PontoReferencia");
                return false;
            }
            return true;
        }

        private bool ValidarPontoReferenciaTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 300;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.PontoReferencia, tamanhoCampo))
            {
                resposta.SetCampoInvalido("PontoReferencia", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarBairroInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Bairro))
            {
                resposta.SetCampoVazio("Bairro");
                return false;
            }
            return true;
        }

        private bool ValidarBairroTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Bairro, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Bairro", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarCidadeInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Cidade))
            {
                resposta.SetCampoVazio("Cidade");
                return false;
            }
            return true;
        }

        private bool ValidarCidadeTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 100;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Cidade, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Cidade", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        private bool ValidarUfInformada(EnderecoDto dado, RespostaPadrao resposta)
        {
            if (!ValidacaoUtil.ValidarString(dado.Uf))
            {
                resposta.SetCampoVazio("Uf");
                return false;
            }
            return true;
        }

        private bool ValidarUfTamanho(EnderecoDto dado, RespostaPadrao resposta)
        {
            var tamanhoCampo = 2;
            if (!ValidacaoUtil.ValidarTamanhoString(dado.Uf, tamanhoCampo))
            {
                resposta.SetCampoInvalido("Uf", "O campo não pode conter mais que " + tamanhoCampo.ToString() + " caracteres.");
                return false;
            }
            return true;
        }

        #endregion
    }
}
