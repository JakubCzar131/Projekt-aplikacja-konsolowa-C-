using System;
using System.Collections.Generic;
#nullable disable

namespace projekt_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            BazaPlikowa bazaPlikowa = new BazaPlikowa();
            bazaPlikowa.WczytajUzytkownikow();
            bazaPlikowa.WczytajPrzedmioty();
            bazaPlikowa.WczytajOceny();

            MenedzerOcen menedzerOcen = new MenedzerOcen(bazaPlikowa);

            Console.WriteLine("Elektroniczny System Oceniania");
            bool wyjscie = false;

            while (!wyjscie)
            {
                Console.WriteLine("\nWybierz opcję:");
                Console.WriteLine("1. Zaloguj się");
                Console.WriteLine("2. Wyjście");
                Console.Write("Opcja: ");

                string opcja = Console.ReadLine();
                switch (opcja)
                {
                    case "1":
                        Uzytkownik zalogowany = Zaloguj(bazaPlikowa.Uzytkownicy);
                        if (zalogowany != null)
                        {
                            Console.WriteLine("Zalogowano jako: " + zalogowany.Login + " (" + zalogowany.Rola + ")");
                            if (zalogowany is Nauczyciel nauczyciel)
                            {
                                MenuNauczyciela(nauczyciel, menedzerOcen);
                            }
                            else if (zalogowany is Student student)
                            {
                                MenuStudenta(student, menedzerOcen);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niepoprawny login lub hasło.");
                        }
                        break;
                    case "2":
                        wyjscie = true;
                        break;
                    default:
                        Console.WriteLine("Nieznana opcja.");
                        break;
                }
            }

            Console.WriteLine("Zamykam program...");
        }

        static Uzytkownik Zaloguj(List<Uzytkownik> uzytkownicy)
        {
            Console.Write("Podaj login: ");
            string login = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string haslo = Console.ReadLine();

            foreach (var u in uzytkownicy)
            {
                if (u.Login == login && u.Haslo == haslo)
                {
                    return u;
                }
            }
            return null;
        }

        static void MenuNauczyciela(Nauczyciel nauczyciel, MenedzerOcen menedzerOcen)
        {
            bool wroc = false;
            while (!wroc)
            {
                Console.WriteLine("\n--- MENU NAUCZYCIELA ---");
                Console.WriteLine("1. Dodaj ocenę");
                Console.WriteLine("2. Edytuj ocenę");
                Console.WriteLine("3. Usuń ocenę");
                Console.WriteLine("4. Wyświetl wszystkie oceny");
                Console.WriteLine("5. Generuj raport");
                Console.WriteLine("6. Wyloguj");
                Console.Write("Opcja: ");

                string opcja = Console.ReadLine();
                switch (opcja)
                {
                    case "1":
                        menedzerOcen.DodajOcene();
                        break;
                    case "2":
                        menedzerOcen.EdytujOcene();
                        break;
                    case "3":
                        menedzerOcen.UsunOcene();
                        break;
                    case "4":
                        menedzerOcen.WyswietlWszystkieOceny();
                        break;
                    case "5":
                        menedzerOcen.GenerujRaport();
                        break;
                    case "6":
                        wroc = true;
                        break;
                    default:
                        Console.WriteLine("Nieznana opcja.");
                        break;
                }
            }
        }

        static void MenuStudenta(Student student, MenedzerOcen menedzerOcen)
        {
            bool wroc = false;
            while (!wroc)
            {
                Console.WriteLine("\n--- MENU STUDENTA ---");
                Console.WriteLine("1. Wyświetl moje oceny");
                Console.WriteLine("2. Wyloguj");
                Console.Write("Opcja: ");

                string opcja = Console.ReadLine();
                switch (opcja)
                {
                    case "1":
                        menedzerOcen.WyswietlOcenyStudenta(student);
                        break;
                    case "2":
                        wroc = true;
                        break;
                    default:
                        Console.WriteLine("Nieznana opcja.");
                        break;
                }
            }
        }
    }
}