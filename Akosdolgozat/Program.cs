using System;
using System.Collections.Generic;
using System.Linq;

public class KonyvListaGenerator
{
    private static Random veletlen = new Random();

    public static List<Konyv> GeneralKonyvek(int darabszam)
    {
        List<Konyv> konyvek = new List<Konyv>();
        HashSet<string> generaltISBNek = new HashSet<string>();

        string[] cimek = { "A titokzatos erdő", "The Silent Ocean", "Der verlorene Schatz", "Kalandorok" };
        string[] szerzok = { "John Doe", "Jane Smith", "Mark Brown", "Emma White", "László Kovács" };

        for (int i = 0; i < darabszam; i++)
        {
            string cim = cimek[veletlen.Next(cimek.Length)];
            string nyelv = veletlen.NextDouble() < 0.8 ? "magyar" : "angol";
            int kiadasEve = veletlen.Next(2007, DateTime.Now.Year + 1);
            int ar = veletlen.Next(10, 101) * 100;

            var konyvSzerzok = Enumerable.Range(0, veletlen.Next(1, 4)).Select(_ => szerzok[veletlen.Next(szerzok.Length)]).ToList();
            string isbn;
            do isbn = GeneralVeletlenISBN();
            while (!generaltISBNek.Add(isbn)); 

            int keszlet = veletlen.NextDouble() < 0.3 ? 0 : veletlen.Next(5, 11);

            konyvek.Add(new Konyv(cim, nyelv, kiadasEve, ar, keszlet, konyvSzerzok.ToArray()));
        }
        return konyvek;
    }
    private static string GeneralVeletlenISBN()
    {
        return string.Join("", Enumerable.Range(0, 10).Select(_ => veletlen.Next(0, 10).ToString()));
    }
}

public class KonyvBoltSzimulacio
{
    private static Random veletlen = new Random();

    public static void FuttatSzimulacio(List<Konyv> konyvek, int ismetlesek)
    {
        int osszBevetel = 0;

        for (int i = 0; i < ismetlesek; i++)
        {
            if (konyvek.Count == 0) break;

            var veletlenKonyv = konyvek[veletlen.Next(konyvek.Count)];

            if (veletlenKonyv.Keszlet > 0)
            {
                veletlenKonyv.Keszlet--;
                osszBevetel += veletlenKonyv.Ar;
                Console.WriteLine($"Vásárlás: {veletlenKonyv.Cim}, Ár: {veletlenKonyv.Ar} Ft, Készlet: {veletlenKonyv.Keszlet} db");
            }
            else
            {
                Console.WriteLine($"Könyv nem elérhető: {veletlenKonyv.Cim}, Készlet: beszerzés alatt");
                if (veletlen.NextDouble() < 0.5)
                {
                    veletlenKonyv.Keszlet += veletlen.Next(1, 11);
                    Console.WriteLine($"Új készlet érkezett: {veletlenKonyv.Cim}, Készlet: {veletlenKonyv.Keszlet} db");
                }
                else
                {
                    konyvek.Remove(veletlenKonyv);
                    Console.WriteLine($"Könyv már nem elérhető: {veletlenKonyv.Cim}");
                }
            }
        }

        Console.WriteLine($"Teljes bevétel: {osszBevetel} Ft");
    }

    static void Main()
    {
        var konyvek = KonyvListaGenerator.GeneralKonyvek(15);
        KonyvBoltSzimulacio.FuttatSzimulacio(konyvek, 100);
    }
}


////using Akosdolgozata;

////Book book1 = new(
////    title: "hét törpe",
////    year: 2010,
////    authors: new string[] { "Fekete Pityu", "Kiss Jani" },
////    language: "magyar",
////    stock: 1,
////    price: 3500
////    );
////Book book2 = new(
////    title: "jancsi juliska",
////    year: 2010,
////    authors: new string[] { "Nagy Feri" },
////    language: "magyar",
////    stock: 0,
////    price: 3500
////    );

////Console.WriteLine(book1.ToString());
////Console.WriteLine(book2.ToString());

