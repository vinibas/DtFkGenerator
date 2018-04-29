using DtFkGenerator.Core;
using DtFkGenerator.UI.WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /*
        Olá, esse software foi desenvolvido por mim como um utilitário para auxiliar no desenvolvimento de outros softwares,
        e também como uma pequena demonstração da minha codificação. Porém foi o primeiro aplicativo em WPF que desenvolvi,
        minha experiência de fato se dá com aplicações Web. Se tiver sugestões de melhorias, fique à vontade para me enviar.
        Para mim é importante ouvir críticas e sugestões, como forma de aprendizado em como melhorar.
        Também estou aberto a sugestões de funcionalidades ou melhoria na usabilidade.
     */
namespace DtFkGenerator.UI.WPF
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public IControlGerador ControleGeradorSelecionado
            { get => (IControlGerador)((TabItem)tabControlMain.SelectedItem).Content; }

        public MainWindow()
            => InitializeComponent();

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            GerarNovo();
        }
        
        // TODO: Subir no bitbucket
        // TODO: Colocar barra de status para "copiado" ou "gerado"

        private void btnCopiar_Click(object sender, RoutedEventArgs e)
            => Copiar();

        private void btnGerarNovo_Click(object sender, RoutedEventArgs e)
            => GerarNovo();

        private void btnGerarLista_Click(object sender, RoutedEventArgs e)
            => GerarLista();

        private void btnFechar_Click(object sender, RoutedEventArgs e)
         => Fechar();

        private void Copiar()
            => Clipboard.SetText(ControleGeradorSelecionado.ResultadoGerado);

        private void GerarNovo()
            => ControleGeradorSelecionado.GerarNovo(1);

        private void GerarLista()
        {
            var InputBoxQuantidadeLista = new InputBoxQuantidadeLista(InputBoxQuantidadeListaConfirmarClick);
            InputBoxQuantidadeLista.Owner = this;
            InputBoxQuantidadeLista.ShowDialog();
        }

        private void InputBoxQuantidadeListaConfirmarClick(int quantidade)
            => ControleGeradorSelecionado.GerarNovo(quantidade);

        private void Fechar()
         => Close();

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.C:
                    Copiar();
                    break;
                case Key.N:
                case Key.G:
                    GerarNovo();
                    break;
                case Key.L:
                    GerarLista();
                    break;
                case Key.F:
                case Key.Escape:
                    Fechar();
                    break;

                case Key.D1:
                case Key.NumPad1:
                    tabItemGuid.IsSelected = true;
                    break;
                case Key.D2:
                case Key.NumPad2:
                    tabItemNome.IsSelected = true;
                    break;
                case Key.D3:
                case Key.NumPad3:
                    tabItemData.IsSelected = true;
                    break;
                case Key.D4:
                case Key.NumPad4:
                    tabItemCpf.IsSelected = true;
                    break;
                case Key.D5:
                case Key.NumPad5:
                    tabItemRut.IsSelected = true;
                    break;
                case Key.D6:
                case Key.NumPad6:
                    tabItemSobre.IsSelected = true;
                    break;
            }
        }

        private void HabilitarBotões(bool habilitar)
            => btnCopiar.IsEnabled = btnGerarNovo.IsEnabled = btnGerarLista.IsEnabled = habilitar;
        
        private void tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HabilitarBotões(((TabItem)tabControlMain.SelectedItem).Name != tabItemSobre.Name);

            if (btnGerarNovo.IsEnabled && ControleGeradorSelecionado.ResultadoGerado == string.Empty)
                GerarNovo();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
