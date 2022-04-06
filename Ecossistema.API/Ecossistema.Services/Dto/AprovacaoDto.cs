using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class AprovacaoDto
    {
        public int? Id { get; set; }
        public int? OrigemId { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public int? UsuarioAprovacaoId { get; set; }
        public string? Motivo { get; set; }
        public int? InstituicaoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? EventoId { get; set; }
        public int? NoticiaId { get; set; }
        public int? DocumentoId { get; set; }
        public int? SituacaoAprovacaoId { get; set; }
    }
}
