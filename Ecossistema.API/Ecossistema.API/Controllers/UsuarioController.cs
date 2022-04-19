using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usurioService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usurioService = usuarioService;
        }

        [HttpPost]
        [Route("desativar-ativar-usuario")]
        public async Task<RespostaPadrao> DesativarAtivarUsuario([FromBody] AtivarDesativarUserDto model)
        {
            return await _usurioService.DesativarAtivarUsuario(model, UsuarioId);
        }

        [HttpPost]
        [Route("editar-usuario")]
        public async Task<RespostaPadrao> EditarUsuario([FromBody] UsuarioCriacaoDto model)
        {
            return await _usurioService.Editar(model, UsuarioId);
        }

        [HttpGet]
        [Route("listar-todos")]
        public async Task<RespostaPadrao> ListarTodos()
        {
            return await _usurioService.ListarTodos();
        }

        [HttpGet]
        [Route("detalhes")]
        public async Task<RespostaPadrao> Detalhes(int id)
        {
            return await _usurioService.Detalhes(id);
        }



    }
}
