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

        [HttpPost("cadastrar")]
        public async Task<RespostaPadrao> Cadastrar([FromBody] UsuarioCriacaoDto obj)
        {
            return await _usurioService.Cadastrar(obj, UsuarioId);
        }


    }
}
