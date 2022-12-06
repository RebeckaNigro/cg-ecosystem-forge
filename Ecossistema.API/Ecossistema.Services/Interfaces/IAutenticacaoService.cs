using Ecossistema.Services.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<RespostaPadrao> Login(LoginDto dado);
        Task<RespostaPadrao> Logout();
        Task<RespostaPadrao> RegistrarAdmin(ResgistrarLoginDto dado);
        Task<RespostaPadrao> GerarCodigoRedefinirSenha(string dado);
        Task<RespostaPadrao> RedefinirSenha(RedefinirSenhaDto dado);
        //Task<IAsyncResult> Editar(DocumentoDto dado, int usuarioId);
        //Task<IAsyncResult> Excluir(int id);
    }
}
