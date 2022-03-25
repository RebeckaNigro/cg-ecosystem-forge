using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Instituicao
    {
        public Instituicao()
        {
            ArquivosOrigens = new HashSet<ArquivoOrigem>();
            Documentos = new HashSet<Documento>();
            Eventos = new HashSet<Evento>();
            Usuarios = new HashSet<Usuario>();
            HistoricoDocumentos = new HashSet<HistoricoDocumento>();
            HistoricoEventos = new HashSet<HistoricoEvento>();
            HistoricoInstituicoes = new HashSet<HistoricoInstituicao>();
            HistoricoUsuarios = new HashSet<HistoricoUsuario>();
        }

        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string? Responsável { get; set; }
        public int InstituicaoAreaId { get; set; }
        public virtual InstituicaoArea InstituicaoArea { get; set; }
        public int InstituicaoClassificacaoId { get; set; }
        public virtual InstituicaoClassificacao InstituicaoClassificacao { get; set; }
        public string? Descricao { get; set; }
        public string? Missao { get; set; }
        public string? Visao { get; set; }
        public string? Valores { get; set; }
        public int TipoInstituicaoId { get; set; }
        public virtual TipoInstituicao TipoInstituicao { get; set; }
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
        public virtual ICollection<Documento> Documentos { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<HistoricoDocumento> HistoricoDocumentos { get; set; }
        public virtual ICollection<HistoricoEvento> HistoricoEventos { get; set; }
        public virtual ICollection<HistoricoInstituicao> HistoricoInstituicoes { get; set; }
        public virtual ICollection<HistoricoUsuario> HistoricoUsuarios { get; set; }
    }
}