using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromBody] EventoDto obj)
        {
            return await _eventoService.Incluir(obj, UsuarioId);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] EventoDto obj)
        {
            return await _eventoService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            return await _eventoService.Excluir(id);
        }

        [HttpGet("listarUltimas")]
        public async Task<RespostaPadrao> ListarUltimas()
        {
            return await _eventoService.ListarUltimas();
        }

        [HttpGet("listarTodas")]
        public async Task<RespostaPadrao> ListarTodas()
        {
            return await _eventoService.ListarTodas();
        }

        [HttpGet("detalhes")]
        public async Task<RespostaPadrao> Detalhes(int id)
        {
            return await _eventoService.Detalhes(id);
        }
    }
}