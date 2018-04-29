using System;
using System.Collections.Generic;
using System.Text;

namespace DtFkGenerator.Core.Models
{
    public static class GuidModel
    {
        public static Guid Gerar()
            => Guid.NewGuid();
    }
}
