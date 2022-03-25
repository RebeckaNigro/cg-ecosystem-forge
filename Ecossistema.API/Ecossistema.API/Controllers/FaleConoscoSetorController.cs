using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaleConoscoSetorController : ControllerBase
    {
        private readonly IFaleConoscoSetorService _faleConoscoSetorService;
        public FaleConoscoSetorController(IFaleConoscoSetorService faleConoscoSetorService)
        {
            _faleConoscoSetorService = faleConoscoSetorService;
        }

        [HttpGet("ObterFaleConoscoSetores")]
        public async Task<RespostaPadrao> GetAll()
        {
            return await _faleConoscoSetorService.ObterTodosFaleConoscoSetor();
        }
    }
}
