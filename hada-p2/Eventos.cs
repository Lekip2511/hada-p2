using System;

namespace Hada
{
    // TocadoArgs
    public class TocadoArgs : EventArgs
    {
        public string Nombre { get; set; }
        public string CoordenadaTocada { get; set; }
        public string Etiqueta { get; set; }

        public TocadoArgs(string nombre, string coordenadaTocada, string etiqueta)
        {
            Nombre = nombre;
            CoordenadaTocada = coordenadaTocada;
            Etiqueta = etiqueta;
        }
    }
    // EventArgs
    public class HundidoArgs : EventArgs
    {
        public string Nombre { get; set; }

        public HundidoArgs(string nombre)
        {
            Nombre = nombre;
        }
    }

}