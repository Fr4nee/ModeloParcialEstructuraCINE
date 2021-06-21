using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Randomizer
    {
        private Random RandomGen = new Random();

        private DateTime Hoy = DateTime.Now;

        private char[] Letras;


        public Randomizer()
        {
            this.Letras = this.GenerarArraYDeLetras();
        }

        private char[] GenerarArraYDeLetras()
        {
            int cant = 'z' - 'a';

            char[] rv = new char[cant];
            char c = 'a';
            for (int i = 0; i < rv.Length; i++)
            {
                rv[i] = c;
                c++;
            }

         
            return rv;
        }


        public string GenerarStringRandom(int largoMin, int largoMax)
        {
            string rv = "";

            int largoDeLinea = RandomGen.Next(largoMin, largoMax);

            for (int c = 0; c < largoDeLinea; c++)
            {
                rv += Letras[RandomGen.Next(0, Letras.Length)];
            }

            return rv;
        }

        public string GenerarTextoRandom(int cantPalabrasMin, int cantPalabrasMax)
        {
            int cantPalabras = this.RandomGen.Next(cantPalabrasMin, cantPalabrasMax);
            string rv = "";

            for (int i = 0; i < cantPalabras; i++)
            {
                rv += GenerarStringRandom(1, 8) + " ";
            }

            return rv.TrimEnd();
        }



        public DateTime GenerarDateTimeRandom()
        {
            int mes = this.RandomGen.Next(this.Hoy.Month, 13);
            int diaMax = DateTime.DaysInMonth(this.Hoy.Year, mes);

            int dia = this.RandomGen.Next(1, diaMax+1);

            return new DateTime(this.Hoy.Year, mes, dia, this.RandomGen.Next(0, 24), this.RandomGen.Next(0, 60), 0);
        }

        public int GenerarIntRandom(int min, int max)
        {

            return this.RandomGen.Next(min, max);
        }

        public short GenerarShortRandom(short min, short max)
        {
            return (short)this.RandomGen.Next(min, max);
        }



        public T ObtenerElementoAleatorio<T>(List<T> lista)
        {
            return lista[this.RandomGen.Next(0, lista.Count)];
        }

    }
}
