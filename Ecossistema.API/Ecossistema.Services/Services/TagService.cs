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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaPadrao> CadastrarTag(TagDto tag, int usuarioId)
        {
            RespostaPadrao resposta = new RespostaPadrao();
            tag.Descricao = tag.Descricao.ToLower();
            try
            {
                var busca = await _unitOfWork.Tags.FindAsync(x => x.Descricao == tag.Descricao);
                if(busca != null)
                {
                    resposta.SetMensagem("Tag já cadastrada, tente novamente");
                    return resposta;
                }
                var query = new Tag(tag.Descricao, 1, DateTime.Now);
                await _unitOfWork.Tags.AddAsync(query);
                resposta.Retorno = _unitOfWork.Complete() > 0;
                resposta.SetMensagem("Dados gravados com sucesso!");
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message);
            }
            return resposta;
        }

        public async Task<RespostaPadrao> ListarTodas()
        {
            RespostaPadrao resposta = new RespostaPadrao();
            var query = await _unitOfWork.Tags.FindAllAsync(x => x.Ativo);
            var result = query.Select(x => new
            {
                x.Id,
                x.Descricao
            })
            .OrderBy(x => x.Descricao)
            .Distinct()
            .ToList();
            resposta.Retorno = result;
            return resposta;
        }
    }
}
