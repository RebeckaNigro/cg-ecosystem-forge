namespace Ecossistema.Domain.Entities
{
    public class FaleConoscoSetor
    {
        public FaleConoscoSetor()
        {
            FaleConoscoSetoresContatos = new HashSet<FaleConoscoSetorContato>();
        }
        public int Id { get; set; }
        public string Descricao { get; set; }        
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int UsuarioOperacaoId { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual ICollection<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; set; }
    }
}
