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

            tablero.eventoFinPartida += cuandoFinPartida;

            // Bucle del juego
            while (!finPartida)
            {
                // Pedir al usuario que introduzca una coordenada
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir):");
                string entrada = Console.ReadLine();

                // Comprobar si el usuario quiere salir del juego
                if (entrada.ToLower() == "s")
                {
                    Console.WriteLine("Has salido del juego.");                 // Evento juego terminado
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

            }

            Console.WriteLine("PARTIDA TERMINADA :)");
        }

        private void cuandoFinPartida(object sender, EventArgs e)
        {
            finPartida = true;
        }
    }
}