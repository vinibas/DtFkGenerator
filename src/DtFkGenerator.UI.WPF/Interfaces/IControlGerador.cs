using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtFkGenerator.UI.WPF.Interfaces
{
    public interface IControlGerador
    {
        string ResultadoGerado { get; }

        void GerarNovo(int quantidade);
    }
}
