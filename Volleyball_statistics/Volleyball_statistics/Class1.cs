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
        public string FindPath(string text)
        {
            string s = "volleyball";
            string startuppath = Path.Combine(Environment.CurrentDirectory);
            int k = 0;
            int KontrolDelka = 0;
            for (int i = 0; i < startuppath.Length; i++)
                for (int j = i; j < i + s.Length; j++)
                {
                    if (startuppath[i] == s[0])
                    {
                        if (startuppath[j] == s[j - i]) KontrolDelka++;
                        if (s.Length == KontrolDelka)
                        {
                            k = j;
                            break;
                        }
                    }
                }
            string startuppath2 = "";
            for (int i = 0; i < k + 1; i++)
                startuppath2 += startuppath[i];
            if (text == "") return Path.Combine(startuppath2, "Sestavy");
            string t = "Sestavy\\" + text + ".txt";
            return Path.Combine(startuppath2, t);
        }
        public void SaveNewTeam(string path, TableLayoutPanel tableLayoutPanel1)
        {
            string[] write = new string[45];
            int index = 0;
            try
            {
                TextWriter tw = new StreamWriter(path, true);
                for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
                    for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                    {
                        Control c = tableLayoutPanel1.GetControlFromPosition(j, i);
                        if (c.Text == null) break;
                        write[index] = c.Text;
                        index++;
                    }

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
    }
    public class Hriste
    {
        public int[,] hriste_procenta = new int[3, 6];
        public int[] hriste_outy = new int[2]; // 0 = Domaci, 1 = Hoste;
        public int PocetBoduDomaci = 0;
        public int PocetBoduHoste = 0;

        public void ProcentaDopadu(int X, int Y, PictureBox p)
        {
            int RealnePoziceDopaduX = X - p.Location.X;
            int RealnePoziceDopaduY = Y - p.Location.Y;
            if (RealnePoziceDopaduX < 315)
            {
                PocetBoduHoste++;
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
            if (RealnePoziceDopaduX >= 315)
            {
                PocetBoduDomaci++;
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
        public bool Podani;
        public bool PosledniPodani;

        public Skore(bool podani)
        {
            SkoreDomaci = 0;
            SkoreHoste = 0;
            Podani = podani;
            PosledniPodani = podani;
        }
    }
}
