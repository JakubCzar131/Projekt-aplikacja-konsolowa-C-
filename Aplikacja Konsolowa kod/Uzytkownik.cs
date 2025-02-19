using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_lab
{
    public abstract class Uzytkownik
    {
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string Rola { get; protected set; }

        public Uzytkownik(string login, string haslo, string rola)
        {
            Login = login;
            Haslo = haslo;
            Rola = rola;
        }
    }
}
