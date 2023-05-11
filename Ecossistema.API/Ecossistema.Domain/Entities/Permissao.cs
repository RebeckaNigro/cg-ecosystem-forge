//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ecossistema.Domain.Entities
//{
//    public class Permissao
//    {
//        public Permissao()
//        {
//            Usuarios = new HashSet<Usuario>();
//            HistoricoUsuarios = new HashSet<HistoricoUsuario>();
//        }

//        public int Id { get; set; }
//        public string Descricao { get; set; }
//        public bool Ativo { get; set; } = true;
//        public DateTime DataCriacao { get; set; }
//        public int UsuarioCriacaoId { get; set; }
//        public virtual Usuario UsuarioCriacao { get; set; }
//        public string NaturezaOperacao { get; set; }
//        public DateTime DataOperacao { get; set; }
//        public int UsuarioOperacaoId { get; set; }
//        public virtual Usuario UsuarioOperacao { get; set; }
//        public virtual ICollection<Usuario> Usuarios { get; set; }
//        public virtual ICollection<HistoricoUsuario> HistoricoUsuarios { get; set; }
//    }
//}