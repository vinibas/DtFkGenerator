using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DtFkGenerator.Core.Models
{
    public static class Cpf
    {
        public static string Gerar(bool formatado)
        {
            var cpfSemDigitoVerificador = GerarDigitosCpfSemDígitoVerificador();
            var dígitoVerificador = ObterDígitoVerificador(cpfSemDigitoVerificador);

            var resultado = string.Join("", cpfSemDigitoVerificador) + string.Join("", dígitoVerificador);

            if (formatado)
                resultado = Formatar(resultado, formatado);

            return resultado;
        }

        private static int[] GerarDigitosCpfSemDígitoVerificador()
        {
            int[] cpfSemDigitoVerificador;
            bool TodosOsDigitosSaoIguais;
            do
            {
                // os 8 primeiros dígitos de um cpf são números aleatórios, e o 9ª é o código da região que pode ir de 0 a 9
                cpfSemDigitoVerificador = Útil.TransformarIntEmArrayDeInt(Útil.RandomSingle.Next(100000000, 999999998));
                // Não é permitido em CPF números sequenciais como 111111111, 222222222 [...] 999999999
                TodosOsDigitosSaoIguais = cpfSemDigitoVerificador.Distinct().Count() == 1;
            }
            while (TodosOsDigitosSaoIguais);

            return cpfSemDigitoVerificador;
        }

        private static int[] ObterDígitoVerificador(int[] cpfSemDigitoVerificador)
        {
            int resultado = 0;

            foreach (var item in cpfSemDigitoVerificador.Reverse()
                .Select((valor, index) => new {valor, multiplicador = index + 2}))
                resultado += item.valor * item.multiplicador;

            resultado = resultado % 11;

            if (resultado < 2)
                resultado = 0;
            else
                resultado = 11 - resultado;

            if (cpfSemDigitoVerificador.Length == 9)
            {
                var cpfComUmDigitoVerificador = new int[10];
                Array.Copy(cpfSemDigitoVerificador, cpfComUmDigitoVerificador, 9);
                cpfComUmDigitoVerificador[9] = resultado;
                resultado = (resultado * 10) + ObterDígitoVerificador(cpfComUmDigitoVerificador)[1];
            }

            return resultado < 10 ? new[] { 0, resultado } : Útil.TransformarIntEmArrayDeInt(resultado);
        }

        public static string Formatar(string cpf, bool comPontosETraço)
        {
            var regexFormatado = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
            var regexNãoFormatado = @"^\d{11}$";

            if (
                (comPontosETraço && Regex.IsMatch(cpf, regexFormatado))
                ||
                (!comPontosETraço && Regex.IsMatch(cpf, regexNãoFormatado))
                )
                throw new ArgumentException("O dado informado está em um formato inválido para a operação solicitada");

            if (comPontosETraço)
                return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            else
                return cpf.Replace(".", "").Replace("-", "");
        }

        public static bool Validar(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
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
    }
}
