using DtFkGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace DtFkGenerator.Core.Tests
{
    public class RunTests
    {
        [Fact]
        public void Gerar_GerarEValidar_RunGeradoÉValido()
        {
            for (var i = 0; i < 50; i++)
            {
                var run = Run.Gerar(false); // TODO: validar formatação do rut
                try
                {
                    Assert.True(Run.Validar(run));
                }
                catch (TrueException ex)
                {
                    throw new Exception("Valor inválido gerado: " + run, ex);
                }
            }
        }

        [Theory]
        [InlineData("165964853")]
        [InlineData("279197682")]
        [InlineData("756373471")]
        [InlineData("859797199")]
        [InlineData("359897324")]
        [InlineData("432317277")]
        [InlineData("371994777")]
        [InlineData("975846768")]
        [InlineData("174947279")]
        [InlineData("931193732")]
        public void Validar_ValidarRunsValidos_RunÉVálido(string run)
            => Assert.True(Run.Validar(run));

        [Theory]
        [InlineData("165964852")]
        [InlineData("279197688")]
        [InlineData("756373476")]
        [InlineData("859797195")]
        [InlineData("359897329")]
        [InlineData("432317270")]
        [InlineData("371994771")]
        [InlineData("975846762")]
        [InlineData("174947276")]
        [InlineData("931193737")]
        public void Validar_ValidarRunsInvalidos_RunNãoÉVálido(string run)
            => Assert.False(Run.Validar(run));
    }
}