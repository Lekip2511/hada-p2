using System;

namespace HADA
{
    // TocadoArgs
    public class TocadoArgs : EventArgs
    {
        private Coordenada c;

        public string Nombre { get; set; }
        public Coordenada CoordenadaTocada { get; set; }
        public string Etiqueta { get; set; }

        public TocadoArgs(string nombre, Coordenada coordenadaTocada, string etiqueta)
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