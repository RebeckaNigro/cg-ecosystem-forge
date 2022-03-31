using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class TagItem
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int OrigemId { get; set; }
        public virtual Origem Origem { get; set; }
        public int? DocumentoId { get; set; }
        public virtual Documento Documento { get; set; }
        public int? NoticiaId { get; set; }
        public virtual Noticia Noticia { get; set; }
        public int? EventoId { get; set; }
        public virtual Evento Evento { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
    }
}