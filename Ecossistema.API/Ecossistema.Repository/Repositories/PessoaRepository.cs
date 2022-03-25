using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Repositories
{
    public class PessoaRepository : BaseRepository<FaleConosco>, IPessoaRepository
    {
        private readonly EcossistemaContext _context;

        public PessoaRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }
    }
}