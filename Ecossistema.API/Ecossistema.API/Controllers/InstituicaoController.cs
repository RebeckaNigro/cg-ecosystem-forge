using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicaoService _instituicaoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        private int InstituicaoId
        {
            get
            {
                return 1;
            }
        }

        public InstituicaoController(IInstituicaoService instituicaoService)
        {
            _instituicaoService = instituicaoService;
        }
        [Authorize(Roles = UserRolesDto.AdminGeral)]
        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromBody] InstituicaoDto obj)
        {
            var token = Request.Headers["Authorization"];
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _instituicaoService.Incluir(obj, idLogin);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] InstituicaoDto obj)
        {
            return await _instituicaoService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            return await _instituicaoService.Excluir(id);
        }

        [HttpPost("vincularEndereco")]
        public async Task<RespostaPadrao> VincularEndereco([FromBody] EnderecoDto obj)
        {
            return await _instituicaoService.VincularEndereco(obj, InstituicaoId, UsuarioId);
        }

        [HttpGet("buscarInstituicoes")]
        public async Task<RespostaPadrao> BuscarInstituicoes()
        {
            return await _instituicaoService.BuscarInstituicoes();
        }

        [HttpGet("buscarInstituicaoPorId")]
        public async Task<RespostaPadrao> BuscarInstituicaoPorId(int id)
        {
            return await _instituicaoService.BuscarInstituicaoPorId(id);
        }

        [HttpGet("buscarParceirosPorArea")]
        public async Task<RespostaPadrao> BuscarParceirosPorArea(int idArea)
        {
            return await _instituicaoService.BuscarParceirosPorArea(idArea);
        }

    }
}
