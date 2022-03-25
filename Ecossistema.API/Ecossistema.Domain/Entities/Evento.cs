using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Evento
    {
        public Evento()
        {
            Aprovacoes = new HashSet<Aprovacao>();
            ArquivosOrigens = new HashSet<ArquivoOrigem>();
            HistoricoEventos = new HashSet<HistoricoEvento>();
        }

        public int Id { get; set; }
        public int InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public int TipoEventoId { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public string? Local { get; set; }
        public int? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public string? LinkExterno { get; set; }
        public bool ExibirMaps { get; set; }
        public string Responsavel { get; set; }
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
        public virtual ICollection<HistoricoEvento> HistoricoEventos { get; set; }
    }
}