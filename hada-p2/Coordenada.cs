using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HADA
{
    public class Coordenada
    {

        private int fila;       // Propiedad de la clase 
        private int columna;    // Propiedad de la clase 
        public int Fila
        {
            get{ return fila; }
            set{ fila = value; }
        }
        public int Columna
        {
            get { return columna; }
            set { columna = value; }
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
            this.Fila = fila;
            this.Columna = columna;
        }
        // Constructor pasando los argumentos como Strings
        public Coordenada(string fila, string columna)
        {
            if ((int.TryParse(fila, out int filaInt) && int.TryParse(columna, out int columnaInt)))
            {      // Sacado del GitHub xd
                Fila = filaInt;
                Columna = columnaInt;
            }
            else
                Console.WriteLine("Los argumentos pasados no son números");
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

        public override bool Equals(object obj)
        {
            return Equalsa(obj);
        }
        public bool Equalsa(object o)
        {
            if (GetType() != o.GetType() && o == null)
                return false;

            Coordenada otherCoordenada = (Coordenada)o;
            return this.Fila == otherCoordenada.Fila && this.Columna == otherCoordenada.Columna;
        }
    }
}

