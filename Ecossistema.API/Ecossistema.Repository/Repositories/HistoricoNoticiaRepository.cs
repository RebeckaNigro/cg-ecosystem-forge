using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Repositories
{
    public class HistoricoNoticiaRepository : BaseRepository<FaleConosco>, IHistoricoNoticiaRepository
    {
        private readonly EcossistemaContext _context;

        public HistoricoNoticiaRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }
    }
}