using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lineas
{
    /// <summary>
    /// Formulario para demostrar las tres variantes de algoritmos de recorte de líneas.
    /// Permite al usuario dibujar líneas arrastrando el mouse y ver el resultado del recorte.
    /// </summary>
    public partial class FormRecortarLineas : Form
    {
        private Bitmap bmp;
        private Graphics g;
        private RecortarLineas recortar;
        private Rectangle areaRecorte;

        // Variables para dibujo de líneas
        private Point startPoint;
        private bool isDrawing = false;

        // Variante del algoritmo seleccionado
        private int varianteSeleccionada = 0; // 0=Cohen-Sutherland, 1=Liang-Barsky, 2=Cyrus-Beck

        public FormRecortarLineas()
        {
            try
            {
                InitializeComponent();

                // Verificar que los controles se inicializaron
                if (cmbVariante != null)
                {
                    // Agregar items al ComboBox
                    cmbVariante.Items.Clear();
                    cmbVariante.Items.Add("Cohen-Sutherland");
                    cmbVariante.Items.Add("Liang-Barsky");
                    cmbVariante.Items.Add("Cyrus-Beck");
                    cmbVariante.SelectedIndex = 0;
                }

                InitializeCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el formulario: {ex.Message}\n\n{ex.StackTrace}",
                    "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicializa el lienzo y el área de recorte.
        /// </summary>
        private void InitializeCanvas()
        {
            try
            {
                // Verificar que pictureBox1 existe
                if (pictureBox1 == null)
                {
                    MessageBox.Show("Error: PictureBox no inicializado.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar dimensiones válidas
                if (pictureBox1.Width <= 0 || pictureBox1.Height <= 0)
                {
                    MessageBox.Show("Error: Dimensiones del PictureBox inválidas.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bmp);
                g.Clear(Color.White);

                recortar = new RecortarLineas();

                // Definir área de recorte (rectángulo central)
                int margen = 50;
                areaRecorte = new Rectangle(
                    margen,
                    margen,
                    pictureBox1.Width - 2 * margen,
                    pictureBox1.Height - 2 * margen
                );

                DibujarAreaRecorte();
                pictureBox1.Image = bmp;

                ActualizarInstrucciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el lienzo: {ex.Message}\n\nDetalles: {ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Dibuja el rectángulo de recorte en color azul.
        /// </summary>
        private void DibujarAreaRecorte()
        {
            if (g == null) return;

            using (Pen penArea = new Pen(Color.Blue, 2))
            {
                penArea.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(penArea, areaRecorte);
            }
        }

        /// <summary>
        /// Actualiza el texto de instrucciones según la variante seleccionada.
        /// </summary>
        private void ActualizarInstrucciones()
        {
            string algoritmo = "";
            switch (varianteSeleccionada)
            {
                case 0:
                    algoritmo = "Cohen-Sutherland";
                    break;
                case 1:
                    algoritmo = "Liang-Barsky";
                    break;
                case 2:
                    algoritmo = "Cyrus-Beck";
                    break;
            }

            lblInstruccion.Text = $"Algoritmo: {algoritmo} | Arrastra para dibujar línea. " +
                                  "Línea original: Negro | Recortada: Rojo";
        }

        /// <summary>
        /// Maneja el evento MouseDown para iniciar el dibujo de una línea.
        /// </summary>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Validar coordenadas dentro del PictureBox
                if (e.X >= 0 && e.X < pictureBox1.Width && e.Y >= 0 && e.Y < pictureBox1.Height)
                {
                    startPoint = e.Location;
                    isDrawing = true;
                }
            }
        }

        /// <summary>
        /// Maneja el evento MouseUp para finalizar el dibujo y aplicar el recorte.
        /// </summary>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                isDrawing = false;

                // Validar coordenadas finales dentro del PictureBox
                int endX = Math.Max(0, Math.Min(e.X, pictureBox1.Width - 1));
                int endY = Math.Max(0, Math.Min(e.Y, pictureBox1.Height - 1));

                Point endPoint = new Point(endX, endY);

                // Validar que no sean el mismo punto (con tolerancia pequeña)
                int distancia = Math.Abs(startPoint.X - endPoint.X) + Math.Abs(startPoint.Y - endPoint.Y);
                if (distancia < 2)
                {
                    MessageBox.Show("La línea es demasiado corta. Dibuja una línea más larga.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DibujarLineaConRecorte(startPoint, endPoint);
            }
        }

        /// <summary>
        /// Dibuja la línea original y aplica el algoritmo de recorte seleccionado.
        /// </summary>
        private void DibujarLineaConRecorte(Point p1, Point p2)
        {
            try
            {
                // Dibujar línea original en negro (siempre, incluso si está fuera)
                using (Pen penOriginal = new Pen(Color.Gray, 1))
                {
                    penOriginal.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    g.DrawLine(penOriginal, p1, p2);
                }

                // Copiar coordenadas para el recorte
                double x0 = p1.X, y0 = p1.Y;
                double x1 = p2.X, y1 = p2.Y;

                bool visible = false;

                // Aplicar el algoritmo de recorte según la variante seleccionada
                switch (varianteSeleccionada)
                {
                    case 0:
                        visible = recortar.CohenSutherland(areaRecorte, ref x0, ref y0, ref x1, ref y1);
                        break;
                    case 1:
                        visible = recortar.LiangBarsky(areaRecorte, ref x0, ref y0, ref x1, ref y1);
                        break;
                    case 2:
                        visible = recortar.CyrusBeck(areaRecorte, ref x0, ref y0, ref x1, ref y1);
                        break;
                }

                // Si la línea es visible después del recorte, dibujarla en rojo
                if (visible)
                {
                    using (Pen penRecortada = new Pen(Color.Red, 3))
                    {
                        g.DrawLine(penRecortada,
                            (int)Math.Round(x0), (int)Math.Round(y0),
                            (int)Math.Round(x1), (int)Math.Round(y1));
                    }
                }

                pictureBox1.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Error en parámetros: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar la línea: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia el lienzo y redibuja el área de recorte.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (g == null || bmp == null)
                {
                    InitializeCanvas();
                    return;
                }

                g.Clear(Color.White);
                DibujarAreaRecorte();

                if (pictureBox1 != null)
                    pictureBox1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar el lienzo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cambia la variante del algoritmo de recorte.
        /// </summary>
        private void cmbVariante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbVariante == null) return;

                varianteSeleccionada = cmbVariante.SelectedIndex;
                ActualizarInstrucciones();

                // Limpiar el lienzo al cambiar de variante
                btnLimpiar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar variante: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra información detallada sobre el algoritmo seleccionado.
        /// </summary>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            string info = "";

            switch (varianteSeleccionada)
            {
                case 0:
                    info = "Cohen-Sutherland\n\n" +
                           "Descripción: Utiliza códigos de región de 4 bits para clasificar puntos " +
                           "respecto al rectángulo de recorte. Realiza rechazo trivial cuando ambos " +
                           "puntos están en la misma región externa, y acepta trivialmente cuando " +
                           "ambos están dentro.\n\n" +
                           "Complejidad: O(n) donde n es el número de iteraciones necesarias.\n\n" +
                           "Ventajas: Simple de implementar, eficiente para rechazos/aceptaciones triviales.\n\n" +
                           "Desventajas: Puede requerir múltiples iteraciones para líneas que cruzan esquinas.";
                    break;

                case 1:
                    info = "Liang-Barsky\n\n" +
                           "Descripción: Utiliza ecuaciones paramétricas de la línea y calcula los " +
                           "parámetros t para las intersecciones con los bordes del rectángulo. " +
                           "Opera en el rango [0,1] del parámetro t.\n\n" +
                           "Complejidad: O(1) - siempre realiza 4 comparaciones.\n\n" +
                           "Ventajas: Más eficiente que Cohen-Sutherland, no requiere divisiones repetidas, " +
                           "ideal para múltiples líneas.\n\n" +
                           "Desventajas: Requiere aritmética de punto flotante precisa.";
                    break;

                case 2:
                    info = "Cyrus-Beck\n\n" +
                           "Descripción: Generalización del Liang-Barsky que funciona con cualquier " +
                           "polígono convexo. Utiliza las normales de los bordes para determinar " +
                           "si la línea entra o sale del área de recorte.\n\n" +
                           "Complejidad: O(n) donde n es el número de lados del polígono.\n\n" +
                           "Ventajas: Funciona con polígonos convexos arbitrarios, muy flexible.\n\n" +
                           "Desventajas: Más complejo de implementar, requiere más cálculos que Liang-Barsky " +
                           "para rectángulos.";
                    break;
            }

            MessageBox.Show(info, "Información del Algoritmo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Libera los recursos gráficos al cerrar el formulario.
        /// </summary>
        private void LiberarRecursosGraficos()
        {
            if (g != null)
            {
                g.Dispose();
                g = null;
            }

            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }
        }

        /// <summary>
        /// Sobrescribe el método FormClosing para liberar recursos al cerrar.
        /// </summary>
        protected override void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)
        {
            LiberarRecursosGraficos();
            base.OnFormClosing(e);
        }
    }
}