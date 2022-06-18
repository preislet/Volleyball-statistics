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
            string path = Form1.menu.FindPath(textBox43.Text);
            Form1.menu.SaveNewTeam(path, tableLayoutPanel1);
            this.Close();
        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            if (textBox43.Text.Length != 0) button_Ulozit.Enabled = true;
        }
    }
}
