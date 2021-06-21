using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Funcion
    {
        public Pelicula Pelicula { get; private set; }
        public Sala Sala { get; private set; }
        public EstadoAsiento[,] EstadoAsientos { get; private set; }

        public DateTime FechaHora { get; private set; }

        public Funcion(Pelicula p, Sala s, DateTime fechaHora)
        {
            if (p == null)
                throw new ArgumentNullException();
            if (s == null)
                throw new ArgumentNullException();

            this.Pelicula = p;
            this.Sala = s;
            this.FechaHora = fechaHora;
            this.EstadoAsientos = new EstadoAsiento[this.Sala.Asientos.GetLength(0), this.Sala.Asientos.GetLength(1)];
        }

        public int CantidadDeColumnas()
        {
            return this.EstadoAsientos.GetLength(0);
        }

        public int CantidadDeFilas()
        {
            return this.EstadoAsientos.GetLength(1);
        }

        public bool EstaLibre(int fila, int col)
        {
            return this.EstadoAsientos[fila, col] == EstadoAsiento.Libre;
        }

        public bool IntentarOcuparAsiento(int fila, int col)
        {
            if (!this.EstaLibre(fila, col))
                return false;

            this.EstadoAsientos[fila, col] = EstadoAsiento.Ocupado;

            return true;
        }


        public bool HayAsientosLibres()
        {
            for (int f = 0; f < this.EstadoAsientos.GetLength(0); f++)
            {
                for (int c = 0; c < this.EstadoAsientos.GetLength(1); c++) 
                {
                    if (this.EstaLibre(f, c))
                        return true;
                }
            }

            return false;
        }

    }
}
