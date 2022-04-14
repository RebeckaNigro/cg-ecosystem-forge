using Ecossistema.Util;
using Ecossistema.Util.Const;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Aprovacoes = new HashSet<Aprovacao>();
            HistoricoUsuarios = new HashSet<HistoricoUsuario>();
            UsuariosAprovacoes = new HashSet<Aprovacao>();
            UsuariosCriacoesAprovacoes = new HashSet<Aprovacao>();
            UsuariosOperacoesAprovacoes = new HashSet<Aprovacao>();
            UsuariosCriacoesArquivos = new HashSet<Arquivo>();
            UsuariosCriacoesArquivos = new HashSet<Arquivo>();
            UsuariosCriacoesArquivosOrigens = new HashSet<ArquivoOrigem>();
            UsuariosOperacoesArquivosOrigens = new HashSet<ArquivoOrigem>();
            UsuariosCriacoesContatos = new HashSet<Contato>();
            UsuariosOperacoesContatos = new HashSet<Contato>();
            UsuariosCriacoesDocumentosAreas = new HashSet<DocumentoArea>();
            UsuariosOperacoesDocumentosAreas = new HashSet<DocumentoArea>();
            UsuariosCriacoesDocumentos = new HashSet<Documento>();
            UsuariosOperacoesDocumentos = new HashSet<Documento>();
            UsuariosCriacoesEnderecos = new HashSet<Endereco>();
            UsuariosOperacoesEnderecos = new HashSet<Endereco>();
            UsuariosCriacoesEventos = new HashSet<Evento>();
            UsuariosOperacoesEventos = new HashSet<Evento>();
            UsuariosCriacoesFaleConoscos = new HashSet<FaleConosco>();
            UsuariosOperacoesFaleConoscos = new HashSet<FaleConosco>();
            UsuariosCriacoesFaleConoscoSetores = new HashSet<FaleConoscoSetor>();
            UsuariosOperacoesFaleConoscoSetores = new HashSet<FaleConoscoSetor>();
            UsuariosCriacoesFaleConoscoSetoresContatos = new HashSet<FaleConoscoSetorContato>();
            UsuariosOperacoesFaleConoscoSetoresContatos = new HashSet<FaleConoscoSetorContato>();
            UsuariosCriacoesHistoricoDocumentos = new HashSet<HistoricoDocumento>();
            UsuariosOperacoesHistoricoDocumentos = new HashSet<HistoricoDocumento>();
            UsuariosCriacoesHistoricoEventos = new HashSet<HistoricoEvento>();
            UsuariosOperacoesHistoricoEventos = new HashSet<HistoricoEvento>();
            UsuariosCriacoesHistoricoInstituicoes = new HashSet<HistoricoInstituicao>();
            UsuariosOperacoesHistoricoInstituicoes = new HashSet<HistoricoInstituicao>();
            UsuariosCriacoesHistoricoNoticias = new HashSet<HistoricoNoticia>();
            UsuariosOperacoesHistoricoNoticias = new HashSet<HistoricoNoticia>();
            UsuariosCriacoesHistoricoUsuarios = new HashSet<HistoricoUsuario>();
            UsuariosOperacoesHistoricoUsuarios = new HashSet<HistoricoUsuario>();
            UsuariosCriacoesInstituicoesAreas = new HashSet<InstituicaoArea>();
            UsuariosOperacoesInstituicoesAreas = new HashSet<InstituicaoArea>();
            UsuariosCriacoesInstituicoesClassificacoes = new HashSet<InstituicaoClassificacao>();
            UsuariosOperacoesInstituicoesClassificacoes = new HashSet<InstituicaoClassificacao>();
            UsuariosCriacoesInstituicoes = new HashSet<Instituicao>();
            UsuariosOperacoesInstituicoes = new HashSet<Instituicao>();
            UsuariosCriacoesNoticias = new HashSet<Noticia>();
            UsuariosOperacoesNoticias = new HashSet<Noticia>();
            UsuariosCriacoesOrigens = new HashSet<Origem>();
            UsuariosOperacoesOrigens = new HashSet<Origem>();
            UsuariosCriacoesPaginas = new HashSet<Pagina>();
            UsuariosOperacoesPaginas = new HashSet<Pagina>();
            UsuariosCriacoesPaginasSegmentos = new HashSet<PaginaSegmento>();
            UsuariosOperacoesPaginasSegmentos = new HashSet<PaginaSegmento>();
            //UsuariosCriacoesPermissoes = new HashSet<Permissao>();
            //UsuariosOperacoesPermissoes = new HashSet<Permissao>();
            //UsuariosCriacoesPessoas = new HashSet<Pessoa>();
            UsuariosOperacoesPessoas = new HashSet<Pessoa>();
            UsuariosCriacoesPessoasContatos = new HashSet<PessoaContato>();
            UsuariosOperacoesPessoasContatos = new HashSet<PessoaContato>();
            UsuariosCriacoesPessoasEnderecos = new HashSet<PessoaEndereco>();
            UsuariosOperacoesPessoasEnderecos = new HashSet<PessoaEndereco>();
            UsuariosCriacoesSituacoesAprovacoes = new HashSet<SituacaoAprovacao>();
            UsuariosOperacoesSituacoesAprovacoes = new HashSet<SituacaoAprovacao>();
            UsuariosCriacoesTags = new HashSet<Tag>();
            UsuariosOperacoesTags = new HashSet<Tag>();
            UsuariosCriacoesTagItens = new HashSet<TagItem>();
            UsuariosOperacoesTagItens = new HashSet<TagItem>();
            UsuariosCriacoesTiposContatos = new HashSet<TipoContato>();
            UsuariosOperacoesTiposContatos = new HashSet<TipoContato>();
            UsuariosCriacoesTiposDocumentos = new HashSet<TipoDocumento>();
            UsuariosOperacoesTiposDocumentos = new HashSet<TipoDocumento>();
            UsuariosCriacoesTiposEnderecos = new HashSet<TipoEndereco>();
            UsuariosOperacoesTiposEnderecos = new HashSet<TipoEndereco>();
            UsuariosCriacoesTiposEventos = new HashSet<TipoEvento>();
            UsuariosOperacoesTiposEventos = new HashSet<TipoEvento>();
            UsuariosCriacoesTiposInstituicoes = new HashSet<TipoInstituicao>();
            UsuariosOperacoesTiposInstituicoes = new HashSet<TipoInstituicao>();
            UsuariosCriacoesTiposSegmentos = new HashSet<TipoSegmento>();
            UsuariosOperacoesTiposSegmentos = new HashSet<TipoSegmento>();
            UsuariosCriacoesUsuarios = new HashSet<Usuario>();
            UsuariosOperacoesUsuarios = new HashSet<Usuario>();
        }

        public Usuario(int pessoaId, int instituicaoId , string aspNetUserId, DateTime ultimoLogin, 
            string cargo, int usuarioId, DateTime dataAtual)
        {
           PessoaId = pessoaId;
           InstituicaoId = instituicaoId;
           AspNetUserId = aspNetUserId;
            UltimoLogin = ultimoLogin;
            Cargo = cargo;
           Aprovado = false;
            //Aprovacoes = new List<Aprovacao> { new Aprovacao(EOrigem.Usuario, usuarioId, dataAtual) };
            Ativo = true;
            Recursos.Auditoria(this, usuarioId, dataAtual);
        }

        public int Id { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public string AspNetUserId { get; set; }
       // public UserManager<IdentityUser> AspNetUser { get; set; }
        //public int PermissaoId { get; set; }
        //public virtual Permissao Permissao { get; set; }
        //public string Email { get; set; }
        
        //public string SenhaHash { get; set; }
        //public string SenhaSalt { get; set; }
        //public string? Token { get; set; }
        //public DateTime? TokenExpiracao { get; set; }
        public DateTime UltimoLogin { get; set; }
        public string Cargo { get; set; }
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
        public virtual ICollection<Aprovacao> UsuariosAprovacoes { get; set; }
        public virtual ICollection<Aprovacao> UsuariosCriacoesAprovacoes { get; set; }
        public virtual ICollection<Aprovacao> UsuariosOperacoesAprovacoes { get; set; }
        public virtual ICollection<Arquivo> UsuariosCriacoesArquivos { get; set; }
        public virtual ICollection<Arquivo> UsuariosOperacoesArquivos { get; set; }
        public virtual ICollection<ArquivoOrigem> UsuariosCriacoesArquivosOrigens { get; set; }
        public virtual ICollection<ArquivoOrigem> UsuariosOperacoesArquivosOrigens { get; set; }
        public virtual ICollection<Contato> UsuariosCriacoesContatos { get; set; }
        public virtual ICollection<Contato> UsuariosOperacoesContatos { get; set; }
        public virtual ICollection<DocumentoArea> UsuariosCriacoesDocumentosAreas { get; set; }
        public virtual ICollection<DocumentoArea> UsuariosOperacoesDocumentosAreas { get; set; }
        public virtual ICollection<Documento> UsuariosCriacoesDocumentos { get; set; }
        public virtual ICollection<Documento> UsuariosOperacoesDocumentos { get; set; }
        public virtual ICollection<Endereco> UsuariosCriacoesEnderecos { get; set; }
        public virtual ICollection<Endereco> UsuariosOperacoesEnderecos { get; set; }
        public virtual ICollection<Evento> UsuariosCriacoesEventos { get; set; }
        public virtual ICollection<Evento> UsuariosOperacoesEventos { get; set; }
        public virtual ICollection<FaleConosco> UsuariosCriacoesFaleConoscos { get; set; }
        public virtual ICollection<FaleConosco> UsuariosOperacoesFaleConoscos { get; set; }
        public virtual ICollection<FaleConoscoSetor> UsuariosCriacoesFaleConoscoSetores { get; set; }
        public virtual ICollection<FaleConoscoSetor> UsuariosOperacoesFaleConoscoSetores { get; set; }
        public virtual ICollection<FaleConoscoSetorContato> UsuariosCriacoesFaleConoscoSetoresContatos { get; set; }
        public virtual ICollection<FaleConoscoSetorContato> UsuariosOperacoesFaleConoscoSetoresContatos { get; set; }
        public virtual ICollection<InstituicaoArea> UsuariosCriacoesInstituicoesAreas { get; set; }
        public virtual ICollection<InstituicaoArea> UsuariosOperacoesInstituicoesAreas { get; set; }
        public virtual ICollection<InstituicaoClassificacao> UsuariosCriacoesInstituicoesClassificacoes { get; set; }
        public virtual ICollection<InstituicaoClassificacao> UsuariosOperacoesInstituicoesClassificacoes { get; set; }
        public virtual ICollection<Instituicao> UsuariosCriacoesInstituicoes { get; set; }
        public virtual ICollection<Instituicao> UsuariosOperacoesInstituicoes { get; set; }
        public virtual ICollection<Noticia> UsuariosCriacoesNoticias { get; set; }
        public virtual ICollection<Noticia> UsuariosOperacoesNoticias { get; set; }
        public virtual ICollection<Origem> UsuariosCriacoesOrigens { get; set; }
        public virtual ICollection<Origem> UsuariosOperacoesOrigens { get; set; }
        public virtual ICollection<Pagina> UsuariosCriacoesPaginas { get; set; }
        public virtual ICollection<Pagina> UsuariosOperacoesPaginas { get; set; }
        public virtual ICollection<PaginaSegmento> UsuariosCriacoesPaginasSegmentos { get; set; }
        public virtual ICollection<PaginaSegmento> UsuariosOperacoesPaginasSegmentos { get; set; }
        //public virtual ICollection<Permissao> UsuariosCriacoesPermissoes { get; set; }
        //public virtual ICollection<Permissao> UsuariosOperacoesPermissoes { get; set; }
        public virtual ICollection<Pessoa> UsuariosCriacoesPessoas { get; set; }
        public virtual ICollection<Pessoa> UsuariosOperacoesPessoas { get; set; }
        public virtual ICollection<PessoaContato> UsuariosCriacoesPessoasContatos { get; set; }
        public virtual ICollection<PessoaContato> UsuariosOperacoesPessoasContatos { get; set; }
        public virtual ICollection<PessoaEndereco> UsuariosCriacoesPessoasEnderecos { get; set; }
        public virtual ICollection<PessoaEndereco> UsuariosOperacoesPessoasEnderecos { get; set; }
        public virtual ICollection<SituacaoAprovacao> UsuariosCriacoesSituacoesAprovacoes { get; set; }
        public virtual ICollection<SituacaoAprovacao> UsuariosOperacoesSituacoesAprovacoes { get; set; }
        public virtual ICollection<Tag> UsuariosCriacoesTags { get; set; }
        public virtual ICollection<Tag> UsuariosOperacoesTags { get; set; }
        public virtual ICollection<TagItem> UsuariosCriacoesTagItens { get; set; }
        public virtual ICollection<TagItem> UsuariosOperacoesTagItens { get; set; }
        public virtual ICollection<TipoContato> UsuariosCriacoesTiposContatos { get; set; }
        public virtual ICollection<TipoContato> UsuariosOperacoesTiposContatos { get; set; }
        public virtual ICollection<TipoDocumento> UsuariosCriacoesTiposDocumentos { get; set; }
        public virtual ICollection<TipoDocumento> UsuariosOperacoesTiposDocumentos { get; set; }
        public virtual ICollection<TipoEndereco> UsuariosCriacoesTiposEnderecos { get; set; }
        public virtual ICollection<TipoEndereco> UsuariosOperacoesTiposEnderecos { get; set; }
        public virtual ICollection<TipoEvento> UsuariosCriacoesTiposEventos { get; set; }
        public virtual ICollection<TipoEvento> UsuariosOperacoesTiposEventos { get; set; }
        public virtual ICollection<TipoInstituicao> UsuariosCriacoesTiposInstituicoes { get; set; }
        public virtual ICollection<TipoInstituicao> UsuariosOperacoesTiposInstituicoes { get; set; }
        public virtual ICollection<TipoSegmento> UsuariosCriacoesTiposSegmentos { get; set; }
        public virtual ICollection<TipoSegmento> UsuariosOperacoesTiposSegmentos { get; set; }
        public virtual ICollection<Usuario> UsuariosCriacoesUsuarios { get; set; }
        public virtual ICollection<Usuario> UsuariosOperacoesUsuarios { get; set; }
        public virtual ICollection<HistoricoDocumento> UsuariosCriacoesHistoricoDocumentos { get; set; }
        public virtual ICollection<HistoricoDocumento> UsuariosOperacoesHistoricoDocumentos { get; set; }
        public virtual ICollection<HistoricoEvento> UsuariosCriacoesHistoricoEventos { get; set; }
        public virtual ICollection<HistoricoEvento> UsuariosOperacoesHistoricoEventos { get; set; }
        public virtual ICollection<HistoricoInstituicao> UsuariosCriacoesHistoricoInstituicoes { get; set; }
        public virtual ICollection<HistoricoInstituicao> UsuariosOperacoesHistoricoInstituicoes { get; set; }
        public virtual ICollection<HistoricoNoticia> UsuariosCriacoesHistoricoNoticias { get; set; }
        public virtual ICollection<HistoricoNoticia> UsuariosOperacoesHistoricoNoticias { get; set; }
        public virtual ICollection<HistoricoUsuario> HistoricoUsuarios { get; set; }
        public virtual ICollection<HistoricoUsuario> UsuariosCriacoesHistoricoUsuarios { get; set; }
        public virtual ICollection<HistoricoUsuario> UsuariosOperacoesHistoricoUsuarios { get; set; }

    }
}