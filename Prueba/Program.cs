using Prueba.Controller;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            CineController cineController = new CineController();
            cineController.Ejecutar();

            Console.ReadLine();
        }
    }
}