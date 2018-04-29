using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.Services
{
    public class GuidMassService
    {
        public static IEnumerable<Guid> Gerar(int quantidade)
        {
            var resultado = new List<Guid>();

            for (; quantidade > 0; quantidade--)
                resultado.Add(Guid.NewGuid());

            return resultado;
        }

    }
}
