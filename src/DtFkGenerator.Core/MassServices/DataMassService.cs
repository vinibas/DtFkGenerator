using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.MassServices
{
    public static class DataMassService
    {
        public static IEnumerable<string> Gerar(
            int quantidade,
            Data.DataConfiguraçãoGeração dataConfiguraçãoGeração)
        {
            var resultado = new List<string>();

            for (; quantidade > 0; quantidade--)
                resultado.Add(Data.Gerar(dataConfiguraçãoGeração));

            return resultado;
        }
    }
}
