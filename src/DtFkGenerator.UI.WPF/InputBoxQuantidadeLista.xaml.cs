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
using System.Windows.Shapes;

namespace DtFkGenerator.UI.WPF
{
    /// <summary>
    /// Lógica interna para InputBox.xaml
    /// </summary>
    public partial class InputBoxQuantidadeLista : Window
    {
        public delegate void BtnConfirmadoHandler(int quantidade);
        private event BtnConfirmadoHandler BtnConfirmadoEvent;

        public InputBoxQuantidadeLista(BtnConfirmadoHandler btnConfirmadoEvent)
        {
            InitializeComponent();
            BtnConfirmadoEvent += btnConfirmadoEvent;
        }

        private void txtQuantidade_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new System.Text.RegularExpressions.Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }

        private void txtQuantidade_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
                case Key.Enter:
                    Confirmar();
                    break;
            }
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
            => Confirmar();

        private void Confirmar()
        {
            if (int.TryParse(txtQuantidade.Text, out int quantidade))
            {
                BtnConfirmadoEvent(quantidade);
                Close();
            }
            else
                MessageBox.Show(this, "É obrigatório inserir uma quantidade.", "Atenção");
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}
