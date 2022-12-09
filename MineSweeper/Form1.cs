namespace MineSweeper
{
    public partial class Form1 : Form
    {
        int velikost = 10;
        int pocetMin = 10;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            mrizka1.Setup(velikost, pocetMin);
            this.Width = mrizka1.Width + 50;
            this.Height = mrizka1.Height + 90;
        }
    }
}