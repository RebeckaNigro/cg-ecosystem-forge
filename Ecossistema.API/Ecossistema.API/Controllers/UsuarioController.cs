using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecossistema.API.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usurioService;

        private int UsuarioId
        {
            get
            {
                return 1;
            }
        }

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usurioService = usuarioService;
        }


    }
}
