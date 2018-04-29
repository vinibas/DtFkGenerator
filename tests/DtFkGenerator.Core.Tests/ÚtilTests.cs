using System;
using Xunit;

namespace DtFkGenerator.Core.Tests
{
    public class ÚtilTests
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        [InlineData(10, "10")]
        [InlineData(123456789, "123456789")]
        [InlineData(999999999, "999999999")]
        public void TransformarIntEmArrayDeInt_TransformaIntEmArrayDeInt_True(int entrada, string resultadoEmString)
        {
            var array = Útil.TransformarIntEmArrayDeInt(entrada);

            Assert.Equal(string.Join("", array), resultadoEmString);
        }
    }
}
