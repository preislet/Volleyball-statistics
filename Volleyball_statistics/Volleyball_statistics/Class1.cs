using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volleyball_statistics
{
    public class Menu
    {
        public string? Domaci;
        public string? Hoste;
        public string? Sestava;

        //Konstruktory
        public Menu(string domaci, string hoste, string sestava)
        {
            Domaci = domaci;
            Hoste = hoste;
            Sestava = sestava;
        }
        public Menu()
        {
            Domaci = null;
            Hoste = null;
            Sestava = null;
        }

        //Public functins
        public string FindPath(string s)
        {
            //new použito při tvoření nové složky na Desktopu
            if (s == "new") return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volleyball statistics");

            // null použit, při hledání cesty do složky Sestavy
            if (s == "sestavy") return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volleyball statistics", "Sestavy");

            //excel použit při tvoření nové složky ve složce "Volleyball statistics" se jménem Tabulky Excel
            if (s == "excel") return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volleyball statistics", "Tabulky Excel");

            //Tvoření nového zápisu sestavy
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volleyball statistics", "Sestavy", s + ".txt");
        }
        public void SaveNewTeam(string path, TableLayoutPanel tableLayoutPanel1)
        {
            string[] write = new string[45];  //Ukládání zípisu z formuláře
            int index = 0;
            
            try
            {
                //Vyplňování pole write
                for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
                    for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                    {
                        Control c = tableLayoutPanel1.GetControlFromPosition(j, i);
                        if (c.Text == null) break;
                        write[index] = c.Text;
                        index++;
                    }

                //Zapisování do souboru (.txt)
                TextWriter tw = new StreamWriter(path, true);
                for (int i = 0; i < write.Length; i += 3)
                {
                    if (write[i] == "") break;
                    tw.WriteLine("#" + write[i] + "*" + write[i + 1] + "*" + write[i + 2] + "#");
                }
                tw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chybaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
        }
        public void CreateFolder()
        {
            string path = FindPath("new");  //Vytvoření složky na Ploše
            string path2 = FindPath("sestavy"); //Vytvoření složky ve složce programu (Sestavy)
            string path3 = FindPath("excel"); //Vytvoření složky ve složce programu (Tabulky Exel)

            //Vytvoření složky
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    //Vytvoření hlavní složky
                    System.IO.Directory.CreateDirectory(path);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error 1: " + e.Message);
                }
                try
                {
                    //Vytvoření podsložky Sestavy
                    System.IO.Directory.CreateDirectory(path2);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error 2: " + e.Message);
                }
                try
                {
                    //Vytvoření podsložky Tabulky Excel
                    System.IO.Directory.CreateDirectory(path3);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error 3: " + e.Message);
                }
            }

        }
    }
    public class Hriste
    {
        public int[,] hriste_procenta = new int[3, 6];
        public int[] hriste_outy = new int[2]; // 0 = Domaci, 1 = Hoste;
        public int PocetBoduDomaci = 0;
        public int PocetBoduHoste = 0;
        public bool LastBod;  //true-Domácí, false-Hosté

        //Public functions
        public void ProcentaDopadu(int X, int Y, PictureBox p)
        {
            //Vypocet pozice na obrázku je vypočítaný jako pozice kliku - x a y souřadnice obrázku hřiště (rozměry 630x318)
            int RealnePoziceDopaduX = X - p.Location.X;
            int RealnePoziceDopaduY = Y - p.Location.Y;
            if (RealnePoziceDopaduX < 315) //Balón dopadl na polovinu Domácích
            {
                LastBod = false;  //Určuje, jaký tým naposledy dal bod (false = hoste)
                PocetBoduHoste++;

                //Hledání přesného sektoru, kam míč dopadl
                if (RealnePoziceDopaduX < 105) 
                {
                    if (RealnePoziceDopaduY < 106) hriste_procenta[0, 0]++;
                    else if (RealnePoziceDopaduY > 212) hriste_procenta[2, 0]++;
                    else hriste_procenta[1, 0]++;

                }
                else if (RealnePoziceDopaduX > 215)
                {
                    if (RealnePoziceDopaduY < 106) hriste_procenta[0, 2]++;
                    else if (RealnePoziceDopaduY > 212) hriste_procenta[2, 2]++;
                    else hriste_procenta[1, 2]++;
                }
                else
                {
                    if (RealnePoziceDopaduY < 106) hriste_procenta[0, 1]++;
                    else if (RealnePoziceDopaduY > 212) hriste_procenta[2, 1]++;
                    else hriste_procenta[1, 1]++;
                }
            }
            if (RealnePoziceDopaduX >= 315)  //Balón dopadl na polovinu Hostů
            {
                LastBod = true; //Určuje, jaký tým naposledy dal bod (true = domácí)
                PocetBoduDomaci++;
                //Hledání přesného sektoru, kam míč dopadl
                if (RealnePoziceDopaduX < 420)
                {
                    if (RealnePoziceDopaduY < 106) hriste_procenta[0, 3]++;
                    else if (RealnePoziceDopaduY > 212) hriste_procenta[2, 3]++;
                    else hriste_procenta[1, 3]++;
                }
                else if (RealnePoziceDopaduX > 525)
                {
                    if (RealnePoziceDopaduY < 106) hriste_procenta[0, 5]++;
                    else if (RealnePoziceDopaduY > 212) hriste_procenta[2, 5]++;
                    else hriste_procenta[1, 5]++;
                }
                else
                {
                    if (RealnePoziceDopaduY < 106) hriste_procenta[0, 4]++;
                    else if (RealnePoziceDopaduY > 212) hriste_procenta[2, 4]++;
                    else hriste_procenta[1, 4]++;
                }
            }
        }
    }
    public class Skore
    {
        public int SkoreDomaci;
        public int SkoreHoste;
        public bool Podani;  //true-Domací, false-Hosté
        public int CurrSet;
        public int DomaciSety;
        public int HosteSety;
        
        //Konstruktory
        public Skore(bool podani)
        {
            SkoreDomaci = 0;
            SkoreHoste = 0;
            Podani = podani;
            CurrSet = 1;
            HosteSety = 0;
            DomaciSety = 0;
        }
    }
    public class Postaveni_a_StatistikaHracu
    {
        abstract class Hrac
        {
            // index 0 - za 1, index 1 - za 3; index 2 - za 5; index 3 - chyba (pole ma pouze 1 nebo chyba)
            // Chyba je hodnocena známkou 0
            protected int[] servis = new int[4];
            protected int[] blok = new int[4];
            protected int[] pole = new int[2];
            protected int[] utok = new int[4];

            // Public functions

            /// <summary>
            /// Zajišťování zápisu statistiky servisu hráče (int z je známka servisu)
            /// 1 - eso
            /// 3 - soupeřící tým nepřijal za 1
            /// 5 - soupeřící tým přijal za 1
            /// chyba - chyba
            /// </summary>
            protected void Servis(int z) { }


            /// <summary>
            /// Zajišťování zápisu statistiky bloku hráče (int z je známka bloku)
            /// 1 - bodový blok
            /// 3 - soupeřící tým po bloku ve velikých problémech
            /// 5 -soupeřící tým zvedl v pohodě balón
            /// chyba - chyba bloku
            /// </summary>
            protected void Blok(int z) { }


            /// <summary>
            /// Zajišťování zápisu statistiky pole hráče (int z je známka pole)
            /// 1 - výborný zákrok
            /// chyba - kritická (nevynucená chyba v poli)
            /// </summary>
            protected void Pole(int z) { }


            /// <summary>
            /// Zajišťování zápisu statistiky útoku hráče (int z je známka útoku)
            /// 1 - bodový útok
            /// 3 - soupeřící tým ve velkých problémech
            /// 5 - soupeřící tým přijal balón bez problému
            /// chyba - chyba na útoku (out, blok)
            /// </summary>
            protected void Utok(int z) { }
        }
        class Smecar: Hrac 
        {
            string Pozice = "Smecar";
        }
        class Blokar: Hrac 
        {
            string Pozice = "Smecar";
        }
        class Nahravac: Hrac 
        { 

        }
        class Univerzal: Hrac 
        { 

        }
        class Libero: Hrac 
        { 
        
        }
        public int[] HraciDomaci;
        public int[] PostaveniDomac;
        public int[] PostaveniHoste;

        //Konstruktory
        public Postaveni_a_StatistikaHracu(int[] postaveniDomac, int[] postaveniHoste)
        {
            PostaveniDomac = postaveniDomac;
            PostaveniHoste = postaveniHoste;
        }   
    }
}
