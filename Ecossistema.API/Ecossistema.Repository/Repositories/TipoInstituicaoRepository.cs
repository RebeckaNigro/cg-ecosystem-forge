using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Repositories
{
    public class TipoInstituicaoRepository : BaseRepository<FaleConosco>, ITipoInstituicaoRepository
    {
        private readonly EcossistemaContext _context;

        public TipoInstituicaoRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }
    }
}