using Ecossistema.Data.Interfaces;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;

namespace Ecossistema.Services.Services
{
    public class FaleConoscoSetorService : IFaleConoscoSetorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FaleConoscoSetorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RespostaPadrao> ObterTodosFaleConoscoSetor()
        {
            RespostaPadrao resposta = new RespostaPadrao();

            try
            {
                var query = await _unitOfWork.FaleConoscoSetores.FindAllAsync(x => x.Ativo == true);

                var result = query.Select(x => new
                {
                    id = x.Id,
                    descricao = x.Descricao
                });

                resposta.Retorno = result;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
                return resposta;
            }
        }
    }
}
