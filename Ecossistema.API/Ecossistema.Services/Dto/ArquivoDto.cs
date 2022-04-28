using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Services.Dto
{
    public class ArquivoDto
    {
        public int Id { get; set; }
        public string NomeOriginal { get; set; }
        public string Extensao { get; set; }
        public byte[] Arquivo { get; set; }
    }
}