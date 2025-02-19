using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace projekt_lab
{
    public class BazaPlikowa
    {
        private string plikUzytkownicy = "Users.txt";
        private string plikPrzedmioty = "Subjects.txt";
        private string plikOceny = "Grades.txt";

        public List<Uzytkownik> Uzytkownicy { get; set; } = new List<Uzytkownik>();
        public List<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();
        public List<Ocena> Oceny { get; set; } = new List<Ocena>();

        public void WczytajUzytkownikow()
        {
            Uzytkownicy.Clear();
            if (!File.Exists(plikUzytkownicy))
            {
                UtworzPrzykladowyPlikUzytkownicy();
            }
            var linie = File.ReadAllLines(plikUzytkownicy);
            foreach (var l in linie)
            {
                var czesci = l.Split(';');
                if (czesci.Length == 3)
                {
                    string login = czesci[0];
                    string haslo = czesci[1];
                    string rola = czesci[2];
                    if (rola == "Nauczyciel")
                    {
                        Uzytkownicy.Add(new Nauczyciel(login, haslo));
                    }
                    else if (rola == "Student")
                    {
                        Uzytkownicy.Add(new Student(login, haslo));
                    }
                }
            }
        }

        private void UtworzPrzykladowyPlikUzytkownicy()
        {
            var przykladowiUzytkownicy = new List<string>
            {
                "nauczyciel;haslo123;Nauczyciel",
         
                "anna;anna123;Student"0
            };
            File.WriteAllLines(plikUzytkownicy, przykladowiUzytkownicy);
        }

        public void ZapiszUzytkownikow()
        {
            var linie = new List<string>();
            foreach (var u in Uzytkownicy)
            {
                linie.Add(u.Login + ";" + u.Haslo + ";" + u.Rola);
            }
            File.WriteAllLines(plikUzytkownicy, linie);
        }

        public void WczytajPrzedmioty()
        {
            Przedmioty.Clear();
            if (!File.Exists(plikPrzedmioty))
            {
                UtworzPrzykladowyPlikPrzedmioty();
            }
            var linie = File.ReadAllLines(plikPrzedmioty);
            foreach (var l in linie)
            {
                if (!string.IsNullOrWhiteSpace(l))
                {
                    Przedmioty.Add(new Przedmiot(l));
                }
            }
        }

        private void UtworzPrzykladowyPlikPrzedmioty()
        {
            var przykladowePrzedmioty = new List<string>
            {
                "Matematyka",
                "Biologia",
                "Historia"
            };
            File.WriteAllLines(plikPrzedmioty, przykladowePrzedmioty);
        }

        public void ZapiszPrzedmioty()
        {
            var linie = Przedmioty.Select(p => p.Nazwa).ToList();
            File.WriteAllLines(plikPrzedmioty, linie);
        }

        public void WczytajOceny()
        {
            Oceny.Clear();
            if (!File.Exists(plikOceny))
            {
                File.Create(plikOceny).Close();
            }
            var linie = File.ReadAllLines(plikOceny);
            foreach (var l in linie)
            {
                var czesci = l.Split(';');
                if (czesci.Length == 3)
                {
                    string loginStudenta = czesci[0];
                    string nazwaPrzedmiotu = czesci[1];
                    if (double.TryParse(czesci[2], out double wartosc))
                    {
                        Oceny.Add(new Ocena(loginStudenta, nazwaPrzedmiotu, wartosc));
                    }
                }
            }
        }

        public void ZapiszOceny()
        {
            var linie = new List<string>();
            foreach (var o in Oceny)
            {
                linie.Add(o.LoginStudenta + ";" + o.NazwaPrzedmiotu + ";" + o.Wartosc);
            }
            File.WriteAllLines(plikOceny, linie);
        }
    }
}
