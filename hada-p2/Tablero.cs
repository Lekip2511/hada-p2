using System;
using System.Collections.Generic;

namespace Hada
{
    public class Tablero
    {
        private int tamTablero;
        private List<Coordenada> coordenadasDisparadas;
        private HashSet<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private HashSet<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

        public int TamTablero
        {
            get { return tamTablero; }
            set
            {
                if (value < 4 || value > 9)
                {
                    throw new ArgumentException("El tamaño del tablero debe estar entre 4 y 9.");
                }
                else
                {
                    tamTablero = value;
                }
            }
        }

        public Tablero(int tamTablero,  List<Barco> barcos)
        {
            TamTablero = tamTablero;
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new HashSet<Coordenada>();
            this.barcos = barcos;
            barcosEliminados = new HashSet<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            // Inicializar las casillas del tablero
            inicializaCasillasTablero();
        }

        public void Disparar(Coordenada c)
        {
            if (c.Fila < 0 || c.Fila >= tamTablero || c.Columna < 0 || c.Columna >= tamTablero)
            {
                Console.WriteLine($"La coordenada ({c.Fila},{c.Columna}) está fuera de las dimensiones del tablero.");
                return;
            }
            
            Console.WriteLine($"Disparando a la coordenada ({c.Fila},{c.Columna})...");

            if (casillasTablero.ContainsKey(c))
            {
                coordenadasDisparadas.Add(c);
                coordenadasTocadas.Add(c);

                string nombreBarco = casillasTablero[c];
                foreach (var barco in barcos)
                {
                    if (barco.Name == nombreBarco)
                    {
                        barco.NumDanyos++;
                        if (barco.NumDanyos == barco.CoordenadasBarcos.Count)
                        {
                            barcosEliminados.Add(barco);
                            Console.WriteLine($"¡Hundido el barco {barco.Name}!");
                            if (barcosEliminados.Count == barcos.Count)
                            {
                                OnFinPartida();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"¡Tocado en la coordenada ({c.Fila},{c.Columna})!");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Agua.");
            }
        }

        public string DibujarTablero()
        {
            string tableroString = "";

            for (int fila = 0; fila < tamTablero; fila++)
            {
                for (int columna = 0; columna < tamTablero; columna++)
                {
                    var coordenada = new Coordenada(fila, columna);
                    string estadoCasilla = casillasTablero.ContainsKey(coordenada) ? casillasTablero[coordenada] : "AGUA";
                    tableroString += estadoCasilla.PadRight(10); // Ajustar la anchura de la casilla
                }
                tableroString += "\n"; // Nueva línea después de cada fila
            }

            return tableroString;
        }

        private void inicializaCasillasTablero()
        {
            foreach (var barco in barcos)
            {
                foreach (var coordenada in barco.CoordenadasBarcos.Keys)
                {
                    casillasTablero[coordenada] = barco.Name;
                }
            }

            // Rellenar las casillas restantes con AGUA
            for (int fila = 0; fila < tamTablero; fila++)
            {
                for (int columna = 0; columna < tamTablero; columna++)
                {
                    var coordenada = new Coordenada(fila, columna);
                    if (!casillasTablero.ContainsKey(coordenada))
                    {
                        casillasTablero.Add(coordenada, "AGUA");
                    }
                }
            }
        }

        protected virtual void OnFinPartida()
        {
            eventoFinPartida?.Invoke(this, EventArgs.Empty);
        }
    }
}
