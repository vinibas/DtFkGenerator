using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.Models
{
    public static class Data
    {
        public static string Gerar(DataConfiguraçãoGeração dataConfiguraçãoGeração)
        {
            DateTime resultado;

            if (dataConfiguraçãoGeração.Tipo == DataConfiguraçãoGeração.TipoGeração.DataAtual)
                resultado = DateTime.Now;
            else if (dataConfiguraçãoGeração.Tipo == DataConfiguraçãoGeração.TipoGeração.DefinidoPeloUsuário)
                resultado = dataConfiguraçãoGeração.InseridoPeloUsuário;
            else
                resultado = GerarDataAleatória(dataConfiguraçãoGeração.Posição);

            return Formatar(resultado, dataConfiguraçãoGeração.Formato, dataConfiguraçãoGeração.FormatoPersonalizado);
        }

        private static DateTime GerarDataAleatória(DataConfiguraçãoGeração.PosiçãoTemporal posiçãoTemporal)
        {
            DateTime resultado;
            var numAleat = Útil.RandomSingle.Next();

            int multiplicador;

            if (posiçãoTemporal == DataConfiguraçãoGeração.PosiçãoTemporal.Futuro)
                multiplicador = 1;
            else if (posiçãoTemporal == DataConfiguraçãoGeração.PosiçãoTemporal.Passado)
                multiplicador = -1;
            else if (posiçãoTemporal == DataConfiguraçãoGeração.PosiçãoTemporal.Indefinido)
                multiplicador = Útil.RandomSingle.Next() % 2 == 0 ? -1 : 1;
            else
                throw new InvalidOperationException();

            resultado = DateTime.Today.AddSeconds(numAleat * multiplicador);
            return resultado;
        }

        private static string Formatar(DateTime data, DataConfiguraçãoGeração.FormatoString formato, string formatoPersonalizado)
        {
            switch (formato)
            {
                case DataConfiguraçãoGeração.FormatoString.DataHora:
                    formatoPersonalizado = "yyyy-MM-dd hh:mm:ss";
                    break;
                case DataConfiguraçãoGeração.FormatoString.Data:
                    formatoPersonalizado = "yyyy-MM-dd";
                    break;
                case DataConfiguraçãoGeração.FormatoString.Hora:
                    formatoPersonalizado = "hh:mm:ss";
                    break;
            }

            return formatoPersonalizado != null ? data.ToString(formatoPersonalizado) : data.ToString();
        }
        
        public class DataConfiguraçãoGeração
        {
            public enum TipoGeração
            {
                DataAtual,
                Aleatória,
                DefinidoPeloUsuário
            }

            public enum PosiçãoTemporal
            {
                Indefinido,
                Passado,
                Futuro
            }

            public enum FormatoString
            {
                DataHora,
                Data,
                Hora,
                Livre
            }

            public DataConfiguraçãoGeração(TipoGeração tipo,
            PosiçãoTemporal posiçãoTemporal,
            FormatoString formato,
            string formatoPersonalizado = null)
            {
                Tipo = tipo;
                Posição = posiçãoTemporal;
                Formato = formato;
                FormatoPersonalizado = formatoPersonalizado;
            }

            public DataConfiguraçãoGeração(
                DateTime inseridoPeloUsuário,
                FormatoString formato, 
                string formatoPersonalizado = null)
            {
                Tipo = TipoGeração.DefinidoPeloUsuário;
                InseridoPeloUsuário = inseridoPeloUsuário;
                Formato = formato;
                FormatoPersonalizado = formatoPersonalizado;
            }

            public TipoGeração Tipo { get; private set; }
            public PosiçãoTemporal Posição { get; private set; }
            public FormatoString Formato { get; private set; }
            public string FormatoPersonalizado { get; private set; }
            public DateTime InseridoPeloUsuário { get; private set; }
        }
    }
}
