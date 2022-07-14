using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form_statistika_Load(object sender, EventArgs e)
        {
            Label_Domaci.Text = Form1.menu.Domaci;  //Jméno domácího týmu
            Label_Hoste.Text = Form1.menu.Hoste;  //Jméno týmu domácích
            button_OutDomaci.Text = "OUT " + Form1.menu.Domaci;
            button_OutHoste.Text = "OUT " + Form1.menu.Hoste;
            postaveni_A_StatistikaHracu.NactiJmenaHracuDoTabulky(tableLayoutPanel_TabulkaHracu);
            UpdateHriste();
        }
        private void button_ExitMaximized_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if ((textBox_PoziceHoste1.Text == "") || (textBox_PoziceHoste2.Text == "") || (textBox_PoziceHoste3.Text == "") || (textBox_PoziceHoste4.Text == "") || (textBox_PoziceHoste5.Text == "") || (textBox_PoziceHoste6.Text == "") ||
                (textBox_PoziceDomaci1.Text == "") || (textBox_PoziceDomaci2.Text == "") || (textBox_PoziceDomaci3.Text == "") || (textBox_PoziceDomaci4.Text == "") || (textBox_PoziceDomaci5.Text == "") || (textBox_PoziceDomaci6.Text == "") || (textBox_7LiberoDomaci.Text == ""))
            {
                MessageBox.Show("Prvni vypňte postavení hráčů do hřiště v levém dolním rohu");
                return;
            }
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
                    postaveni_A_StatistikaHracu.Rotace(true);
                }
                else
                {
                    label_ServisHoste.BackColor = Color.Black;
                    label_ServisDomaci.BackColor = Color.White;
                    postaveni_A_StatistikaHracu.Rotace(false);
                }
                RotacePoziceUpdate();
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

                // Vracení blokaře do hry, když má jít na servis
                if (textBox_PoziceDomaci1.Text != "")
                {
                    if (postaveni_A_StatistikaHracu.aktivniLiberoDomaciPozice == 0)
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

            //Nastavení podání podle tabulky
            if ((skore.CurrSet != 5) && skore.CurrSet > 1)
            {
                if (skore.CurrSet == 2)
                {
                    if (label_ServisSet2.Text == Form1.menu.Hoste) hriste.LastBod = skore.Podani = false;
                    else hriste.LastBod = skore.Podani = true;
                }
                if (skore.CurrSet == 3)
                {
                    if (label_ServisSet3.Text == Form1.menu.Hoste) hriste.LastBod = skore.Podani = false;
                    else hriste.LastBod = skore.Podani = true;
                }
                if (skore.CurrSet == 4)
                {
                    if (label_ServisSet3.Text == Form1.menu.Hoste) hriste.LastBod = skore.Podani = false;
                    else hriste.LastBod = skore.Podani = true;
                }
            }

            //Restart Postavení
            button_PoziceLock.BackColor = Color.Green;
            label_liberoDomaci.Text = "Libero";

            textBox_PoziceHoste1.Enabled = true;
            textBox_PoziceHoste1.BackColor = Color.Red;
            textBox_PoziceHoste1.Text = "";
            textBox_PoziceHoste2.Enabled = true;
            textBox_PoziceHoste2.BackColor = Color.Red;
            textBox_PoziceHoste2.Text = "";
            textBox_PoziceHoste3.Enabled = true;
            textBox_PoziceHoste3.BackColor = Color.Red;
            textBox_PoziceHoste3.Text = "";
            textBox_PoziceHoste4.Enabled = true;
            textBox_PoziceHoste4.BackColor = Color.Red;
            textBox_PoziceHoste4.Text = "";
            textBox_PoziceHoste5.Enabled = true;
            textBox_PoziceHoste5.BackColor = Color.Red;
            textBox_PoziceHoste5.Text = "";
            textBox_PoziceHoste6.Enabled = true;
            textBox_PoziceHoste6.BackColor = Color.Red;
            textBox_PoziceHoste6.Text = "";
            textBox_PoziceDomaci1.Enabled = true;
            textBox_PoziceDomaci1.BackColor = Color.Red;
            textBox_PoziceDomaci1.Text = "";
            textBox_PoziceDomaci2.Enabled = true;
            textBox_PoziceDomaci2.BackColor = Color.Red;
            textBox_PoziceDomaci2.Text = "";
            textBox_PoziceDomaci3.Enabled = true;
            textBox_PoziceDomaci3.BackColor = Color.Red;
            textBox_PoziceDomaci3.Text = "";
            textBox_PoziceDomaci4.Enabled = true;
            textBox_PoziceDomaci4.BackColor = Color.Red;
            textBox_PoziceDomaci4.Text = "";
            textBox_PoziceDomaci5.Enabled = true;
            textBox_PoziceDomaci5.BackColor = Color.Red;
            textBox_PoziceDomaci5.Text = "";
            textBox_PoziceDomaci6.Enabled = true;
            textBox_PoziceDomaci6.BackColor = Color.Red;
            textBox_PoziceDomaci6.Text = "";
            textBox_7LiberoDomaci.Enabled = true;
            textBox_7LiberoDomaci.BackColor = Color.Red;
            textBox_7LiberoDomaci.Text = "";

        }
        private void RotacePoziceUpdate()
        {
            textBox_PoziceDomaci1.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[0].ToString();
            textBox_PoziceDomaci2.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[1].ToString();
            textBox_PoziceDomaci3.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[2].ToString();
            textBox_PoziceDomaci4.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[3].ToString();
            textBox_PoziceDomaci5.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[4].ToString();
            textBox_PoziceDomaci6.Text = postaveni_A_StatistikaHracu.PostaveniDomaci[5].ToString();
            textBox_7LiberoDomaci.Text = postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci.ToString();
            textBox_PoziceHoste1.Text = postaveni_A_StatistikaHracu.PostaveniHoste[0].ToString();
            textBox_PoziceHoste2.Text = postaveni_A_StatistikaHracu.PostaveniHoste[1].ToString();
            textBox_PoziceHoste3.Text = postaveni_A_StatistikaHracu.PostaveniHoste[2].ToString();
            textBox_PoziceHoste4.Text = postaveni_A_StatistikaHracu.PostaveniHoste[3].ToString();
            textBox_PoziceHoste5.Text = postaveni_A_StatistikaHracu.PostaveniHoste[4].ToString();
            textBox_PoziceHoste6.Text = postaveni_A_StatistikaHracu.PostaveniHoste[5].ToString();
            if (postaveni_A_StatistikaHracu.aktivniLiberoDomaciPozice == 7) label_liberoDomaci.Text = "Libero";
            else label_liberoDomaci.Text = "Blokař";
        }
        private void button_PoziceLock_Click(object sender, EventArgs e)
        {
            if ((textBox_PoziceHoste1.Text == "") || (textBox_PoziceHoste2.Text == "") || (textBox_PoziceHoste3.Text == "") || (textBox_PoziceHoste4.Text == "") || (textBox_PoziceHoste5.Text == "") || (textBox_PoziceHoste6.Text == "") ||
                (textBox_PoziceDomaci1.Text == "") || (textBox_PoziceDomaci2.Text == "") || (textBox_PoziceDomaci3.Text == "") || (textBox_PoziceDomaci4.Text == "") || (textBox_PoziceDomaci5.Text == "") || (textBox_PoziceDomaci6.Text == "") || (textBox_7LiberoDomaci.Text == "")) return;
            //Zamknutí všech pozic
            if (button_PoziceLock.BackColor == Color.Green)
            {
                // Načtení Sestavy na hřišti
                postaveni_A_StatistikaHracu.AktivniLiberoBlokarDomaci = Convert.ToInt32(textBox_7LiberoDomaci.Text);
                postaveni_A_StatistikaHracu.NactenySestavNaHristi(Convert.ToInt32(textBox_PoziceDomaci1.Text), Convert.ToInt32(textBox_PoziceDomaci2.Text), Convert.ToInt32(textBox_PoziceDomaci3.Text),
                    Convert.ToInt32(textBox_PoziceDomaci4.Text), Convert.ToInt32(textBox_PoziceDomaci5.Text), Convert.ToInt32(textBox_PoziceDomaci6.Text), Convert.ToInt32(textBox_PoziceHoste1.Text),
                    Convert.ToInt32(textBox_PoziceHoste2.Text), Convert.ToInt32(textBox_PoziceHoste3.Text), Convert.ToInt32(textBox_PoziceHoste4.Text), Convert.ToInt32(textBox_PoziceHoste5.Text), Convert.ToInt32(textBox_PoziceHoste6.Text));

                //Načtení střádačky
                postaveni_A_StatistikaHracu.NactiStridacku(tableLayoutPanel_Stridacka);

                //Zobrazení aktivních hráčů v tabulce
                postaveni_A_StatistikaHracu.ZobrazAktivniHraceVTabulce(tableLayoutPanel_TabulkaHracu);


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
                label_liberoDomaci.Text = "Libero/blok";
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

        /// <summary>
        /// Přičítání střídání obou týmů
        /// </summary>
        private void button_PocetStridaniDomaci_Click(object sender, EventArgs e)
        {
            if ((((Button)sender).Tag == "0") && (Convert.ToInt32(label_DomaciPocetStridani.Text) < 7)) label_DomaciPocetStridani.Text = (Convert.ToInt32(label_DomaciPocetStridani.Text) + 1).ToString();
            if ((((Button)sender).Tag == "1") && (Convert.ToInt32(label_HostePocetStridani.Text) < 7)) label_HostePocetStridani.Text = (Convert.ToInt32(label_HostePocetStridani.Text) + 1).ToString();

            if (Convert.ToInt32(label_DomaciPocetStridani.Text) == 7) label_DomaciPocetStridani.BackColor = Color.Red;
            if (Convert.ToInt32(label_HostePocetStridani.Text) == 7) label_HostePocetStridani.BackColor = Color.Red;



        }

        private void button_TabulkaHracu_Click(object sender, EventArgs e)
        {
            static void ZapniButtony(Button[] b)
            {
                for (int i = 0; i < b.Length; i++)
                {
                    b[i].Visible = b[i].Enabled = true;
                }
            }

            //Listy Buttonu, podle toho, co budou obsluhovat
            Button[] servisButtons = new Button[] { button_servis1, button_servis3, button_servis5, button_servisChyba };
            Button[] prijemButtons = new Button[] { button_Prijem1, button_Prijem3, button_Prijem5, button_PrijemChyba };
            Button[] utokButtons = new Button[] { button_Utok1, button_Utok3, button_Utok5, button_UtokChyba };
            Button[] blokButtons = new Button[] { button_Blok1, button_Blok3, button_Blok5, button_BlokChyba };
            Button[] poleButtons = new Button[] { button_Pole1, button_PoleChyba };

            //Vybarvení daného sloupce
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                Control control = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(Convert.ToInt32(((Button)sender).Tag), i);
                if (control.BackColor == Color.Green)
                {
                    control.BackColor = Color.LightBlue;
                    break;
                }

                //Poud je tento řádek již zvírazněný, tak se znovu nevybarvuje
                if (control.BackColor == Color.Orange) return;
                control.BackColor = Color.Orange;
            }

            //Zobrazení Buttonu
            switch (((Button)sender).Tag)
            {
                case "3":
                    ZapniButtony(servisButtons);
                    break;
                case "4":
                    ZapniButtony(prijemButtons);
                    break;
                case "5":
                    ZapniButtony(utokButtons);
                    break;
                case "6":
                    ZapniButtony(blokButtons);
                    break;
                case "7":
                    ZapniButtony(poleButtons);
                    break;
            }
        }

        /// <summary>
        /// Funkce vypne všechny pomocné buttony a celé tabulce nastavá defaultní barvu 
        /// </summary>
        private void RestartVybrani()
        {
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.ColumnCount; i++)
            {
                for (int j = 1; j < tableLayoutPanel_TabulkaHracu.RowCount; j++)
                {
                    Control control = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(i, j);
                    control.BackColor = default;

                    //Vypnutí všech pomocných buttonů pro servis, prijem, utok atd...
                    button_Pole1.Enabled = button_PoleChyba.Enabled = button_Pole1.Visible = button_PoleChyba.Visible = false;
                    button_servis1.Enabled = button_servis3.Enabled = button_servis5.Enabled = button_servisChyba.Enabled = button_servis1.Visible = button_servis3.Visible = button_servis5.Visible = button_servisChyba.Visible = false;
                    button_Prijem1.Enabled = button_Prijem3.Enabled = button_Prijem5.Enabled = button_PrijemChyba.Enabled = button_Prijem1.Visible = button_Prijem3.Visible = button_Prijem5.Visible = button_PrijemChyba.Visible = false;
                    button_Utok1.Enabled = button_Utok3.Enabled = button_Utok5.Enabled = button_UtokChyba.Enabled = button_Utok1.Visible = button_Utok3.Visible = button_Utok5.Visible = button_UtokChyba.Visible = false;
                    button_Blok1.Enabled = button_Blok3.Enabled = button_Blok5.Enabled = button_BlokChyba.Enabled = button_Blok1.Visible = button_Blok3.Visible = button_Blok5.Visible = button_BlokChyba.Visible = false;
                }
            }
        }
        private void button_Hrac_Click(object sender, EventArgs e)
        {
            //Kontrola jestli je zapsáno ve hřišti ukazující pozice
            if ((textBox_PoziceHoste1.Text == "") || (textBox_PoziceHoste2.Text == "") || (textBox_PoziceHoste3.Text == "") || (textBox_PoziceHoste4.Text == "") || (textBox_PoziceHoste5.Text == "") || (textBox_PoziceHoste6.Text == "") ||
                (textBox_PoziceDomaci1.Text == "") || (textBox_PoziceDomaci2.Text == "") || (textBox_PoziceDomaci3.Text == "") || (textBox_PoziceDomaci4.Text == "") || (textBox_PoziceDomaci5.Text == "") || (textBox_PoziceDomaci6.Text == "") || (textBox_7LiberoDomaci.Text == ""))
            {
                MessageBox.Show("Prvni vypňte postavení hráčů do hřiště v levém dolním rohu");
                return;
            }

            //Vypnutí jiného hráče, poku se uživatel na poprvé překlikl
            RestartVybrani();

            if (((Button)sender).Text == "XXXXXXXXXXXXXXXXX")
            {
                ((Button)sender).Enabled = false;
                return;
            }
            //Zapnutí Buttonu servisu,utoku, bloku...
            button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = button7.Enabled = true;
            //Vybarvení daného řádku
            if (tableLayoutPanel_TabulkaHracu.GetControlFromPosition(1, Convert.ToInt32(((Button)sender).Tag)).BackColor != Color.Green)
            {
                for (int i = 1; i < tableLayoutPanel_TabulkaHracu.ColumnCount; i++)
                {
                    Control control = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(i, Convert.ToInt32(((Button)sender).Tag));
                    control.BackColor = Color.Green;
                }
            }
            else RestartVybrani();
        
        }

        /// <summary>
        /// Funkce zajišťující spuštění zapisování akcí do příslušných polí a následně tabulky
        /// </summary>
        #region Buttony pro Hodnocení jednotlivých akcí ve hře
        private void button_ServisHodnoceni_Click(object sender, EventArgs e)
        {
            Control c = null;
            Control hrac = null;
            int index = 0;

            ///For cyklus se snaží najít světle modré pole ve sloupci servisu
            ///Takto označené pole je to pole, do kterého se má zapsat
            ///poté se program podívý u jakého hráče je tato statistika a poté vyvolá funkci pro vyhodnocení akce
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(3, i);
                hrac = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(0, i);
                index = i;
                if (c.BackColor == Color.LightBlue) break;
            }
            string jmenoHrace = Regex.Replace(hrac.Text, @"[\d-]", string.Empty);
            jmenoHrace = Regex.Replace(jmenoHrace, @"\s+", "");
            int znamka = Convert.ToInt32(((Button)sender).Tag);
            postaveni_A_StatistikaHracu.VyhodnotAkci('S', znamka , jmenoHrace);
            ResetBarevAButtonuTabulky(index, 3);

            // Update tabulky hracu
            postaveni_A_StatistikaHracu.UpdateTabulkyHracu(tableLayoutPanel_TabulkaHracu);

            // Vypnutí pomocných tlačítek
            button_servis1.Enabled = button_servis3.Enabled = button_servis5.Enabled = button_servisChyba.Enabled = button_servis1.Visible = button_servis3.Visible = button_servis5.Visible = button_servisChyba.Visible = false;
        }
        private void button_PrijemHodnoceni_Click(object sender, EventArgs e)
        {
            Control c = null;
            Control hrac = null;
            int index = 0;
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(4, i);
                hrac = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(0, i);
                index = i;
                if (c.BackColor == Color.LightBlue) break;
            }
            string jmenoHrace = Regex.Replace(hrac.Text, @"[\d-]", string.Empty);
            jmenoHrace = Regex.Replace(jmenoHrace, @"\s+", "");
            int znamka = Convert.ToInt32(((Button)sender).Tag);
            postaveni_A_StatistikaHracu.VyhodnotAkci('P', znamka, jmenoHrace);
            ResetBarevAButtonuTabulky(index, 4);
            postaveni_A_StatistikaHracu.UpdateTabulkyHracu(tableLayoutPanel_TabulkaHracu);
            button_Prijem1.Enabled = button_Prijem3.Enabled = button_Prijem5.Enabled = button_PrijemChyba.Enabled = button_Prijem1.Visible = button_Prijem3.Visible = button_Prijem5.Visible = button_PrijemChyba.Visible = false;
        }
        private void button_UtokHodnoceni_Click(object sender, EventArgs e)
        {
            Control c = null;
            Control hrac = null;
            int index = 0;
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(5, i);
                hrac = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(0, i);
                index = i;
                if (c.BackColor == Color.LightBlue) break;
            }
            string jmenoHrace = Regex.Replace(hrac.Text, @"[\d-]", string.Empty);
            jmenoHrace = Regex.Replace(jmenoHrace, @"\s+", "");
            int znamka = Convert.ToInt32(((Button)sender).Tag);
            postaveni_A_StatistikaHracu.VyhodnotAkci('U', znamka, jmenoHrace);
            ResetBarevAButtonuTabulky(index, 5);
            postaveni_A_StatistikaHracu.UpdateTabulkyHracu(tableLayoutPanel_TabulkaHracu);
            button_Utok1.Enabled = button_Utok3.Enabled = button_Utok5.Enabled = button_UtokChyba.Enabled = button_Utok1.Visible = button_Utok3.Visible = button_Utok5.Visible = button_UtokChyba.Visible = false;
        }
        private void button_BlokHodnoceni_Click(object sender, EventArgs e)
        {
            Control c = null;
            Control hrac = null;
            int index = 0;
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(6, i);
                hrac = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(0, i);
                index = i;
                if (c.BackColor == Color.LightBlue) break;
            }
            string jmenoHrace = Regex.Replace(hrac.Text, @"[\d-]", string.Empty);
            jmenoHrace = Regex.Replace(jmenoHrace, @"\s+", "");
            int znamka = Convert.ToInt32(((Button)sender).Tag);
            postaveni_A_StatistikaHracu.VyhodnotAkci('B', znamka, jmenoHrace);
            ResetBarevAButtonuTabulky(index, 6);
            postaveni_A_StatistikaHracu.UpdateTabulkyHracu(tableLayoutPanel_TabulkaHracu);
            button_Blok1.Enabled = button_Blok3.Enabled = button_Blok5.Enabled = button_BlokChyba.Enabled = button_Blok1.Visible = button_Blok3.Visible = button_Blok5.Visible = button_BlokChyba.Visible = false;
        }
        private void button_PoleHodnoceni_Click(object sender, EventArgs e)
        {
            Control c = null;
            Control hrac = null;
            int index = 0;
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(7, i);
                hrac = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(0, i);
                index = i;
                if (c.BackColor == Color.LightBlue) break;
            }
            string jmenoHrace = Regex.Replace(hrac.Text, @"[\d-]", string.Empty);
            jmenoHrace = Regex.Replace(jmenoHrace, @"\s+", "");
            int znamka = Convert.ToInt32(((Button)sender).Tag);
            postaveni_A_StatistikaHracu.VyhodnotAkci('p', znamka, jmenoHrace);
            ResetBarevAButtonuTabulky(index, 7);
            postaveni_A_StatistikaHracu.UpdateTabulkyHracu(tableLayoutPanel_TabulkaHracu);
            button_Pole1.Enabled = button_PoleChyba.Enabled = button_Pole1.Visible = button_PoleChyba.Visible = false;

        }
        private void ResetBarevAButtonuTabulky(int row, int column)
        {
            for (int i = 1; i < tableLayoutPanel_TabulkaHracu.ColumnCount; i++)
            {
                Control c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(i, row);
                c.BackColor = default;
            }
            for(int i = 1; i < tableLayoutPanel_TabulkaHracu.RowCount; i++)
            {
                Control c = tableLayoutPanel_TabulkaHracu.GetControlFromPosition(column, i);
                c.BackColor = default;
            }
            //Vypnutí Buttonu servisu,utoku, bloku...
            button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = button7.Enabled = false;
            RestartVybrani();
        }
        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel(postaveni_A_StatistikaHracu);
        }

        private void buttonScreenshot_Click(object sender, EventArgs e)
        {
            Bitmap memoryImage;
            memoryImage = new Bitmap(1920, 1080);
            Size s = new Size(memoryImage.Width, memoryImage.Height);

            Graphics memoryGraphics = Graphics.FromImage(memoryImage);

            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

            string path = Form1.menu.FindPath("screenshots");
            string JmenoScreenu = "Screenshot_" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss)") + ".png";
            string fullpath = Path.Combine(path, JmenoScreenu);
            memoryImage.Save(fullpath);
        }
    }
}
