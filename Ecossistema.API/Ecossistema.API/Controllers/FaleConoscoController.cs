using Ecossistema.API.Models.DTO;
using Ecossistema.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaleConoscoController : ControllerBase
    {
        private readonly IFaleConoscoService _faceConoscoService;
        public FaleConoscoController(IFaleConoscoService faleConoscoService)
        {
            _faceConoscoService = faleConoscoService;
        }

        [HttpPost("registrar")]
        public async Task<string> SalvarMensagem([FromBody] FaleConoscoDTO obj)
        {
            return await _faceConoscoService.Registrar(obj);
        }
    }
}
