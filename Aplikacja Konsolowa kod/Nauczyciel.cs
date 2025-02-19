using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_lab
{
    public class Nauczyciel : Uzytkownik
    {
        public Nauczyciel(string login, string haslo)
            : base(login, haslo, "Nauczyciel")
        {
        }
    }
}
