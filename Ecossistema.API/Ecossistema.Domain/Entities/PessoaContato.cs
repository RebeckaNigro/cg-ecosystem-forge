using Ecossistema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    
    public class PessoaContato
    {
        public PessoaContato()
        {

        }

        public PessoaContato(int pessoaId, int contatoId, int tipoContatoId, int usuarioId, DateTime dataAtual)
        {
            PessoaId = pessoaId;
            ContatoId = contatoId;
            TipoContatoId = tipoContatoId;
            Recursos.Auditoria(this, usuarioId, dataAtual);
        }
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int ContatoId { get; set; }
        public virtual Contato Contato { get; set; }
        public int TipoContatoId { get; set; }
        public virtual TipoContato TipoContato { get; set; }
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