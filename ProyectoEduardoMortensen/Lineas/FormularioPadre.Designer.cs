namespace Lineas
{
    partial class MDIParent
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirCirculoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirFloodFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirSutherlandCohenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirSutherlandHodgmanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirLineasToolStripMenuItem;

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
            this.abrirCirculoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirFloodFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirSutherlandCohenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirSutherlandHodgmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirLineasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();


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
            this.abrirCirculoToolStripMenuItem,
            this.abrirFloodFillToolStripMenuItem,
            this.abrirSutherlandCohenToolStripMenuItem,
            this.abrirSutherlandHodgmanToolStripMenuItem,
            this.abrirLineasToolStripMenuItem});
            this.archivosToolStripMenuItem.Name = "archivosToolStripMenuItem";
            this.archivosToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.archivosToolStripMenuItem.Text = "Algoritmos";
            // 
            // abrirCirculoToolStripMenuItem
            // 
            this.abrirCirculoToolStripMenuItem.Name = "abrirCirculoToolStripMenuItem";
            this.abrirCirculoToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirCirculoToolStripMenuItem.Text = "Dibujar Círculos";
            this.abrirCirculoToolStripMenuItem.Click += new System.EventHandler(this.abrirCirculoToolStripMenuItem_Click);
            // 
            // abrirFloodFillToolStripMenuItem
            // 
            this.abrirFloodFillToolStripMenuItem.Name = "abrirFloodFillToolStripMenuItem";
            this.abrirFloodFillToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirFloodFillToolStripMenuItem.Text = "Relleno (Flood Fill recursivo)";
            this.abrirFloodFillToolStripMenuItem.Click += new System.EventHandler(this.abrirFloodFillToolStripMenuItem_Click);
            // 
            // abrirSutherlandCohenToolStripMenuItem
            // 
            this.abrirSutherlandCohenToolStripMenuItem.Name = "abrirSutherlandCohenToolStripMenuItem";
            this.abrirSutherlandCohenToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirSutherlandCohenToolStripMenuItem.Text = "Recorte Líneas (Cohen–Sutherland)";
            this.abrirSutherlandCohenToolStripMenuItem.Click += new System.EventHandler(this.abrirSutherlandCohenToolStripMenuItem_Click);
            // 
            // abrirSutherlandHodgmanToolStripMenuItem
            // 
            this.abrirLineasToolStripMenuItem.Name = "abrirSutherlandHodgmanToolStripMenuItem";
            this.abrirLineasToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirLineasToolStripMenuItem.Text = "Dibujar Lineas";
            this.abrirLineasToolStripMenuItem.Click += new System.EventHandler(this.abrirLineasToolStripMenuItem_Click);
            // 
            // recortePoligonoToolStripMenuItem
            // 

            this.abrirSutherlandHodgmanToolStripMenuItem.Name = "abrirSutherlandHodgmanToolStripMenuItem";
            this.abrirSutherlandHodgmanToolStripMenuItem.Size = new System.Drawing.Size(323, 26);
            this.abrirSutherlandHodgmanToolStripMenuItem.Text = "Recorte Poligono";
            this.abrirSutherlandHodgmanToolStripMenuItem.Click += new System.EventHandler(this.abrirSutherlandHodgmanToolStripMenuItem_Click);
            // 
            // MDIParent
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDIParent";
            this.Text = "Practica: Algoritmos Gráficos - MDI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
    }
}
