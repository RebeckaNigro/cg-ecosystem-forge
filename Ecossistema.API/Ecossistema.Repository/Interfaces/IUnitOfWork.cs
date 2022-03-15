using Ecossistema.Data.Repositories;
using Ecossistema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<FaleConosco> FaleConoscos { get; }
        IBaseRepository<FaleConoscoSetor> FaleConoscoSetores { get; }
        IBaseRepository<FaleConoscoSetorContato> FaleConoscoSetoresContatos { get; }
        //IFaleConoscoSetorRepository FaleConoscoSetores { get; }

        int Complete();
    }
}