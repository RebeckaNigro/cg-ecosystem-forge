using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Repositories
{
    public class HistoricoUsuarioRepository : BaseRepository<FaleConosco>, IHistoricoUsuarioRepository
    {
        private readonly EcossistemaContext _context;

        public HistoricoUsuarioRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }
    }
}