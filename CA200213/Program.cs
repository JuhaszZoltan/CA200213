using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA200213
{
    struct Kuldetes
    {
        public string Kod;
        public DateTime Kezdes;
        public string Siklo;
        public TimeSpan Ido;
        public string Tamaszpont;
        public int Legenyseg;
    }
    
    class Program
    {
        static List<Kuldetes> kuldetesek;
        static void Main()
        {
            F02();
            F03();
            F04();
            F05();
            F06();
            F07();
            F08();
            F09();
            F10();
            Console.ReadKey();
        }

        private static void F10()
        {
            var dic = new Dictionary<string, double>();

            foreach (var k in kuldetesek)
            {
                if (!dic.ContainsKey(k.Siklo))
                {
                    dic.Add(k.Siklo, k.Ido.TotalDays);
                }
                else dic[k.Siklo] += k.Ido.TotalDays;
            }
            var sw = new StreamWriter("ursiklok.txt", false, Encoding.UTF8);
            foreach (var kvp in dic)
            {
                sw.WriteLine("{0, -12} {1: 000.00}", kvp.Key, kvp.Value);
            }
            sw.Close();
        }

        private static void F09()
        {
            int nem = 0;
            int kndy = 0;
            foreach (var k in kuldetesek)
            {
                if (k.Tamaszpont == "Kennedy") kndy++;
                if (k.Tamaszpont == "nem landolt") nem++;
            }

            Console.WriteLine($"A landolsok {kndy / (float)(kuldetesek.Count - nem) * 100}% volt a Kennedy űá.-n");
        }

        private static void F08()
        {
            Console.WriteLine("8. feladat:");
            Console.Write("\tév: ");
            var ev = int.Parse(Console.ReadLine());

            int c = 0;

            foreach (var k in kuldetesek)
            {
                if (ev == k.Kezdes.Year) c++;
            }

            if(c != 0) Console.WriteLine($"\t {ev}-ben {c} db küldi volt");
            else Console.WriteLine($"\t{ev}-ben nem volt küldi");
        }

        private static void F07()
        {
            Console.WriteLine("7. feladat:");
            var maxIdoInd = 0;
            for (int i = 1; i < kuldetesek.Count; i++)
            {
                if(kuldetesek[maxIdoInd].Ido < kuldetesek[i].Ido)
                {
                    maxIdoInd = i;
                }
            }
            Console.WriteLine(
                $"\tA legtöbb időt a {kuldetesek[maxIdoInd].Siklo} töltötte az űrben," +
                $"a {kuldetesek[maxIdoInd].Kod} küldetésén" +
                $" összesen {kuldetesek[maxIdoInd].Ido.TotalHours} órát");
        }

        private static void F06()
        {
            Console.WriteLine("6. feladat:");
            int i = 0;
            while ((
                (kuldetesek[i].Kezdes + kuldetesek[i].Ido).Date.AddDays(1)
                != new DateTime(2003, 02, 01) || 
                kuldetesek[i].Siklo != "Columbia"))
            {
                i++;
            }
            Console.WriteLine($"\tColimbián {kuldetesek[i].Legenyseg} asztronauta vesztette életét");
        }
        private static void F05()
        {
            Console.WriteLine("5. feladat:");
            int c = 0;
            foreach (var k in kuldetesek)
            {
                if (k.Legenyseg < 5) c++;
            }
            Console.WriteLine($"\t{c} alkalommal volt kevesebb, mint 5 utas");
        }
        private static void F04()
        {
            Console.WriteLine("4. feladat:");
            int sum = 0;
            foreach (var k in kuldetesek)
            {
                sum += k.Legenyseg;
            }
            Console.WriteLine($"\tÖsszesen {sum} ember utazott");
        }
        private static void F03()
        {
            Console.WriteLine("3. feladat:");
            Console.WriteLine($"\tÖsszesen {kuldetesek.Count} küldetés volt");
        }
        private static void F02()
        {
            kuldetesek = new List<Kuldetes>();

            var sr = new StreamReader(@"..\..\res\kuldetesek.csv", Encoding.UTF8);


            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine().Split(';');
                kuldetesek.Add(new Kuldetes()
                {
                    Kod = sor[0],
                    Kezdes = DateTime.Parse(sor[1]),
                    Siklo = sor[2],
                    Ido = new TimeSpan(int.Parse(sor[3]), int.Parse(sor[4]), 0, 0),
                    Tamaszpont = sor[5],
                    Legenyseg = int.Parse(sor[6]),
                });
            }

            sr.Close();
        }
    }
}
