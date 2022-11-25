using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
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

        [HttpPost("incluir")]
        public async Task<RespostaPadrao> Incluir([FromForm] DocumentoDto obj, IFormFile arquivo)
        {
            return await _documentoService.Incluir(obj, arquivo ,UsuarioId);
        }

        [HttpPut("editar")]
        public async Task<RespostaPadrao> Editar([FromBody] DocumentoDto obj)
        {
            return await _documentoService.Editar(obj, UsuarioId);
        }

        [HttpDelete("excluir")]
        public async Task<RespostaPadrao> Excluir(int id)
        {
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

        public async Task<IActionResult> DownloadDocumento(int id, string nome)
        {
            var result = await _arquivoService.DownloadArquivo(id, nome);
            if(result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}