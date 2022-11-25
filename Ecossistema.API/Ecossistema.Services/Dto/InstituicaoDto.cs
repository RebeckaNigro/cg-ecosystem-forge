using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class InstituicaoDto
    {
        public int? Id { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Cnpj { get; set; }
        public string? Responsavel { get; set; }
        public int? InstituicaoAreaId { get; set; }
        public int? InstituicaoClassificacaoId { get; set; }
        public string? Descricao { get; set; }
        public string? Missao { get; set; }
        public string? Visao { get; set; }
        public string? Valores { get; set; }
        public int? TipoInstituicaoId { get; set; }
    }
}
