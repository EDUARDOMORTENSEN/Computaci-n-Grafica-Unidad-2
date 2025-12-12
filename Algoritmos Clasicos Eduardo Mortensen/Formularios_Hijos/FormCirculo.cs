using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lineas
{
    public partial class FormCirculo : Form
    {
        private CirculoAlgoritmo circulo;

        public FormCirculo()
        {
            InitializeComponent();
            circulo = new CirculoAlgoritmo();
        }

        private void btnDibujarBresenham_Click(object sender, EventArgs e)
        {
            var g = pictureBox1.CreateGraphics();
            ClearPictureBox();
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            int r = (int)numericRadio.Value;
            int ps = (int)numericPixel.Value;
            circulo.DibujarCirculoBresenham(g, cx, cy, r, ps, btnColor.BackColor);
            g.Dispose();
        }

        private void btnDibujarPuntoMedio_Click(object sender, EventArgs e)
        {
            var g = pictureBox1.CreateGraphics();
            ClearPictureBox();
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            int r = (int)numericRadio.Value;
            int ps = (int)numericPixel.Value;
            circulo.DibujarCirculoPuntoMedio(g, cx, cy, r, ps, btnColor.BackColor);
            g.Dispose();
        }

        private void btnDibujarParametrico_Click(object sender, EventArgs e)
        {
            var g = pictureBox1.CreateGraphics();
            ClearPictureBox();
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            int r = (int)numericRadio.Value;
            int ps = (int)numericPixel.Value;
            circulo.DibujarCirculoParametrico(g, cx, cy, r, ps, btnColor.BackColor);
            g.Dispose();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (var cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                    btnColor.BackColor = cd.Color;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearPictureBox();
        }

        private void ClearPictureBox()
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);
            }
        }

        // Para evitar pérdida de dibujo cuando se minimiza/restaura, redibujamos en Paint (simple).
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // nada: dibujo inmediato en los botones. Si quieres conservar dibujo usa Bitmap y DrawImage.
        }
    }
}

