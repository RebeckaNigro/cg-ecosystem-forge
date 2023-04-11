using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class EventoArquivosDto
    {
        public string Evento { get; set; }
        public List<IFormFile>? Arquivo { get; set; }
        public List<TagDto>? Tags { get; set; }
    }
}
