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

        private void abrirCurvaBezierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormDibujarCurvaBezier { MdiParent = this };
            f.Show();
        }
        private void abrirCurvaBSplineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormDibujarBSpline { MdiParent = this };
            f.Show();
        }
    }
}
