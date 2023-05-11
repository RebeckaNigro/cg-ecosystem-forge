using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class HistoricoUsuario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public string AspNetUserId { get; set; }
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
        public DateTime DataHistorico { get; set; }
    }
}