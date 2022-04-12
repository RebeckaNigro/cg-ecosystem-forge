using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaService _noticiaService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public NoticiaController(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromBody] NoticiaDto obj)
        {
            return await _noticiaService.Incluir(obj, UsuarioId);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] NoticiaDto obj)
        {
            return await _noticiaService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            return await _noticiaService.Excluir(id);
        }

        [HttpGet("listarUltimas")]
        public async Task<RespostaPadrao> ListarUltimas()
        {
            return await _noticiaService.ListarUltimas();
        }

        [HttpGet("listarTodas")]
        public async Task<RespostaPadrao> ListarTodas()
        {
            return await _noticiaService.ListarTodas();
        }

        [HttpGet("detalhes")]
        public async Task<RespostaPadrao> Detalhes(int id)
        {
            return await _noticiaService.Detalhes(id);
        }
    }
}