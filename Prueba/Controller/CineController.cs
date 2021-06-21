using Prueba.Modelo;
using Prueba.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Controller
{
    public class CineController
    {

        private Cine Modelo = new Cine("CineMaster");
        private CineView Vista = new CineView();



        private List<PeliculaItemViewModel> PeliculaItemViewModels()
        {
            List<PeliculaItemViewModel> pivm = new List<PeliculaItemViewModel>();
            List<Pelicula> modelo = this.Modelo.Peliculas;

            for (int i = 0; i < modelo.Count; i++)
            {
                pivm.Add(new PeliculaItemViewModel()
                {
                    Titulo = modelo[i].Titulo,
                    TituloOriginal = modelo[i].TituloOriginal
                });

            }

            return pivm;
        }
        private PeliculaViewModel PeliculaViewModel(Pelicula p)
        {
            PeliculaViewModel rv = new PeliculaViewModel()
            {
                Titulo = p.Titulo,
                TituloOrignal = p.TituloOriginal,
                Duracion = p.Duracion,
                NombreDirector = p.Director.Nombre,
                ApellidoDirector = p.Director.Apellido,
                Sinopsis = p.Sinopsis,
                Reparto = new List<PersonajeViewModel>()
            };

            for (int i = 0; i < p.Reparto.Count; i++)
            {
                rv.Reparto.Add(new PersonajeViewModel()
                {
                    Nombre = p.Reparto[i].Nombre,
                    NombreActor = p.Reparto[i].Actor.Nombre,
                    ApellidoActor = p.Reparto[i].Actor.Apellido
                });
            }


            return rv;
        }

        private List<FuncionViewModel> FuncionViewModels(List<Funcion> funciones)
        {
            List<FuncionViewModel> rv = new List<FuncionViewModel>();

            for (int i = 0; i < funciones.Count; i++)
            {
                rv.Add(new FuncionViewModel() {
                NroSala = funciones[i].Sala.Nro,
                FechaHora = funciones[i].FechaHora
                });
            }

            return rv;
        }


        private SalaViewModel SalaViewModel(Funcion funcion)
        {
            SalaViewModel svm = new SalaViewModel();
            svm.EstadoAsientos = new bool[funcion.CantidadDeFilas(), funcion.CantidadDeColumnas()];
            svm.EsVip = new bool[funcion.CantidadDeFilas(), funcion.CantidadDeColumnas()];
            svm.EsPromo = Entrada.EsDiaPromo(funcion.FechaHora);
            svm.PrecioComun = Entrada.CalcularPrecio(funcion.FechaHora, false);
            svm.PrecioVIP = Entrada.CalcularPrecio(funcion.FechaHora, true);

            for (int f = 0; f < funcion.CantidadDeFilas(); f++)
            {
                for (int c = 0; c < funcion.CantidadDeColumnas(); c++)
                {
                    svm.EstadoAsientos[f,c] = funcion.EstaLibre(f, c);
                    svm.EsVip[f, c] = funcion.Sala.EsAsientoVip(f,c);
                }
            }

            return svm;
        }



      

        private void ImprimirEntrada(Funcion funcion,int fila, int col)
        {
            Entrada entrada = new Entrada(funcion, fila, col);
            this.Modelo.Entradas.Add(entrada);
            this.Vista.ImprimirEntrada(this.Modelo.Nombre, new EntradaViewModel() { 
                        Pelicula = funcion.Pelicula.Titulo,
                        FechaHoraEmision = entrada.FechaEmision,
                        Precio = entrada.Precio,
                        EsVIP = funcion.Sala.EsAsientoVip(fila, col),
                        FilaAsiento = fila,
                        ColumnaAsiento = col,
                        NroSala = funcion.Sala.Nro,
                        FechaHoraFuncion = funcion.FechaHora,
            });
        }

        private bool PedirElegirAsiento(Funcion funcion, out int fila, out int columna)
        {
            bool noCancelo = false;
            SalaViewModel svm = this.SalaViewModel(funcion);

            noCancelo = this.Vista.MostrarSeleccionDeAsiento(svm, out fila, out columna);

            while (noCancelo && !funcion.IntentarOcuparAsiento(fila, columna))
            {
                this.Vista.MostrarError("Asiento ocupado");
                noCancelo = this.Vista.MostrarSeleccionDeAsiento(svm, out fila, out columna);
            }

            return noCancelo;
        }

      

        private Funcion PedirElegirFuncion(Pelicula p)
        {
            int opcionElegida;
            List<Funcion> funcionesDisponibles;
            List<FuncionViewModel> funcionViewModels;


            funcionesDisponibles = this.Modelo.BuscarFunciones(p);

            if (funcionesDisponibles.Count == 0)
            {
                this.Vista.MostrarError("No quedan funciones disponibles");
                return null;
            }

            funcionViewModels = this.FuncionViewModels(funcionesDisponibles);

            opcionElegida = this.Vista.MostrarMenuFunciones(PeliculaViewModel(p), funcionViewModels);

            if (opcionElegida == -1)
                return null;

            return funcionesDisponibles[opcionElegida];
        }



        private Pelicula PedirElegirPelicula()
        {
            int opcionElegida = this.Vista.MostrarMenuPeliculas(this.Modelo.Nombre, this.PeliculaItemViewModels());

            return this.Modelo.Peliculas[opcionElegida];
        }



        public void Ejecutar()
        {
            while (true)
            {
                int fila;
                int columna;
                Pelicula peliculaElegida = this.PedirElegirPelicula();
                Funcion funcionElegida = this.PedirElegirFuncion(peliculaElegida);

                if (funcionElegida != null && this.PedirElegirAsiento(funcionElegida, out fila, out columna))
                {
                    this.ImprimirEntrada(funcionElegida, fila, columna);
                }
            }
        }

    }
}
