using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Vista
{
    public class CineView
    {

        private string PedirString(string msg)
        {
            Console.Write($"{msg}: ");
            return Console.ReadLine();
        }


        private int PedirIntEnRango(string msg, int min, int max)
        {
            int rv;
            string rvstr;

            rvstr = PedirString(msg);

            while (!int.TryParse(rvstr, out rv) || rv < min || rv > max)
            {
                rvstr = PedirString(msg);
            }

            return rv;
        }

        private int MostrarMenu(string[] opciones, bool puedeCancelar)
        {
            int optMax = opciones.GetUpperBound(0);

            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"{i} - {opciones[i]}");
            }

            if (puedeCancelar)
            {
                Console.WriteLine($"{opciones.Length} - Cancelar");
                optMax = opciones.Length;
            }



            int optElegida = this.PedirIntEnRango("Su eleccion", 0, optMax);

            if (puedeCancelar && optElegida == optMax)
                return -1;

            return optElegida;
        }



        public int MostrarMenuPeliculas(string nombreCine, List<PeliculaItemViewModel> peliculas)
        {

            Console.Clear();
            string[] opts = new string[peliculas.Count];

            for (int i = 0; i < opts.Length; i++)
            {
                opts[i] = $"{peliculas[i].Titulo} ({peliculas[i].TituloOriginal})";
            }

            Console.WriteLine($"Bienvenido a {nombreCine}");
            Console.WriteLine("¿Qué película quieres ver?");
            return this.MostrarMenu(opts, false);
        }

        public int MostrarMenuFunciones(PeliculaViewModel pelicula, List<FuncionViewModel> funciones)
        {
            Console.Clear();
            string[] opts = new string[funciones.Count];

            this.MostrarInfoPelicula(pelicula);


            for (int i = 0; i < opts.Length; i++)
            {
                opts[i] = $"{funciones[i].FechaHora.Day}/{funciones[i].FechaHora.Month}/{funciones[i].FechaHora.Year}  {funciones[i].FechaHora.Hour:00}:{funciones[i].FechaHora.Minute:00}";
            }
            Console.WriteLine("Funciones:");
            return this.MostrarMenu(opts, true);
        }

        private void MostrarInfoPelicula(PeliculaViewModel pelicula)
        {
            Console.WriteLine($"Titulo: {pelicula.Titulo}");
            Console.WriteLine($"Titulo Original: {pelicula.TituloOrignal}");
            Console.WriteLine($"Duracion: {pelicula.Duracion} minutos");
            Console.WriteLine($"Director: {pelicula.NombreDirector} {pelicula.ApellidoDirector}");
            Console.WriteLine($"Reparto:");
            for (int i = 0; i < pelicula.Reparto.Count; i++)
            {
                Console.WriteLine($"* {pelicula.Reparto[i].NombreActor} {pelicula.Reparto[i].ApellidoActor} como {pelicula.Reparto[i].Nombre}");
            }

            Console.WriteLine($"Sinopsis: {pelicula.Sinopsis}");

        }

        public bool MostrarSeleccionDeAsiento(SalaViewModel svm, out int fila, out int columna)
        {
            Console.Clear();
            this.DibujarSala(svm);


            fila = 0;
            columna = 0;

            fila = this.PedirIntEnRango("Ingrese fila (-1 para cancelar)", -1, svm.EstadoAsientos.GetUpperBound(0));
            if (fila == -1)
                return false;

            columna = this.PedirIntEnRango("Ingrese columna (-1 para cancelar)", -1, svm.EstadoAsientos.GetUpperBound(1));
            if (columna == -1)
                return false;

            return true;
        }

        private void DibujarSala(SalaViewModel svm)
        {

            int anchoHdrFila = $"{svm.EstadoAsientos.GetLength(0)}".Length;
            int anchoHdrCol = $"{svm.EstadoAsientos.GetLength(1)}".Length;

            Console.Write("".PadLeft(anchoHdrFila + 1));
            Console.WriteLine(this.Centrar("PANTALLA", (anchoHdrCol+2) * svm.EstadoAsientos.GetLength(1), '='));

            Console.Write("".PadLeft(anchoHdrFila+1));

            for (int i = 0; i < svm.EstadoAsientos.GetLength(1); i++)
            {
                string col = this.Centrar($"{i}", anchoHdrCol, ' ');
                Console.Write($" {col} ");
            }
            Console.WriteLine();

            for (int f = 0; f < svm.EstadoAsientos.GetLength(0); f++)
            {
                string fila = this.Centrar($"{f}", anchoHdrFila, ' ');
                Console.Write($"{fila} ");

                for (int c = 0; c < svm.EstadoAsientos.GetLength(1); c++)
                {
                    string tipo = " ";
                    if (!svm.EstadoAsientos[f, c])
                    {
                        tipo = "O";
                    }
                    else if (svm.EsVip[f, c])
                    {
                        tipo = "V";
                    }

                    string col = this.Centrar(tipo, anchoHdrCol, ' ');

                     
                    Console.Write($"[{col}]");
                }
                Console.WriteLine();
            }

            string txtPromo = "";
            if (svm.EsPromo)
            {
                txtPromo = "(precio promo)";
            }

            Console.WriteLine("Referencias:");
            Console.WriteLine($"[ ] = Libre ${svm.PrecioComun:0.00} {txtPromo}");
            Console.WriteLine($"[V] = VIP Libre ${svm.PrecioVIP:0.00} {txtPromo}");
            Console.WriteLine("[O] = Ocupado");

        }

        public void ImprimirEntrada(string nombreCine, EntradaViewModel entrada)
        {
            string vipStr = "NO";

            if (entrada.EsVIP)
            {
                vipStr = "SI";
            }

            Console.Clear();
            Console.WriteLine("Detalle de entrada:");
            Console.WriteLine($"{nombreCine}   {entrada.FechaHoraEmision}");
            Console.WriteLine($"Pelicula: {entrada.Pelicula}");
            Console.WriteLine($"Sala: {entrada.NroSala} Fecha y Hora: {entrada.FechaHoraFuncion}");
            Console.WriteLine($"Asiento: {entrada.FilaAsiento} {entrada.ColumnaAsiento} VIP: {vipStr}");
            Console.WriteLine($"Precio: ${entrada.Precio:0.00}");
            Console.ReadLine();

        }

        private string Centrar(string str, int largo, char c)
        {
            int espacios = largo - str.Length;
            int padLeft = espacios / 2 + str.Length;

            return str.PadLeft(padLeft, c).PadRight(largo, c);
        }

        public void MostrarError(string v)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(v);
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
