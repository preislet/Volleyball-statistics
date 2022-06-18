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
        public Form_statistika()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
        }

        private void Form_statistika_Load(object sender, EventArgs e)
        {
            Label_Domaci.Text = Form1.menu.Domaci;
            Label_Hoste.Text = Form1.menu.Hoste;
            label_Skore.Text = Form1.menu.Domaci.Remove(1).ToString() + "   Skóre   " + Form1.menu.Hoste.Remove(1).ToString();
            button_OutDomaci.Text = "OUT " + Form1.menu.Domaci;
            button_OutHoste.Text = "OUT " + Form1.menu.Hoste;

            UpdateHriste();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            hriste.ProcentaDopadu(X, Y, pictureBox1);
            UpdateHriste();
        }
        private void UpdateHriste()
        {
            skore.SkoreDomaci = hriste.PocetBoduDomaci + hriste.hriste_outy[1];
            skore.SkoreHoste = hriste.PocetBoduHoste + hriste.hriste_outy[0];
            label_SkoreDomaci.Text = skore.SkoreDomaci.ToString();
            label_SkoreHoste.Text= skore.SkoreHoste.ToString();
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

        private void button_Out_Click(object sender, EventArgs e)
        {
            hriste.hriste_outy[Convert.ToInt32((sender as Button).Tag)]++;
            UpdateHriste();
        }

        private void button_Servis_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Tag == 1.ToString())
            {
                label_ServisHoste.BackColor = Color.Black;
                label_ServisDomaci.BackColor = Color.White;
            }
            if ((sender as Button).Tag == 0.ToString())
            {
                label_ServisHoste.BackColor = Color.White;
                label_ServisDomaci.BackColor = Color.Black;
            }
        }
    }
}
