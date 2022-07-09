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
        #region Třídy Hráčů
        /// <summary>
        /// Class Hrac - vzorová třída pro další specializované pozice
        /// </summary>
        abstract class Hrac
        {
            // index 0 - za 1, index 1 - za 3; index 2 - za 5; index 3 - chyba (pole ma pouze 1 nebo chyba)
            // Chyba je hodnocena známkou 0
            protected int[] servis = new int[4];
            protected int[] blok = new int[4];
            protected int[] pole = new int[2];
            protected int[] utok = new int[4];
            protected int[] prijem = new int[4];
            protected int cislo;
            protected string? jmeno;

            #region Get/Set metody pro třídu Hrac a její syny
            public int[] Servis
            {
                get { return servis; }
            }
            public int[] Blok
            {
                get { return blok; }
            }
            public int[] Pole
            {
                get { return pole; }
            }
            public int[] Utok
            {
                get { return utok; }
            }
            public int[] Prijem
            {
                get { return prijem; }
            }
            public int Cislo
            {
                get { return cislo; }
                set { cislo = value; }
            }
            public string Jmeno
            {
                get { return jmeno; }
                set { jmeno = value; }
            }
            #endregion


            //Konstruktor
            public Hrac(string jmeno, int cislo)
            {
                this.jmeno = jmeno;
                this.cislo = cislo;
            }
            
            /// <summary>
            /// Zajišťování zápisu statistiky servisu hráče (int z je známka servisu)
            /// 1 - eso
            /// 3 - soupeřící tým nepřijal za 1
            /// 5 - soupeřící tým přijal za 1
            /// chyba - chyba
            /// </summary>
            public virtual void Servis_zmena(int z) 
            {
                if (z == 0) servis[3]++;
                if (z == 5) servis[2]++;
                if (z == 3) servis[1]++;
                if (z == 1) servis[0]++;
            }


            /// <summary>
            /// Zajišťování zápisu statistiky bloku hráče (int z je známka bloku)
            /// 1 - bodový blok
            /// 3 - soupeřící tým po bloku ve velikých problémech
            /// 5 -soupeřící tým zvedl v pohodě balón
            /// chyba - chyba bloku
            /// </summary>
            public virtual void Blok_zmena(int z) 
            {
                if (z == 0) blok[3]++;
                if (z == 5) blok[2]++;
                if (z == 3) blok[1]++;
                if (z == 1) blok[0]++;
            }


            /// <summary>
            /// Zajišťování zápisu statistiky pole hráče (int z je známka pole)
            /// 1 - výborný zákrok
            /// chyba - kritická (nevynucená chyba v poli)
            /// </summary>
            public virtual void Pole_zmena(int z) 
            {
                if (z == 0) pole[1]++;
                if (z == 1) pole[0]++;
            }


            /// <summary>
            /// Zajišťování zápisu statistiky útoku hráče (int z je známka útoku)
            /// 1 - bodový útok
            /// 3 - soupeřící tým ve velkých problémech
            /// 5 - soupeřící tým přijal balón bez problému
            /// chyba - chyba na útoku (out, blok)
            /// </summary>
            public virtual void Utok_zmena(int z) 
            {
                if (z == 0) utok[3]++;
                if (z == 5) utok[2]++;
                if (z == 3) utok[1]++;
                if (z == 1) utok[0]++;
            }


            /// <summary>
            /// Zajišťování zápisu statistiky příjmu hráče (int z je známka příjmu)
            /// 1 - příjem za 1
            /// 3 - příjem do trojky
            /// 5 - špatný příjem, ale hraje se dál
            /// chyba - eso
            /// </summary>
            public virtual void Prijem_zmena(int z)
            {
                if (z == 0) prijem[3]++;
                if (z == 5) prijem[2]++;
                if (z == 3) prijem[1]++;
                if (z == 1) prijem[0]++;
            }
        }


        /// <summary>
        /// Class Smecar - třída zajišťující statistiku smečařů
        /// </summary>
        sealed class Smecar: Hrac
        {
            protected string tag = "Smecar";
            protected bool prijemVeSmene = false;

            /// <summary>
            /// 2D pole - sleduje, zda hráč utočí lépe, když balón zároveň přijmul nebo ne
            /// index i 0 - za 1, index i 1 - za 3; index i 2 - za 5; index i 3 - chyba 
            /// index j 0 - prijemVeSmene = true (hrac zároveň přijímal balón), inde j 1 - prijemVeSmene = false (hrac nepřijímal balón)
            /// </summary>
            public int[,] utokPoPrijmu = new int[4,2];

            public Smecar(string jmeno, int cislo) : base(jmeno, cislo){}
            public string Tag { get { return tag; } }
            public override void Prijem_zmena(int z)
            {
                if (z == 0) prijem[3]++;
                if (z == 5) prijem[2]++;
                if (z == 3) prijem[1]++;
                if (z == 1) prijem[0]++;
                prijemVeSmene = true;
            }
            //TODO - Musím zajistit, aby se prijemVeSmene vždy po Smene nastavil na false
            public override void Utok_zmena(int z)
            {
                base.Utok_zmena(z);
                if (prijemVeSmene)
                {
                    if (z == 0) utokPoPrijmu[3, 0]++;
                    if (z == 5) utokPoPrijmu[2, 0]++;
                    if (z == 3) utokPoPrijmu[1, 0]++;
                    if (z == 1) utokPoPrijmu[0, 0]++;
                    prijemVeSmene = false;
                }
                else
                {
                    if (z == 0) utokPoPrijmu[3, 1]++;
                    if (z == 5) utokPoPrijmu[2, 1]++;
                    if (z == 3) utokPoPrijmu[1, 1]++;
                    if (z == 1) utokPoPrijmu[0, 1]++;
                }
            }
            public void RestartPrijmuVeSmene() { prijemVeSmene = false; }
        }


        /// <summary>
        /// Class Blokar - třída zajišťující statistiku blokařů
        /// </summary>
        sealed class Blokar: Hrac 
        {
            protected string tag = "Blokar";
            public string Tag { get { return tag; } }
            public Blokar(string jmeno, int cislo) : base(jmeno, cislo) { }
        }


        /// <summary>
        /// Class Nahravac - třída zajišťující statistiku nahravačů
        /// </summary>
        sealed class Nahravac: Hrac 
        {
            protected string tag = "Nahravac";
            public string Tag { get { return tag; } }
            public Nahravac(string jmeno, int cislo) : base(jmeno, cislo) { }
        }


        /// <summary>
        /// Class Univerzal - třída zajišťující statistiku univerzálů
        /// </summary>
        sealed class Univerzal: Hrac 
        {
            protected string tag = "Univerzal";
            public string Tag { get { return tag; } }
            public Univerzal(string jmeno, int cislo) : base(jmeno, cislo) { }
        }

        /// <summary>
        /// Class Libero - třída zajišťující statistiku liber
        /// </summary>
        sealed class Libero: Hrac 
        {
            string tag = "Libero";
            public string Tag { get { return tag; } }
            public Libero(string jmeno, int cislo) : base(jmeno, cislo) { }
        }
        #endregion

        protected string sestava;
        protected object[] hraciDomaci = new object[14];
        protected int[] postaveniDomaci = new int[6];
        protected int[] postaveniHoste = new int[6];

        #region Get/Set
        public string Sestava
        {
            get { return sestava; }
            set { sestava = value; }
        }
        public object[] HraciDomaci
        {
            get { return hraciDomaci; }
            set { hraciDomaci = value; }
        }
        public int[] PostaveniDomaci
        {
            get { return postaveniDomaci; }
            set { postaveniDomaci = value; }
        }
        public int[] PostaveniHoste
        {
            get { return PostaveniHoste; }
            set { PostaveniHoste = value; }
        }
        #endregion
        //Konstruktory
        public Postaveni_a_StatistikaHracu(string sestava)
        {
            this.sestava = sestava;
            NacteniSestavy(sestava);
        }
        //Public Funkce

        /// <summary>
        /// Funkce zajišťuje rotaci týmů
        /// true = rotace domácích
        /// false = rotace hostů
        /// </summary>
        public void Rotace(bool z)
        {
            if (z)
            {
                int posledniPozice = postaveniDomaci[6];
                for (int i = 1; i < postaveniDomaci.Length; i++)
                {
                    postaveniDomaci[postaveniDomaci.Length - i] = postaveniDomaci[postaveniDomaci.Length - i + 1];
                }
                postaveniDomaci[0] = posledniPozice;
            }
            else
            {
                int posledniPozice = postaveniHoste[6];
                for (int i = 1; i < postaveniHoste.Length; i++)
                {
                    postaveniHoste[postaveniHoste.Length - i] = postaveniDomaci[postaveniHoste.Length - i + 1];
                }
                postaveniHoste[0] = posledniPozice;
            }
        }


        // Private Funkce 
        private void NacteniSestavy(string sestava)
        {
            int index = 0; // Pomocný index pro vkládání tříd hráčů do pole hraciDomaci 
            string[] lines = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volleyball statistics", "Sestavy", sestava + ".txt"));

            foreach (string line in lines)
            {
                // Pomocné zámky
                bool nacteniJmeno = false;
                bool nacteniCislo = false;

                // Proměné, které se zapíší do třídy dle pozice
                string jmeno = "";
                int cislo = 0;
                char pozice = 'x';

                // Nacteni řádku
                for (int i = 1; i < line.Length - 1; i++)
                {
                    if ((line[i] == '*') && (!nacteniJmeno))
                    {
                        nacteniJmeno = true;
                        continue;
                    }
                    if ((line[i] == '*') && (!nacteniCislo))
                    {
                        nacteniCislo = true;
                        continue;
                    }
                    if (!nacteniJmeno) jmeno += line[i];
                    else if (!nacteniCislo) cislo = Convert.ToInt32(line[i]);
                    else pozice = line[i];
                }

                //Vytvoření třídy
                if (pozice == 'S') hraciDomaci[index] = new Smecar(jmeno, cislo); 
                if (pozice == 'B') hraciDomaci[index] = new Blokar(jmeno, cislo);
                if (pozice == 'N') hraciDomaci[index] = new Nahravac(jmeno, cislo);
                if (pozice == 'U') hraciDomaci[index] = new Univerzal(jmeno, cislo);
                if (pozice == 'L') hraciDomaci[index] = new Libero(jmeno, cislo);
                index++;
                                   
            }
        }
    }
}
