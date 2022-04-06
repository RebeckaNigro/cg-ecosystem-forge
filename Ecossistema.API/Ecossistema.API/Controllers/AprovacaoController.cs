using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprovacaoController : ControllerBase
    {
        private readonly IAprovacaoService _aprovacaoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public AprovacaoController(IAprovacaoService aprovacaoService)
        {
            _aprovacaoService = aprovacaoService;
        }

        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromBody] AprovacaoDto obj)
        {
            return await _aprovacaoService.Incluir(obj, UsuarioId);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] AprovacaoDto obj)
        {
            return await _aprovacaoService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            return await _aprovacaoService.Excluir(id);
        }
    }
}

