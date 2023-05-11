using Ecossistema.Util.Const;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ecossistema.Util
{
    public static class Recursos
    {
        public static T Auditoria<T>(T obj, int usuarioId, DateTime? data = null)
        {
            try
            {
                var dataAtual = data ?? DateTime.Now;

                Type t = obj.GetType();

                var id = (int?)null;

                try
                {
                    var propId = obj.GetType().GetProperty("Id");

                    if (propId != null) id = propId.GetValue(obj, null).ToInt32();
                }
                catch
                {
                    id = null;
                }

                foreach (var propriedade in t.GetProperties())
                {
                    var valor = propriedade.GetValue(obj, null);

                    switch (propriedade.Name)
                    {
                        case "DataCriacao":
                            if (id == null && valor == null || id <= 0) propriedade.SetValue(obj, dataAtual, null);
                            break;
                        case "UsuarioCriacaoId":
                            if (id == null && valor == null || id <= 0) propriedade.SetValue(obj, usuarioId, null);
                            break;
                        case "DataOperacao":
                            propriedade.SetValue(obj, dataAtual, null);
                            break;
                        case "UsuarioOperacaoId":
                            propriedade.SetValue(obj, usuarioId, null);
                            break;
                        case "NaturezaOperacao":
                            propriedade.SetValue(obj, id <= 0 || id == null && valor == null ? ENaturezaOperacao.I.ToString() : ENaturezaOperacao.A.ToString(), null);
                            break;
                        default:
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
            return obj;
        }

        public static List<T> Auditoria<T>(List<T> obj, int usuarioId)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                Auditoria(obj[i], usuarioId);
            }
            return obj;
        }

        public static List<T> Auditoria<T>(ICollection<T> obj, int usuarioId)
        {
            var list = obj.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Auditoria(list[i], usuarioId);
            }
            return obj.ToList();
        }

        public static List<T> Auditoria<T>(IList<T> obj, int usuarioId)
        {
            var list = obj.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Auditoria(list[i], usuarioId);
            }
            return obj.ToList();
        }

        public static List<T> Auditoria<T>(IEnumerable<T> obj, int usuarioId)
        {
            var list = obj.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Auditoria(list[i], usuarioId);
            }
            return obj.ToList();
        }
    }
}