using Ecossistema.Util;
using Ecossistema.Util.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class Arquivo
    {
        public Arquivo()
        {
            ArquivosOrigens = new HashSet<ArquivoOrigem>();
        }

        public Arquivo(EOrigem origem, string nomeOriginal, int usuarioId, DateTime dataAtual, int? id = null)
        {
            Extensao = nomeOriginal.Substring(nomeOriginal.LastIndexOf(".") + 1, nomeOriginal.Length - nomeOriginal.LastIndexOf(".") - 1);
            NomeOriginal = nomeOriginal.Replace("." + Extensao, "");
            Extensao = Extensao.ToLower();
            ArquivosOrigens = new List<ArquivoOrigem>
            {
                new ArquivoOrigem(origem, NomeOriginal, usuarioId, dataAtual, id)
            };
            Ativo = true;
            Recursos.Auditoria(this, usuarioId, dataAtual);
        }

        public int Id { get; set; }
        public string NomeOriginal { get; set; }
        public string Extensao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual ICollection<ArquivoOrigem> ArquivosOrigens { get; set; }
    }
}