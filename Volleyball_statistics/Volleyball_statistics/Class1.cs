using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volleyball_statistics
{
    internal class Menu
    {
        string Domaci;
        string Hoste;
        int Sestava;

        public Menu(string domaci, string hoste, int sestava)
        {
            Domaci = domaci;
            Hoste = hoste;
            Sestava = sestava;
        }   
    }
}
