using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Mrizka : UserControl
    {
        private int velikost;
        private int pocetMin;
        private Policko[,] policka;

        public Mrizka() {
            InitializeComponent();
        }

        public void Setup(int velikost, int pocetMin) {
            this.velikost = velikost;
            this.pocetMin = pocetMin;

            policka = new Policko[velikost, velikost];

            Policko p = null;
            for(int i = 0; i < policka.GetLength(0); i++) {
                for (int j = 0; j < policka.GetLength(1); j++) {
                    p = new Policko();
                    policka[i, j] = p;
                    p.Location = new Point(i * p.Width, j * p.Height);
                    Controls.Add(p);
                }
            }

            this.Size = new Size(velikost * p.Width, velikost*p.Height);

            VygenerujMiny();
        }

        private void VygenerujMiny() {
            Random random = new Random();
            // vygenerovat až po prvním kliknutí
        }
    }
}
