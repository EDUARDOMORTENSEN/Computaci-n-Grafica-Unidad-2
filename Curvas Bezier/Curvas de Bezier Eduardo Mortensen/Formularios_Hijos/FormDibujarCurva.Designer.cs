namespace Lineas
{
    partial class FormDibujarCurva
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblInstruccion = new System.Windows.Forms.Label();
            this.cmbVariante = new System.Windows.Forms.ComboBox();
            this.lblVariante = new System.Windows.Forms.Label();
            this.btnAnimar = new System.Windows.Forms.Button();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.grpControles = new System.Windows.Forms.GroupBox();
            this.lblLeyenda = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // lblInstruccion
            // 
            this.lblInstruccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInstruccion.Location = new System.Drawing.Point(12, 12);
            this.lblInstruccion.Name = "lblInstruccion";
            this.lblInstruccion.Size = new System.Drawing.Size(700, 30);
            this.lblInstruccion.TabIndex = 1;
            this.lblInstruccion.Text = "Instrucciones";
            this.lblInstruccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbVariante
            // 
            this.cmbVariante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariante.FormattingEnabled = true;
            this.cmbVariante.Items.AddRange(new object[] {
            "De Casteljau",
            "Bézier Cuadrática",
            "Bézier Cúbica"});
            this.cmbVariante.Location = new System.Drawing.Point(15, 55);
            this.cmbVariante.Name = "cmbVariante";
            this.cmbVariante.Size = new System.Drawing.Size(170, 23);
            this.cmbVariante.TabIndex = 1;
            this.cmbVariante.SelectedIndex = 0;
            this.cmbVariante.SelectedIndexChanged += new System.EventHandler(this.cmbVariante_SelectedIndexChanged);
            // 
            // lblVariante
            // 
            this.lblVariante.AutoSize = true;
            this.lblVariante.Location = new System.Drawing.Point(15, 30);
            this.lblVariante.Name = "lblVariante";
            this.lblVariante.Size = new System.Drawing.Size(90, 15);
            this.lblVariante.TabIndex = 0;
            this.lblVariante.Text = "Tipo de Curva:";
            // 
            // btnAnimar
            // 
            this.btnAnimar.BackColor = System.Drawing.Color.LightGreen;
            this.btnAnimar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnimar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAnimar.Location = new System.Drawing.Point(15, 100);
            this.btnAnimar.Name = "btnAnimar";
            this.btnAnimar.Size = new System.Drawing.Size(170, 40);
            this.btnAnimar.TabIndex = 2;
            this.btnAnimar.Text = "▶ Iniciar Animación";
            this.btnAnimar.UseVisualStyleBackColor = false;
            this.btnAnimar.Click += new System.EventHandler(this.btnAnimar_Click);
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReiniciar.Location = new System.Drawing.Point(15, 150);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(170, 35);
            this.btnReiniciar.TabIndex = 3;
            this.btnReiniciar.Text = "↻ Reiniciar Puntos";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.Location = new System.Drawing.Point(15, 195);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(170, 35);
            this.btnInfo.TabIndex = 4;
            this.btnInfo.Text = "ℹ Info del Algoritmo";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // grpControles
            // 
            this.grpControles.Controls.Add(this.lblLeyenda);
            this.grpControles.Controls.Add(this.btnInfo);
            this.grpControles.Controls.Add(this.lblVariante);
            this.grpControles.Controls.Add(this.cmbVariante);
            this.grpControles.Controls.Add(this.btnReiniciar);
            this.grpControles.Controls.Add(this.btnAnimar);
            this.grpControles.Location = new System.Drawing.Point(730, 50);
            this.grpControles.Name = "grpControles";
            this.grpControles.Size = new System.Drawing.Size(200, 500);
            this.grpControles.TabIndex = 2;
            this.grpControles.TabStop = false;
            this.grpControles.Text = "Controles";
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeyenda.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLeyenda.Location = new System.Drawing.Point(15, 250);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(170, 230);
            this.lblLeyenda.TabIndex = 5;
            this.lblLeyenda.Text = "LEYENDA:\r\n\r\n● Punto Inicio\r\n  (Verde)\r\n\r\n● Punto Control\r\n  (Naranja)\r\n\r\n● Punt" +
    "o Final\r\n  (Rojo)\r\n\r\n━━ Curva Bézier\r\n  (Azul grueso)\r\n\r\n---- Polígono Control\r\n " +
    " (Gris punteado)\r\n\r\nAnimación muestra\r\nconstrucción paso a paso";
            // 
            // FormDibujarCurva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 570);
            this.Controls.Add(this.grpControles);
            this.Controls.Add(this.lblInstruccion);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormDibujarCurva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Curvas de Bézier - Computación Gráfica";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpControles.ResumeLayout(false);
            this.grpControles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblInstruccion;
        private System.Windows.Forms.ComboBox cmbVariante;
        private System.Windows.Forms.Label lblVariante;
        private System.Windows.Forms.Button btnAnimar;
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.GroupBox grpControles;
        private System.Windows.Forms.Label lblLeyenda;
    }
}