using Ecossistema.Util;
using Ecossistema.Util.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Aprovacao
    {
        public Aprovacao()
        {
            Documentos = new HashSet<Documento>();
            Eventos = new HashSet<Evento>();
            Insituicoes = new HashSet<Instituicao>();
            Noticias = new HashSet<Noticia>();
            Usuarios = new HashSet<Usuario>();
            HistoricoDocumentos = new HashSet<HistoricoDocumento>();
            HistoricoEventos = new HashSet<HistoricoEvento>();
            HistoricoInstituicoes = new HashSet<HistoricoInstituicao>();
            HistoricoNoticias = new HashSet<HistoricoNoticia>();
            HistoricoUsuarios = new HashSet<HistoricoUsuario>();
        }

        public Aprovacao(EOrigem origem, int usuarioId, DateTime data, int? id = null)
        {
            OrigemId = origem.Int32Val();

            if (id != null)
            {
                switch (origem)
                {
                    case EOrigem.Parceiro:
                        InstituicaoId = id;
                        break;
                    case EOrigem.Documento:
                        DocumentoId = id;
                        break;
                    case EOrigem.Noticia:
                        NoticiaId = id;
                        break;
                    case EOrigem.Evento:
                        EventoId = id;
                        break;
                    default:
                        UsuarioId = id;
                        break;
                }
            }

            SituacaoAprovacaoId = ESituacaoAprovacao.Pendente.Int32Val();
            Ativo = true;
            Recursos.Auditoria(this, usuarioId, data);
        }

        public int Id { get; set; }
        public int OrigemId { get; set; }
        public virtual Origem Origem { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public int? UsuarioAprovacaoId { get; set; }
        public virtual Usuario UsuarioAprovacao { get; set; }
        public string? Motivo { get; set; }
        public int? InstituicaoId { get; set; }
        public virtual Instituicao Insituicao { get; set; }
        public int? UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int? EventoId { get; set; }
        public virtual Evento Evento { get; set; }
        public int? NoticiaId { get; set; }
        public virtual Noticia Noticia { get; set; }
        public int? DocumentoId { get; set; }
        public virtual Documento Documento { get; set; }
        public int SituacaoAprovacaoId { get; set; }
        public virtual SituacaoAprovacao SituacaoAprovacao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual ICollection<Documento> Documentos { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
        public virtual ICollection<Instituicao> Insituicoes { get; set; }
        public virtual ICollection<Noticia> Noticias { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<HistoricoDocumento> HistoricoDocumentos { get; set; }
        public virtual ICollection<HistoricoEvento> HistoricoEventos { get; set; }
        public virtual ICollection<HistoricoInstituicao> HistoricoInstituicoes { get; set; }
        public virtual ICollection<HistoricoNoticia> HistoricoNoticias { get; set; }
        public virtual ICollection<HistoricoUsuario> HistoricoUsuarios { get; set; }
    }
}