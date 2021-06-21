using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class Cine
    {
        public string Nombre { get; private set; }
      
        public List<Pelicula> Peliculas { get; private set; } = new List<Pelicula>();
        public List<Sala> Salas { get; private set; } = new List<Sala>();
        public List<Entrada> Entradas { get; private set; } = new List<Entrada>();


        private List<Funcion> Funciones { get; set; } = new List<Funcion>();

        //USADO PARA GENERAR DATOS DE PRUEBA.
        private Randomizer Randomizer = new Randomizer();


        public Cine(string nombre)
        {
            this.Nombre = nombre;
            this.CargarDatosDePrueba();
        }


      


       

        private Pelicula CrearPeliculaRandom()
        {

            Persona director = new Persona(this.Randomizer.GenerarStringRandom(2, 11), this.Randomizer.GenerarStringRandom(2, 11));
            int cantPersonas = this.Randomizer.GenerarIntRandom(2, 11);
            List<Personaje> personajes = new List<Personaje>();


            for (int i = 0; i < cantPersonas; i++)
            {
                Persona p = new Persona(this.Randomizer.GenerarStringRandom(2, 11), this.Randomizer.GenerarStringRandom(2, 11));
                personajes.Add(new Personaje(this.Randomizer.GenerarStringRandom(2, 11), p));
            }


            return new Pelicula(this.Randomizer.GenerarStringRandom(2, 15),
                                this.Randomizer.GenerarStringRandom(2, 15),
                                director,
                                personajes,
                                this.Randomizer.GenerarShortRandom(60, 200),
                                this.Randomizer.GenerarTextoRandom(10, 40));
        }

        private void CargarSalas()
        {
            int cantSalas = 10;

            for (int i = 0; i < cantSalas; i++)
            {
                this.Salas.Add(new Sala(i+1, 10, 10));
            }
            
        }

        private void CargarPeliculas()
        {
            int cantPeliculas = this.Randomizer.GenerarIntRandom(4, 11);

            for (int i = 0; i < cantPeliculas; i++)
            {
                this.Peliculas.Add(this.CrearPeliculaRandom());
            }
        }

        private void CargarFunciones()
        {
            for (int i = 0; i < this.Peliculas.Count; i++)
            {
                int cantFunciones = this.Randomizer.GenerarIntRandom(2, 4);

                for (int f = 0; f < cantFunciones; f++)
                {
                    Sala s = this.Randomizer.ObtenerElementoAleatorio(this.Salas);
                    this.Funciones.Add(new Funcion(this.Peliculas[i], s, this.Randomizer.GenerarDateTimeRandom()));
                }
            }
        }

        private void CargarDatosDePrueba()
        {
            this.CargarSalas();
            this.CargarPeliculas();
            this.CargarFunciones();
        }




        public List<Funcion> BuscarFunciones(Pelicula p)
        {
            //CON LAMBDA
            //return this.Funciones.FindAll((Funcion fn) => fn.Pelicula == p && fn.HayAsientosLibres());

            //SIN LAMBDA
            List<Funcion> resultado = new List<Funcion>();
            for (int i = 0; i < this.Funciones.Count; i++)
            {
                Funcion fn = this.Funciones[i];
                if (fn.Pelicula == p && fn.HayAsientosLibres())
                {
                    resultado.Add(fn);
                }
            }

            return resultado;
        }

        
    }
}
