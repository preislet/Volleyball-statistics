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

        private void button_Ulozit_Click(object sender, EventArgs e)
        {
            string path = FindPath();
            SaveNewTeam(path);
            this.Close();
        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            if (textBox43.Text.Length != 0) button_Ulozit.Enabled = true;
        }
        
        private string FindPath()
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
            string t = "Sestavy\\" + textBox43.Text + ".txt";
            return Path.Combine(startuppath2, t);
        }
        private void SaveNewTeam(string path)
        {
            string[] write = new string[45];
            int index = 0;
            try
            {
                TextWriter tw = new StreamWriter(path, true);
                for (int i = 1; i < tableLayoutPanel1.RowCount;i++)
                    for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                    {
                        Control c = this.tableLayoutPanel1.GetControlFromPosition(j, i);
                        if (c.Text == null) break;
                        write[index] = c.Text;
                        index++;
                    }
                
                for (int i = 0; i < write.Length; i += 3)
                {
                    if (write[i] == "") break;
                    tw.WriteLine("#" + write[i] + "*" + write[i+1] + "*" + write[i+2]);
                }
                tw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chybaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
        }
    }
}
