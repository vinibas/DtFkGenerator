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
    public partial class TextoResultado : UserControl
    {
        public TextoResultado()
            => InitializeComponent();

        public string Texto { get => txtTextoResultado.Text; private set => txtTextoResultado.Text = value; }

        public IEnumerable<string> TextoLista { get => Texto.Split('\n'); set => Texto = string.Join("\n", value); }
    }
}
