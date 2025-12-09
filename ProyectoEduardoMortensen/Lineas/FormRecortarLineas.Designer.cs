namespace Lineas
{
    partial class FormRecortarLineas
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
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblInstruccion = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cmbVariante = new System.Windows.Forms.ComboBox();
            this.lblVariante = new System.Windows.Forms.Label();
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
            this.pictureBox1.Size = new System.Drawing.Size(600, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // lblInstruccion
            // 
            this.lblInstruccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInstruccion.Location = new System.Drawing.Point(12, 12);
            this.lblInstruccion.Name = "lblInstruccion";
            this.lblInstruccion.Size = new System.Drawing.Size(600, 30);
            this.lblInstruccion.TabIndex = 1;
            this.lblInstruccion.Text = "Instrucciones";
            this.lblInstruccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.Location = new System.Drawing.Point(15, 100);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(170, 35);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar Lienzo";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // cmbVariante
            // 
            this.cmbVariante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariante.FormattingEnabled = true;
            this.cmbVariante.Location = new System.Drawing.Point(15, 55);
            this.cmbVariante.Name = "cmbVariante";
            this.cmbVariante.Size = new System.Drawing.Size(170, 23);
            this.cmbVariante.TabIndex = 1;
            this.cmbVariante.SelectedIndexChanged += new System.EventHandler(this.cmbVariante_SelectedIndexChanged);
            // 
            // lblVariante
            // 
            this.lblVariante.AutoSize = true;
            this.lblVariante.Location = new System.Drawing.Point(15, 30);
            this.lblVariante.Name = "lblVariante";
            this.lblVariante.Size = new System.Drawing.Size(120, 15);
            this.lblVariante.TabIndex = 0;
            this.lblVariante.Text = "Algoritmo de Recorte:";
            // 
            // btnInfo
            // 
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.Location = new System.Drawing.Point(15, 145);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(170, 35);
            this.btnInfo.TabIndex = 3;
            this.btnInfo.Text = "Info del Algoritmo";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // grpControles
            // 
            this.grpControles.Controls.Add(this.lblLeyenda);
            this.grpControles.Controls.Add(this.btnInfo);
            this.grpControles.Controls.Add(this.lblVariante);
            this.grpControles.Controls.Add(this.cmbVariante);
            this.grpControles.Controls.Add(this.btnLimpiar);
            this.grpControles.Location = new System.Drawing.Point(630, 50);
            this.grpControles.Name = "grpControles";
            this.grpControles.Size = new System.Drawing.Size(200, 450);
            this.grpControles.TabIndex = 2;
            this.grpControles.TabStop = false;
            this.grpControles.Text = "Controles";
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeyenda.Location = new System.Drawing.Point(15, 200);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(170, 120);
            this.lblLeyenda.TabIndex = 4;
            this.lblLeyenda.Text = "LEYENDA:\r\n\r\n---- Area de Recorte\r\n     (Azul punteado)\r\n\r\n—  Linea Original\r\n" +
    "     (Negro)\r\n\r\n—  Linea Recortada\r\n     (Rojo grueso)";
            // 
            // FormRecortarLineas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 520);
            this.Controls.Add(this.grpControles);
            this.Controls.Add(this.lblInstruccion);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRecortarLineas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algoritmos de Recorte de Lineas - Computacion Grafica";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpControles.ResumeLayout(false);
            this.grpControles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblInstruccion;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.ComboBox cmbVariante;
        private System.Windows.Forms.Label lblVariante;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.GroupBox grpControles;
        private System.Windows.Forms.Label lblLeyenda;
    }
}