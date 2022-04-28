using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class RedefinirSenhaDto
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }   
    }
}
