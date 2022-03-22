using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaleConoscoController : ControllerBase
    {
        private readonly IFaleConoscoService _faleConoscoService;
        public FaleConoscoController(IFaleConoscoService faleConoscoService)
        {
            _faleConoscoService = faleConoscoService;
        }

        [HttpPost("registrar")]
        public async Task<RespostaPadrao> Registrar([FromBody] FaleConoscoDTO obj)
        {
            return await _faleConoscoService.Registrar(obj);
        }

        [HttpGet("ObterFaleConosco")]
        public async Task<object> ObterFaleConosco()
        {
            return await _faleConoscoService.ObterFaleConosco();
        }

        [HttpGet("ObterContatosSetor")]
        public async Task<object> GetAll(int faleConoscoSetorId)
        {
            return await _faleConoscoService.ObterContatosSetor(faleConoscoSetorId);
        }
    }
}
