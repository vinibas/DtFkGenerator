using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.Services
{
    public class RunMassService
    {
        public static IEnumerable<string> Gerar(int quantidade, bool formatado)
        {
            var resultado = new List<string>();

            for (; quantidade > 0; quantidade--)
                resultado.Add(Run.Gerar(formatado));

            return resultado;
        }

        public static IEnumerable<string> Formatar(IEnumerable<string> runs, bool comPontosETraço)
        {
            var resultado = new List<string>();

            foreach (var run in runs)
                resultado.Add(Run.Formatar(run, comPontosETraço));

            return resultado;
        }
    }
}
