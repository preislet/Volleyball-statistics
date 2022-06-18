namespace Volleyball_statistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Button_NovaSestava_Click(object sender, EventArgs e)
        {
            Form_NovaSestava NovaSestava = new Form_NovaSestava();
            NovaSestava.Show();
        }
    }
}