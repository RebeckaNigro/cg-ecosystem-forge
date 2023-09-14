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

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromForm] EventoArquivosDto obj)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return await _eventoService.Incluir(obj, accessToken);
        }
        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromForm] EventoArquivosDto obj)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return await _eventoService.Editar(obj, accessToken);
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _eventoService.Excluir(id, idLogin);
        }

        
        [HttpGet("listarUltimos")]
        public async Task<RespostaPadrao> ListarUltimos()
        {
            var listagem = "ultimos";
            return await _eventoService.ListarEventos(listagem, null, 0, null, 0);
        }

        [HttpGet("listarUltimosPorUsuarioId")]
        public async Task<RespostaPadrao> ListarUltimosPorUsuarioId()
        {
            var listagem = "ultimosPorUsuarioId";
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _eventoService.ListarEventos(listagem, idLogin, 0, null, 0);
        }

        //[Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpGet("listarTodos")]
        public async Task<RespostaPadrao> ListarTodos(int paginacao, string? organizador)
        {
            var listagem = "todos";
            return await _eventoService.ListarEventos(listagem, null, paginacao, organizador, 0);
        }

        [Authorize(Roles = "UsuarioComum")]
        [HttpGet("listarPorUsuarioId")]
        public async Task<RespostaPadrao> ListarPorUsuarioId(int paginacao)
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            var listagem = "id";
            return await _eventoService.ListarEventos(listagem, idLogin, paginacao, null, 0);
        }

        [HttpGet("listarEventosPorArea")]
        public async Task<RespostaPadrao> ListarPorArea(int areaEventoId)
        {
            var listagem = "area";
            var paginacao = 0;
            return await _eventoService.ListarEventos(listagem, null, paginacao, null, areaEventoId);
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

        [HttpGet("listarAreasEventos")]
        public async Task<RespostaPadrao> ListarAreasEventos()
        {
            return await _eventoService.ListarAreasEventos();
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

        [HttpGet("listarOrganizadores")]
        public async Task<RespostaPadrao> ListarOrganizadores()
        {
            return await _eventoService.ListarOrganizadores();
        }
    }
}