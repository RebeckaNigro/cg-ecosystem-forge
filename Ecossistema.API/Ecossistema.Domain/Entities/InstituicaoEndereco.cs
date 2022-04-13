using Ecossistema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class InstituicaoEndereco
    {
        public InstituicaoEndereco()
        {
        }

        public InstituicaoEndereco(int instituicaoId,
            int enderecoId,
            int tipoEnderecoId,
            int usuarioId,
            DateTime data)
        {
            InstituicaoId = instituicaoId;
            EnderecoId = enderecoId;
            TipoEnderecoId = tipoEnderecoId;
            Ativo = true;
            Recursos.Auditoria(this, usuarioId, data);
        }

        public int Id { get; set; }
        public int InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public int TipoEnderecoId { get; set; }
        public virtual TipoEndereco TipoEndereco { get; set; }
        public bool Ativo { get; set; }        
        public DateTime DataCriacao { get; set; }        
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
    }
}