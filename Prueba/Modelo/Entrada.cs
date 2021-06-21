using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Entrada
    {
        public int Fila { get; private set; }
        public int Columna { get; private set; }

        public Funcion Funcion { get; set; }
        public DateTime FechaEmision { get; private set; } = DateTime.Now;
        public decimal Precio { get; private set; }


        public const decimal PRECIO_VIP = 400;
        public const decimal PRECIO_COMUN = 200;


        public Entrada(Funcion funcion, int f, int c)
        {
            if (funcion == null)
                throw new ArgumentNullException();
            if (f < 0 || f >= funcion.CantidadDeFilas())
                throw new ArgumentOutOfRangeException();
            if(c < 0 || c >= funcion.CantidadDeColumnas())
                throw new ArgumentOutOfRangeException();

            this.Funcion = funcion;
            this.Fila = f;
            this.Columna = c;

            this.Precio = CalcularPrecio(this.Funcion.FechaHora, this.Funcion.Sala.Asientos[f,c] == TipoAsiento.VIP);
        }


        public static bool EsDiaPromo(DateTime fechaHora)
        {
            return fechaHora.DayOfWeek == DayOfWeek.Thursday || fechaHora.DayOfWeek == DayOfWeek.Wednesday;
        }

        public static decimal CalcularPrecio(DateTime fechaHora, bool esVip)
        {
            decimal precio = PRECIO_COMUN;
            int divisor = 1;
            if (EsDiaPromo(fechaHora))
            {
                divisor = 2;
            }

            if (esVip)
                precio = PRECIO_VIP;

            return precio / divisor;
        }
    }
}
