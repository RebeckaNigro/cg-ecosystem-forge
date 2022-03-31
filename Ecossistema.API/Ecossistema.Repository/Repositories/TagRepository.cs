using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Repositories
{
    public class TagRepository : BaseRepository<FaleConosco>, ITagRepository
    {
        private readonly EcossistemaContext _context;

        public TagRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }
    }
}