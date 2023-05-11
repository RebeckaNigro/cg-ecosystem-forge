

using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<RespostaPadrao> Login([FromBody] LoginDto model)
        {
            return await _autenticacaoService.Login(model);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<RespostaPadrao> Logout()
        {
            return await _autenticacaoService.Logout();
        }

        [HttpPost]
        [Route("registrar-admin")]
        public async Task<RespostaPadrao> RegistrarAdmin([FromBody] ResgistrarLoginDto model)
        {
            return await _autenticacaoService.RegistrarAdmin(model);
        }

        [HttpPost]
        [Route("registrar-usuario-comum")]
        public async Task<RespostaPadrao> RegistrarUsuarioComum([FromBody] ResgistrarUsuarioComumDto model)
        {
            model.Role = 4;
            return await _autenticacaoService.RegistrarUsuarioComum(model);
        }

        [HttpPost]
        [Route("gerarCodigoRefinirSenha")]
        public async Task<RespostaPadrao> GerarCodigoRedefinirSenha([FromBody] string email)
        {
            return await _autenticacaoService.GerarCodigoRedefinirSenha(email);
        }

        [HttpPost]
        [Route("refinir-senha")]
        public async Task<RespostaPadrao> RedefinirSenha([FromBody] RedefinirSenhaDto model)
        {
            return await _autenticacaoService.RedefinirSenha(model);    
        }

    }
}
