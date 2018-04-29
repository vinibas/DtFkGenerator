using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace DtFkGenerator.Core.Tests
{
    public class CpfTests
    {
        [Fact]
        public void Gerar_GerarEValidar_CpfGeradoÉVálido()
        {
            for (var i = 0; i < 50; i++)
            {
                var cpf = Cpf.Gerar(false); // TODO: validar formatação do cpf
                try
                {
                    Assert.True(Cpf.Validar(cpf));
                }
                catch(TrueException ex)
                {
                    throw new Exception("Valor inválido gerado: " + cpf, ex);
                }
            }
        }

        [Fact]
        public void Formatar_FormatarComPontosETraços_ResultadoÉVálido()
        {
            string cpfSemPontosETraço = "66772717309", cpfComPontosETraço = "667.727.173-09";

            var resultado = Cpf.Formatar(cpfSemPontosETraço, true);

            Assert.Equal(resultado, cpfComPontosETraço);
        }

        [Fact]
        public void Formatar_FormatarSemPontosETraços_ResultadoÉVálido()
        {
            string cpfSemPontosETraço = "66772717309", cpfComPontosETraço = "667.727.173-09";

            var resultado = Cpf.Formatar(cpfComPontosETraço, false);

            Assert.Equal(resultado, cpfSemPontosETraço);
        }

        [Theory]
        [InlineData("66772717309")]
        [InlineData("93783675189")]
        [InlineData("37329087475")]
        [InlineData("66434747446")]
        [InlineData("62767259868")]
        [InlineData("48393686865")]
        [InlineData("97189694625")]
        [InlineData("40118573519")]
        [InlineData("12491715988")]
        [InlineData("56564656165")]
        public void Validar_ValidarCpfsValidos_CpfÉVálido(string cpf)
            => Assert.True(Cpf.Validar(cpf));

        [Theory]
        [InlineData("90620797972")]
        [InlineData("81500177600")]
        [InlineData("82179972774")]
        [InlineData("66204209036")]
        [InlineData("32752927091")]
        [InlineData("37618838588")]
        [InlineData("43050834052")]
        [InlineData("97845389410")]
        [InlineData("59723067904")]
        [InlineData("92828956422")]
        public void Validar_ValidarCpfsInvalidos_CpfNãoÉVálido(string cpf)
            => Assert.False(Cpf.Validar(cpf));
    }
}
