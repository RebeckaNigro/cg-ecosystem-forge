using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Instituicao
    {
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
    }
}