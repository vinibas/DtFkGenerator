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

namespace DtFkGenerator.UI.WPF.TabItemContents
{
    /// <summary>
    /// Interação lógica para Guid.xam
    /// </summary>
    public partial class Cpf : UserControl, IControlGerador
    {
        public Cpf()
            => InitializeComponent();

        public string ResultadoGerado { get => ucTextoResultado.Texto; }

        public void GerarNovo(int quantidade)
            => ucTextoResultado.TextoLista = CpfMassService.Gerar(quantidade, chkFormatado.IsChecked.Value);

        private void chkFormatado_Click(object sender, RoutedEventArgs e)
            => ucTextoResultado.TextoLista = CpfMassService.Formatar(ucTextoResultado.TextoLista, chkFormatado.IsChecked.Value);
    }
}
