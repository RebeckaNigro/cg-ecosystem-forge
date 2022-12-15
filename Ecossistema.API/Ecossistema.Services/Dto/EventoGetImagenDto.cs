using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class EventoGetImagenDto
    {
        public int Id { get; set; } 
        public string? Titulo { get; set; }
        public string? Responsavel { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string? Local { get; set; }
        public int UsuarioId { get; set; }

        public Byte[]? Arquivo { get; set; }
        public DateTime? DataOperacao { get; set; }

        public string? LinkImagem { get; set; }
    }
}
