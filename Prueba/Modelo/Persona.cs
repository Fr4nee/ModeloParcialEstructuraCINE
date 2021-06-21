using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Persona
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }

        public Persona(string nombre, string apellido)
        {

            if (nombre == null)
                throw new ArgumentNullException("nombre");
            if (apellido == null)
                throw new ArgumentNullException("apellido");


            this.Nombre = nombre;
            this.Apellido = apellido;

        }

    }
}
