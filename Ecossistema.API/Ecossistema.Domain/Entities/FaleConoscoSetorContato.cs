using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Domain.Entities
{
    public class FaleConoscoSetorContato
    {
        public int Id { get; set; }
        public int FaleConoscoSetorId { get; set; }
        public virtual FaleConoscoSetor FaleConoscoSetor { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}