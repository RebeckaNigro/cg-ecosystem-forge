using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class DocumentoEditDto
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? DocumentoAreaId { get; set; }
        public int? InstituicaoId { get; set; }
        public DateTime? Data { get; set; }
    }
}