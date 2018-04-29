using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.Services
{
    public class CpfMassService
    {
        public static IEnumerable<string> Gerar(int quantidade, bool formatado)
        {
            var resultado = new List<string>();

            for (; quantidade > 0; quantidade--)
                resultado.Add(Cpf.Gerar(formatado));

            return resultado;
        }
        
        public static IEnumerable<string> Formatar(IEnumerable<string> cpfs, bool comPontosETraço)
        {
            var resultado = new List<string>();

            foreach (var cpf in cpfs)
                resultado.Add(Cpf.Formatar(cpf, comPontosETraço));

            return resultado;
        }
    }
}
