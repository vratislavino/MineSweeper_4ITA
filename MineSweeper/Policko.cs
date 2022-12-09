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
    public partial class Policko : UserControl
    {
        int hodnotaPolicka;

        public bool JeMina => hodnotaPolicka == -1;

        private bool jeOznaceny = false;

        private bool jeOdhaleny = false;


        public Policko() {
            InitializeComponent();
        }

        private void Policko_MouseClick(object sender, MouseEventArgs e) {
            if (jeOdhaleny)
                return;

            if(e.Button == MouseButtons.Left) {
                Odhal();
            } else if(e.Button == MouseButtons.Right) {
                Oznac();
            }
        }

        public void Odhal() {
            jeOdhaleny = true;
            Refresh();
        }

        private void Oznac() {
            jeOznaceny = true;
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
                    e.Graphics.DrawString(hodnotaPolicka.ToString(), SystemFonts.MenuFont, Brushes.White, new Point(3, 3));
                }
            }
        }
    }
}
