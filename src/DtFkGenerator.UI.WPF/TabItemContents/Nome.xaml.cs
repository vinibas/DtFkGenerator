using DtFkGenerator.Core;
using DtFkGenerator.Core.Services;
using DtFkGenerator.UI.WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sexo = DtFkGenerator.Core.Models.Nome.Sexo;

namespace DtFkGenerator.UI.WPF.TabItemContents
{
    /// <summary>
    /// Interação lógica para Guid.xam
    /// </summary>
    public partial class Nome : UserControl, IControlGerador
    {
        public Nome()
            => InitializeComponent();

        public string ResultadoGerado { get => ucTextoResultado.Texto; }

        public void GerarNovo(int quantidade)
        {
            ucTextoResultado.TextoLista = NomeMassService.Gerar(quantidade, ObterValorComSobrenome(), ObterValorRadioSexo());
        }

        public Sexo ObterValorRadioSexo()
        {
            if (rbSexo_Ambos.IsChecked.Value)
                return Sexo.NaoEspecificado;
            else if (rbSexo_Masculino.IsChecked.Value)
                return Sexo.Masculino;
            else if (rbSexo_Feminino.IsChecked.Value)
                return Sexo.Feminino;
            else
                return Sexo.NaoEspecificado;
        }

        public bool ObterValorComSobrenome()
            => rbComSobrenome_NomeESobrenome.IsChecked.Value;
    }
}
