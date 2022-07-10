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
    public partial class Form_NovaSestava : Form
    {
        public Form_NovaSestava()
        {
            InitializeComponent();
        }

        private void button_Ulozit_Click(object sender, EventArgs e)  //Ukložení nové sestavy do složky vytvořené na ploše
        {
            if (KontrolaDuplicity())
            {
                Varovani();
                return;
            }
            string path = Form1.menu.FindPath(textBox43.Text);  //textBox43 je kolonka, kam se vyplňuje název sestavy
            Form1.menu.SaveNewTeam(path, tableLayoutPanel1);
            this.Close();
        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            if (textBox43.Text.Length != 0) button_Ulozit.Enabled = true;  //Sestava bez jména se nedá uložit

            // TODO - kontorla, zda se už takhle nějaká sestava nejmenuje
        }


        /// <summary>
        /// Funkce kontroluje, zda se ve složce nachází sestava se stehným jménem či nikoliv
        /// </summary>
        private bool KontrolaDuplicity()
        {
            string jmeno = textBox43.Text;
            List<string> jmenaSouboru = new List<string>();
            string path = Form1.menu.FindPath("sestavy");
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] Files = dir.GetFiles("*.txt"); //Nalezení všexh .txt files ve složce
            for (int i = 0; i < Files.Length; i++)
            {
                if(Files[i].Name.Remove(Files[i].Name.Length - 4) == textBox43.Text) return true;
            }
            return false; 
        }


        /// <summary>
        /// Funkce spoušrící MassageBox
        /// </summary>
        private void Varovani() 
        {
            string message = "Sestava s tímto jménem již existuje. Zvolte jiné jméno";
            MessageBox.Show(message);
        }


    }
}
