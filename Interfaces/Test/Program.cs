using Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string tarjeta = "6272472345678915";
            string ofuscado1 = string.Empty;
            string ofuscado2 = string.Empty;
            ofuscado1 = Util.OfuscaTarjeta(tarjeta, 6, 4);
            ofuscado2 = Util.OfuscaTarjeta(tarjeta);
            Console.ReadLine();
        }
    }
}
