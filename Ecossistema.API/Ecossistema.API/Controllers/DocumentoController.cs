using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;
        private readonly IArquivoService _arquivoService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public DocumentoController(IDocumentoService documentoService, IArquivoService arquivoService)
        {
            _documentoService = documentoService;
            _arquivoService = arquivoService;
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromForm] DocumentoDto obj, IFormFile arquivo)
        {
            var idLogin = User.Claims.FirstOrDefault().Value;
            return await _documentoService.Incluir(obj, arquivo , idLogin);
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] DocumentoDto obj)
        {
            return await _documentoService.Editar(obj, UsuarioId);
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
            await _arquivoService.ExcluirArquivo(id, "documento");
            return await _documentoService.Excluir(id);
        }

        [HttpGet("listarUltimas")]
        public async Task<RespostaPadrao> ListarUltimas()
        {
            return await _documentoService.ListarUltimas();
        }

        [HttpGet("listarTodas")]
        public async Task<RespostaPadrao> ListarTodas()
        {
            return await _documentoService.ListarTodas();
        }

        [HttpGet("detalhes")]
        public async Task<RespostaPadrao> Detalhes(int id)
        {
            return await _documentoService.Detalhes(id);
        }

        [HttpGet("listarTiposDocumentos")]
        public async Task<RespostaPadrao> ListarTiposDocumentos()
        {
            return await _documentoService.ListarTiposDocumentos();
        }

        [HttpGet("downloadDocumento")]

        public async Task<IActionResult> DownloadDocumento(int id, string nome, EOrigem origem)
        {
            var result = await _arquivoService.DownloadArquivo(id, nome, origem);
            if(result == null)
            {
                return NotFound();
            }
            return result;
        }

        [Authorize(Roles = UserRolesDto.UsuarioComum)]
        [HttpPut("atualizaDocumento")]
        public async Task<IActionResult> AtualizaDocumento([FromForm] ArquivoDto obj)
        {
            var result = await _arquivoService.Atualizar(obj, EOrigem.Documento, UsuarioId);
            return Ok(result);
        }
    }
}