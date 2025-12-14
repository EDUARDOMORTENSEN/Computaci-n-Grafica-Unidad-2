using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lineas
{
    /// <summary>
    /// Formulario para demostrar curvas B-Spline con animación.
    /// Implementa tres variantes: Uniforme, No Uniforme y Cox-de Boor.
    /// </summary>
    public partial class FormDibujarBSpline : Form
    {
        private Bitmap bmp;
        private Graphics g;
        private DibujarBSpline dibujador;

        // Puntos de control
        private List<PointF> puntosControl;
        private int puntoSeleccionado = -1;
        private bool arrastrando = false;

        // Variante seleccionada
        private int varianteSeleccionada = 0; // 0=Uniforme, 1=No Uniforme, 2=Cox-de Boor

        // Animación
        private Timer timerAnimacion;
        private float parametroT = 0f;
        private bool animacionActiva = false;
        private List<PointF> puntosCurvaCompleta;
        private int puntoActualAnimacion = 0;

        // Configuración visual
        private const int RADIO_PUNTO = 6;
        private const int RADIO_SELECCION = 10;

        public FormDibujarBSpline()
        {
            InitializeComponent();

            // Inicializar ComboBox
            if (cmbVariante != null)
            {
                cmbVariante.Items.Clear();
                cmbVariante.Items.Add("B-Spline Uniforme");
                cmbVariante.Items.Add("B-Spline No Uniforme");
                cmbVariante.Items.Add("Cox-de Boor");
                cmbVariante.SelectedIndex = 0;
            }

            InicializarCanvas();
            InicializarAnimacion();
        }

        /// <summary>
        /// Inicializa el lienzo y las estructuras de datos.
        /// </summary>
        private void InicializarCanvas()
        {
            try
            {
                if (pictureBox1 == null || pictureBox1.Width <= 0 || pictureBox1.Height <= 0)
                {
                    MessageBox.Show("Error: PictureBox no inicializado.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                dibujador = new DibujarBSpline();
                puntosControl = new List<PointF>();
                puntosCurvaCompleta = new List<PointF>(); // Inicializar siempre

                // Inicializar puntos de control por defecto
                InicializarPuntosPorDefecto();

                pictureBox1.Image = bmp;
                ActualizarInstrucciones();

                // Dibujar inicial
                DibujarTodo();
                pictureBox1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicializa puntos de control por defecto.
        /// </summary>
        private void InicializarPuntosPorDefecto()
        {
            puntosControl.Clear();
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            // Crear 6 puntos de control distribuidos
            puntosControl.Add(new PointF(80, h / 2));
            puntosControl.Add(new PointF(180, h / 2 - 80));
            puntosControl.Add(new PointF(280, h / 2 + 80));
            puntosControl.Add(new PointF(380, h / 2 - 80));
            puntosControl.Add(new PointF(480, h / 2 + 80));
            puntosControl.Add(new PointF(580, h / 2));

            GenerarCurvaCompleta();
        }

        /// <summary>
        /// Inicializa el timer para la animación.
        /// </summary>
        private void InicializarAnimacion()
        {
            timerAnimacion = new Timer();
            timerAnimacion.Interval = 30; // ~33 FPS
            timerAnimacion.Tick += TimerAnimacion_Tick;
        }

        /// <summary>
        /// Evento del timer de animación.
        /// </summary>
        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (puntosCurvaCompleta == null || puntosCurvaCompleta.Count == 0)
            {
                timerAnimacion.Stop();
                animacionActiva = false;
                btnAnimar.Text = "▶ Iniciar Animación";
                return;
            }

            puntoActualAnimacion++;

            if (puntoActualAnimacion >= puntosCurvaCompleta.Count)
            {
                puntoActualAnimacion = 0;
            }

            parametroT = puntoActualAnimacion / (float)puntosCurvaCompleta.Count;

            DibujarTodo();
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Genera la curva completa con los puntos de control actuales.
        /// </summary>
        private void GenerarCurvaCompleta()
        {
            try
            {
                if (puntosControl == null || puntosControl.Count < 4)
                {
                    if (puntosCurvaCompleta == null)
                        puntosCurvaCompleta = new List<PointF>();
                    else
                        puntosCurvaCompleta.Clear();
                    return;
                }

                switch (varianteSeleccionada)
                {
                    case 0: // Uniforme
                        puntosCurvaCompleta = dibujador.BSplineUniforme(puntosControl, 150);
                        break;

                    case 1: // No Uniforme
                        puntosCurvaCompleta = dibujador.BSplineNoUniforme(puntosControl, null, 150);
                        break;

                    case 2: // Cox-de Boor
                        puntosCurvaCompleta = dibujador.BSplineCoxDeBoor(puntosControl, 150);
                        break;
                }

                // Asegurar que no sea null
                if (puntosCurvaCompleta == null)
                    puntosCurvaCompleta = new List<PointF>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar curva: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (puntosCurvaCompleta == null)
                    puntosCurvaCompleta = new List<PointF>();
                else
                    puntosCurvaCompleta.Clear();
            }
        }

        /// <summary>
        /// Dibuja todo el contenido del lienzo.
        /// </summary>
        private void DibujarTodo()
        {
            if (g == null) return;

            g.Clear(Color.White);

            // Dibujar la curva B-Spline
            DibujarCurva();

            // Dibujar polígono de control
            DibujarPoligonoControl();

            // Dibujar puntos de control
            DibujarPuntosControl();

            // Si la animación está activa, dibujar progreso
            if (animacionActiva)
            {
                DibujarAnimacion();
            }

            // Dibujar información
            DibujarInformacion();
        }

        /// <summary>
        /// Dibuja la curva B-Spline completa.
        /// </summary>
        private void DibujarCurva()
        {
            if (puntosCurvaCompleta == null || puntosCurvaCompleta.Count < 2)
                return;

            int puntosADibujar = animacionActiva ? puntoActualAnimacion : puntosCurvaCompleta.Count;
            puntosADibujar = Math.Max(2, Math.Min(puntosADibujar, puntosCurvaCompleta.Count));

            using (Pen penCurva = new Pen(Color.DarkBlue, 3))
            {
                for (int i = 0; i < puntosADibujar - 1; i++)
                {
                    g.DrawLine(penCurva, puntosCurvaCompleta[i], puntosCurvaCompleta[i + 1]);
                }
            }
        }

        /// <summary>
        /// Dibuja el polígono de control.
        /// </summary>
        private void DibujarPoligonoControl()
        {
            if (puntosControl.Count < 2) return;

            using (Pen penControl = new Pen(Color.LightGray, 1))
            {
                penControl.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                for (int i = 0; i < puntosControl.Count - 1; i++)
                {
                    g.DrawLine(penControl, puntosControl[i], puntosControl[i + 1]);
                }
            }
        }

        /// <summary>
        /// Dibuja los puntos de control.
        /// </summary>
        private void DibujarPuntosControl()
        {
            for (int i = 0; i < puntosControl.Count; i++)
            {
                PointF p = puntosControl[i];

                // Color según posición
                Color colorPunto;
                if (varianteSeleccionada == 2) // Cox-de Boor
                {
                    // Primer y último punto en colores especiales (curva pasa por ellos)
                    if (i == 0)
                        colorPunto = Color.Green;
                    else if (i == puntosControl.Count - 1)
                        colorPunto = Color.Red;
                    else
                        colorPunto = Color.Orange;
                }
                else
                {
                    // Todos los puntos en naranja (curva no pasa por ninguno)
                    colorPunto = Color.Orange;
                }

                // Punto
                using (Brush brush = new SolidBrush(colorPunto))
                {
                    g.FillEllipse(brush, p.X - RADIO_PUNTO, p.Y - RADIO_PUNTO,
                        RADIO_PUNTO * 2, RADIO_PUNTO * 2);
                }

                // Borde
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    g.DrawEllipse(pen, p.X - RADIO_PUNTO, p.Y - RADIO_PUNTO,
                        RADIO_PUNTO * 2, RADIO_PUNTO * 2);
                }

                // Etiqueta
                using (Font font = new Font("Arial", 8, FontStyle.Bold))
                using (Brush brushText = new SolidBrush(Color.Black))
                {
                    string etiqueta = $"P{i}";
                    g.DrawString(etiqueta, font, brushText, p.X + 10, p.Y - 10);
                }
            }
        }

        /// <summary>
        /// Dibuja la animación del progreso de la curva.
        /// </summary>
        private void DibujarAnimacion()
        {
            if (puntosCurvaCompleta == null || puntosCurvaCompleta.Count == 0 ||
                puntoActualAnimacion >= puntosCurvaCompleta.Count)
                return;

            // Dibujar punto actual en la curva
            PointF puntoActual = puntosCurvaCompleta[puntoActualAnimacion];

            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, puntoActual.X - 5, puntoActual.Y - 5, 10, 10);
            }

            using (Pen pen = new Pen(Color.Red, 2))
            {
                g.DrawEllipse(pen, puntoActual.X - 8, puntoActual.Y - 8, 16, 16);
            }

            // Dibujar línea desde el último punto de control relevante
            if (puntoActualAnimacion > 0 && puntosControl != null && puntosControl.Count > 0)
            {
                using (Pen pen = new Pen(Color.FromArgb(128, Color.Purple), 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                    // Encontrar puntos de control cercanos
                    int indiceControl = (int)((puntoActualAnimacion / (float)puntosCurvaCompleta.Count) * (puntosControl.Count - 1));
                    indiceControl = Math.Max(0, Math.Min(indiceControl, puntosControl.Count - 1));

                    g.DrawLine(pen, puntosControl[indiceControl], puntoActual);
                }
            }
        }

        /// <summary>
        /// Dibuja información en pantalla.
        /// </summary>
        private void DibujarInformacion()
        {
            if (puntosControl == null) return;

            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.DarkBlue))
            {
                string info = $"Puntos: {puntosControl.Count}";

                if (animacionActiva && puntosCurvaCompleta != null && puntosCurvaCompleta.Count > 0)
                {
                    info += $" | Progreso: {(parametroT * 100):F1}%";
                }

                g.DrawString(info, font, brush, 10, 10);
            }
        }

        /// <summary>
        /// Maneja el evento MouseDown para seleccionar puntos.
        /// </summary>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Buscar punto cercano
                for (int i = 0; i < puntosControl.Count; i++)
                {
                    float dx = e.X - puntosControl[i].X;
                    float dy = e.Y - puntosControl[i].Y;
                    float distancia = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (distancia < RADIO_SELECCION)
                    {
                        puntoSeleccionado = i;
                        arrastrando = true;
                        return;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Agregar nuevo punto de control
                puntosControl.Add(new PointF(e.X, e.Y));
                GenerarCurvaCompleta();
                DibujarTodo();
                pictureBox1.Refresh();
                ActualizarInstrucciones();
            }
        }

        /// <summary>
        /// Maneja el evento MouseMove para arrastrar puntos.
        /// </summary>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando && puntoSeleccionado >= 0)
            {
                puntosControl[puntoSeleccionado] = new PointF(
                    Math.Max(0, Math.Min(e.X, pictureBox1.Width)),
                    Math.Max(0, Math.Min(e.Y, pictureBox1.Height))
                );

                GenerarCurvaCompleta();
                DibujarTodo();
                pictureBox1.Refresh();
            }
        }

        /// <summary>
        /// Maneja el evento MouseUp para soltar puntos.
        /// </summary>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
            puntoSeleccionado = -1;
        }

        /// <summary>
        /// Actualiza las instrucciones en pantalla.
        /// </summary>
        private void ActualizarInstrucciones()
        {
            if (lblInstruccion == null) return;

            string algoritmo = "";
            switch (varianteSeleccionada)
            {
                case 0:
                    algoritmo = "B-Spline Uniforme";
                    break;
                case 1:
                    algoritmo = "B-Spline No Uniforme";
                    break;
                case 2:
                    algoritmo = "Cox-de Boor";
                    break;
            }

            int numPuntos = puntosControl != null ? puntosControl.Count : 0;
            lblInstruccion.Text = $"Algoritmo: {algoritmo} | {numPuntos} puntos | Click izq: Mover | Click der: Agregar punto";
        }

        /// <summary>
        /// Cambia la variante del algoritmo.
        /// </summary>
        private void cmbVariante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbVariante == null) return;

                varianteSeleccionada = cmbVariante.SelectedIndex;
                GenerarCurvaCompleta();
                ActualizarInstrucciones();
                DibujarTodo();
                pictureBox1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar variante: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicia o detiene la animación.
        /// </summary>
        private void btnAnimar_Click(object sender, EventArgs e)
        {
            // Verificar que hay una curva válida
            if (puntosCurvaCompleta == null || puntosCurvaCompleta.Count == 0)
            {
                MessageBox.Show("Debe haber al menos 4 puntos de control para generar la curva.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            animacionActiva = !animacionActiva;

            if (animacionActiva)
            {
                puntoActualAnimacion = 0;
                timerAnimacion.Start();
                btnAnimar.Text = "⏸ Detener Animación";
            }
            else
            {
                timerAnimacion.Stop();
                btnAnimar.Text = "▶ Iniciar Animación";
                DibujarTodo();
                pictureBox1.Refresh();
            }
        }

        /// <summary>
        /// Reinicia los puntos de control.
        /// </summary>
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            InicializarPuntosPorDefecto();
            puntoActualAnimacion = 0;
            DibujarTodo();
            pictureBox1.Refresh();
            ActualizarInstrucciones();
        }

        /// <summary>
        /// Muestra información del algoritmo.
        /// </summary>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            string info = "";

            switch (varianteSeleccionada)
            {
                case 0:
                    info = "B-Spline Uniforme\n\n" +
                           "Curva suave que NO pasa por los puntos de control (excepto posiblemente extremos).\n" +
                           "Usa espaciado uniforme en el vector de nodos.\n\n" +
                           "Características:\n" +
                           "- Continuidad C² (muy suave)\n" +
                           "- Control local: mover un punto afecta solo región cercana\n" +
                           "- No pasa por puntos de control\n\n" +
                           "Ventajas: Curvas muy suaves, control local excelente.\n\n" +
                           "Uso: Modelado 3D, animación, diseño CAD.";
                    break;

                case 1:
                    info = "B-Spline No Uniforme\n\n" +
                           "Permite espaciado no uniforme en el vector de nodos, dando mayor flexibilidad.\n" +
                           "Puede hacer que la curva pase más cerca de ciertos puntos de control.\n\n" +
                           "Características:\n" +
                           "- Mayor control sobre densidad de la curva\n" +
                           "- Puede tener mayor detalle en ciertas regiones\n" +
                           "- Más flexible que uniforme\n\n" +
                           "Ventajas: Control fino sobre forma, adaptabilidad.\n\n" +
                           "Uso: Diseño complejo, curvas con requisitos específicos.";
                    break;

                case 2:
                    info = "Cox-de Boor\n\n" +
                           "Algoritmo estándar y eficiente para calcular B-Splines.\n" +
                           "Usa vector de nodos abierto: la curva PASA por primer y último punto.\n\n" +
                           "Características:\n" +
                           "- Interpolación en extremos\n" +
                           "- Muy eficiente computacionalmente\n" +
                           "- Estable numéricamente\n\n" +
                           "Ventajas: Método estándar en la industria, predicible.\n\n" +
                           "Uso: Software CAD profesional, gráficos vectoriales, NURBS.";
                    break;
            }

            MessageBox.Show(info, "Información del Algoritmo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Agrega un nuevo punto de control en el centro.
        /// </summary>
        private void btnAgregarPunto_Click(object sender, EventArgs e)
        {
            int x = pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2;
            puntosControl.Add(new PointF(x, y));
            GenerarCurvaCompleta();
            DibujarTodo();
            pictureBox1.Refresh();
            ActualizarInstrucciones();
        }

        /// <summary>
        /// Libera recursos al cerrar.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timerAnimacion != null)
            {
                timerAnimacion.Stop();
                timerAnimacion.Dispose();
            }

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

            base.OnFormClosing(e);
        }
    }
}