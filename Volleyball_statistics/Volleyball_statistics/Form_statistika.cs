using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Volleyball_statistics
{
    public partial class Form_statistika : Form
    {
        public static Hriste hriste = new Hriste();
        public static Skore skore = new Skore(false);
        public static Postaveni_a_StatistikaHracu postaveni_A_StatistikaHracu = new Postaveni_a_StatistikaHracu(Form1.menu.Sestava, skore, hriste);
        public Form_statistika()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
        }

        private void Form_statistika_Load(object sender, EventArgs e)
        {
            Label_Domaci.Text = Form1.menu.Domaci;  //Jméno domácího týmu
            Label_Hoste.Text = Form1.menu.Hoste;  //Jméno týmu domácích
            button_OutDomaci.Text = "OUT " + Form1.menu.Domaci;
            button_OutHoste.Text = "OUT " + Form1.menu.Hoste;

            UpdateHriste();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Pozice kurzoru při kliknutí na obrázek hřiště
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;

            hriste.ProcentaDopadu(X, Y, pictureBox1);  //Přepočet procentuálních dopadů do určitých zón
            UpdateHriste();
        }
        private void UpdateHriste()  //Kompletné Update celé statistiky (kromě setů)
        {
            //Skore update
            if (hriste.PocetBoduDomaci + hriste.PocetBoduHoste + hriste.hriste_outy[0] + hriste.hriste_outy[1] != 0)
            {
                if (hriste.LastBod) skore.SkoreDomaci++;
                if (!hriste.LastBod) skore.SkoreHoste++;
            }
            label_SkoreDomaci.Text = skore.SkoreDomaci.ToString();
            label_SkoreHoste.Text = skore.SkoreHoste.ToString();

            //Update Setů
            if ((skore.SkoreDomaci >= 25) || (skore.SkoreHoste >= 25))
            {
                if ((skore.SkoreDomaci - 1) > skore.SkoreHoste) UpdateSet();
                if ((skore.SkoreHoste - 1) > skore.SkoreDomaci) UpdateSet();
            }  
            if (((skore.SkoreDomaci >= 15) || (skore.SkoreHoste >= 15)) && skore.CurrSet == 5)
            {
                if ((skore.SkoreDomaci - 1) > skore.SkoreHoste) UpdateSet();
                if ((skore.SkoreHoste - 1) > skore.SkoreDomaci) UpdateSet();
            }
                

            //Update servisu
            if (hriste.LastBod != skore.Podani)  //Pokud se liší tým, který podával a tým, který jako poslední získal bod, tak se musí změnit podávající strana
            {
                if (hriste.LastBod is true) skore.Podani = true;
                else if (hriste.LastBod is false) skore.Podani = false;
                if (skore.Podani)
                {
                    label_ServisHoste.BackColor = Color.White;
                    label_ServisDomaci.BackColor = Color.Black;
                }
                else
                {
                    label_ServisHoste.BackColor = Color.Black;
                    label_ServisDomaci.BackColor = Color.White;
                }
            }

            //Update procent
            if ((hriste.PocetBoduHoste + hriste.hriste_outy[1]) != 0)
            {
                label1.Text = (hriste.hriste_procenta[0, 0] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label2.Text = (hriste.hriste_procenta[0, 1] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label3.Text = (hriste.hriste_procenta[0, 2] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label4.Text = (hriste.hriste_procenta[1, 0] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label5.Text = (hriste.hriste_procenta[1, 1] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label6.Text = (hriste.hriste_procenta[1, 2] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label7.Text = (hriste.hriste_procenta[2, 0] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label8.Text = (hriste.hriste_procenta[2, 1] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
                label9.Text = (hriste.hriste_procenta[2, 2] * 100 / (hriste.PocetBoduHoste + hriste.hriste_outy[1])).ToString() + "%";
            }
            else
            {
                label1.Text = "0%";
                label2.Text = "0%";
                label3.Text = "0%";
                label4.Text = "0%";
                label5.Text = "0%";
                label6.Text = "0%";
                label7.Text = "0%";
                label8.Text = "0%";
                label9.Text = "0%";
            }
            if ((hriste.PocetBoduDomaci + hriste.hriste_outy[0]) != 0)
            {
                label10.Text = (hriste.hriste_procenta[0, 3] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label11.Text = (hriste.hriste_procenta[0, 4] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label12.Text = (hriste.hriste_procenta[0, 5] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label13.Text = (hriste.hriste_procenta[1, 3] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label14.Text = (hriste.hriste_procenta[1, 4] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label15.Text = (hriste.hriste_procenta[1, 5] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label16.Text = (hriste.hriste_procenta[2, 3] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label17.Text = (hriste.hriste_procenta[2, 4] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
                label18.Text = (hriste.hriste_procenta[2, 5] * 100 / (hriste.PocetBoduDomaci + hriste.hriste_outy[0])).ToString() + "%";
            }
            else
            {
                label10.Text = "0%";
                label11.Text = "0%";
                label12.Text = "0%";
                label13.Text = "0%";
                label14.Text = "0%";
                label15.Text = "0%";
                label16.Text = "0%";
                label17.Text = "0%";
                label18.Text = "0%";
            }
        }

        private void button_Out_Click(object sender, EventArgs e)   //Outy
        {
            hriste.hriste_outy[Convert.ToInt32((sender as Button).Tag)]++;
            if (Convert.ToInt32((sender as Button).Tag) == 1) hriste.LastBod = true;
            if (Convert.ToInt32((sender as Button).Tag) == 0) hriste.LastBod = false;
            UpdateHriste();
        }

        private void button_Servis_Click(object sender, EventArgs e)
        {
            
            if ((sender as Button).Tag == 1.ToString())  //Button Servis Hosté
            {
                // Vracení blokaře do hry, když má jít na servis
                if (textBox_PoziceDomaci1.Text != "")
                {
                    if(postaveni_A_StatistikaHracu.aktivniLiberoDomaciPozice == 0)
                    {
                        int cisloLibera = postaveni_A_StatistikaHracu.PostaveniDomaci[0];
                        postaveni_A_StatistikaHracu.PostaveniDomaci[0] = postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci;
                        postaveni_A_StatistikaHracu.aktivniLiberoDomaciPozice = 7;
                        postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci = cisloLibera;
                        textBox_PoziceDomaci1.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[0].ToString();
                        textBox_7LiberoDomaci.Text = postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci.ToString();
                    }
                }
                if ((hriste.PocetBoduDomaci + hriste.hriste_outy[0] + hriste.PocetBoduHoste + hriste.hriste_outy[1] == 0)) //Počáteční nastavení servisů
                {
                    label_ServisSet1.Text = Form1.menu.Hoste;
                    label_ServisSet2.Text = Form1.menu.Domaci;
                    label_ServisSet3.Text = Form1.menu.Hoste;
                    label_ServisSet4.Text = Form1.menu.Domaci;
                }
                else if (skore.CurrSet == 5) label_ServisSet5.Text = Form1.menu.Hoste;

                //Přenastavení servisu v průběhu setu
                skore.Podani = false;
                label_ServisHoste.BackColor = Color.Black;
                label_ServisDomaci.BackColor = Color.White;
            }
            if ((sender as Button).Tag == 0.ToString()) //Button Servis Domácí
            { 

                // Vrácení libera na pozici 1
                if (textBox_PoziceDomaci1.Text != "")
                {
                    if ((postaveni_A_StatistikaHracu.aktivniLiberoDomaciPozice == 7) && (postaveni_A_StatistikaHracu.CisloBlokare(postaveni_A_StatistikaHracu.PostaveniDomaci[0])))
                    {
                        int cisloBlokare = postaveni_A_StatistikaHracu.PostaveniDomaci[0];
                        postaveni_A_StatistikaHracu.PostaveniDomaci[0] = postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci;
                        postaveni_A_StatistikaHracu.aktivniLiberoDomaciPozice = 0;
                        postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci = cisloBlokare;
                        textBox_PoziceDomaci1.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[0].ToString();
                        textBox_7LiberoDomaci.Text = postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci.ToString();
                    }
                }
            
                if ((hriste.PocetBoduDomaci + hriste.hriste_outy[0] + hriste.PocetBoduHoste + hriste.hriste_outy[1] == 0)) //Počáteční nastavení servisů
                {
                    label_ServisSet1.Text = Form1.menu.Domaci;
                    label_ServisSet2.Text = Form1.menu.Hoste;
                    label_ServisSet3.Text = Form1.menu.Domaci;
                    label_ServisSet4.Text = Form1.menu.Hoste;
                }
                else if (skore.CurrSet == 5) label_ServisSet5.Text = Form1.menu.Domaci;

                //Přenastavení servisu v průběhu setu
                skore.Podani = true;
                label_ServisHoste.BackColor = Color.White;
                label_ServisDomaci.BackColor = Color.Black;
            }
        }
        private void UpdateSet()
        {
            int Set = skore.CurrSet;
            if (Set == 1) label_SkoreSet1.Text = skore.SkoreDomaci + " - " + skore.SkoreHoste;
            if (Set == 2) label_SkoreSet2.Text = skore.SkoreDomaci + " - " + skore.SkoreHoste;
            if (Set == 3) label_SkoreSet3.Text = skore.SkoreDomaci + " - " + skore.SkoreHoste;
            if (Set == 4) label_SkoreSet4.Text = skore.SkoreDomaci + " - " + skore.SkoreHoste;
            if (Set == 5) label_SkoreSet5.Text = skore.SkoreDomaci + " - " + skore.SkoreHoste;
            skore.SkoreDomaci = skore.SkoreHoste = 0;
            label_SkoreDomaci.Text = label_SkoreHoste.Text = "0";

            //Přechod do dalšího setu
            skore.CurrSet++;
        }

        private void button_PoziceLock_Click(object sender, EventArgs e)
        {

            //Zamknutí všech pozic
            if (button_PoziceLock.BackColor == Color.Green)
            {
                // Načtení Sestavy na hřišti
                postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci = Convert.ToInt32(textBox_7LiberoDomaci.Text);
                postaveni_A_StatistikaHracu.NactenySestavNaHristi(Convert.ToInt32(textBox_PoziceDomaci1.Text), Convert.ToInt32(textBox_PoziceDomaci2.Text), Convert.ToInt32(textBox_PoziceDomaci3.Text),
                    Convert.ToInt32(textBox_PoziceDomaci4.Text), Convert.ToInt32(textBox_PoziceDomaci5.Text), Convert.ToInt32(textBox_PoziceDomaci6.Text), Convert.ToInt32(textBox_PoziceHoste1.Text),
                    Convert.ToInt32(textBox_PoziceHoste2.Text), Convert.ToInt32(textBox_PoziceHoste3.Text), Convert.ToInt32(textBox_PoziceHoste4.Text), Convert.ToInt32(textBox_PoziceHoste5.Text), Convert.ToInt32(textBox_PoziceHoste6.Text));


                button_PoziceLock.BackColor = Color.Red;
                textBox_PoziceHoste1.Enabled = false;
                textBox_PoziceHoste1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceHoste2.Enabled = false;
                textBox_PoziceHoste2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceHoste3.Enabled = false;
                textBox_PoziceHoste3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceHoste4.Enabled = false;
                textBox_PoziceHoste4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceHoste5.Enabled = false;
                textBox_PoziceHoste5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceHoste6.Enabled = false;
                textBox_PoziceHoste6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceDomaci1.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[0].ToString();
                textBox_PoziceDomaci1.Enabled = false;
                textBox_PoziceDomaci1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceDomaci2.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[1].ToString();
                textBox_PoziceDomaci2.Enabled = false;
                textBox_PoziceDomaci2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceDomaci3.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[2].ToString();
                textBox_PoziceDomaci3.Enabled = false;
                textBox_PoziceDomaci3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceDomaci4.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[3].ToString();
                textBox_PoziceDomaci4.Enabled = false;
                textBox_PoziceDomaci4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceDomaci5.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[4].ToString();
                textBox_PoziceDomaci5.Enabled = false;
                textBox_PoziceDomaci5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_PoziceDomaci6.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[5].ToString();
                textBox_PoziceDomaci6.Enabled = false;
                textBox_PoziceDomaci6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(31)))));
                textBox_7LiberoDomaci.Text = postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci.ToString();
                textBox_7LiberoDomaci.Enabled = false;
                textBox_7LiberoDomaci.BackColor = Color.Blue;
            }
            //Odemknutí všech pozic
            else
            {
                button_PoziceLock.BackColor = Color.Green;
                textBox_PoziceHoste1.Enabled = true;
                textBox_PoziceHoste1.BackColor = Color.Red;
                textBox_PoziceHoste2.Enabled = true;
                textBox_PoziceHoste2.BackColor = Color.Red;
                textBox_PoziceHoste3.Enabled = true;
                textBox_PoziceHoste3.BackColor = Color.Red;
                textBox_PoziceHoste4.Enabled = true;
                textBox_PoziceHoste4.BackColor = Color.Red;
                textBox_PoziceHoste5.Enabled = true;
                textBox_PoziceHoste5.BackColor = Color.Red;
                textBox_PoziceHoste6.Enabled = true;
                textBox_PoziceHoste6.BackColor = Color.Red;
                textBox_PoziceDomaci1.Enabled = true;
                textBox_PoziceDomaci1.BackColor = Color.Red;
                textBox_PoziceDomaci2.Enabled = true;
                textBox_PoziceDomaci2.BackColor = Color.Red;
                textBox_PoziceDomaci3.Enabled = true;
                textBox_PoziceDomaci3.BackColor = Color.Red;
                textBox_PoziceDomaci4.Enabled = true;
                textBox_PoziceDomaci4.BackColor = Color.Red;
                textBox_PoziceDomaci5.Enabled = true;
                textBox_PoziceDomaci5.BackColor = Color.Red;
                textBox_PoziceDomaci6.Enabled = true;
                textBox_PoziceDomaci6.BackColor = Color.Red;
                textBox_7LiberoDomaci.Enabled = true;
                textBox_7LiberoDomaci.BackColor = Color.Red;
            }

        }
    }
}
