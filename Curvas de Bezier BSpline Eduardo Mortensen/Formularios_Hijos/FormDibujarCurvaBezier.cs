using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lineas
{
    /// <summary>
    /// Formulario para demostrar curvas de Bézier con animación de construcción.
    /// Implementa tres variantes: De Casteljau, Cuadrática y Cúbica.
    /// </summary>
    public partial class FormDibujarCurvaBezier : Form
    {
        private Bitmap bmp;
        private Graphics g;
        private DibujarCurvaBezier dibujador;

        // Puntos de control
        private List<PointF> puntosControl;
        private int puntoSeleccionado = -1;
        private bool arrastrando = false;

        // Variante seleccionada
        private int varianteSeleccionada = 0; // 0=De Casteljau, 1=Cuadrática, 2=Cúbica

        // Animación
        private Timer timerAnimacion;
        private float parametroT = 0f;
        private bool animacionActiva = false;
        private List<PointF> puntosCurvaCompleta;

        // Configuración visual
        private const int RADIO_PUNTO = 6;
        private const int RADIO_SELECCION = 10;

        public FormDibujarCurvaBezier()
        {
            InitializeComponent();
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

                dibujador = new DibujarCurvaBezier();
                puntosControl = new List<PointF>();
                puntosCurvaCompleta = new List<PointF>();

                // Inicializar puntos de control por defecto según variante
                InicializarPuntosPorDefecto();

                pictureBox1.Image = bmp;
                ActualizarInstrucciones();
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
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            switch (varianteSeleccionada)
            {
                case 0: // De Casteljau - 4 puntos
                    puntosControl.Add(new PointF(100, centerY + 50));
                    puntosControl.Add(new PointF(200, centerY - 100));
                    puntosControl.Add(new PointF(400, centerY - 100));
                    puntosControl.Add(new PointF(500, centerY + 50));
                    break;

                case 1: // Cuadrática - 3 puntos
                    puntosControl.Add(new PointF(100, centerY + 50));
                    puntosControl.Add(new PointF(centerX, centerY - 100));
                    puntosControl.Add(new PointF(500, centerY + 50));
                    break;

                case 2: // Cúbica - 4 puntos
                    puntosControl.Add(new PointF(100, centerY));
                    puntosControl.Add(new PointF(200, centerY - 100));
                    puntosControl.Add(new PointF(400, centerY + 100));
                    puntosControl.Add(new PointF(500, centerY));
                    break;
            }

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
            parametroT += 0.01f;

            if (parametroT > 1.0f)
            {
                parametroT = 0f;
            }

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
                if (puntosControl.Count < 2)
                {
                    puntosCurvaCompleta.Clear();
                    return;
                }

                switch (varianteSeleccionada)
                {
                    case 0: // De Casteljau
                        puntosCurvaCompleta = dibujador.GenerarCurvaBezier(puntosControl, 100);
                        break;

                    case 1: // Cuadrática
                        if (puntosControl.Count >= 3)
                        {
                            puntosCurvaCompleta = dibujador.GenerarCurvaCuadratica(
                                puntosControl[0], puntosControl[1], puntosControl[2], 100);
                        }
                        break;

                    case 2: // Cúbica
                        if (puntosControl.Count >= 4)
                        {
                            puntosCurvaCompleta = dibujador.GenerarCurvaCubica(
                                puntosControl[0], puntosControl[1], puntosControl[2], puntosControl[3], 100);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar curva: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Dibujar la curva completa
            DibujarCurva();

            // Dibujar polígono de control
            DibujarPoligonoControl();

            // Dibujar puntos de control
            DibujarPuntosControl();

            // Si la animación está activa, dibujar la construcción
            if (animacionActiva && puntosControl.Count >= 2)
            {
                DibujarConstruccion(parametroT);
            }

            // Dibujar información
            DibujarInformacion();
        }

        /// <summary>
        /// Dibuja la curva de Bézier completa.
        /// </summary>
        private void DibujarCurva()
        {
            if (puntosCurvaCompleta == null || puntosCurvaCompleta.Count < 2)
                return;

            using (Pen penCurva = new Pen(Color.Blue, 3))
            {
                for (int i = 0; i < puntosCurvaCompleta.Count - 1; i++)
                {
                    g.DrawLine(penCurva, puntosCurvaCompleta[i], puntosCurvaCompleta[i + 1]);
                }
            }
        }

        /// <summary>
        /// Dibuja el polígono de control (líneas entre puntos de control).
        /// </summary>
        private void DibujarPoligonoControl()
        {
            if (puntosControl.Count < 2) return;

            using (Pen penControl = new Pen(Color.Gray, 1))
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
                Color colorPunto = (i == 0) ? Color.Green : (i == puntosControl.Count - 1) ? Color.Red : Color.Orange;

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
        /// Dibuja la construcción animada del algoritmo de De Casteljau.
        /// </summary>
        private void DibujarConstruccion(float t)
        {
            try
            {
                List<List<PointF>> pasos = dibujador.DeCasteljauConPasos(puntosControl, t);

                Color[] colores = { Color.Purple, Color.Magenta, Color.Violet, Color.Pink };

                // Dibujar cada nivel de la construcción
                for (int nivel = 1; nivel < pasos.Count; nivel++)
                {
                    Color color = colores[(nivel - 1) % colores.Length];

                    using (Pen pen = new Pen(color, 2))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                        // Dibujar líneas entre puntos del nivel
                        for (int i = 0; i < pasos[nivel].Count - 1; i++)
                        {
                            g.DrawLine(pen, pasos[nivel][i], pasos[nivel][i + 1]);
                        }
                    }

                    // Dibujar puntos del nivel
                    using (Brush brush = new SolidBrush(color))
                    {
                        foreach (PointF p in pasos[nivel])
                        {
                            g.FillEllipse(brush, p.X - 4, p.Y - 4, 8, 8);
                        }
                    }
                }

                // Dibujar el punto final en la curva
                if (pasos.Count > 0 && pasos[pasos.Count - 1].Count > 0)
                {
                    PointF puntoFinal = pasos[pasos.Count - 1][0];
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillEllipse(brush, puntoFinal.X - 5, puntoFinal.Y - 5, 10, 10);
                    }
                }
            }
            catch (Exception ex)
            {
                // Silenciar errores de animación para no interrumpir el flujo
                Console.WriteLine($"Error en animación: {ex.Message}");
            }
        }

        /// <summary>
        /// Dibuja información en pantalla.
        /// </summary>
        private void DibujarInformacion()
        {
            if (!animacionActiva) return;

            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.DarkBlue))
            {
                string info = $"t = {parametroT:F3}";
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
                // Buscar si se hizo clic cerca de algún punto de control
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
        }

        /// <summary>
        /// Maneja el evento MouseMove para arrastrar puntos.
        /// </summary>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando && puntoSeleccionado >= 0)
            {
                // Actualizar posición del punto
                puntosControl[puntoSeleccionado] = new PointF(
                    Math.Max(0, Math.Min(e.X, pictureBox1.Width)),
                    Math.Max(0, Math.Min(e.Y, pictureBox1.Height))
                );

                // Regenerar curva
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
                    algoritmo = "De Casteljau";
                    break;
                case 1:
                    algoritmo = "Bézier Cuadrática";
                    break;
                case 2:
                    algoritmo = "Bézier Cúbica";
                    break;
            }

            lblInstruccion.Text = $"Algoritmo: {algoritmo} | Arrastra los puntos de control para modificar la curva";
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
                InicializarPuntosPorDefecto();
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
            animacionActiva = !animacionActiva;

            if (animacionActiva)
            {
                parametroT = 0f;
                timerAnimacion.Start();
                btnAnimar.Text = "Detener Animación";
            }
            else
            {
                timerAnimacion.Stop();
                btnAnimar.Text = "Iniciar Animación";
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
            parametroT = 0f;
            DibujarTodo();
            pictureBox1.Refresh();
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
                    info = "Algoritmo de De Casteljau\n\n" +
                           "Método fundamental para calcular curvas de Bézier mediante interpolación " +
                           "lineal recursiva. Funciona con cualquier número de puntos de control.\n\n" +
                           "Funcionamiento: Interpola linealmente entre pares de puntos consecutivos, " +
                           "luego repite el proceso con los puntos resultantes hasta obtener un único punto.\n\n" +
                           "Ventajas: Muy estable numéricamente, fácil de visualizar, funciona para " +
                           "cualquier grado de curva.\n\n" +
                           "Uso: Animaciones, subdivisión de curvas, evaluación precisa.";
                    break;

                case 1:
                    info = "Curva de Bézier Cuadrática\n\n" +
                           "Curva definida por 3 puntos de control: inicio, control y fin.\n" +
                           "Fórmula: B(t) = (1-t)²P₀ + 2(1-t)tP₁ + t²P₂\n\n" +
                           "Características: Genera curvas suaves y predecibles. El punto de control " +
                           "atrae la curva pero no la toca.\n\n" +
                           "Ventajas: Simple, rápida de calcular, suficiente para muchas aplicaciones.\n\n" +
                           "Uso: Fuentes TrueType, gráficos 2D, animaciones simples.";
                    break;

                case 2:
                    info = "Curva de Bézier Cúbica\n\n" +
                           "Curva definida por 4 puntos de control: inicio, dos puntos de control y fin.\n" +
                           "Fórmula: B(t) = (1-t)³P₀ + 3(1-t)²tP₁ + 3(1-t)t²P₂ + t³P₃\n\n" +
                           "Características: Mayor flexibilidad que la cuadrática. Los dos puntos " +
                           "de control permiten crear formas más complejas.\n\n" +
                           "Ventajas: Estándar en diseño gráfico, balance entre control y simplicidad.\n\n" +
                           "Uso: Fuentes PostScript, SVG, herramientas de diseño (Illustrator, Inkscape).";
                    break;
            }

            MessageBox.Show(info, "Información del Algoritmo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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