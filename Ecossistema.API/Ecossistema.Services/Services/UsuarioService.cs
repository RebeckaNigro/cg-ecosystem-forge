using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RespostaPadrao> Cadastrar(UsuarioCriacaoDto dado)
        {
            /*var resposta = new RespostaPadrao();

            try
            {


                var obj = new Usuario();

     

                _unitOfWork.Complete();


                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }

            return resposta;*/
            throw new NotImplementedException();
        }

        public Task<RespostaPadrao> Editar(UsuarioCriacaoDto dado, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<RespostaPadrao> Excluir(int id)
        {
            throw new NotImplementedException();
        }

    }
}
