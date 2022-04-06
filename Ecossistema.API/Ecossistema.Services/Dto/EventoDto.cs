using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class EventoDto
    {
        public int? Id { get; set; }
        public int? InstituicaoId { get; set; }
        public int? TipoEventoId { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string? Local { get; set; }
        public int? EnderecoId { get; set; }
        public string LinkExterno { get; set; }
        public bool? ExibirMaps { get; set; }
        public string? Responsavel { get; set; }
    }
}
