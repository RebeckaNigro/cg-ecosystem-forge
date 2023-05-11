using Ecossistema.Util;
using Ecossistema.Util.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class TagItem
    {
        public TagItem()
        {

        }

        public TagItem(EOrigem origem, int tagId, int usuarioId, DateTime data, int? id)
        {
            TagId = tagId;
            if (id != null)
            {
                switch (origem)
                {
                    case EOrigem.Documento:
                        if (id != null) DocumentoId = id;
                        break;
                    case EOrigem.Noticia:
                        if (id != null) NoticiaId = id;
                        break;
                    case EOrigem.Evento:
                        if (id != null) EventoId = id;
                        break;
                }
            }
            Ativo = true;
            Recursos.Auditoria(this, usuarioId, data);
        }
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