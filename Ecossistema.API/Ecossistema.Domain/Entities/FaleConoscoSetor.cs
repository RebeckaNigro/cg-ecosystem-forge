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
        public virtual ICollection<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; set; }
    }
}
