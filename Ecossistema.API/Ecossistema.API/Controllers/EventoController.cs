using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly IArquivoService _arquivoService;
        private readonly IAutenticacaoService _autenticacaoService;


        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        private int InstituicaoId
        {
            get
            {
                return 1;
            }
        }

        public EventoController(IEventoService eventoService, IArquivoService arquivoService, IAutenticacaoService autenticacaoService)
        {
            _eventoService = eventoService;
            _arquivoService = arquivoService;
            _autenticacaoService = autenticacaoService;
        }

        [Authorize(Roles = UserRolesDto.AdminParceiro)]
        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromForm] EventoArquivosDto obj)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return await _eventoService.Incluir(obj, accessToken);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromForm] EventoArquivosDto obj)
        {
            return await _eventoService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            await _arquivoService.ExcluirArquivo(id, "evento");
            return await _eventoService.Excluir(id);
        }

        [HttpGet("listarUltimas")]
        public async Task<RespostaPadrao> ListarUltimas()
        {
            var listagem = 2;
            return await _eventoService.ListarEventos(listagem);
        }

        [HttpGet("listarTodas")]
        public async Task<RespostaPadrao> ListarTodas()
        {
            var listagem = 1;
            return await _eventoService.ListarEventos(listagem);
        }

        [HttpGet("detalhes")]
        public async Task<RespostaPadrao> Detalhes(int id)
        {
            return await _eventoService.Detalhes(id);
        }

        [HttpGet("listarTiposEventos")]
        public async Task<RespostaPadrao> ListarTiposEventos()
        {
            return await _eventoService.ListarTiposEventos();
        }

        [HttpGet("listarEnderecos")]
        public async Task<RespostaPadrao> ListarEnderecos(int tipoEnderecoId)
        {
            return await _eventoService.ListarEnderecos(InstituicaoId, tipoEnderecoId);
        }

        [HttpGet("listarTiposEnderecos")]
        public async Task<RespostaPadrao> ListarTiposEnderecos()
        {
            return await _eventoService.ListarTiposEnderecos();
        }
    }
}