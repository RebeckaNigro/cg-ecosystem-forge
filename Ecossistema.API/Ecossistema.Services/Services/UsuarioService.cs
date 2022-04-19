using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util;
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

        public async Task<RespostaPadrao> DesativarAtivarUsuario([FromBody] AtivarDesativarUserDto model, int usuarioId)
        {
            RespostaPadrao resposta = new RespostaPadrao("Ok");
            try
            {
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.Id == model.Id);
                if (usuario != null)
                {
                    var dataAtual = DateTime.Now;
                    usuario.Ativo = !usuario.Ativo;
                    Recursos.Auditoria(usuario, usuarioId, dataAtual);
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

        public async Task<RespostaPadrao> Editar(UsuarioCriacaoDto dado, int usuarioId)
        {
            RespostaPadrao resposta = new RespostaPadrao("Ok");
            var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.Id == dado.Id);
            if (usuario != null)
            {
                var dataAtual = DateTime.Now;
                usuario.Cargo = dado.Cargo;
                Recursos.Auditoria(usuario, usuarioId, dataAtual);
                _unitOfWork.Usuarios.Update(usuario);
                resposta.Retorno = _unitOfWork.Complete() > 0;
                resposta.SetMensagem("Usuário atualizado com sucesso");
            }
            else
            {
                resposta.SetErroInterno("Usuario inexistente.");
            }


            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodos()
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Usuarios.FindAllAsync(x => x.Ativo
                                                                 && x.Aprovado);

            var result = query.Select(x => new
            {
                x.Id,
                x.Cargo,
                x.InstituicaoId
            })
            .Distinct()
            .OrderBy(x => x.Id)
            .ToList();

            resposta.Retorno = result;

            return resposta;
        }

        public async Task<RespostaPadrao> Detalhes(int id)
        {
            var resposta = new RespostaPadrao();

            var query = await _unitOfWork.Usuarios.FindAllAsync(x => x.Id == id && x.Aprovado);

            var result = query.Select(x => new
            {
                x.Id,
                x.Cargo,
                x.Aprovado,
                x.InstituicaoId
            })
            .Distinct();

            if (result.Any()) resposta.Retorno = result.FirstOrDefault();
            else resposta.SetNaoEncontrado("Nenhum registro encontrado!");

            return resposta;
        }


        public Task<RespostaPadrao> Excluir(int id)
        {
            throw new NotImplementedException();
        }

    }
}
