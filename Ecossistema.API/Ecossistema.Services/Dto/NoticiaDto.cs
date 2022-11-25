using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class NoticiaDto
    {
        public int? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? SubTitulo { get; set; }
        public DateTime? DataPublicacao { get; set; }
    }
}