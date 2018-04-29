using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.Services
{
    public static class NomeMassService
    {
        public static IEnumerable<string> Gerar(
            int quantidade, 
            bool comSobrenome = true, 
            Nome.Sexo sexo = Nome.Sexo.NaoEspecificado)
        {
            var nome = new Nome();

            var resultado = new List<string>();

            for (; quantidade > 0; quantidade--)
                resultado.Add(nome.Gerar(comSobrenome, sexo));

            return resultado;
        }
    }
}
