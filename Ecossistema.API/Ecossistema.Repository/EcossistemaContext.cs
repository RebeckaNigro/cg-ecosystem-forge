using Ecossistema.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecossistema.Data
{
    public class EcossistemaContext : IdentityDbContext<IdentityUser>//Antes era DbContext
    {
        public EcossistemaContext(DbContextOptions<EcossistemaContext> options) : base(options) { }

        #region DbSets

        #region A

        public virtual DbSet<Aprovacao> Aprovacoes { get; set; }
        public virtual DbSet<Arquivo> Arquivos { get; set; }
        public virtual DbSet<ArquivoOrigem> ArquivosOrigens { get; set; }

        #endregion

        #region B

        #endregion

        #region c

        public virtual DbSet<Contato> Contatos { get; set; }

        #endregion

        #region D

        public virtual DbSet<Documento> Documentos { get; set; }
        public virtual DbSet<DocumentoArea> DocumentosAreas { get; set; }

        #endregion

        #region E

        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }

        #endregion

        #region F

        public virtual DbSet<FaleConosco> FaleConoscos { get; set; }
        public virtual DbSet<FaleConoscoSetor> FaleConoscoSetores { get; set; }
        public virtual DbSet<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; set; }

        #endregion

        #region G

        #endregion

        #region H

        public virtual DbSet<HistoricoDocumento> HistoricosDocumentos { get; set; }
        public virtual DbSet<HistoricoEvento> HistoricosEventos { get; set; }
        public virtual DbSet<HistoricoInstituicao> HistoricosInstituicaos { get; set; }
        public virtual DbSet<HistoricoNoticia> HistoricosNoticias { get; set; }
        public virtual DbSet<HistoricoUsuario> HistoricosUsuarios { get; set; }

        #endregion

        #region I

        public virtual DbSet<Instituicao> Instituicoes { get; set; }
        public virtual DbSet<InstituicaoArea> InstituicoesAreas { get; set; }
        public virtual DbSet<InstituicaoClassificacao> InstituicoesClassificacoes { get; set; }
        public virtual DbSet<InstituicaoEndereco> InstituicoesEnderecos { get; set; }

        #endregion

        #region J

        #endregion

        #region L

        #endregion

        #region M

        #endregion

        #region N

        public virtual DbSet<Noticia> Noticias { get; set; }

        #endregion

        #region O

        public virtual DbSet<Origem> Origens { get; set; }

        #endregion

        #region P

        public virtual DbSet<Pagina> Paginas { get; set; }
        public virtual DbSet<PaginaSegmento> PaginasSegmentos { get; set; }
        public virtual DbSet<Permissao> Permissioes { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<PessoaContato> PessoasContatos { get; set; }
        public virtual DbSet<PessoaEndereco> PessoasEnderecos { get; set; }

        #endregion

        #region Q

        #endregion

        #region R

        #endregion

        #region S

        public virtual DbSet<SituacaoAprovacao> SituacoesAprovacoes { get; set; }

        #endregion

        #region T

        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagItem> TagsItens { get; set; }
        public virtual DbSet<TipoContato> TiposContatos { get; set; }
        public virtual DbSet<TipoDocumento> TiposDocumentos { get; set; }
        public virtual DbSet<TipoEndereco> TiposEnderecos { get; set; }
        public virtual DbSet<TipoEvento> TiposEventos { get; set; }
        public virtual DbSet<TipoInstituicao> TiposInstituicoes { get; set; }
        public virtual DbSet<TipoSegmento> TiposSegmentos { get; set; }

        #endregion

        #region U

        public virtual DbSet<Usuario> Usuarios { get; set; }

        #endregion

        #region V

        #endregion

        #region X

        #endregion

        #region Z

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}