using System;

class ConvertorBaza
{
    // Conversie partea intreaga intr-o alta baza
    static string ConversieParteIntreaga(int numar, int bazaTinta)
    {
        if (numar == 0)
            return "0";

        string rezultat = "";
        while (numar > 0)
        {
            int rest = numar % bazaTinta;
            rezultat = ObtineCifraPentruValoare(rest) + rezultat;
            numar /= bazaTinta;
        }
        return rezultat;
    }

    // Conversie partea fractionara intr-o alta baza
    static string ConversieParteFractiunara(double fractiune, int bazaTinta, int precizie = 10)
    {
        string rezultat = "";
        for (int i = 0; i < precizie && fractiune > 0; i++)
        {
            fractiune *= bazaTinta;
            int cifra = (int)fractiune;
            rezultat += ObtineCifraPentruValoare(cifra);
            fractiune -= cifra;
        }
        return rezultat;
    }


    // Obtine caracterul unei valori din baza
    static char ObtineCifraPentruValoare(int valoare)
    {
        if (valoare >= 0 && valoare <= 9)
            return (char)('0' + valoare);
        else
            return (char)('A' + (valoare - 10));
    }

    // Obtine valoarea unui caracter pentru o baza
    static int ObtineValoarePentruCifra(char cifra)
    {
        if (cifra >= '0' && cifra <= '9')
            return cifra - '0';
        else if (cifra >= 'A' && cifra <= 'F')
            return cifra - 'A' + 10;
        else if (cifra >= 'a' && cifra <= 'f')
            return cifra - 'a' + 10;
        throw new ArgumentException($"Cifra '{cifra}' este invalidă pentru baza specificată.");
    }

    // Conversie numar din orice baza în zecimal
    static double ConversieInZecimal(string numar, int bazaInitiala)
    {
        string[] parti = numar.Split('.');
        double parteIntreaga = 0;
        double parteFractiunara = 0;


        for (int i = 0; i < parti[0].Length; i++)
        {
            parteIntreaga = parteIntreaga * bazaInitiala + ObtineValoarePentruCifra(parti[0][i]);
        }


        if (parti.Length > 1)
        {
            for (int i = 0; i < parti[1].Length; i++)
            {
                parteFractiunara += ObtineValoarePentruCifra(parti[1][i]) / Math.Pow(bazaInitiala, i + 1);
            }
        }

        return parteIntreaga + parteFractiunara;
    }

    // Functia principala pentru conversie
    static void ConversieNumar(string numar, int bazaInitiala, int bazaTinta)
    {

        double valoareZecimala = ConversieInZecimal(numar, bazaInitiala);


        int parteIntreaga = (int)valoareZecimala;
        double parteFractiunara = valoareZecimala - parteIntreaga;


        string parteIntreagaInBazaTinta = ConversieParteIntreaga(parteIntreaga, bazaTinta);
        string parteFractiunaraInBazaTinta = ConversieParteFractiunara(parteFractiunara, bazaTinta);


        if (parteFractiunaraInBazaTinta.Length > 0)
            Console.WriteLine($"{parteIntreagaInBazaTinta}.{parteFractiunaraInBazaTinta}");
        else
            Console.WriteLine(parteIntreagaInBazaTinta);
    }

    static void Main()
    {
        Console.WriteLine("Introduceti numarul pentru conversie:");
        string numar = Console.ReadLine();

        Console.WriteLine("Introduceti baza numarului (2-16):");
        int bazaInitiala = int.Parse(Console.ReadLine());

        Console.WriteLine("Introduceti baza tinta pentru conversie (2-16):");
        int bazaTinta = int.Parse(Console.ReadLine());

        ConversieNumar(numar, bazaInitiala, bazaTinta);
    }
}
