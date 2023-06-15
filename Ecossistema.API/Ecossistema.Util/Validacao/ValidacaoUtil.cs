using System.Globalization;
using System.Text.RegularExpressions;

namespace Ecossistema.Util.Validacao
{
    public class ValidacaoUtil
    {
        public static bool ValidaLogin(int loginId, string login)
        {
            if (loginId == 1) // Email
            {
                return ValidaEMail(login);
            }
            else
            {
                return false;
            }
        }

        public static bool ValidaEMail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normaliza o domínio
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examina a parte domínio do email e o normaliza
                string DomainMapper(Match match)
                {
                    // IdnMapping para converter nomes de domínio Unicode
                    var idn = new IdnMapping();

                    // Tira e processo o nome de domínio
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool ValidaNumeroDocumento(string numeroDocumento)
        {
            bool valido = true;

            try
            {
                valido = ValidaCpf(numeroDocumento) || ValidaCnpj(numeroDocumento);

                return valido;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaTelefone(string telefone)
        {
            Regex Rgx = new Regex(@"^(\(?\d{2}\)?) ?9?\d{4}-?\d{4}$"); //Regex(@"^\(\d{2}\)\d{5}-\d{4}$"); //formato (XX)XXXXX-XXXX

            if (!Rgx.IsMatch(telefone))
                return false;
            else
                return true;
        }

        public static bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            if (!cpf.All(Char.IsDigit))
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            if (!cnpj.All(Char.IsDigit))
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool ValidarString(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return false;
            return true;
        }

        public static bool ValidarTamanhoString(string valor, int tamanho)
        {
            if (valor != null && valor.Length > tamanho) return false;
            return true;
        }

        public static bool ValidarInteiro(int? valor)
        {
            return valor != null;
        }

        public static bool ValidarInteiroValido(int? valor)
        {
            if (!ValidarInteiro(valor) || valor <= 0) return false;
            return true;
        }

        public static bool ValidarLogico(bool? valor)
        {
            return valor != null;
        }

        public static bool ValidarDouble(double? valor)
        {
            return valor != null;
        }

        public static bool ValidarDoubleValido(double? valor)
        {
            if (!ValidarDouble(valor) || valor <= 0) return false;
            return true;
        }

        public static bool ValidarFloat(float? valor)
        {
            return valor != null;
        }

        public static bool ValidarFloatValido(float? valor)
        {
            if (!ValidarFloat(valor) || valor <= 0) return false;
            return true;
        }

        public static bool ValidarFloatMaiorIgualZero(float? valor)
        {
            if (!ValidarFloat(valor) || valor < 0) return false;
            return true;
        }

        public static bool ValidarData(DateTime? valor)
        {
            try
            {
                var data = (DateTime)valor;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}