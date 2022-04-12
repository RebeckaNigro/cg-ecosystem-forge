using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromBody] EnderecoDto obj)
        {
            return await _enderecoService.Incluir(obj, UsuarioId);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] EnderecoDto obj)
        {
            return await _enderecoService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            return await _enderecoService.Excluir(id);
        }
    }
}
