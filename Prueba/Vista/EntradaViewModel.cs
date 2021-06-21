using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Vista
{
    public class EntradaViewModel
    {
        public string Pelicula { get; set; }
        public int  NroSala { get; set; }
        public DateTime FechaHoraFuncion { get; set; }
        public DateTime FechaHoraEmision { get; set; }

        public decimal Precio { get; set; }
        public bool EsVIP { get; set; }

        public int FilaAsiento { get; set; }
        public int ColumnaAsiento { get; set; }

    }
}
