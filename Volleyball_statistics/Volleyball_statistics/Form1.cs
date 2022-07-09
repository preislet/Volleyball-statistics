namespace Volleyball_statistics
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public static Menu menu;
        public static Label l;
        public static string Domaci;
        public static string Hoste;
        public Form1()
        {
            InitializeComponent();
            instance = this;
            menu = new Menu();
            l = label_VybranaSestava;
            Domaci = textBox_Domaci.Text;
            Hoste = textBox_Hoste.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menu.CreateFolder();
        }

        private void Button_NovaSestava_Click(object sender, EventArgs e)
        {
            Form_NovaSestava NovaSestava = new Form_NovaSestava();
            NovaSestava.Show();
        }

        private void button_sestava_Click(object sender, EventArgs e)
        {
            Form_VyberSestavy VyberSestavy = new Form_VyberSestavy();
            VyberSestavy.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox_Domaci.Text != "") && (textBox_Domaci.Text != "") && (label_VybranaSestava.Text != "Nebyla vybrána sestava"))
            {
                Form_statistika statistika = new Form_statistika();
                this.Hide();
                statistika.Closed += (s, args) => this.Close();
                statistika.Show();
            }
            
        }

        private void textBox_Domaci_TextChanged(object sender, EventArgs e)
        {
            menu.Domaci = textBox_Domaci.Text;
        }

        private void textBox_Hoste_TextChanged(object sender, EventArgs e)
        {
            menu.Hoste = textBox_Hoste.Text;
        }
    }
}