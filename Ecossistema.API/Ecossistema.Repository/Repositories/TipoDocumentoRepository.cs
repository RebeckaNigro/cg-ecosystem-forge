using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Repositories
{
    public class TipoDocumentoRepository : BaseRepository<FaleConosco>, ITipoDocumentoRepository
    {
        private readonly EcossistemaContext _context;

        public TipoDocumentoRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }
    }
}