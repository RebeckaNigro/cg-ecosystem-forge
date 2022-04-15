using Ecossistema.Util;
using Ecossistema.Util.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class ArquivoOrigem
    {
        public ArquivoOrigem()
        {

        }

        public ArquivoOrigem(EOrigem origem, string titulo, int usuarioId, DateTime data, int? id = null)
        {
            OrigemId = origem.Int32Val();
            Titulo = titulo;

            if (origem != EOrigem.Pagina) TipoSegmentoId = ETipoSegmento.Arquivo.Int32Val();

            if (id != null)
            {
                switch (origem)
                {
                    case EOrigem.Parceiro:
                        if (id != null) InstituicaoId = id;
                        break;
                    case EOrigem.Documento:
                        if (id != null) DocumentoId = id;
                        break;
                    case EOrigem.Noticia:
                        if (id != null) NoticiaId = id;
                        break;
                    case EOrigem.Evento:
                        if (id != null) EventoId = id;
                        break;
                    default:
                        if (id != null) PaginaId = id;
                        break;
                }
            }

            Ativo = true;
            Recursos.Auditoria(this, usuarioId, data);
        }
        public int Id { get; set; }
        public int OrigemId { get; set; }
        public virtual Origem Origem { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public int TipoSegmentoId { get; set; }
        public virtual TipoSegmento TipoSegmento { get; set; }
        public int ArquivoId { get; set; }
        public virtual Arquivo Arquivo { get; set; }
        public int? InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public int? NoticiaId { get; set; }
        public virtual Noticia Noticia { get; set; }
        public int? EventoId { get; set; }
        public virtual Evento Evento { get; set; }
        public int? DocumentoId { get; set; }
        public virtual Documento Documento { get; set; }
        public int? PaginaId { get; set; }
        public virtual Pagina Pagina { get; set; }
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