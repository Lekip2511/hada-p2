using System;
using System.Collections.Generic;

namespace Hada
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
            Tablero tablero = InicializarTablero();
            List<Barco> barcos = InicializarBarcos();

            // Bucle del juego
            while (!finPartida)
            {
                // Pedir al usuario que introduzca una coordenada
                Coordenada coordenada = PedirCoordenada();

                // Disparar en el tablero
                tablero.Disparar(coordenada);

                // Comprobar si la partida ha terminado
                if (TodosBarcosHundidos(barcos) || UsuarioQuiereSalir())
                {
                    finPartida = true;
                }
            }

            Console.WriteLine("Juego terminado.");
        }

       

        private List<Barco> InicializarBarcos()
        {
            // Crea e inicializa los barcos
            List<Barco> barcos = new List<Barco>();

            // Ejemplo: crear un barco llamado "Barco1" de longitud 3 en la coordenada (0,0)
            Barco barco1 = new Barco("Barco1", 3, 'h', new Coordenada(0, 0));
            barcos.Add(barco1);

            // Agregar más barcos según sea necesario...

            return barcos;
        }

        private Tablero InicializarTablero()
        {
            List<Barco> barcos = InicializarBarcos();
            // Crea e inicializa el tablero
            Tablero tablero = new Tablero(10, barcos); 
            return tablero;
        }

        private Coordenada PedirCoordenada()
        {
            Coordenada coordenada = null;
            bool formatoCorrecto = false;

            while (!formatoCorrecto)
            {
                Console.WriteLine("Introduce una coordenada (formato: NUMERO,NUMERO): ");
                string entrada = Console.ReadLine();
                string[] partes = entrada.Split(',');

                if (partes.Length == 2 && int.TryParse(partes[0], out int fila) && int.TryParse(partes[1], out int columna))
                {
                    coordenada = new Coordenada(fila, columna);
                    formatoCorrecto = true;
                }
                else
                {
                    Console.WriteLine("Formato de coordenada incorrecto. Inténtalo de nuevo.");
                }
            }

            return coordenada;
        }

        private bool TodosBarcosHundidos(List<Barco> barcos)
        {
            foreach (var barco in barcos)
            {
                if (barco.hundido())
                {
                    return false;
                }
            }
            return true;
        }

        private bool UsuarioQuiereSalir()
        {
            Console.WriteLine("¿Quieres salir del juego? (s/n): ");
            string respuesta = Console.ReadLine();
            return respuesta.ToLower() == "s";
        }
    }
}
