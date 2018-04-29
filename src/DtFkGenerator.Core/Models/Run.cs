using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DtFkGenerator.Core.Models
{
    public static class Run
    {
        public static string Gerar(bool formatado)
        {
            var runSemDigitoVerificador = Útil.TransformarIntEmArrayDeInt(Útil.RandomSingle.Next(10000000, 99999999));

            var digitoVerificador = ObterDígitoVerificador(runSemDigitoVerificador);

            var resultado = string.Join("", runSemDigitoVerificador) + digitoVerificador;

            if (formatado)
                resultado = resultado.Insert(2, ".").Insert(6, ".").Insert(10, "-");

            return resultado;
        }

        private static char ObterDígitoVerificador(int[] runSemDigitoVerificador)
        {
            int resultado = 0;
            for (int i = 7, m = 2; i >= 0; i--, m = m != 7 ? m + 1 : 2)
                resultado += runSemDigitoVerificador[i] * m;

            resultado = AplicarMódulo11(resultado);

            return TransformarModulo11EmDigitoVerificador(resultado);
        }

        private static char TransformarModulo11EmDigitoVerificador(int valor)
        {
            switch (valor)
            {
                case 11:
                    return '0';
                case 10:
                    return 'K';
                default:
                    return valor.ToString()[0];
            }
        }

        private static int AplicarMódulo11(int valor)
        {
            var resultado = valor / 11;
            resultado = valor - (11 * resultado);

            return 11 - resultado;
        }

        public static string Formatar(string cpf, bool comPontosETraço)
        {
            var regexFormatado = @"^\d{2}\.\d{3}\.\d{3}-\d{1}$";
            var regexNãoFormatado = @"^\d{9}$";

            if (
                (comPontosETraço && Regex.IsMatch(cpf, regexFormatado))
                ||
                (!comPontosETraço && Regex.IsMatch(cpf, regexNãoFormatado))
                )
                throw new ArgumentException("O dado informado está em um formato inválido para a operação solicitada");

            if (comPontosETraço)
                return cpf.Insert(2, ".").Insert(6, ".").Insert(10, "-");
            else
                return cpf.Replace(".", "").Replace("-", "");
        }

        public static bool Validar(string run)
        {
            Regex expresion = new Regex("^([0-9]+[0-9K])$");
            string dv = run.Substring(run.Length - 1, 1);
            if (!expresion.IsMatch(run))
            {
                return false;
            }
            string[] rutTemp = new[] { run.Substring(0, 8), run[8].ToString() };
            if (dv != ObterDigitoVerificador(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;

            string ObterDigitoVerificador(int runSemDigito)
            {
                int suma = 0;
                int multiplicador = 1;
                while (runSemDigito != 0)
                {
                    multiplicador++;
                    if (multiplicador == 8)
                        multiplicador = 2;
                    suma += (runSemDigito % 10) * multiplicador;
                    runSemDigito = runSemDigito / 10;
                }
                suma = 11 - (suma % 11);
                if (suma == 11)
                {
                    return "0";
                }
                else if (suma == 10)
                {
                    return "K";
                }
                else
                {
                    return suma.ToString();
                }
            }
        }
    }
}
