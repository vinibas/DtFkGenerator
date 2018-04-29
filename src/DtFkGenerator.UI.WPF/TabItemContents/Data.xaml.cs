using DtFkGenerator.Core;
using DtFkGenerator.Core.MassServices;
using DtFkGenerator.Core.Models;
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
using DataConfiguraçãoGeração = DtFkGenerator.Core.Models.Data.DataConfiguraçãoGeração;
using TipoGeração = DtFkGenerator.Core.Models.Data.DataConfiguraçãoGeração.TipoGeração;
using PosiçãoTemporal = DtFkGenerator.Core.Models.Data.DataConfiguraçãoGeração.PosiçãoTemporal;
using FormatoString = DtFkGenerator.Core.Models.Data.DataConfiguraçãoGeração.FormatoString;

namespace DtFkGenerator.UI.WPF.TabItemContents
{
    /// <summary>
    /// Interação lógica para Guid.xam
    /// </summary>
    public partial class Data : UserControl, IControlGerador
    {
        public Data()
         => InitializeComponent();

        public string ResultadoGerado { get => ucTextoResultado.Texto; }

        public void GerarNovo(int quantidade)
        {
            DataConfiguraçãoGeração configuração;

            var tipo = ObterTipo();
            if (tipo == TipoGeração.DefinidoPeloUsuário)
                configuração = new DataConfiguraçãoGeração(
                    dtpDefinidoPeloUsuario.Value.Value,
                    ObterFormato(),
                    txtFormatoPersonalizado.Text);
            else
                configuração = new DataConfiguraçãoGeração(
                    ObterTipo(),
                    ObterPosiçãoTemporal(),
                    ObterFormato(),
                    txtFormatoPersonalizado.Text);

            ucTextoResultado.TextoLista = DataMassService.Gerar(quantidade, configuração);
        }

        private TipoGeração ObterTipo()
        {
            if (rbTipoGeração_Aleatória.IsChecked.Value)
                return TipoGeração.Aleatória;
            else if (rbTipoGeração_DataAtual.IsChecked.Value)
                return TipoGeração.DataAtual;
            else if (rbTipoGeração_DefinidoPeloUsuário.IsChecked.Value)
                return TipoGeração.DefinidoPeloUsuário;
            else
                return TipoGeração.Aleatória;
        }

        private PosiçãoTemporal ObterPosiçãoTemporal()
        {
            if (rbPosiçãoTemporal_Ambos.IsChecked.Value)
                return PosiçãoTemporal.Indefinido;
            else if (rbPosiçãoTemporal_Passado.IsChecked.Value)
                return PosiçãoTemporal.Passado;
            else if (rbPosiçãoTemporal_Futuro.IsChecked.Value)
                return PosiçãoTemporal.Futuro;
            else
                return PosiçãoTemporal.Indefinido;
        }

        private FormatoString ObterFormato()
        {
            if (rbDados_DataEHora.IsChecked.Value)
                return FormatoString.DataHora;
            else if (rbDados_Data.IsChecked.Value)
                return FormatoString.Data;
            else if (rbDados_Hora.IsChecked.Value)
                return FormatoString.Hora;
            else if (rbDados_Livre.IsChecked.Value)
                return FormatoString.Livre;
            else
                return FormatoString.Livre;
        }
    }
}
