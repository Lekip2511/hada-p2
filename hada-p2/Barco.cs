using System;
using System.Collections.Generic;
using System.Linq;
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

        public String Name
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
            int inicFila = c.Fila;
            int inicColumna = c.Columna;

            if (longitud <= 0)
                throw new ArgumentException("Longitud no puede ser menor o igual a 0");

            if (orientacion != 'h' && orientacion != 'v')
                throw new ArgumentException("La orientacion solo puede ser \'h\' o \'v\'");
            try
            {
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
            catch (Exception e)
            {
                throw new ArgumentException("Coordenada fuera de los límites");
            }
        }



    }
}
