using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Personaje
    {
        public string Nombre { get; private set; }
        public Persona Actor { get; private set; }


        public Personaje(string nombre, Persona actor)
        {
            if (nombre == null)
                throw new ArgumentNullException("nombre");
            if (actor == null)
                throw new ArgumentNullException("actor");

            this.Nombre = nombre;
            this.Actor = actor;

        }



    }
}
