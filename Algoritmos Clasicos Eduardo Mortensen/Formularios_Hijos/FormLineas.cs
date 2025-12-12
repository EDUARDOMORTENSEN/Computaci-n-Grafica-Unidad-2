using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using LineDrawingAlgorithms;

namespace Lineas
{
    public partial class FormLineas : Form
    {
        // escala: cuántos píxeles representa 1 unidad del usuario.
        // Por defecto 1 (1 unidad = 1 píxel). Cámbialo si quieres "zoom".
        private int scale = 1;

        public FormLineas()
        {
            InitializeComponent();


            txtX0.KeyPress += SoloNumerosDecimalesNegativos;
            txtY0.KeyPress += SoloNumerosDecimalesNegativos;
            txtXf.KeyPress += SoloNumerosDecimalesNegativos;
            txtYf.KeyPress += SoloNumerosDecimalesNegativos;
        }

        // ----------------------------------------------------
        // VALIDACIÓN: permite dígitos, un punto decimal y signo '-' al inicio
        // ----------------------------------------------------
        private void SoloNumerosDecimalesNegativos(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Permitir Backspace
            if (e.KeyChar == (char)Keys.Back)
                return;

            // Permitir dígitos
            if (char.IsDigit(e.KeyChar))
                return;

            // Permitir punto '.' si no existe ya
            if (e.KeyChar == '.')
            {
                if (txt.Text.Contains("."))
                    e.Handled = true;
                return;
            }

            // Permitir signo negativo solo al inicio y solo una vez
            if (e.KeyChar == '-')
            {
                if (txt.SelectionStart == 0 && !txt.Text.Contains("-"))
                    return;
                e.Handled = true;
                return;
            }

            // Bloquear todo lo demás
            e.Handled = true;
        }

        // ----------------------------------------------------
        // Intentar obtener entradas: acepta ',' o '.' como decimal
        // Convierte texto a double usando InvariantCulture
        // ----------------------------------------------------
        private bool TryGetInputs(out double x0, out double y0, out double xf, out double yf)
        {
            x0 = y0 = xf = yf = 0;
            // Reemplazar coma por punto para usuarios que escriban con coma
            string sX0 = txtX0.Text.Replace(',', '.').Trim();
            string sY0 = txtY0.Text.Replace(',', '.').Trim();
            string sXf = txtXf.Text.Replace(',', '.').Trim();
            string sYf = txtYf.Text.Replace(',', '.').Trim();

            CultureInfo ci = CultureInfo.InvariantCulture;

            bool ok =
                double.TryParse(sX0, NumberStyles.Float, ci, out x0) &&
                double.TryParse(sY0, NumberStyles.Float, ci, out y0) &&
                double.TryParse(sXf, NumberStyles.Float, ci, out xf) &&
                double.TryParse(sYf, NumberStyles.Float, ci, out yf);

            if (!ok)
            {
                MessageBox.Show("Ingrese números válidos. Use '.' o ',' para decimales (ej: 4.3 ó 4,3).", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ----------------------------------------------------
        // Convierte coordenadas en unidades del usuario a píxeles en el Bitmap
        // Origen centrado en pictureBox (para que coordenadas negativas sean visibles)
        // scale define cuántos píxeles representa 1 unidad
        // ----------------------------------------------------
        private void ToPixelCoordinates(double x, double y, out int px, out int py, int originX, int originY, int scale)
        {
            // Redondeo adecuado: multiplicar por scale primero
            px = originX + (int)Math.Round(x * scale);
            // Invertir Y para sistema cartesiano (y positivo hacia arriba)
            py = originY - (int)Math.Round(y * scale);
        }

        // ----------------------------------------------------
        // DIBUJAR LÍNEA: convierte entradas y llama a LineasAlgoritmo
        // ----------------------------------------------------
        private void DrawLine(string method)
        {
            if (!TryGetInputs(out double x0, out double y0, out double xf, out double yf))
                return;

            // Evitar entradas absurdas
            const double MAX_COORD = 1e6;
            if (Math.Abs(x0) > MAX_COORD || Math.Abs(y0) > MAX_COORD || Math.Abs(xf) > MAX_COORD || Math.Abs(yf) > MAX_COORD)
            {
                MessageBox.Show("Coordenadas demasiado grandes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear bitmap y graphics
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            // Para ver píxeles reales (útil para algoritmos raster)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.Clear(Color.White);

            // Origen en el centro del pictureBox
            int originX = bmp.Width / 2;
            int originY = bmp.Height / 2;

            // Dibujar cuadricula centrada (opcional)
            DrawGridCentered(g, bmp.Width, bmp.Height, originX, originY);

            // Transformar coordenadas del usuario a píxeles
            ToPixelCoordinates(x0, y0, out int x0p, out int y0p, originX, originY, scale);
            ToPixelCoordinates(xf, yf, out int xfp, out int yfp, originX, originY, scale);

            Pen p = new Pen(Color.Black, 1);

            try
            {
                switch (method)
                {
                    case "DDA":
                        // DDA acepta floats: pasar coordenadas en píxeles como float
                        LineasAlgoritmo.DrawDDA(g, p, x0p, y0p, xfp, yfp);
                        break;

                    case "Bresenham":
                        LineasAlgoritmo.DrawBresenham(g, p, x0p, y0p, xfp, yfp);
                        break;

                    case "MidPoint":
                        LineasAlgoritmo.DrawMidPoint(g, p, x0p, y0p, xfp, yfp);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dibujar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pictureBox1.Image = bmp;
        }

        // ----------------------------------------------------
        // DIBUJAR CUADRÍCULA CENTRADA (para referencia visual)
        // ----------------------------------------------------
        private void DrawGridCentered(Graphics g, int width, int height, int originX, int originY)
        {
            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                // lineas verticales a la derecha
                for (int x = originX; x < width; x += 20)
                    g.DrawLine(gridPen, x, 0, x, height);
                // lineas verticales a la izquierda
                for (int x = originX; x > 0; x -= 20)
                    g.DrawLine(gridPen, x, 0, x, height);

                // horizontales hacia abajo
                for (int y = originY; y < height; y += 20)
                    g.DrawLine(gridPen, 0, y, width, y);
                // horizontales hacia arriba
                for (int y = originY; y > 0; y -= 20)
                    g.DrawLine(gridPen, 0, y, width, y);
            }

            // ejes más visibles
            using (Pen axis = new Pen(Color.DarkGray, 2))
            {
                g.DrawLine(axis, 0, originY, width, originY);   // eje X
                g.DrawLine(axis, originX, 0, originX, height);  // eje Y
            }
        }

        // ----------------------------------------------------
        // BOTONES (conecta estos handlers desde Designer)
        // ----------------------------------------------------
        private void btnDDA_Click(object sender, EventArgs e) { DrawLine("DDA"); }
        private void btnBresenham_Click(object sender, EventArgs e) { DrawLine("Bresenham"); }
        private void btnMidPoint_Click(object sender, EventArgs e) { DrawLine("MidPoint"); }
    }
}









