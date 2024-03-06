using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {

        private String nombre;
        private Dictionary<Coordenada, String> etiqueta;
        private int numDanyos;

        public Dictionary<Coordenada, String> CoordenadasBarcos { get; private set; }

        public event EventHandler Hundimiento;  // Evento de hundimiento
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int NumDanyos
        {
            get
            {
                return numDanyos;
            }
            set
            {
                numDanyos = value;
            }
        }

        public Barco(String nombre, int longitud, char orientacion, Coordenada c)
        {
            this.nombre = nombre;       // La comprobación de esto se hará en la clase Game ***
            this.NumDanyos = 0;
            CoordenadasBarcos = new Dictionary<Coordenada, string>();
            int inicFila = c.Fila;
            int inicColumna = c.Columna;

            if (longitud <= 0)
                Console.WriteLine("Longitud no puede ser menor o igual a 0");

            if (orientacion != 'h' && orientacion != 'v')
                Console.WriteLine("La orientacion solo puede ser \'h\' o \'v\'");
            if (orientacion == 'h')
            {
                for (int i = 0; i < longitud - 1; i++)
                {
                    Coordenada nuevaCoordenada = new Coordenada(inicFila, inicColumna + i);
                    CoordenadasBarcos[nuevaCoordenada] = nombre;
                }
            }
            else
            {
                for (int i = 0; i < longitud - 1; i++)
                {
                    Coordenada nuevaCoordenada = new Coordenada(inicFila + i, inicColumna);
                    CoordenadasBarcos[nuevaCoordenada] = nombre;
                }

            }
        }
        public void Disparo(Coordenada c)
        {
            if (etiqueta.ContainsKey(c))
            {
                CoordenadasBarcos[c] += "_T";
                NumDanyos++;

                if (numDanyos >= CoordenadasBarcos.Count)
                {
                    OnHundimiento();
                }
            }

        }
        protected virtual void OnHundimiento()
        {
            Hundimiento?.Invoke(this, EventArgs.Empty);
        }

        public bool hundido()
        {
            // Verificar si todas las etiquetas coinciden con el nombre del barco más el sufijo "_T"
            return CoordenadasBarcos.Values.All(etiqueta => etiqueta == $"{nombre}_T");
        }

        public string toString()
        {
            string estado = NumDanyos == CoordenadasBarcos.Count ? "TRUE" : "FALSE";

            string infoCoordenadas = string.Join(" ", CoordenadasBarcos.Select(kv => $"({kv.Key.Fila},{kv.Key.Columna}):{kv.Value}"));

            return $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{estado}] - COORDENADAS: [{infoCoordenadas}]";

        }
    }
}
