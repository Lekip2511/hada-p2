using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HADA
{
    public class Program
    {

        // Los barcos van apareciendo en el tablero conforme le vas dando
        static void Main(string[] args)
        {
            Game juego = new Game();
            juego.gameLoop();
        }

    }
}
