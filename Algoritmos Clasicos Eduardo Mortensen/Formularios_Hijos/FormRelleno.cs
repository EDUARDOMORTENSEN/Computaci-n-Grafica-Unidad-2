using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lineas
{
    public partial class FormRelleno : Form
    {
        private Bitmap lienzo;
        private Graphics g;

        private List<Point> poligono = new List<Point>();
        private Color colorRelleno = Color.Red;

        public FormRelleno()
        {
            InitializeComponent();
            InicializarLienzo();
        }

        private void InicializarLienzo()
        {
            lienzo = new Bitmap(panelDibujo.Width, panelDibujo.Height);
            g = Graphics.FromImage(lienzo);
            g.Clear(Color.White);
            panelDibujo.BackgroundImage = lienzo;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                colorRelleno = cd.Color;
                panelColor.BackColor = colorRelleno;
            }
        }

        // ===================== DIBUJAR FIGURAS ======================

        private void btnRectangulo_Click(object sender, EventArgs e)
        {
            g.DrawRectangle(Pens.Black, 100, 100, 200, 150);
            panelDibujo.Invalidate();
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            g.DrawEllipse(Pens.Black, 150, 120, 200, 200);
            panelDibujo.Invalidate();
        }

        private void panelDibujo_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioPoligono.Checked)
            {
                poligono.Add(new Point(e.X, e.Y));

                if (poligono.Count > 1)
                {
                    g.DrawLine(Pens.Black, poligono[poligono.Count - 2], poligono[poligono.Count - 1]);
                }

                panelDibujo.Invalidate();
            }

            textBoxX.Text = e.X.ToString();
            textBoxY.Text = e.Y.ToString();
        }

        private void btnCerrarPoligono_Click(object sender, EventArgs e)
        {
            if (poligono.Count > 2)
            {
                g.DrawLine(Pens.Black,
                           poligono[poligono.Count - 1],
                           poligono[0]); // cerrar figura
                panelDibujo.Invalidate();
            }
        }

        // ===================== RELLENO ======================

        private void btnRellenar_Click(object sender, EventArgs e)
        {
            if (comboAlgoritmo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un algoritmo.");
                return;
            }

            int x = int.Parse(textBoxX.Text);
            int y = int.Parse(textBoxY.Text);

            switch (comboAlgoritmo.SelectedIndex)
            {
                case 0:
                    RellenoAlgoritmos.FloodFillBFS(lienzo, x, y, colorRelleno);
                    break;
                case 1:
                    RellenoAlgoritmos.FloodFillDFS(lienzo, x, y, colorRelleno);
                    break;
                case 2:
                    RellenoAlgoritmos.FloodFillScanline(lienzo, x, y, colorRelleno);
                    break;
            }

            panelDibujo.Invalidate();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            poligono.Clear();
            InicializarLienzo();
        }
    }
}
