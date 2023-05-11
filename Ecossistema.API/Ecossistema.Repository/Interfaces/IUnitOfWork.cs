using Ecossistema.Data.Repositories;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region A

        IBaseRepository<Aprovacao> Aprovacoes { get; }
        IBaseRepository<Arquivo> Arquivos { get; }
        IBaseRepository<ArquivoOrigem> ArquivosOrigens { get; }

        #endregion

        #region B

        #endregion

        #region c

        IBaseRepository<Contato> Contatos { get; }

        #endregion

        #region D

        IBaseRepository<Documento> Documentos { get; }
        IBaseRepository<DocumentoArea> DocumentosAreas { get; }

        #endregion

        #region E

        IBaseRepository<Endereco> Enderecos { get; }
        IBaseRepository<Evento> Eventos { get; }

        #endregion

        #region F

        IBaseRepository<FaleConosco> FaleConoscos { get; }
        IBaseRepository<FaleConoscoSetor> FaleConoscoSetores { get; }
        IBaseRepository<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; }
        //IFaleConoscoSetorRepository FaleConoscoSetores { get; }

        #endregion

        #region G

        #endregion

        #region H

        IBaseRepository<HistoricoDocumento> HistoricosDocumentos { get; }
        IBaseRepository<HistoricoEvento> HistoricosEventos { get; }
        IBaseRepository<HistoricoInstituicao> HistoricosInstituicaos { get; }
        IBaseRepository<HistoricoNoticia> HistoricosNoticias { get; }
        IBaseRepository<HistoricoUsuario> HistoricosUsuarios { get; }

        #endregion

        #region I

        IBaseRepository<Instituicao> Instituicoes { get; }
        IBaseRepository<InstituicaoArea> InstituicoesAreas { get; }
        IBaseRepository<InstituicaoClassificacao> InstituicoesClassificacoes { get; }
        IBaseRepository<InstituicaoEndereco> InstituicoesEnderecos { get; }

        #endregion

        #region J

        #endregion

        #region L

        #endregion

        #region M

        #endregion

        #region N

        IBaseRepository<Noticia> Noticias { get; }

        #endregion

        #region O

        IBaseRepository<Origem> Origens { get; }

        #endregion

        #region P

        IBaseRepository<Pagina> Paginas { get; }
        IBaseRepository<PaginaSegmento> PaginasSegmentos { get; }
        //IBaseRepository<Permissao> Permissioes { get; }
        IBaseRepository<Pessoa> Pessoas { get; }
        IBaseRepository<PessoaContato> PessoasContatos { get; }
        IBaseRepository<PessoaEndereco> PessoasEnderecos { get; }

        #endregion

        #region Q

        #endregion

        #region R

        #endregion

        #region S

        IBaseRepository<SituacaoAprovacao> SituacoesAprovacoes { get; }

        #endregion

        #region T

        IBaseRepository<Tag> Tags { get; }
        IBaseRepository<TagItem> TagsItens { get; }
        IBaseRepository<TipoContato> TiposContatos { get; }
        //IBaseRepository<TipoDocumento> TiposDocumentos { get; }
        IBaseRepository<TipoEndereco> TiposEnderecos { get; }
        IBaseRepository<TipoEvento> TiposEventos { get; }
        IBaseRepository<TipoInstituicao> TiposInstituicoes { get; }
        IBaseRepository<TipoSegmento> TiposSegmentos { get; }

        #endregion

        #region U

        IBaseRepository<Usuario> Usuarios { get; }

        #endregion

        #region V

        #endregion

        #region X

        #endregion

        #region Z

        #endregion

        int Complete();
    }
}