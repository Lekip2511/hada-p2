using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {

        private int fila;       // Propiedad de la clase 
        private int columna;    // Propiedad de la clase 
        public int Fila
        {
            get
            {
                return fila;
            }
            set
            {
                if (value < 4 || value > 9)
                    throw new ArgumentException("Coordenada fuera de los límites");
                else
                    fila = value;
            }
        }
        public int Columna
        {
            get
            {
                return columna;
            }
            set
            {
                if (value < 4 || value > 9)
                    throw new ArgumentException("Coordenada fuera de los límites");
                else
                    columna = value;
            }
        }
        // Constructor por defecto
        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }
        // Constructor por parámetros
        public Coordenada(int fila, int columna)
        {
            this.Fila = Fila;
            this.Columna = Columna;
        }
        // Constructor pasando los argumentos como Strings
        public Coordenada(string fila, string columna)
        {
            if ((!int.TryParse(fila, out int filaInt) || !int.TryParse(columna, out int columnaInt)))       // Sacado del GitHub xd
                throw new ArgumentException("Los argumentos pasados no son números");
            Fila = filaInt;
            Columna = columnaInt;
        }
        // Constructor copia
        public Coordenada(Coordenada c)
        {
            this.Fila = c.Fila;
            this.Columna = c.Columna;
        }

        public override String ToString()
        {
            return ("(" + Fila + "," + Columna + ")");
        }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public bool Equals(Coordenada o)
        {
            return (o.Columna == this.Columna && o.Fila == this.Fila);
        }
    }
}

