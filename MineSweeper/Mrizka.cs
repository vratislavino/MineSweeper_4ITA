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
    // https://github.com/vratislavino/MineSweeper_4ITA

    public partial class Mrizka : UserControl
    {
        private int velikost;
        private int pocetMin;
        private Policko[,] policka;
        bool won = false;

        bool minyVytvoreny = false;

        public Mrizka() {
            InitializeComponent();
        }

        private bool CheckForWin() {
            int pocetNeodhalenych = 0;
            foreach(var p in policka) {
                if (p.JeOdhaleny == false) {
                    pocetNeodhalenych++;
                    if(pocetNeodhalenych > pocetMin) {
                        return false;
                    }
                }
            }
            won = true;
            return true;
        }

        public void Setup(int velikost, int pocetMin) {
            this.velikost = velikost;
            this.pocetMin = pocetMin;

            policka = new Policko[velikost, velikost];

            Policko p = null;
            for (int i = 0; i < policka.GetLength(0); i++) {
                for (int j = 0; j < policka.GetLength(1); j++) {
                    p = new Policko(i, j);
                    policka[i, j] = p;
                    p.Location = new Point(i * p.Width, j * p.Height);
                    p.PolickoOdhaleno += NaPolickoOdhaleno;
                    Controls.Add(p);
                }
            }

            this.Size = new Size(velikost * p.Width, velikost * p.Height);
        }

        private void NaPolickoOdhaleno(Policko policko) {
            if (!minyVytvoreny) {
                VygenerujMiny(policko);
                OhodnotPolicka();
                minyVytvoreny = true;
            }

            if (policko.JeMina) {
                MessageBox.Show("Prohrál jsi :(");
                Application.Restart();
            }

            if (policko.JePrazdny) {
                for (int i = -1; i <= 1; i++) {
                    for (int j = -1; j <= 1; j++) {
                        if (policko.X + i >= 0 && policko.X + i < velikost && policko.Y + j >= 0 && policko.Y + j < velikost) {
                            if (policka[policko.X + i, policko.Y + j].JeOdhaleny == false) {
                                if(policka[policko.X + i, policko.Y + j].JeOznaceny == false) {
                                    policka[policko.X + i, policko.Y + j].Odhal();
                                }
                            }
                        }
                    }
                }
            }

            if(!won && CheckForWin()) {
                MessageBox.Show("Vyhrál jsi!");
            }
        }

        private void OhodnotPolicka() {
            for (int i = 0; i < velikost; i++) {
                for (int j = 0; j < velikost; j++) {
                    if (policka[i, j].JeMina) {
                        for (int x = -1; x <= 1; x++) {
                            for (int y = -1; y <= 1; y++) {
                                if (i + x >= 0 && i + x < velikost && j + y >= 0 && j + y < velikost)
                                    policka[i + x, j + y].ZvysHodnotu();
                            }
                        }
                    }
                }
            }
        }

        private void VygenerujMiny(Policko nemuzeBytMina) {
            Random random = new Random();

            int x, y;
            for (int i = 0; i < pocetMin; i++) {
                x = random.Next(0, velikost);
                y = random.Next(0, velikost);

                if (policka[x, y] == nemuzeBytMina) {
                    i--;
                    continue;
                }

                if (policka[x, y].JeMina) {
                    i--;
                    continue;
                }

                policka[x, y].NastavHodnotu(-1);
            }
        }
    }
}
