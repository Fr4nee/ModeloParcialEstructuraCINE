using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Sala
    {
        public int Nro { get; private set; }
        public TipoAsiento[,] Asientos { get; private set; }

        public Sala(int nro, int cantFilas, int cantCols)
        {
            if (cantFilas <= 0)
                throw new ArgumentOutOfRangeException("cantFilas", "no puede ser <= 0");
            if (cantCols <= 0)
                throw new ArgumentOutOfRangeException("cantCols", "no puede ser <= 0");

            this.Nro = nro;
            this.Asientos = new TipoAsiento[cantFilas, cantCols];

            int ultimaFila = this.Asientos.GetUpperBound(0);
            for (int c = 0; c < this.Asientos.GetLength(1); c++)
            {
                this.Asientos[ultimaFila, c] = TipoAsiento.VIP;
            }
        }


        public bool EsAsientoVip(int f, int c)
        {
            return this.Asientos[f, c] == TipoAsiento.VIP;
        }

    }
}
