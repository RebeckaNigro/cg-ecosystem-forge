using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Documento
    {
        public Documento()
        {
            Aprovacoes = new HashSet<Aprovacao>();
            ArquivosOrigens = new HashSet<ArquivoOrigem>();
            HistoricoDocumentos = new HashSet<HistoricoDocumento>();
            TagsItens = new HashSet<TagItem>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public int DocumentoAreaId { get; set; }
        public virtual DocumentoArea DocumentoArea { get; set; }
        public int InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public DateTime Data { get; set; }
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
        public virtual ICollection<HistoricoDocumento> HistoricoDocumentos { get; set; }
        public virtual ICollection<TagItem> TagsItens { get; set; }
    }
}