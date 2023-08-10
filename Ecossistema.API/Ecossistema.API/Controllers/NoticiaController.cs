using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaService _noticiaService;
        private readonly IArquivoService _arquivoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public NoticiaController(INoticiaService noticiaService, IArquivoService arquivoService)
        {
            _noticiaService = noticiaService;
            _arquivoService = arquivoService;
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromForm] NoticiaDto obj, IFormFile? arquivo)
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _noticiaService.Incluir(obj, arquivo, idLogin);
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromForm] NoticiaDto obj, IFormFile? arquivo)
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _noticiaService.Editar(obj, arquivo, idLogin);
        }
        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _noticiaService.Excluir(id, idLogin);
        }

        [Authorize(Roles = "UsuarioComum")]
        [HttpGet("listarUltimasPorUsuarioId")]
        public async Task<RespostaPadrao> ListarUltimasPorUsuarioId()
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _noticiaService.ListarUltimasPorUsuarioId(idLogin);
        }

        [HttpGet("listarUltimas")]
        public async Task<RespostaPadrao> ListarUltimas()
        {
            return await _noticiaService.ListarUltimas();
        }

        [HttpGet("listarTodas")]
        public async Task<RespostaPadrao> ListarTodas(int paginacao, int? autorId)
        {
            return await _noticiaService.ListarTodas(paginacao, autorId);
        }

        [Authorize(Roles = "UsuarioComum")]
        [HttpGet("listarPorUsuarioId")]
        public async Task<RespostaPadrao> ListarPorUsuarioId(int paginacao)
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _noticiaService.ListarPorUsuarioId(idLogin, paginacao);
        }

        [HttpGet("detalhes")]
        public async Task<RespostaPadrao> Detalhes(int id)
        {
            return await _noticiaService.Detalhes(id);
        }
    }
}