using System;

namespace Hada
{
    // TocadoArgs
    public class TocadoArgs : EventArgs
    {
        public string Nombre { get; set; }
        public string CoordenadaImpacto { get; set; }
        public string Etiqueta { get; set; }

        public TocadoArgs(string nombre, string coordenadaImpacto, string etiqueta)
        {
            Nombre = nombre;
            CoordenadaImpacto = coordenadaImpacto;
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