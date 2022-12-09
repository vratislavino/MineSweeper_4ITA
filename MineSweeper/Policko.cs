using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Policko : UserControl
    {
        public event Action<Policko> PolickoOdhaleno;

        int hodnotaPolicka;




        public bool JeMina => hodnotaPolicka == -1;

        public bool JePrazdny => hodnotaPolicka == 0;

        private bool jeOznaceny = false;

        public bool JeOznaceny => jeOznaceny;

        private bool jeOdhaleny = false;

        public bool JeOdhaleny => jeOdhaleny;

        private int x;
        public int X => x;

        private int y;
        public int Y => y;

        public Policko(int x, int y) {
            InitializeComponent();
            this.x = x;
            this.y = y;
        }

        public void NastavHodnotu(int hodnota) {
            hodnotaPolicka = hodnota;
        }

        public void ZvysHodnotu() {
            if (!JeMina)
                hodnotaPolicka++;
        }

        private void Policko_MouseClick(object sender, MouseEventArgs e) {
            if (jeOdhaleny)
                return;

            if(e.Button == MouseButtons.Left) {
                if(!jeOznaceny)
                    Odhal();
            } else if(e.Button == MouseButtons.Right) {
                Oznac();
            }
        }

        public void Odhal() {
            jeOdhaleny = true;
            PolickoOdhaleno?.Invoke(this);
            Refresh();
        }

        private void Oznac() {
            jeOznaceny = !jeOznaceny;
            Refresh();
        }

        private void Policko_Paint(object sender, PaintEventArgs e) {
            // vykreslení pozadí
            if(jeOdhaleny) {
                e.Graphics.FillRectangle(Brushes.DarkGray, 0, 0, Width, Height);
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width, Height);
            } else {
                e.Graphics.FillRectangle(Brushes.LightGray, 0, 0, Width, Height);
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width, Height);
            }

            if(!jeOdhaleny) {
                if(jeOznaceny) {
                    e.Graphics.DrawLine(new Pen(Color.Black, 3f), Width / 3, Height / 4, Width / 3, Height * 3 / 4);
                    e.Graphics.FillPolygon(Brushes.Red, new Point[] { 
                        new Point(Width / 3, Height / 4),
                        new Point(Width / 3, Height / 2),
                        new Point(Width * 2 / 3, Height / 3)
                    });
                }
            } else {
                if(JeMina) {
                    e.Graphics.FillEllipse(Brushes.Black, Width/4, Height/4, Width/2, Height/2);
                } else if (hodnotaPolicka > 0) {
                    e.Graphics.DrawString(hodnotaPolicka.ToString(), new Font(new FontFamily("Calibri"), 15f ,FontStyle.Bold), Brushes.White, new Point(5, 3));
                }
            }
        }
    }
}
