using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecossistema.Data.Repositories
{
    public class FaleConoscoSetorRepository : BaseRepository<FaleConoscoSetor>, IFaleConoscoSetorRepository
    {
        private readonly EcossistemaContext _context;

        public FaleConoscoSetorRepository(EcossistemaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<object> ObterTodosGenerico()
        {
            return await _context.FaleConoscoSetores.Select(x => new
            {
                id = x.Id,
                descricao = x.Descricao
            }).ToListAsync();
        }
    }
}