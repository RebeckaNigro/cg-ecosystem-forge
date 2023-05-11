using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class HistoricoDocumento
    {
        public int Id { get; set; }
        public int DocumentoId { get; set; }
        public virtual Documento Documento { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
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
        public DateTime DataHistorico { get; set; }
    }
}