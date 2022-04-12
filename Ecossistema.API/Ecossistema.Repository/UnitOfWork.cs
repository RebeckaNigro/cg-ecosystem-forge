using Ecossistema.Data.Interfaces;
using Ecossistema.Data.Repositories;
using Ecossistema.Domain.Entities;

namespace Ecossistema.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcossistemaContext _context;

        #region Repositories

        #region A

        public IBaseRepository<Aprovacao> Aprovacoes { get; private set; }
        public IBaseRepository<Arquivo> Arquivos { get; private set; }
        public IBaseRepository<ArquivoOrigem> ArquivosOrigens { get; private set; }

        #endregion

        #region B

        #endregion

        #region c

        public IBaseRepository<Contato> Contatos { get; private set; }

        #endregion

        #region D

        public IBaseRepository<Documento> Documentos { get; private set; }
        public IBaseRepository<DocumentoArea> DocumentosAreas { get; private set; }

        #endregion

        #region E

        public IBaseRepository<Endereco> Enderecos { get; private set; }
        public IBaseRepository<Evento> Eventos { get; private set; }

        #endregion

        #region F

        public IBaseRepository<FaleConosco> FaleConoscos { get; private set; }
        public IBaseRepository<FaleConoscoSetor> FaleConoscoSetores { get; private set; }
        public IBaseRepository<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; private set; }
        //public IFaleConoscoSetorRepository FaleConoscoSetores { get; private set; }

        #endregion

        #region G

        #endregion

        #region H

        public IBaseRepository<HistoricoDocumento> HistoricosDocumentos { get; private set; }
        public IBaseRepository<HistoricoEvento> HistoricosEventos { get; private set; }
        public IBaseRepository<HistoricoInstituicao> HistoricosInstituicaos { get; private set; }
        public IBaseRepository<HistoricoNoticia> HistoricosNoticias { get; private set; }
        public IBaseRepository<HistoricoUsuario> HistoricosUsuarios { get; private set; }

        #endregion

        #region I

        public IBaseRepository<Instituicao> Instituicoes { get; private set; }
        public IBaseRepository<InstituicaoArea> InstituicoesAreas { get; private set; }
        public IBaseRepository<InstituicaoClassificacao> InstituicoesClassificacoes { get; private set; }
        public IBaseRepository<InstituicaoEndereco> InstituicoesEnderecos { get; private set; }

        #endregion

        #region J

        #endregion

        #region L

        #endregion

        #region M

        #endregion

        #region N

        public IBaseRepository<Noticia> Noticias { get; private set; }

        #endregion

        #region O

        public IBaseRepository<Origem> Origens { get; private set; }

        #endregion

        #region P

        public IBaseRepository<Pagina> Paginas { get; private set; }
        public IBaseRepository<PaginaSegmento> PaginasSegmentos { get; private set; }
        public IBaseRepository<Permissao> Permissioes { get; private set; }
        public IBaseRepository<Pessoa> Pessoas { get; private set; }
        public IBaseRepository<PessoaContato> PessoasContatos { get; private set; }
        public IBaseRepository<PessoaEndereco> PessoasEnderecos { get; private set; }

        #endregion

        #region Q

        #endregion

        #region R

        #endregion

        #region S

        public IBaseRepository<SituacaoAprovacao> SituacoesAprovacoes { get; private set; }

        #endregion

        #region T

        public IBaseRepository<Tag> Tags { get; private set; }
        public IBaseRepository<TagItem> TagsItens { get; private set; }
        public IBaseRepository<TipoContato> TiposContatos { get; private set; }
        public IBaseRepository<TipoDocumento> TiposDocumentos { get; private set; }
        public IBaseRepository<TipoEndereco> TiposEnderecos { get; private set; }
        public IBaseRepository<TipoEvento> TiposEventos { get; private set; }
        public IBaseRepository<TipoInstituicao> TiposInstituicoes { get; private set; }
        public IBaseRepository<TipoSegmento> TiposSegmentos { get; private set; }

        #endregion

        #region U

        public IBaseRepository<Usuario> Usuarios { get; private set; }

        #endregion

        #region V

        #endregion

        #region X

        #endregion

        #region Z

        #endregion

        #endregion

        public UnitOfWork(EcossistemaContext context)
        {
            _context = context;

            #region Constructors

            #region A

            Aprovacoes = new BaseRepository<Aprovacao>(_context);
            Arquivos = new BaseRepository<Arquivo>(_context);
            ArquivosOrigens = new BaseRepository<ArquivoOrigem>(_context);

            #endregion

            #region B

            #endregion

            #region c

            Contatos = new BaseRepository<Contato>(_context);

            #endregion

            #region D

            Documentos = new BaseRepository<Documento>(_context);
            DocumentosAreas = new BaseRepository<DocumentoArea>(_context);

            #endregion

            #region E

            Enderecos = new BaseRepository<Endereco>(_context);
            Eventos = new BaseRepository<Evento>(_context);

            #endregion

            #region F

            FaleConoscos = new BaseRepository<FaleConosco>(_context);
            FaleConoscoSetores = new BaseRepository<FaleConoscoSetor>(_context);
            FaleConoscoSetoresContatos = new BaseRepository<FaleConoscoSetorContato>(_context);
            //FaleConoscoSetores = new FaleConoscoSetorRepository(_context);

            #endregion

            #region G

            #endregion

            #region H

            HistoricosDocumentos = new BaseRepository<HistoricoDocumento>(_context);
            HistoricosEventos = new BaseRepository<HistoricoEvento>(_context);
            HistoricosInstituicaos = new BaseRepository<HistoricoInstituicao>(_context);
            HistoricosNoticias = new BaseRepository<HistoricoNoticia>(_context);
            HistoricosUsuarios = new BaseRepository<HistoricoUsuario>(_context);

            #endregion

            #region I

            Instituicoes = new BaseRepository<Instituicao>(_context);
            InstituicoesAreas = new BaseRepository<InstituicaoArea>(_context);
            InstituicoesClassificacoes = new BaseRepository<InstituicaoClassificacao>(_context);
            InstituicoesEnderecos = new BaseRepository<InstituicaoEndereco>(_context);

            #endregion

            #region J

            #endregion

            #region L

            #endregion

            #region M

            #endregion

            #region N

            Noticias = new BaseRepository<Noticia>(_context);

            #endregion

            #region O

            Origens = new BaseRepository<Origem>(_context);

            #endregion

            #region P

            Paginas = new BaseRepository<Pagina>(_context);
            PaginasSegmentos = new BaseRepository<PaginaSegmento>(_context);
            Permissioes = new BaseRepository<Permissao>(_context);
            Pessoas = new BaseRepository<Pessoa>(_context);
            PessoasContatos = new BaseRepository<PessoaContato>(_context);
            PessoasEnderecos = new BaseRepository<PessoaEndereco>(_context);

            #endregion

            #region Q

            #endregion

            #region R

            #endregion

            #region S

            SituacoesAprovacoes = new BaseRepository<SituacaoAprovacao>(_context);

            #endregion

            #region T

            Tags = new BaseRepository<Tag>(_context);
            TagsItens = new BaseRepository<TagItem>(_context);
            TiposContatos = new BaseRepository<TipoContato>(_context);
            TiposDocumentos = new BaseRepository<TipoDocumento>(_context);
            TiposEnderecos = new BaseRepository<TipoEndereco>(_context);
            TiposEventos = new BaseRepository<TipoEvento>(_context);
            TiposInstituicoes = new BaseRepository<TipoInstituicao>(_context);
            TiposSegmentos = new BaseRepository<TipoSegmento>(_context);

            #endregion

            #region U

            Usuarios = new BaseRepository<Usuario>(_context);

            #endregion

            #region V

            #endregion

            #region X

            #endregion

            #region Z

            #endregion

            #endregion
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}