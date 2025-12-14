namespace Lineas
{
    partial class MDIParent
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirCurvaBezierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirCurvaBSplineToolStripMenuItem;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirCurvaBezierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirCurvaBSplineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();


            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivosToolStripMenuItem
            // 
            this.archivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirCurvaBezierToolStripMenuItem, this.abrirCurvaBSplineToolStripMenuItem
            });
            this.archivosToolStripMenuItem.Name = "archivosToolStripMenuItem";
            this.archivosToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.archivosToolStripMenuItem.Text = "Algoritmos";
            // 
            // abrirCurvaBezierToolStripMenuItem
            //
            this.abrirCurvaBezierToolStripMenuItem.Name = "abrirCurvaBezierToolStripMenuItem";
            this.abrirCurvaBezierToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirCurvaBezierToolStripMenuItem.Text = "Dibujar Curvas Bezier";
            this.abrirCurvaBezierToolStripMenuItem.Click += new System.EventHandler(this.abrirCurvaBezierToolStripMenuItem_Click);
            // 
            // abrirCurvaBSplineToolStripMenuItem
            //
            this.abrirCurvaBSplineToolStripMenuItem.Name = "abrirCurvaBSplineToolStripMenuItem";
            this.abrirCurvaBSplineToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirCurvaBSplineToolStripMenuItem.Text = "Dibujar Curvas B Spline";
            this.abrirCurvaBSplineToolStripMenuItem.Click += new System.EventHandler(this.abrirCurvaBSplineToolStripMenuItem_Click);
            // 
            // MDIParent
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDIParent";
            this.Text = "Practica: Curvas de Bezier";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
    }
}
