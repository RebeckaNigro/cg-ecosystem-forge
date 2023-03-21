using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IArquivoService _arquivoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("cadastrarTag")]
        public async Task<RespostaPadrao> Incluir([FromBody] TagDto obj, int usuarioId)
        {
            return await _tagService.CadastrarTag(obj, usuarioId);
        }

        [HttpGet("listarTags")]
        public async Task<RespostaPadrao> ListarTags()
        {
            return await _tagService.ListarTodas();
        }

    }
}