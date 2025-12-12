using System.Drawing;

namespace Lineas
{
    partial class FormRelleno
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelDibujo;
        private System.Windows.Forms.ComboBox comboAlgoritmo;
        private System.Windows.Forms.Button btnRellenar;
        private System.Windows.Forms.Button btnLimpiar;

        private System.Windows.Forms.Button btnRectangulo;
        private System.Windows.Forms.Button btnCirculo;
        private System.Windows.Forms.Button btnCerrarPoligono;

        private System.Windows.Forms.RadioButton radioPoligono;

        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;

        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Panel panelColor;

        private System.Windows.Forms.Label labelAlgoritmo;
        private System.Windows.Forms.Label labelFiguras;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelDibujo = new System.Windows.Forms.Panel();
            this.comboAlgoritmo = new System.Windows.Forms.ComboBox();
            this.btnRellenar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();

            this.btnRectangulo = new System.Windows.Forms.Button();
            this.btnCirculo = new System.Windows.Forms.Button();
            this.btnCerrarPoligono = new System.Windows.Forms.Button();

            this.radioPoligono = new System.Windows.Forms.RadioButton();

            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();

            this.btnColor = new System.Windows.Forms.Button();
            this.panelColor = new System.Windows.Forms.Panel();

            this.labelAlgoritmo = new System.Windows.Forms.Label();
            this.labelFiguras = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // panelDibujo
            this.panelDibujo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDibujo.Location = new System.Drawing.Point(12, 12);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(650, 450);
            this.panelDibujo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelDibujo_MouseClick);

            // Algoritmos
            this.comboAlgoritmo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAlgoritmo.Items.AddRange(new object[] {
                "Flood Fill BFS",
                "Flood Fill DFS",
                "Flood Fill Scanline"
            });
            this.comboAlgoritmo.Location = new System.Drawing.Point(700, 50);
            this.comboAlgoritmo.Size = new System.Drawing.Size(180, 23);

            this.labelAlgoritmo.Text = "Algoritmo:";
            this.labelAlgoritmo.Location = new System.Drawing.Point(700, 30);

            // Coordenadas
            this.labelX.Text = "X:";
            this.labelX.Location = new System.Drawing.Point(700, 90);

            this.textBoxX.Location = new System.Drawing.Point(730, 90);
            this.textBoxX.Size = new System.Drawing.Size(50, 23);

            this.labelY.Text = "Y:";
            this.labelY.Location = new System.Drawing.Point(790, 90);

            this.textBoxY.Location = new System.Drawing.Point(820, 90);
            this.textBoxY.Size = new System.Drawing.Size(50, 23);

            // Rellenar
            this.btnRellenar.Location = new System.Drawing.Point(700, 130);
            this.btnRellenar.Size = new System.Drawing.Size(180, 30);
            this.btnRellenar.Text = "Rellenar";
            this.btnRellenar.Click += new System.EventHandler(this.btnRellenar_Click);

            // Limpiar
            this.btnLimpiar.Location = new System.Drawing.Point(700, 170);
            this.btnLimpiar.Size = new System.Drawing.Size(180, 30);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // Figuras
            this.labelFiguras.Text = "Figuras:";
            this.labelFiguras.Location = new System.Drawing.Point(700, 220);

            this.btnRectangulo.Text = "Rectángulo";
            this.btnRectangulo.Location = new System.Drawing.Point(700, 250);
            this.btnRectangulo.Size = new System.Drawing.Size(180, 30);
            this.btnRectangulo.Click += new System.EventHandler(this.btnRectangulo_Click);

            this.btnCirculo.Text = "Círculo";
            this.btnCirculo.Location = new System.Drawing.Point(700, 290);
            this.btnCirculo.Size = new System.Drawing.Size(180, 30);
            this.btnCirculo.Click += new System.EventHandler(this.btnCirculo_Click);

            this.radioPoligono.Text = "Dibujar Polígono";
            this.radioPoligono.Location = new System.Drawing.Point(700, 330);

            this.btnCerrarPoligono.Text = "Cerrar Polígono";
            this.btnCerrarPoligono.Location = new System.Drawing.Point(700, 360);
            this.btnCerrarPoligono.Click += new System.EventHandler(this.btnCerrarPoligono_Click);

            // Color
            this.btnColor.Text = "Color Relleno";
            this.btnColor.Location = new System.Drawing.Point(700, 400);
            this.btnColor.Size = new System.Drawing.Size(120, 30);
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);

            this.panelColor.Location = new System.Drawing.Point(830, 400);
            this.panelColor.Size = new System.Drawing.Size(50, 30);
            this.panelColor.BackColor = Color.Red;

            // Form
            this.ClientSize = new System.Drawing.Size(910, 480);
            this.Controls.Add(this.panelDibujo);
            this.Controls.Add(this.comboAlgoritmo);
            this.Controls.Add(this.btnRellenar);
            this.Controls.Add(this.btnLimpiar);

            this.Controls.Add(this.btnRectangulo);
            this.Controls.Add(this.btnCirculo);
            this.Controls.Add(this.btnCerrarPoligono);
            this.Controls.Add(this.radioPoligono);

            this.Controls.Add(this.labelFiguras);

            this.Controls.Add(this.labelAlgoritmo);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.textBoxY);

            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.panelColor);

            this.Name = "FormRelleno";
            this.Text = "Relleno de Figuras";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
