using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class FaleConosco
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public int FaleConoscoSetorId { get; set; }
        public virtual FaleConoscoSetor FaleConoscoSetor { get; set; }
        public string Descricao { get; set; }
        public bool? Ativo { get; set; } = true;
        public DateTime? DataCriacao { get; set; }
        public int? UsuarioCriacaoId { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime? DataOperacao { get; set; }
        public int? UsuarioOperacaoId { get; set; }
    }
}