using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class NoticiaListaDto
    {
        public int? Id { get; set; }
        public string? Titulo { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public DateTime? DataOperacao { get; set; }
        public string? Descricao { get; set; }
        public string? SubTitulo { get; set; }
        public List<TagDto>? Tags { get; set; }
        public Byte[]? Arquivo { get; set; }
    }
}