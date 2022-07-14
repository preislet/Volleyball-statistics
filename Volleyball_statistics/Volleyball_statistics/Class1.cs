using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.WriteLine("Error: " + ex.ToString());
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
            public int body = 0;
            public int chyby = 0;
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
                if (z == 0)
                {
                    servis[3]++;
                    chyby++;
                }
                if (z == 5) servis[2]++;
                if (z == 3) servis[1]++;
                if (z == 1)
                {
                    servis[0]++;
                    body++;
                }
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
                if (z == 0)
                {
                    blok[3]++;
                    chyby++;
                }
                if (z == 5) blok[2]++;
                if (z == 3) blok[1]++;
                if (z == 1)
                {
                    blok[0]++;
                    body++;
                }
            }


            /// <summary>
            /// Zajišťování zápisu statistiky pole hráče (int z je známka pole)
            /// 1 - výborný zákrok
            /// chyba - kritická (nevynucená chyba v poli)
            /// </summary>
            public virtual void Pole_zmena(int z)
            {
                if (z == 0)
                {
                    pole[1]++;
                    chyby++;
                }
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
                if (z == 0)
                {
                    utok[3]++;
                    chyby++;
                }
                if (z == 5) utok[2]++;
                if (z == 3) utok[1]++;
                if (z == 1)
                {
                    utok[0]++;
                    body++;
                }
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
                if (z == 0)
                {
                    prijem[3]++;
                    chyby++;
                }
                if (z == 5) prijem[2]++;
                if (z == 3) prijem[1]++;
                if (z == 1) prijem[0]++;
            }
        }


        /// <summary>
        /// Class Smecar - třída zajišťující statistiku smečařů
        /// </summary>
        sealed class Smecar : Hrac
        {
            protected string tag = "Smecar";
            protected bool prijemVeSmene = false;

            /// <summary>
            /// 2D pole - sleduje, zda hráč utočí lépe, když balón zároveň přijmul nebo ne
            /// index i 0 - za 1, index i 1 - za 3; index i 2 - za 5; index i 3 - chyba 
            /// index j 0 - prijemVeSmene = true (hrac zároveň přijímal balón), inde j 1 - prijemVeSmene = false (hrac nepřijímal balón)
            /// </summary>
            public int[,] utokPoPrijmu = new int[4, 2];

            public string Tag { get { return tag; } }

            public Smecar(string jmeno, int cislo) : base(jmeno, cislo) { }
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
        sealed class Blokar : Hrac
        {
            protected string tag = "Blokar";

            public string Tag { get { return tag; } }
            public Blokar(string jmeno, int cislo) : base(jmeno, cislo) { }
        }


        /// <summary>
        /// Class Nahravac - třída zajišťující statistiku nahravačů
        /// </summary>
        sealed class Nahravac : Hrac
        {
            protected string tag = "Nahravac";

            public string Tag { get { return tag; } }
            public Nahravac(string jmeno, int cislo) : base(jmeno, cislo) { }
        }


        /// <summary>
        /// Class Univerzal - třída zajišťující statistiku univerzálů
        /// </summary>
        sealed class Univerzal : Hrac
        {
            protected string tag = "Univerzal";

            public string Tag { get { return tag; } }
            public Univerzal(string jmeno, int cislo) : base(jmeno, cislo) { }
        }

        /// <summary>
        /// Class Libero - třída zajišťující statistiku liber
        /// </summary>
        sealed class Libero : Hrac
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
        protected int aktivniLiberoBlokarDomaci;
        protected object[] liberaDomaci = new object[3];
        public int aktivniLiberoDomaciPozice = 7;
        public object PosledniPrijimajiciHrac;
        readonly Skore skore;
        readonly Hriste hriste;

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
            get { return postaveniHoste; }
            set { postaveniHoste = value; }
        }
        public int AktivniLiberoBlokarDomaci
        {
            get { return aktivniLiberoBlokarDomaci; }
            set { aktivniLiberoBlokarDomaci = value; }
        }

        #endregion


        //Konstruktory
        public Postaveni_a_StatistikaHracu(string sestava, Skore skore, Hriste hriste)
        {
            this.sestava = sestava;
            NacteniSestavy(sestava);
            this.skore = skore;
            this.hriste = hriste;
        }


        //Public Funkce

        /// <summary>
        /// Funkce zajišťuje rotaci týmů
        /// true = rotace domácích
        /// false = rotace hostů
        /// </summary>
        public void Rotace(bool z)
        {
            //Pokud z je true, tak rotuje strana Domácích
            if (z)
            {
                int prvniPozice = postaveniDomaci[0];
                //Zajištění prohazování s liberem
                if (aktivniLiberoDomaciPozice == 4)
                {
                    int cisloLibera = postaveniDomaci[4];
                    postaveniDomaci[4] = aktivniLiberoBlokarDomaci;
                    aktivniLiberoDomaciPozice = 7;
                    aktivniLiberoBlokarDomaci = cisloLibera;
                }
                // Posun libera
                if (aktivniLiberoDomaciPozice != 7)
                {
                    if (aktivniLiberoDomaciPozice == 0) aktivniLiberoDomaciPozice = 5;
                    else aktivniLiberoDomaciPozice--;
                }

                for (int i = 1; i < postaveniDomaci.Length; i++)
                {
                    postaveniDomaci[i - 1] = postaveniDomaci[i];
                }
                postaveniDomaci[5] = prvniPozice;
            }
            else
            {
                // Po tom, co Blokar dopodával, tak odcházi na stridacku a do gry prichází libero
                if (CisloBlokare(postaveniDomaci[0]))
                {
                    int cisloBlokare = postaveniDomaci[0];
                    postaveniDomaci[0] = aktivniLiberoBlokarDomaci;
                    aktivniLiberoDomaciPozice = 0;
                    aktivniLiberoBlokarDomaci = cisloBlokare;
                }
                int prvniPozice = postaveniHoste[0];
                for (int i = 1; i < postaveniHoste.Length; i++)
                {
                    postaveniHoste[i - 1] = postaveniHoste[i];
                }
                postaveniHoste[5] = prvniPozice;
            }
        }

        /// <summary>
        /// Načítá sestavu, kterou uživatel vepsal do hřiště pro kontrolu pozic, do pole
        /// </summary>
        public void NactenySestavNaHristi(int D1 = 0, int D2 = 0, int D3 = 0, int D4 = 0, int D5 = 0, int D6 = 0, int H1 = 0, int H2 = 0, int H3 = 0, int H4 = 0, int H5 = 0, int H6 = 0)
        {
            postaveniDomaci[0] = D1;
            postaveniDomaci[1] = D2;
            postaveniDomaci[2] = D3;
            postaveniDomaci[3] = D4;
            postaveniDomaci[4] = D5;
            postaveniDomaci[5] = D6;
            postaveniHoste[0] = H1;
            postaveniHoste[1] = H2;
            postaveniHoste[2] = H3;
            postaveniHoste[3] = H4;
            postaveniHoste[4] = H5;
            postaveniHoste[5] = H6;
            KontrolaLiber();
        }

        /// <summary>
        /// Kontroluje zda se blokar nenachází v zadní lajně
        /// </summary>
        public bool CisloBlokare(int z)
        {
            for (int i = 0; i < hraciDomaci.Length; i++)
            {
                if (hraciDomaci[i] is null) return false;
                if (hraciDomaci[i] is Blokar)
                {
                    Blokar blokar = (Blokar)hraciDomaci[i];
                    if (blokar.Cislo == z) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Načítá hráče, kteří nejsou v hlavní sestavě, ale jsou na střídačce
        /// </summary>
        public void NactiStridacku(TableLayoutPanel s)
        {
            int index = 0;
            object[] stridacka = new object[7];

            ///Pokud je hrac na soupisce a neni v hlavní sestavě, tak se hráč vloží do pole stridacka
            for (int i = 0; i < hraciDomaci.Length; i++)
            {
                int Lock = 0;
                if (hraciDomaci[i] is null) break;
                Hrac hrac = (Hrac)hraciDomaci[i];
                for (int j = 0; j < postaveniDomaci.Length; j++)
                {
                    if ((hrac.Cislo == postaveniDomaci[j]) || hrac.Cislo == aktivniLiberoBlokarDomaci)
                    {
                        Lock = 1;
                        break;
                    }
                }
                if (Lock == 0)
                {
                    stridacka[index] = hrac;
                    index++;
                }
            }

            //Zapsání Hráčů do tabulky vedle hřiště
            for (int i = 0; i < stridacka.Length; i++)
            {
                if (stridacka[i] is null) break;
                Hrac hracKZapisu = (Hrac)stridacka[i];
                Control c = s.GetControlFromPosition(0, i);
                c.Text = hracKZapisu.Jmeno;
                c = s.GetControlFromPosition(1, i);
                c.Text = (hracKZapisu.Cislo).ToString();
                c = s.GetControlFromPosition(2, i);
                switch (hracKZapisu)
                {
                    case Nahravac:
                        c.Text = "N";
                        break;
                    case Smecar:
                        c.Text = "S";
                        break;
                    case Univerzal:
                        c.Text = "U";
                        break;
                    case Libero:
                        c.Text = "L";
                        break;
                    case Blokar:
                        c.Text = "B";
                        break;
                }

            }
        }

        /// <summary>
        /// Načítá jména hráčů a zapisuje je na Buttony v tabulce
        /// </summary>
        public void NactiJmenaHracuDoTabulky(TableLayoutPanel s)
        {
            for (int i = 0; i < s.RowCount; i++)
            {
                if (hraciDomaci[i] is null) return;
                Control c = s.GetControlFromPosition(0, i + 1);
                c.Text = ((((Hrac)hraciDomaci[i]).Cislo).ToString() + "  " + ((Hrac)hraciDomaci[i]).Jmeno);
            }
        }

        /// <summary>
        /// Funkce zvírazňuje ty hráče v tabulce, kteří jsou zrovna na hřišti
        /// </summary>
        public void ZobrazAktivniHraceVTabulce(TableLayoutPanel s)
        {
            for (int i = 1; i < s.RowCount; i++)
            {
                Control c = s.GetControlFromPosition(0, i);
                c.BackColor = Color.Gray;
            }

            for (int i = 1; i < s.RowCount; i++)
            {
                Hrac hrac = null;
                Control c = s.GetControlFromPosition(0, i);
                string jmeno = Regex.Replace(c.Text, @"[\d-]", string.Empty);
                jmeno = Regex.Replace(jmeno, @"\s+", "");

                for (int k = 0; k < hraciDomaci.Length; k++)
                {
                    if (jmeno == "XXXXXXXXXXXXXXXXX") break;
                    if (jmeno == ((Hrac)hraciDomaci[k]).Jmeno)
                    {
                        hrac = (Hrac)hraciDomaci[k];
                        break;
                    }
                }
                for (int j = 0; j < postaveniDomaci.Length; j++)
                {
                    if (hrac is null) break;
                    if ((hrac.Cislo == postaveniDomaci[j]) || hrac.Cislo == aktivniLiberoBlokarDomaci)
                    {
                        c.BackColor = Color.LightGreen;
                    }
                }

            }
        }

        /// <summary>
        /// Funkce vyhodnocuj akci zapsanou do tabulky hráčů
        /// Vyhodnocuje všechy typy akcí a zapisuje je do příslušných polí
        /// S - servis, P - příjem, U - útok, B - blok, p - pole
        /// </summary>
        public void VyhodnotAkci(char akce, int znamka, string jmenoHrace)
        {
            Hrac hrac = null;
            for (int i = 0; i < hraciDomaci.Length; i++)
            {
                if (hraciDomaci[i] is null) break;
                hrac = (Hrac)hraciDomaci[i];
                if (hrac.Jmeno == jmenoHrace)
                {
                    switch (akce)
                    {
                        case 'S':
                            hrac.Servis_zmena(znamka);
                            break;
                        case 'P':
                            PosledniPrijimajiciHrac = hrac;
                            hrac.Prijem_zmena(znamka);
                            break;
                        case 'U':
                            hrac.Utok_zmena(znamka);
                            if (PosledniPrijimajiciHrac is Smecar)
                            {
                                Smecar smecar = (Smecar)PosledniPrijimajiciHrac;
                                smecar.RestartPrijmuVeSmene();
                            }
                            break;
                        case 'B':
                            hrac.Blok_zmena(znamka);
                            if (PosledniPrijimajiciHrac is Smecar)
                            {
                                Smecar smecar = (Smecar)PosledniPrijimajiciHrac;
                                smecar.RestartPrijmuVeSmene();
                            }
                            break;
                        case 'p':
                            hrac.Pole_zmena(znamka);
                            break;

                    }
                    return;
                }
            }
        }

        public void UpdateTabulkyHracu(TableLayoutPanel s)
        {
            for (int i = 1; i < s.RowCount; i++)
            {
                if (hraciDomaci[i] is null) break;
                int servisProcenta = Procenta(i, 'S');
                int prijemProcenta = Procenta(i, 'P');
                int utokProcenta = Procenta(i, 'U');
                int blokProcenta = Procenta(i, 'B');
                int poleProcenta = Procenta(i, 'p');
                Hrac hrac = (Hrac)hraciDomaci[i];
                Control c = s.GetControlFromPosition(1, i + 1);
                c.Text = (hrac.body).ToString();
                c = s.GetControlFromPosition(2, i + 1);
                c.Text = (hrac.chyby).ToString();
                c = s.GetControlFromPosition(3, i + 1);
                c.Text = (servisProcenta).ToString() + " %";
                c = s.GetControlFromPosition(4, i + 1);
                c.Text = (prijemProcenta).ToString() + " %";
                c = s.GetControlFromPosition(5, i + 1);
                c.Text = (utokProcenta).ToString() + " %";
                c = s.GetControlFromPosition(6, i + 1);
                c.Text = (blokProcenta).ToString() + " %";
                c = s.GetControlFromPosition(7, i + 1);
                c.Text = (poleProcenta).ToString() + " %";

            }
        }
        // Private Funkce 

        /// <summary>
        /// Funkce zkontroluje, jestli se náhodou libero nenachází na hřišti a na střídačce není blokař
        /// Blokař, co se v zápisu napíše do zadní lajny, tak se automaticky točí se liberem
        /// </summary>
        private void KontrolaLiber()
        {
            int index = 0;  //pomocný index pro vpisování do pole
            for (int i = 0; i < hraciDomaci.Length; i++)
            {
                if (hraciDomaci[i] is null) break;
                if (hraciDomaci[i] is Libero)
                {
                    liberaDomaci[index] = hraciDomaci[i];
                    index++;
                }
            }
            for (int i = 4; i < 6; i++)
            {
                //Blokar muze podávat, ale pokud nepodava, tak strida za libero
                if (CisloBlokare(postaveniDomaci[0]) && !skore.Podani)
                {
                    int cisloBlokare = postaveniDomaci[0];
                    postaveniDomaci[0] = aktivniLiberoBlokarDomaci;
                    aktivniLiberoDomaciPozice = 0;
                    aktivniLiberoBlokarDomaci = cisloBlokare;
                    break;
                }
                if (CisloBlokare(postaveniDomaci[i]))
                {
                    int cisloBlokare = postaveniDomaci[i];
                    postaveniDomaci[i] = aktivniLiberoBlokarDomaci;
                    aktivniLiberoDomaciPozice = i;
                    aktivniLiberoBlokarDomaci = cisloBlokare;
                    break;
                }
            }

        }

        /// <summary>
        /// Funkce načte sestavu z vybraného .zxz souboru, který uživatel vybtal v hlavním menu
        /// Jméno sestavy je zjištěno pomocí třídy menu, které toto jméno nesla z formuláře hlavního menu
        /// </summary>
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
                string cislo = "";
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
                    else if (!nacteniCislo) cislo += line[i];
                    else pozice = line[i];
                }
                int cislo2 = Convert.ToInt32(cislo);

                //Vytvoření třídy
                if (pozice == 'S') hraciDomaci[index] = new Smecar(jmeno, cislo2);
                if (pozice == 'B') hraciDomaci[index] = new Blokar(jmeno, cislo2);
                if (pozice == 'N') hraciDomaci[index] = new Nahravac(jmeno, cislo2);
                if (pozice == 'U') hraciDomaci[index] = new Univerzal(jmeno, cislo2);
                if (pozice == 'L') hraciDomaci[index] = new Libero(jmeno, cislo2);
                index++;
            }
            //Console.WriteLine("Nacteno");
        }

        /// <summary>
        /// Funkce vypočítává procenta z polí ve třídě hráč
        /// </summary>
        private int Procenta(int i, char a)
        {
            Hrac hrac = (Hrac)hraciDomaci[i];
            if (a == 'S')
            {
                int zaJedna = hrac.Servis[0] * 100;
                int zaTri = hrac.Servis[1] * 50;
                int zaPet = hrac.Servis[2] * 25;
                if ((hrac.Servis[0] + hrac.Servis[1] + hrac.Servis[2] + hrac.Servis[3]) == 0) return 0;
                int celkem = (zaJedna + zaTri + zaPet) / (hrac.Servis[0] + hrac.Servis[1] + hrac.Servis[2] + hrac.Servis[3]);
                return celkem;
            }
            else if (a == 'P')
            {
                int zaJedna = hrac.Prijem[0] * 100;
                int zaTri = hrac.Prijem[1] * 50;
                int zaPet = hrac.Prijem[2] * 25;
                if ((hrac.Prijem[0] + hrac.Prijem[1] + hrac.Prijem[2] + hrac.Prijem[3]) == 0) return 0;
                int celkem = (zaJedna + zaTri + zaPet) / (hrac.Prijem[0] + hrac.Prijem[1] + hrac.Prijem[2] + hrac.Prijem[3]);
                return celkem;
            }
            else if (a == 'U')
            {
                int zaJedna = hrac.Utok[0] * 100;
                int zaTri = hrac.Utok[1] * 50;
                int zaPet = hrac.Utok[2] * 25;
                if ((hrac.Utok[0] + hrac.Utok[1] + hrac.Utok[2] + hrac.Utok[3]) == 0) return 0;
                int celkem = (zaJedna + zaTri + zaPet) / (hrac.Utok[0] + hrac.Utok[1] + hrac.Utok[2] + hrac.Utok[3]);
                return celkem;
            }
            else if (a == 'B')
            {
                int zaJedna = hrac.Blok[0] * 100;
                int zaTri = hrac.Blok[1] * 50;
                int zaPet = hrac.Blok[2] * 25;
                if ((hrac.Blok[0] + hrac.Blok[1] + hrac.Blok[2] + hrac.Blok[3]) == 0) return 0;
                int celkem = (zaJedna + zaTri + zaPet) / (hrac.Blok[0] + hrac.Blok[1] + hrac.Blok[2] + hrac.Blok[3]);
                return celkem;
            }
            else if (a == 'p')
            {
                int zaJedna = hrac.Pole[0] * 100;
                if ((hrac.Pole[0] + hrac.Pole[1]) == 0) return 0;
                int celkem = zaJedna / (hrac.Pole[0] + hrac.Pole[1]);
                return celkem;
            }
            else return 0;
        }

        private object SpecializaceHrace(Hrac hrac)
        {
            switch (hrac)
            {
                case Nahravac:
                    return (Nahravac)hrac;
                case Smecar:
                    return (Smecar)hrac;
                case Univerzal:
                    return (Univerzal)hrac;
                case Libero:
                    return (Libero)hrac;
                case Blokar:
                    return(Blokar)hrac;
            }
            return null;
        }
    }
}
