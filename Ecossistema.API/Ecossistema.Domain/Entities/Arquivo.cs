using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Arquivo
    {
        public Arquivo()
        {
            ArquivosOrigens = new HashSet<ArquivoOrigem>();
        }

        public int Id { get; set; }
        public string Caminho { get; set; }
        public string Extensao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual ICollection<ArquivoOrigem> ArquivosOrigens { get; set; }
    }
}