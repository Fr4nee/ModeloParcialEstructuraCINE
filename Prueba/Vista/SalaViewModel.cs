using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Vista
{
    public class SalaViewModel
    {
        public bool[,] EstadoAsientos { get; set; }
        public bool[,] EsVip { get; set; }

        public bool EsPromo { get; set; }
        public decimal PrecioComun { get; set; }
        public decimal PrecioVIP { get; set; }

    }
}
