using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class UsuarioCriacaoDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Cargo { get; set; }

    }
}
