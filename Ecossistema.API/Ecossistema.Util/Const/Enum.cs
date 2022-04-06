using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Util.Const
{
    public enum ENaturezaOperacao
    {
        [Int32Value(1)]
        [StringValue("Inclusão")]
        I = 1,
        [Int32Value(2)]
        [StringValue("Alteração")]
        A = 2,
        [Int32Value(3)]
        [StringValue("Exclusão")]
        E = 3
    }

    public enum EOrigem
    {
        [Int32Value(1)]
        [StringValue("Usuário")]
        Usuario = 1,
        [Int32Value(2)]
        [StringValue("Parceiro")]
        Parceiro = 2,
        [Int32Value(3)]
        [StringValue("Documento")]
        Documento = 3,
        [Int32Value(4)]
        [StringValue("Notícia")]
        Noticia = 4,
        [Int32Value(5)]
        [StringValue("Evento")]
        Evento = 5,
        [Int32Value(6)]
        [StringValue("Página")]
        Pagina = 6
    }

    public enum ESituacaoAprovacao
    {
        [Int32Value(1)]
        [StringValue("Pendente")]
        Pendente = 1,
        [Int32Value(2)]
        [StringValue("Aprovado")]
        Aprovado = 2,
        [Int32Value(3)]
        [StringValue("Não Aprovado")]
        NaoAprovado = 3,
    }
}