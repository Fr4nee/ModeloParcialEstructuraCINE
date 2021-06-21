using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Pelicula
    {
        public string Titulo { get; private set; }
        public string TituloOriginal { get; private set; }
        public Persona Director { get; private set; }
        public List<Personaje> Reparto { get; private set; }
        public short Duracion { get; private set; }
        public string Sinopsis { get; private set; }


        public Pelicula(string titulo, string tituloOriginal, Persona dir, List<Personaje> reparto, short duracion, string sinop)
        {
            if (titulo == null)
                throw new ArgumentNullException("titulo");
            if (tituloOriginal == null)
                throw new ArgumentNullException("tituloOriginal");
            if (dir == null)
                throw new ArgumentNullException("dir");
            if (reparto == null)
                throw new ArgumentNullException("reparto");
            if (sinop == null)
                throw new ArgumentNullException("sinop");

            if (duracion <= 0)
                throw new ArgumentException("duracion invalida");
            

            this.Titulo = titulo;
            this.TituloOriginal = tituloOriginal;
            this.Director = dir;
            this.Reparto = reparto;
            this.Duracion = duracion;
            this.Sinopsis = sinop;
        }





    }
}
