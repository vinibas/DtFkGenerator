using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace DtFkGenerator.Core.Tests
{
    public class NomeTests
    {
        NomeMock nome = new NomeMock();

        [Fact]
        public void CarregarNomesDoArquivo_VerificarSeSãoOEsperado_True()
        {
            Assert.True(ListasSãoIguais(nome.nomesMasculinosEsperados, nome.GetNomesMasculinos));
            Assert.True(ListasSãoIguais(nome.nomesFemininosEsperados, nome.GetNomesFemininos));
            Assert.True(ListasSãoIguais(nome.sobrenomesEsperados, nome.GetSobrenomes));
        }
        
        private bool ListasSãoIguais(IList<string> lista1, IList<string> lista2)
        {
            if (lista1.Count != lista2.Count)
                return false;

            for (var i = 0; i < lista1.Count; i++)
                if (lista1[i] != lista2[i])
                    return false;

            return true;
        }

        [Fact]
        public void Gerar_GeraPadrão_GeradoCorretamente()
        {
            var nomeGerado = nome.Gerar();

            var primeiroNome = nome.nomesMasculinosEsperados
                .Union(nome.nomesFemininosEsperados)
                .FirstOrDefault(p => nomeGerado.StartsWith(p));
            
            var sobrenome = nome.sobrenomesEsperados
                .FirstOrDefault(p => nomeGerado.Replace(primeiroNome + " ", "") == p);

            Assert.NotNull(primeiroNome);
            Assert.NotNull(sobrenome);
        }

        class NomeMock : Nome
        {
            public IList<string> GetNomesMasculinos { get => nomesMasculinos; }
            public IList<string> GetNomesFemininos { get => nomesFemininos; }
            public IList<string> GetSobrenomes { get => sobrenomes; }

            public IList<string> nomesMasculinosEsperados = new List<string> { "João", "José", "Mário", "Maurício" };
            public IList<string> nomesFemininosEsperados = new List<string> { "Márcia", "Isaura", "Ivone", "Josefina" };
            public IList<string> sobrenomesEsperados = new List<string> { "da Silva", "Álvares", "Oliveira", "das Dores" };

            private string nomesMock =
                "#NOMES_MASCULINOS" + "\n" +
                "João" + "\n" +
                "José" + "\n" +
                "Mário" + "\n" +
                "Maurício" + "\n" +
                "" + "\n" +
                "#NOMES_FEMININOS" + "\n" +
                "Márcia" + "\n" +
                "Isaura" + "\n" +
                "Ivone" + "\n" +
                "Josefina" + "\n" +
                "" + "\n" +
                "#SOBRENOMES" + "\n" +
                "da Silva" + "\n" +
                "Álvares" + "\n" +
                "Oliveira" + "\n" +
                "das Dores";

            public override StreamReader ObterStreamDeArquivo()
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(nomesMock);
                writer.Flush();
                stream.Position = 0;

                return new StreamReader(stream);
            }
        }
    }
}
