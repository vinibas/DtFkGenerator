using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core
{
    public static class Útil
    {
        public static int[] TransformarIntEmArrayDeInt(int n)
        {
            var length = n.ToString().Length;

            int[] resultado = new int[length];

            for (int i = length - 1; n != 0; i--)
            {
                resultado[i] = n % 10;
                n = n / 10;
            }

            return resultado;
        }

        private static Lazy<Random> _randomSingle = new Lazy<Random>();
        public static Random RandomSingle { get => _randomSingle.Value; }
    }
}
