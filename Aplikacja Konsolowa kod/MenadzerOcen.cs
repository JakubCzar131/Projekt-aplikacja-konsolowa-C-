using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace projekt_lab
{
    public class MenedzerOcen
    {
        private BazaPlikowa bazaPlikowa;

        public MenedzerOcen(BazaPlikowa baza)
        {
            bazaPlikowa = baza;
        }

        public void DodajOcene()
        {
            Console.Write("Podaj login ucznia: ");
            string loginStudenta = Console.ReadLine();

            var uczen = bazaPlikowa.Uzytkownicy
                .FirstOrDefault(u => u.Rola == "Student" && u.Login == loginStudenta);

            if (uczen == null)
            {
                Console.WriteLine("Nie znaleziono ucznia.");
                return;
            }

           
            Console.WriteLine("Dostępne przedmioty:");
            foreach (var przedm in bazaPlikowa.Przedmioty)
            {
                Console.WriteLine("- " + przedm.Nazwa);
            }
          

            Console.Write("Podaj nazwę przedmiotu: ");
            string nazwaPrzedmiotu = Console.ReadLine();

            var przedmiot = bazaPlikowa.Przedmioty
                .FirstOrDefault(p => p.Nazwa.Equals(nazwaPrzedmiotu, StringComparison.OrdinalIgnoreCase));

            if (przedmiot == null)
            {
                Console.WriteLine("Nie znaleziono przedmiotu.");
                return;
            }

            Console.Write("Podaj wartość oceny (1-6): ");
            string wartoscString = Console.ReadLine();

            if (double.TryParse(wartoscString, out double wartosc))
            {
                if (wartosc < 1 || wartosc > 6)
                {
                    Console.WriteLine("Błędny zakres oceny (1-6).");
                    return;
                }

                Ocena nowa = new Ocena(loginStudenta, przedmiot.Nazwa, wartosc);
                bazaPlikowa.Oceny.Add(nowa);
                bazaPlikowa.ZapiszOceny();
                Console.WriteLine("Ocena dodana.");
            }
            else
            {
                Console.WriteLine("Niepoprawna wartość oceny.");
            }
        }

        public void EdytujOcene()
        {
            Console.Write("Podaj login ucznia: ");
            string loginStudenta = Console.ReadLine();

            var ocenyUcznia = bazaPlikowa.Oceny
                .Where(o => o.LoginStudenta == loginStudenta).ToList();

            if (!ocenyUcznia.Any())
            {
                Console.WriteLine("Brak ocen dla podanego ucznia.");
                return;
            }

            for (int i = 0; i < ocenyUcznia.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + ocenyUcznia[i]);
            }

            Console.Write("Wybierz numer oceny: ");
            string indeksString = Console.ReadLine();

            if (!int.TryParse(indeksString, out int indeks) || indeks < 1 || indeks > ocenyUcznia.Count)
            {
                Console.WriteLine("Niepoprawny numer.");
                return;
            }

            var edytowana = ocenyUcznia[indeks - 1];
            Console.Write("Podaj nową wartość (1-6): ");
            string nowaString = Console.ReadLine();

            if (double.TryParse(nowaString, out double nowa))
            {
                if (nowa < 1 || nowa > 6)
                {
                    Console.WriteLine("Błędny zakres oceny.");
                    return;
                }

                edytowana.Wartosc = nowa;
                bazaPlikowa.ZapiszOceny();
                Console.WriteLine("Ocena zaktualizowana.");
            }
            else
            {
                Console.WriteLine("Niepoprawna wartość oceny.");
            }
        }

        public void UsunOcene()
        {
            Console.Write("Podaj login ucznia: ");
            string loginStudenta = Console.ReadLine();

            var ocenyUcznia = bazaPlikowa.Oceny
                .Where(o => o.LoginStudenta == loginStudenta).ToList();

            if (!ocenyUcznia.Any())
            {
                Console.WriteLine("Brak ocen dla podanego ucznia.");
                return;
            }

            for (int i = 0; i < ocenyUcznia.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + ocenyUcznia[i]);
            }

            Console.Write("Wybierz numer oceny: ");
            string indeksString = Console.ReadLine();

            if (!int.TryParse(indeksString, out int indeks) || indeks < 1 || indeks > ocenyUcznia.Count)
            {
                Console.WriteLine("Niepoprawny numer.");
                return;
            }

            var doUsuniecia = ocenyUcznia[indeks - 1];
            bazaPlikowa.Oceny.Remove(doUsuniecia);
            bazaPlikowa.ZapiszOceny();
            Console.WriteLine("Ocena usunięta.");
        }

        public void WyswietlWszystkieOceny()
        {
            if (!bazaPlikowa.Oceny.Any())
            {
                Console.WriteLine("Brak ocen w systemie.");
                return;
            }

            foreach (var ocena in bazaPlikowa.Oceny)
            {
                Console.WriteLine(ocena);
            }
        }

        public void WyswietlOcenyStudenta(Student student)
        {
            var ocenyUcznia = bazaPlikowa.Oceny
                .Where(o => o.LoginStudenta == student.Login).ToList();

            if (!ocenyUcznia.Any())
            {
                Console.WriteLine("Brak ocen.");
                return;
            }

            foreach (var ocena in ocenyUcznia)
            {
                Console.WriteLine("Przedmiot: " + ocena.NazwaPrzedmiotu + ", Ocena: " + ocena.Wartosc);
            }
        }

        public void GenerujRaport()
        {
            if (!bazaPlikowa.Oceny.Any())
            {
                Console.WriteLine("Brak ocen w systemie.");
                return;
            }

            double sredniaWszystkich = bazaPlikowa.Oceny.Average(o => o.Wartosc);
            Console.WriteLine("Średnia wszystkich ocen: " + sredniaWszystkich.ToString("F2"));

            var grupy = bazaPlikowa.Oceny
                .GroupBy(o => o.NazwaPrzedmiotu)
                .Select(g => new { Przedmiot = g.Key, Srednia = g.Average(x => x.Wartosc) });

            foreach (var grupa in grupy)
            {
                Console.WriteLine("Przedmiot: " + grupa.Przedmiot + ", Średnia: " + grupa.Srednia.ToString("F2"));
            }
        }
    }
}
