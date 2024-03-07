using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {

        public event EventHandler Hundimiento;  // Evento de hundimiento
        public event EventHandler Tocado;  // Evento de tocado

        public Dictionary<Coordenada, String> CoordenadasBarcos { get; private set; }

        
        public String Nombre
        {
            get;
            set;
        }

        public int NumDanyos
        {
            get;
            set;
        }

        public Barco(String nombre, int longitud, char orientacion, Coordenada c)
        {
            this.Nombre = nombre;       // La comprobación de esto se hará en la clase Game ***
            this.NumDanyos = 0;
            CoordenadasBarcos = new Dictionary<Coordenada, string>();
            int inicFila = c.Fila;
            int inicColumna = c.Columna;

            if (longitud <= 0)
            {
                Console.WriteLine("Longitud no puede ser menor o igual a 0");
                for (int i = 0; i < longitud ; i++)
                {
                    Coordenada nuevaCoordenada = new Coordenada(inicFila, inicColumna + i);
                    CoordenadasBarcos[nuevaCoordenada] = nombre;
                }
            }
            else {
                if (orientacion != 'h' && orientacion != 'v')
                {
                    Console.WriteLine("La orientacion solo puede ser \'h\' o \'v\'");
                    Coordenada nuevaCoordenada = new Coordenada(inicFila, inicColumna);
                }
                else if (orientacion == 'h')
                {
                    for (int i = 0; i < longitud; i++)
                    {
                        Coordenada nuevaCoordenada = new Coordenada(inicFila, inicColumna + i);
                        CoordenadasBarcos[nuevaCoordenada] = nombre;
                    }
                }
                else
                {
                    for (int i = 0; i < longitud; i++)
                    {
                        Coordenada nuevaCoordenada = new Coordenada(inicFila + i, inicColumna);
                        CoordenadasBarcos[nuevaCoordenada] = nombre;
                    }
                }

            }
        }
        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarcos.ContainsKey(c) && !(CoordenadasBarcos[c].EndsWith("_T")))
            {
                CoordenadasBarcos[c] += "_T";
                NumDanyos++;

                Tocado?.Invoke(this, EventArgs.Empty);                  // Lanzar evento tocado

                if (NumDanyos >= CoordenadasBarcos.Count)
                    Hundimiento?.Invoke(this, EventArgs.Empty);         // Evento hundimiento
            }

        }


        public bool hundido()
        {
            // Verificar si todas las etiquetas coinciden con el nombre del barco más el sufijo "_T"
            return CoordenadasBarcos.Values.All(value => value.EndsWith("_T"));
        }

        public string toString()
        {
            string estado;

            if (!hundido())
                estado = "TRUE";
            else
                estado = "FALSE";

            string infoCoordenadas = string.Join(" ", CoordenadasBarcos.Select(barco => $"({barco.Key.Fila},{barco.Key.Columna}):{barco.Value}"));

            return $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{estado}] - COORDENADAS: [{infoCoordenadas}]";

        }
    }
}
