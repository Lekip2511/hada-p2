using System;

namespace Hada
{
    public class Eventos
    {
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

        public class HundidoArgs : EventArgs
        {
            public string Nombre { get; set; }

            public HundidoArgs(string nombre)
            {
                Nombre = nombre;
            }
        }

        public event EventHandler<TocadoArgs> Tocado;
        public event EventHandler<HundidoArgs> Hundido;

        public void OnTocado(object sender, TocadoArgs e)
        {
            Tocado?.Invoke(sender, e);
        }

        public void OnHundido(object sender, HundidoArgs e)
        {
            Hundido?.Invoke(sender, e);
        }
    }
}
