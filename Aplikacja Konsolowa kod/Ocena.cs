using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_lab
{
    public class Ocena
    {
        public string LoginStudenta { get; set; }
        public string NazwaPrzedmiotu { get; set; }
        public double Wartosc { get; set; }

        public Ocena(string loginStudenta, string nazwaPrzedmiotu, double wartosc)
        {
            LoginStudenta = loginStudenta;
            NazwaPrzedmiotu = nazwaPrzedmiotu;
            Wartosc = wartosc;
        }

        public override string ToString()
        {
            return "Uczeń: " + LoginStudenta + ", Przedmiot: " + NazwaPrzedmiotu + ", Ocena: " + Wartosc;
        }
    }
}
