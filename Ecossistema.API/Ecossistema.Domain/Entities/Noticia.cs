using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Noticia
    {
        public Noticia()
        {
            Aprovacoes = new HashSet<Aprovacao>();
            ArquivosOrigens = new HashSet<ArquivoOrigem>();
            HistoricoNoticias = new HashSet<HistoricoNoticia>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string? SubTitulo { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int? AprovacaoId { get; set; }
        public virtual Aprovacao Aprovacao { get; set; }
        public bool Aprovado { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual ICollection<Aprovacao> Aprovacoes { get; set; }
        public virtual ICollection<ArquivoOrigem> ArquivosOrigens { get; set; }
        public virtual ICollection<HistoricoNoticia> HistoricoNoticias { get; set; }
    }
}