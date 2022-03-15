using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class FaleConoscoService : IFaleConoscoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FaleConoscoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> ObterFaleConosco()
        {
            var query = await _unitOfWork.FaleConoscos.FindAllAsync(x => true, new[] { "FaleConoscoSetor" });

            var result = query.Select(x => new
            {
                id = x.Id,
                nome = x.Nome,
                email = x.Email,
                telefone = x.Telefone,
                empresa = x.Empresa,
                cargo = x.Cargo,
                faleconoscoSetorId = x.FaleConoscoSetorId,
                faleConoscoSetor = x.FaleConoscoSetor.Descricao,
                descricao = x.Descricao
            });

            return result;
        }

        public async Task<object> ObterContatosSetor(int faleConoscoSetorId)
        {
            var query = await _unitOfWork.FaleConoscoSetores.FindAllAsync(x => x.Id == faleConoscoSetorId, new[] { "FaleConoscoSetoresContatos" });

            var result = query.Select(x => new
            {
                id = x.Id,
                descricao = x.Descricao,
                contatos = x.FaleConoscoSetoresContatos.Select(y => new
                {
                    id = y.Id,
                    nome = y.Nome,
                    email = y.Email
                }).Distinct()                
            });

            return result;
        }

        public async Task<bool> Atualizar(FaleConosco obj)
        {
            var item = _unitOfWork.FaleConoscos.GetById(obj.Id);

            item.Nome = obj.Nome;

            _unitOfWork.FaleConoscos.Update(item);

            _unitOfWork.Complete();
        }
    }
}
