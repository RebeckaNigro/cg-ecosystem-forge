using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Endereco
    {
        public Endereco()
        {
            Eventos = new HashSet<Evento>();
            PessoasEnderecos = new HashSet<PessoaEndereco>();
            HistoricoEventos = new HashSet<HistoricoEvento>();
        }

        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string PontoReferencia { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
        public virtual ICollection<PessoaEndereco> PessoasEnderecos { get; set; }
        public virtual ICollection<HistoricoEvento> HistoricoEventos { get; set; }
    }
}