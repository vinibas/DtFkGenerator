using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DtFkGenerator.Core.Models
{
    public class Nome
    {
        public Nome()
            => CarregarNomesDoArquivo();

        protected IList<string> nomesMasculinos;
        protected IList<string> nomesFemininos;
        protected IList<string> sobrenomes;

        public enum Sexo
        {
            Feminino,
            Masculino,
            NaoEspecificado
        }
        
        public string Gerar(bool comSobrenome = true, Sexo sexo = Sexo.NaoEspecificado)
        {
            IList<string> nomesDisponíveis = null;
            switch (sexo)
            {
                case Sexo.Feminino:
                    nomesDisponíveis = nomesFemininos;
                    break;
                case Sexo.Masculino:
                    nomesDisponíveis = nomesMasculinos;
                    break;
                case Sexo.NaoEspecificado:
                    nomesDisponíveis = nomesFemininos.Union(nomesMasculinos).ToList();
                    break;
            }
            
            var nomeRandom = nomesDisponíveis[Útil.RandomSingle.Next(0, nomesDisponíveis.Count)];
            var sobrenomeRandom = comSobrenome ? (" " + sobrenomes[Útil.RandomSingle.Next(0, sobrenomes.Count)]) : "";

            return nomeRandom + sobrenomeRandom;
        }

        public void CarregarNomesDoArquivo()
        {
            const string seçãoNomesMasculinos = "#NOMES_MASCULINOS";
            const string seçãoNomesFemininos = "#NOMES_FEMININOS";
            const string seçãoSobrenomes = "#SOBRENOMES";

            nomesMasculinos = new List<string>();
            nomesFemininos = new List<string>();
            sobrenomes = new List<string>();

            StreamReader stream = ObterStreamDeArquivo();
            try
            {
                string seçãoAtual = null;

                string linha = stream.ReadLine();
                while (linha != null)
                {
                    if (linha == seçãoNomesMasculinos || linha == seçãoNomesFemininos || linha == seçãoSobrenomes)
                        seçãoAtual = linha;
                    else if (!string.IsNullOrWhiteSpace(linha))
                    {
                        if (seçãoAtual == seçãoNomesMasculinos)
                            nomesMasculinos.Add(linha);
                        else if (seçãoAtual == seçãoNomesFemininos)
                            nomesFemininos.Add(linha);
                        else if (seçãoAtual == seçãoSobrenomes)
                            sobrenomes.Add(linha);
                    }

                    linha = stream.ReadLine();
                }
            }
            finally
            {
                stream.Close();
            }
        }

        public virtual StreamReader ObterStreamDeArquivo()
        {
            return File.OpenText("Nomes.txt");
        }
    }
}
