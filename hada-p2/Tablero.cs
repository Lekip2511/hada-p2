using System;
using System.Collections.Generic;
using System.Text;

namespace Hada
{
    public class Tablero
    {
        private int tamTablero;

        public int TamTablero
        {
            get { return tamTablero; }
            set
            {
                if (value < 4 || value > 9)
                {
                    tamTablero = 4;
                    Console.WriteLine("El tamaño del tablero debe estar entre 4 y 9.");
                }
                else
                {
                    tamTablero = value;
                }
            }
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;            // No pueden haber repetidos
        private List<Barco> barcos;                             // Se utilizarán para inicializar las casillas del tablero
        private Dictionary<Coordenada, string> casillasTablero; // AGUA/NOMBRE_BARCO/NOMBRE_BARCO_T
        public List<Barco> barcosEliminados;                    // No pueden haber repetidos

        public event EventHandler<EventArgs> eventoFinPartida;

        public Tablero(int tamTablero,  List<Barco> barcos)
        {
            this.TamTablero = tamTablero;
            this.coordenadasDisparadas = new List<Coordenada>();
            this.coordenadasTocadas = new List<Coordenada>();
            this.barcos = barcos;
            this.barcosEliminados = new List<Barco>();
            this.casillasTablero = new Dictionary<Coordenada, string>();

            // Inicializar los eventos de tocado y hundido para cada barco
            foreach (var barco in this.barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

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

            if (EstaTocado(c))
            {
                Console.WriteLine("Barco ya tocado");    
            }
            
            else if (LeHaDado(c) != "no")     // Se mete aun que le hayan dado antes xd
            {
                foreach (var barco in barcos)
                {
                    if (barco.Nombre == LeHaDado(c))
                    {
                        barco.NumDanyos++;
                        if (barco.NumDanyos == barco.CoordenadasBarcos.Count)
                        {
                            barcosEliminados.Add(barco);
                            Console.WriteLine($"¡Hundido el barco {barco.Nombre}!");
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
                Console.WriteLine("Agua");
            }
        }

        private String LeHaDado(Coordenada c)
        {
            foreach (var barco in barcos)
            {
                foreach (var coordenada in barco.CoordenadasBarcos.Keys)
                {
                    if (coordenada.Fila == c.Fila && coordenada.Columna == c.Columna) {
                        coordenadasDisparadas.Add(c);
                        coordenadasTocadas.Add(c);
                        string nombreBarco = barco.Nombre;
                        return nombreBarco;
                    }
                }
            }
            return "no";
        }

        private bool EstaTocado(Coordenada c)
        {
            foreach (var cor in coordenadasTocadas)
            {
                if(c.Fila == cor.Fila && c.Columna == cor.Columna)
                {
                    return true;
                }
            }

            return false;
        }

        public string DibujarTablero()
        {
            string tableroString = "";

            for (int fila = 0; fila < tamTablero; fila++)
            {
                for (int columna = 0; columna < tamTablero; columna++)
                {
                    var coordenada = new Coordenada(fila, columna);
                    string estadoCasilla = coordenadasTocadas.Contains(coordenada) ? casillasTablero[coordenada] : "AGUA";      // PROBLEMA; siempre escribe agua
                    
                    tableroString += "[" + estadoCasilla.PadRight(2) + "]"; // Ajustar la anchura de la casilla
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
                    casillasTablero[coordenada] = barco.Nombre;
                }
            }

        }

        protected virtual void OnFinPartida()
        {
            eventoFinPartida?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var barco in barcos)
            {
                sb.AppendLine(barco.ToString());
            }

            sb.AppendLine("Coordenadas Disparadas:");
            foreach (var coordenada in coordenadasDisparadas)
            {
                sb.AppendLine(coordenada.ToString());
            }

            sb.AppendLine("Coordenadas Tocadas:");
            foreach (var coordenada in coordenadasTocadas)
            {
                sb.AppendLine(coordenada.ToString());
            }

            sb.AppendLine("Tablero:");
            sb.AppendLine(DibujarTablero());

            return sb.ToString();
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] tocado en Coordenada: {e.CoordenadaTocada}");
        }
        
        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Barco barcoHundido = sender as Barco;
            barcosEliminados.Add(barcoHundido);
            Console.WriteLine($"TABLERO: Barco [{barcoHundido.Nombre}] hundido!!");

            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
