using Ecossistema.Data.Interfaces;
using Ecossistema.Data.Repositories;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcossistemaContext _context;

        public IBaseRepository<FaleConosco> FaleConoscos { get; private set; }
        public IBaseRepository<FaleConoscoSetor> FaleConoscoSetores { get; private set; }
        public IBaseRepository<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; private set; }
        //public IFaleConoscoSetorRepository FaleConoscoSetores { get; private set; }

        public UnitOfWork(EcossistemaContext context)
        {
            _context = context;

            FaleConoscos = new BaseRepository<FaleConosco>(_context);
            FaleConoscoSetores = new BaseRepository<FaleConoscoSetor>(_context);
            FaleConoscoSetoresContatos = new BaseRepository<FaleConoscoSetorContato>(_context);
            //FaleConoscoSetores = new FaleConoscoSetorRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}