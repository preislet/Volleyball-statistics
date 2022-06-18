using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volleyball_statistics
{
    public class Menu
    {
        public string? Domaci;
        public string? Hoste;
        public string? Sestava;

        public Menu(string domaci, string hoste, string sestava)
        {
            Domaci = domaci;
            Hoste = hoste;
            Sestava = sestava;
        }
        public Menu()
        {
            Domaci = null;
            Hoste = null;
            Sestava = null;
        }
    }
}
