using System;
using System.Collections.Generic;

namespace HADA
{
    public class Game
    {
        private bool finPartida;

        public Game()
        {
            finPartida = false;
        }

        public void gameLoop()
        {
            // Inicializar tablero y barcos

            var barcos = new List<Barco>();
            barcos.Add(new Barco("Titan", 3, 'h', new Coordenada(0, 0)));
            barcos.Add(new Barco("Bestia", 4, 'v', new Coordenada(3, 3)));
            barcos.Add(new Barco("Fiera", 2, 'v', new Coordenada(5,5)));

            var tablero = new Tablero(7, barcos);

            // Bucle del juego
            while (!finPartida)
            {
                // Pedir al usuario que introduzca una coordenada
                Console.WriteLine("Introduce una coordenada (fila,columna):");
                string entrada = Console.ReadLine();

                // Comprobar si el usuario quiere salir del juego
                if (entrada.ToLower() == "s")
                {
                    Console.WriteLine("Has salido del juego.");
                    break;
                }

                // Comprobar el formato de la entrada
                string[] partes = entrada.Split(',');
                if (partes.Length != 2 || !int.TryParse(partes[0], out int fila) || !int.TryParse(partes[1], out int columna))
                {
                    Console.WriteLine("Formato de coordenada incorrecto. Inténtalo de nuevo.");
                    continue;
                }

                // Ejecutar el método Disparar del tablero con la coordenada introducida
                tablero.Disparar(new Coordenada(fila, columna));

                Console.WriteLine("\n" + tablero.ToString() + "\n");

                // Comprobar si todos los barcos están hundidos
                if (tablero.barcosEliminados.Count == barcos.Count)
                {
                    Console.WriteLine("¡Todos los barcos han sido hundidos!");  // Aquí se tiene que mandar el evento de ganado (partida finalizada)
                    break;
                }
            }
        }
    }
}