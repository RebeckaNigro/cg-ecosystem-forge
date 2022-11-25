using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Linq;
using Ecossistema.Util;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class Extensoes
    {
        //teste
        #region Strings
        public static int ToInt32(this string texto)
        {
            int i;
            return Int32.TryParse(texto, out i) ? i : 0;
        }

        public static int? ToNullInt32(this string texto)
        {
            int i;
            return Int32.TryParse(texto, out i) ? i : new int?();
        }

        public static Int64 ToInt64(this string texto)
        {
            Int64 i;
            return Int64.TryParse(texto, out i) ? i : 0;
        }

        public static long? ToNullInt64(this string texto)
        {
            Int64 i;
            return Int64.TryParse(texto, out i) ? i : new long?();
        }

        public static string ToMoney(this decimal val)
        {
            return val.ToString("c");
        }

        public static string ToMoney(this decimal? val)
        {
            if (val.HasValue)
                return val.Value.ToString("c");
            else
                return "R$ 0.00";
        }

        public static DateTime ToDateTime(this string texto)
        {
            DateTime d;
            return DateTime.TryParse(texto, out d) ? d : DateTime.MinValue;
        }

        public static DateTime? ToNullDateTime(this string texto)
        {
            DateTime d;
            return DateTime.TryParse(texto, out d) ? d : new DateTime?();
        }

        public static decimal ToDecimal(this string texto)
        {
            decimal i;
            return Decimal.TryParse(texto, out i) ? i : 0;
        }

        public static decimal? ToNullDecimal(this string texto)
        {
            decimal i;
            return Decimal.TryParse(texto, out i) ? i : new decimal?();
        }

        public static string TrimNull(this string texto)
        {
            return string.IsNullOrEmpty(texto) ? null : texto.Trim();
        }

        public static string TrimNullUp(this string texto)
        {
            return string.IsNullOrEmpty(texto) ? null : texto.Trim().ToUpper();
        }

        public static int? ToHora(this string texto)
        {
            return texto.Split(':')[0].ToInt32();
        }

        public static int? ToMinuto(this string texto)
        {
            return texto.Split(':')[1].ToInt32();
        }

        #endregion Strings

        #region Int32

        public static string ToNullString(this int? valor)
        {
            return valor.HasValue ? valor.Value.ToString() : null;
        }

        #endregion Int32

        #region Decimal

        public static string To2fDecimal(this decimal? val)
        {
            if (val.HasValue)
                return val.Value.ToString("0.00");
            else
                return null;
        }

        /// <summary>
        /// Despreza os valores que estão depois das 2 casas decimais sem arredondamento. Ex: 12.2896 => 12.28
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Decimal ToDecimalMoney(this decimal val)
        {
            return ((decimal)((int)(Math.Round(val, 2) * 100))) / 100m;

        }

        #endregion Decimal

        #region Double

        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double ToDouble(this string texto)
        {
            double i;
            return double.TryParse(texto, out i) ? i : 0;
        }

        public static double ToCoordinate(this string texto)
        {
            double i;
            return string.IsNullOrWhiteSpace(texto) ? 0 : (double.TryParse(texto.Replace(".", ","), out i) ? i : 0);
        }

        #endregion Double

        #region Float

        public static float ToFloat(this string texto)
        {
            float i;
            return float.TryParse(texto, out i) ? i : 0;
        }

        #endregion

        #region Objects
        public static int ToInt32(this object obj)
        {
            int i;
            return Int32.TryParse(Convert.ToString(obj), out i) ? i : 0;
        }

        public static int? ToNullInt32(this object obj)
        {
            int i;
            return Int32.TryParse(Convert.ToString(obj), out i) ? i : new int?();
        }

        public static Int64 ToInt64(this object obj)
        {
            Int64 i;
            return Int64.TryParse(Convert.ToString(obj), out i) ? i : 0;
        }

        public static long ToLong(this object obj)
        {
            long i;
            return long.TryParse(Convert.ToString(obj), out i) ? i : 0;
        }

        public static long? ToNullInt64(this object obj)
        {
            Int64 i;
            return Int64.TryParse(Convert.ToString(obj), out i) ? i : new long?();
        }

        public static decimal ToDecimal(this object obj)
        {
            decimal d;
            return decimal.TryParse(Convert.ToString(obj), out d) ? d : 0;
        }

        public static decimal? ToNullDecimal(this object obj)
        {
            decimal d;
            return decimal.TryParse(Convert.ToString(obj), out d) ? d : new decimal?();
        }

        public static string ToFormLabel(this object obj)
        {
            if (obj is string)
                return string.IsNullOrEmpty(obj.ToString()) ? "--" : obj.ToString();
            else
                return obj?.ToString() ?? "--";
        }
        #endregion Objects

        #region Enums
        public static string StrVal(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
        public static int Int32Val(this Enum value)
        {

            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            Int32ValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(Int32ValueAttribute), false) as Int32ValueAttribute[];

            return attribs.Length > 0 ? attribs[0].Int32Value : -1;
        }
        public static bool In(this Enum value, params Enum[] objects)
        {
            return objects.Any(o => o.ToString() == value.ToString());
        }
        #endregion Enums

        #region DateTime

        public static string ToNullDateTime(this DateTime? data, string formato = null)
        {
            if (data.HasValue && formato == null)
                return data.Value.ToString();
            else if (data.HasValue && formato != null)
                return data.Value.ToString(formato);
            else
                return null;
        }

        #endregion DateTime

        #region Exclusivos

        public static string RecuperarMsgExcecaoAninhada(this Exception ex)
        {
            if (ex.InnerException != null)
                return ex.InnerException.RecuperarMsgExcecaoAninhada();
            return ex.Message;
        }


        #endregion Exclusivos

        #region ICollection

        public static string ToJson(this object col)
        {
            return JsonConvert.SerializeObject(col, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        #endregion ICollection
    }
}
