namespace Lineas
{
    partial class FormCirculo
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numericRadio;
        private System.Windows.Forms.NumericUpDown numericPixel;
        private System.Windows.Forms.Button btnDibujarBresenham;
        private System.Windows.Forms.Button btnDibujarPuntoMedio;
        private System.Windows.Forms.Button btnDibujarParametrico;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericRadio = new System.Windows.Forms.NumericUpDown();
            this.numericPixel = new System.Windows.Forms.NumericUpDown();
            this.btnDibujarBresenham = new System.Windows.Forms.Button();
            this.btnDibujarPuntoMedio = new System.Windows.Forms.Button();
            this.btnDibujarParametrico = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRadio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPixel)).BeginInit();
            this.SuspendLayout();
            //
            // pictureBox1
            //
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 420);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            //
            // numericRadio
            //
            this.numericRadio.Location = new System.Drawing.Point(590, 40);
            this.numericRadio.Maximum = new decimal(new int[] { 400, 0, 0, 0 });
            this.numericRadio.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericRadio.Name = "numericRadio";
            this.numericRadio.Size = new System.Drawing.Size(80, 20);
            this.numericRadio.Value = new decimal(new int[] { 100, 0, 0, 0 });
            //
            // numericPixel
            //
            this.numericPixel.Location = new System.Drawing.Point(590, 90);
            this.numericPixel.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numericPixel.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericPixel.Name = "numericPixel";
            this.numericPixel.Size = new System.Drawing.Size(80, 20);
            this.numericPixel.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnDibujarBresenham
            //
            this.btnDibujarBresenham.Location = new System.Drawing.Point(590, 140);
            this.btnDibujarBresenham.Name = "btnDibujarBresenham";
            this.btnDibujarBresenham.Size = new System.Drawing.Size(150, 30);
            this.btnDibujarBresenham.Text = "Dibujar Bresenham";
            this.btnDibujarBresenham.UseVisualStyleBackColor = true;
            this.btnDibujarBresenham.Click += new System.EventHandler(this.btnDibujarBresenham_Click);
            //
            // btnDibujarPuntoMedio
            //
            this.btnDibujarPuntoMedio.Location = new System.Drawing.Point(590, 180);
            this.btnDibujarPuntoMedio.Name = "btnDibujarPuntoMedio";
            this.btnDibujarPuntoMedio.Size = new System.Drawing.Size(150, 30);
            this.btnDibujarPuntoMedio.Text = "Dibujar Punto Medio";
            this.btnDibujarPuntoMedio.UseVisualStyleBackColor = true;
            this.btnDibujarPuntoMedio.Click += new System.EventHandler(this.btnDibujarPuntoMedio_Click);
            //
            // btnDibujarParametrico
            //
            this.btnDibujarParametrico.Location = new System.Drawing.Point(590, 220);
            this.btnDibujarParametrico.Name = "btnDibujarParametrico";
            this.btnDibujarParametrico.Size = new System.Drawing.Size(150, 30);
            this.btnDibujarParametrico.Text = "Dibujar Paramétrico";
            this.btnDibujarParametrico.UseVisualStyleBackColor = true;
            this.btnDibujarParametrico.Click += new System.EventHandler(this.btnDibujarParametrico_Click);
            //
            // btnColor
            //
            this.btnColor.Location = new System.Drawing.Point(590, 270);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(80, 30);
            this.btnColor.Text = "Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.BackColor = System.Drawing.Color.Black;
            this.btnColor.ForeColor = System.Drawing.Color.White;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Location = new System.Drawing.Point(670, 270);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 30);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(590, 20);
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Radio";
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(590, 70);
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Tamaño píxel";
            //
            // FormCirculo
            //
            this.ClientSize = new System.Drawing.Size(760, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.numericRadio);
            this.Controls.Add(this.numericPixel);
            this.Controls.Add(this.btnDibujarBresenham);
            this.Controls.Add(this.btnDibujarPuntoMedio);
            this.Controls.Add(this.btnDibujarParametrico);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "FormCirculo";
            this.Text = "Dibujar Círculos";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRadio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPixel)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
