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

        public async Task<RespostaPadrao> Cadastrar(UsuarioCriacaoDto dado, int usuarioId)
        {
            var resposta = new RespostaPadrao();

            try
            {


                var obj = new Usuario(usuarioId,1, "b08f34f5-77c1-4a81-87fd-af0193c88fec", DateTime.Now, dado.Cargo, usuarioId, DateTime.Now);
                
                await _unitOfWork.Usuarios.AddAsync(obj);


                _unitOfWork.Complete();


                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message  +" / "+ ex.InnerException );
            }

            return resposta;
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
