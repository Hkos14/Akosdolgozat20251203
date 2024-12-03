
using Akosdolgozata;
using System;
using System.Collections.Generic;

public class Konyv
{
    public string ISBN { get; private set; }
    public List<Szerzo> Szerzok { get; private set; }
    public string Cim { get; private set; }
    public int KiadasEve { get; private set; }
    public string Nyelv { get; private set; }
    public int Keszlet { get; set; }
    public int Ar { get; private set; }

    // Konstruktor minden mezővel
    public Konyv(string cim, string nyelv, int kiadasEve, int ar, int keszlet, params string[] szerzok)
    {
        if (cim.Length < 3 || cim.Length > 64)
            throw new ArgumentException("A cím hossza 3-64 karakter kell, hogy legyen.");
        if (keszlet < 0)
            throw new ArgumentException("A készlet nem lehet negatív.");
        if (ar < 1000 || ar > 10000 || ar % 100 != 0)
            throw new ArgumentException("Az ár 1000 és 10000 közötti kerek 100-as szám kell legyen.");
        if (kiadasEve < 2007 || kiadasEve > DateTime.Now.Year)
            throw new ArgumentException("A kiadás éve nem lehet a 2007-es évnél régebbi, vagy a jelen évnél későbbi.");
        if (nyelv != "angol" && nyelv != "német" && nyelv != "magyar")
            throw new ArgumentException("A nyelv csak angol, német vagy magyar lehet.");

        ISBN = GeneralVeletlenISBN();
        Szerzok = new List<Szerzo>();
        foreach (var szerzoNev in szerzok)
        {
            Szerzok.Add(new Szerzo(szerzoNev));
        }
        Cim = cim;
        KiadasEve = kiadasEve;
        Nyelv = nyelv;
        Keszlet = keszlet;
        Ar = ar;
    }

    // Konstruktor alapértelmezett értékekkel
    public Konyv(string cim, string szerzo)
    {
        Cim = cim;
        Szerzok = new List<Szerzo> { new Szerzo(szerzo) };
        ISBN = GeneralVeletlenISBN();
        KiadasEve = 2024;
        Nyelv = "magyar";
        Keszlet = 0;
        Ar = 4500;
    }

    private string GeneralVeletlenISBN()
    {
        Random veletlen = new Random();
        return string.Join("", new int[10].Select(x => veletlen.Next(0, 10).ToString()));
    }

    public override string ToString()
    {
        string szerzoSzoveg = Szerzok.Count == 1 ? "szerző:" : "szerzők:";
        string keszletSzoveg = Keszlet == 0 ? "beszerzés alatt" : $"{Keszlet} db";
        return $"{Cim} - {szerzoSzoveg} {string.Join(", ", Szerzok.Select(s => s.KeresztNev + " " + s.VezetekNev))}, Kiadás éve: {KiadasEve}, Nyelv: {Nyelv}, Készlet: {keszletSzoveg}, Ár: {Ar} Ft";
    }
}