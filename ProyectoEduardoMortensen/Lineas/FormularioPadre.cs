using System;
using System.Windows.Forms;

namespace Lineas
{
    public partial class MDIParent : Form
    {
        public MDIParent()
        {
            InitializeComponent();
        }

        private void abrirCirculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormCirculo { MdiParent = this };
            f.Show();
        }

        private void abrirFloodFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormRelleno { MdiParent = this };
            f.Show();
        }

        private void abrirSutherlandCohenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormRecortarLineas { MdiParent = this };
            f.Show();
        }

        private void abrirSutherlandHodgmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormRecortarPoligono { MdiParent = this };
            f.Show();
        }

        private void abrirLineasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormLineas { MdiParent = this };
            f.Show();
        }
    }
}
