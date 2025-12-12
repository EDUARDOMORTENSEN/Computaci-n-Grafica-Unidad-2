using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Lineas
{
    partial class FormLineas
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtX0;
        private System.Windows.Forms.TextBox txtY0;
        private System.Windows.Forms.TextBox txtXf;
        private System.Windows.Forms.TextBox txtYf;

        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.Label lblXf;
        private System.Windows.Forms.Label lblYf;

        private System.Windows.Forms.Button btnDDA;
        private System.Windows.Forms.Button btnBresenham;
        private System.Windows.Forms.Button btnMidPoint;

        private System.Windows.Forms.PictureBox pictureBox1;

        /// <summary>
        /// Liberar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador

        private void InitializeComponent()
        {
            // Inicializar el contenedor de componentes
            this.components = new Container();

            // Crear controles
            this.txtX0 = new System.Windows.Forms.TextBox();
            this.txtY0 = new System.Windows.Forms.TextBox();
            this.txtXf = new System.Windows.Forms.TextBox();
            this.txtYf = new System.Windows.Forms.TextBox();

            this.lblX0 = new System.Windows.Forms.Label();
            this.lblY0 = new System.Windows.Forms.Label();
            this.lblXf = new System.Windows.Forms.Label();
            this.lblYf = new System.Windows.Forms.Label();

            this.btnDDA = new System.Windows.Forms.Button();
            this.btnBresenham = new System.Windows.Forms.Button();
            this.btnMidPoint = new System.Windows.Forms.Button();

            this.pictureBox1 = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // 
            // lblX0
            // 
            this.lblX0.Location = new System.Drawing.Point(20, 20);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(22, 23);
            this.lblX0.TabIndex = 0;
            this.lblX0.Text = "X₀:";
            this.lblX0.AutoSize = true;
            // 
            // txtX0
            // 
            this.txtX0.Location = new System.Drawing.Point(41, 20);
            this.txtX0.Name = "txtX0";
            this.txtX0.Size = new System.Drawing.Size(70, 22);
            this.txtX0.TabIndex = 4;
            // Conectar KeyPress al validador (asegúrate de tener SoloNumerosDecimalesNegativos implementado)
            this.txtX0.KeyPress += new KeyPressEventHandler(this.SoloNumerosDecimalesNegativos);

            // 
            // lblY0
            // 
            this.lblY0.Location = new System.Drawing.Point(20, 60);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(22, 23);
            this.lblY0.TabIndex = 1;
            this.lblY0.Text = "Y₀:";
            this.lblY0.AutoSize = true;
            // 
            // txtY0
            // 
            this.txtY0.Location = new System.Drawing.Point(41, 57);
            this.txtY0.Name = "txtY0";
            this.txtY0.Size = new System.Drawing.Size(70, 22);
            this.txtY0.TabIndex = 5;
            this.txtY0.KeyPress += new KeyPressEventHandler(this.SoloNumerosDecimalesNegativos);

            // 
            // lblXf
            // 
            this.lblXf.Location = new System.Drawing.Point(20, 100);
            this.lblXf.Name = "lblXf";
            this.lblXf.Size = new System.Drawing.Size(22, 23);
            this.lblXf.TabIndex = 2;
            this.lblXf.Text = "Xf:";
            this.lblXf.AutoSize = true;
            // 
            // txtXf
            // 
            this.txtXf.Location = new System.Drawing.Point(41, 97);
            this.txtXf.Name = "txtXf";
            this.txtXf.Size = new System.Drawing.Size(70, 22);
            this.txtXf.TabIndex = 6;
            this.txtXf.KeyPress += new KeyPressEventHandler(this.SoloNumerosDecimalesNegativos);

            // 
            // lblYf
            // 
            this.lblYf.Location = new System.Drawing.Point(20, 140);
            this.lblYf.Name = "lblYf";
            this.lblYf.Size = new System.Drawing.Size(22, 23);
            this.lblYf.TabIndex = 3;
            this.lblYf.Text = "Yf:";
            this.lblYf.AutoSize = true;
            // 
            // txtYf
            // 
            this.txtYf.Location = new System.Drawing.Point(41, 137);
            this.txtYf.Name = "txtYf";
            this.txtYf.Size = new System.Drawing.Size(70, 22);
            this.txtYf.TabIndex = 7;
            this.txtYf.KeyPress += new KeyPressEventHandler(this.SoloNumerosDecimalesNegativos);

            // 
            // btnDDA
            // 
            this.btnDDA.Location = new System.Drawing.Point(12, 190);
            this.btnDDA.Name = "btnDDA";
            this.btnDDA.Size = new System.Drawing.Size(90, 28);
            this.btnDDA.TabIndex = 8;
            this.btnDDA.Text = "DDA";
            this.btnDDA.UseVisualStyleBackColor = true;
            this.btnDDA.Click += new System.EventHandler(this.btnDDA_Click);

            // 
            // btnBresenham
            // 
            this.btnBresenham.Location = new System.Drawing.Point(12, 224);
            this.btnBresenham.Name = "btnBresenham";
            this.btnBresenham.Size = new System.Drawing.Size(90, 28);
            this.btnBresenham.TabIndex = 9;
            this.btnBresenham.Text = "Bresenham";
            this.btnBresenham.UseVisualStyleBackColor = true;
            this.btnBresenham.Click += new System.EventHandler(this.btnBresenham_Click);

            // 
            // btnMidPoint
            // 
            this.btnMidPoint.Location = new System.Drawing.Point(12, 258);
            this.btnMidPoint.Name = "btnMidPoint";
            this.btnMidPoint.Size = new System.Drawing.Size(90, 28);
            this.btnMidPoint.TabIndex = 10;
            this.btnMidPoint.Text = "MidPoint";
            this.btnMidPoint.UseVisualStyleBackColor = true;
            this.btnMidPoint.Click += new System.EventHandler(this.btnMidPoint_Click);

            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(188, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 400);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.BackColor = System.Drawing.Color.White;

            // 
            // FormLineas
            // 
            this.ClientSize = new System.Drawing.Size(820, 450);
            this.Controls.Add(this.lblX0);
            this.Controls.Add(this.lblY0);
            this.Controls.Add(this.lblXf);
            this.Controls.Add(this.lblYf);
            this.Controls.Add(this.txtX0);
            this.Controls.Add(this.txtY0);
            this.Controls.Add(this.txtXf);
            this.Controls.Add(this.txtYf);
            this.Controls.Add(this.btnDDA);
            this.Controls.Add(this.btnBresenham);
            this.Controls.Add(this.btnMidPoint);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLineas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algoritmos de Líneas";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
