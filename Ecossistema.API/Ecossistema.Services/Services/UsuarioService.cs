using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> DesativarAtivarUsuario([FromBody] AtivarDesativarUserDto model)
        {
            RespostaPadrao resposta = new RespostaPadrao("Ok");
            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.Id == model.Id);
                if (usuario != null)
                {
                    usuario.Ativo = !usuario.Ativo;
                    _unitOfWork.Usuarios.Update(usuario);
                    resposta.Retorno = _unitOfWork.Complete() > 0;
                    if (usuario.Ativo == false)
                    {
                        resposta.SetMensagem("Usuário desativado com sucesso!");
                    }
                    else
                    {
                        resposta.SetMensagem("Usuário ativado com sucesso!");
                    }
                }
                else
                {
                    resposta.SetErroInterno("Usuario inexistente.");
                }


                return resposta;

            }
            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }
        }

        public Task<RespostaPadrao> Editar(UsuarioCriacaoDto dado, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<RespostaPadrao> Excluir(int id)
        {
            throw new NotImplementedException();
        }

    }
}
