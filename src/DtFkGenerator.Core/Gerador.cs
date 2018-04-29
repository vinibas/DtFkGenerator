using DtFkGenerator.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DtFkGenerator.Core
{
    public static class Gerador
    {
        //public static IEnumerable<Guid> GerarGuid(int quantidade = 1)
        //    => GuidModel.Gerar(quantidade);

        //public static IEnumerable<string> GerarNome(int quantidade = 1)
        //    => new Nome().Gerar(quantidade);

        public static IEnumerable<DateTime> GerarData(int quantidade = 1, bool dataAtual = false)
            => new List<DateTime> { DateTime.Now };

        //public static IEnumerable<string> GerarCpf(int quantidade = 1, bool formatado = false)
        //    => Cpf.Gerar(quantidade, formatado);

        //public static IEnumerable<string> FormatarCpf(IEnumerable<string> cpf, bool comPontosETraço)
        //    => Cpf.Formatar(cpf, comPontosETraço);

        //public static IEnumerable<string> GerarRun(int quantidade = 1, bool formatado = false)
        //    => Run.Gerar(quantidade, formatado);
    }
}
