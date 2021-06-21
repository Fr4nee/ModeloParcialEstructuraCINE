using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Vista
{
    public class PeliculaViewModel
    {
        public string Titulo { get; set; }
        public string TituloOrignal { get; set; }

        public string NombreDirector { get; set; }
        public string ApellidoDirector { get; set; }

        public short Duracion { get; set; }
        public string Sinopsis { get; set; }

        public List<PersonajeViewModel> Reparto { get; set; }



    }
}
