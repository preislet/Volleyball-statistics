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
    public partial class Form_VyberSestavy : Form
    {
        public static string path; 
        public Form_VyberSestavy()
        {
            InitializeComponent();
        }

        private void Form_VyberSestavy_Load(object sender, EventArgs e)
        {
            path = Form1.menu.FindPath("sestavy");
            VyplnitLayout(path);
        }
        private void VyplnitLayout(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path); 
            FileInfo[] Files = d.GetFiles("*.txt"); //Nalezení všexh .txt files ve složce
            int index = 0;
            int max_index = Files.Length;

            //Vytvoření Buttonu pro vybrání dané sestavy
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    if (index == max_index) return;
                    string txt = Files[index].Name.Remove(Files[index].Name.Length - 4);
                    Button b = new Button();
                    b.Text = txt;
                    b.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                    b.Click += new System.EventHandler(Button_Click_VyberSestavy);
                    b.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    tableLayoutPanel1.Controls.Add(b,i,j) ;
                    index++;
                }
        }
        private void Button_Click_VyberSestavy(object sender, EventArgs e)  //Vybrání sestavy 
        {
            Form1.menu.Sestava = (sender as Button).Text;  //Sestava se zapisuje do třídy menu k pozdějšímu využití
            Form1.l.Text = "Byla vybrána sestava: " + Form1.menu.Sestava;
            this.Close();
        }
    } 
}
