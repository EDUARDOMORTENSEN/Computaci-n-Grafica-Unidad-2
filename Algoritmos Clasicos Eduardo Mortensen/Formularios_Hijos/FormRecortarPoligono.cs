using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lineas
{
    /// <summary>
    /// Formulario para demostrar las tres variantes de algoritmos de recorte de polígonos.
    /// Permite dibujar polígonos haciendo clic en puntos y visualizar el resultado del recorte.
    /// </summary>
    public partial class FormRecortarPoligono : Form
    {
        private Bitmap bmp;
        private Graphics g;
        private RecortarPoligono recortador;

        // Polígonos
        private List<PointF> poligonoSujeto;
        private List<PointF> poligonoRecorte;
        private List<PointF> poligonoResultado;

        // Estado de dibujo
        private bool dibujandoSujeto = true;
        private List<PointF> puntosTemporales;

        // Variante seleccionada
        private int varianteSeleccionada = 0;

        public FormRecortarPoligono()
        {
            InitializeComponent();

            // Inicializar ComboBox - el Designer también conecta el evento SelectedIndexChanged
            try
            {
                if (cmbVariante != null)
                {
                    cmbVariante.Items.Clear();
                    cmbVariante.Items.Add("Sutherland-Hodgman");
                    cmbVariante.Items.Add("Weiler-Atherton");
                    cmbVariante.Items.Add("S-H Optimizado");
                    cmbVariante.SelectedIndex = 0;
                }
            }
            catch
            {
                // No detener la ejecución si el combo no está aún asignado por alguna razón
            }

            InicializarCanvas();
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
                    MessageBox.Show("Error: PictureBox no inicializado correctamente.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Liberar recursos anteriores si existen
                LiberarRecursosGraficos();

                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Crear instancia del recortador (puede lanzar si la clase no existe)
                try
                {
                    recortador = new RecortarPoligono();
                }
                catch
                {
                    recortador = null;
                }

                // Inicializar listas
                poligonoSujeto = new List<PointF>();
                poligonoRecorte = new List<PointF>();
                poligonoResultado = new List<PointF>();
                puntosTemporales = new List<PointF>();

                pictureBox1.Image = bmp;
                ActualizarInstrucciones();
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del mouse para agregar puntos al polígono.
        /// Click izquierdo agrega punto temporal. Click derecho cierra el polígono actual.
        /// </summary>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Agregar punto temporal
                    if (puntosTemporales == null)
                        puntosTemporales = new List<PointF>();

                    PointF punto = new PointF(e.X, e.Y);
                    puntosTemporales.Add(punto);

                    // Redibujar y mostrar puntos temporales
                    RedibujarTodo();
                    DibujarPuntosTemporales();
                    pictureBox1.Refresh();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // Cerrar polígono actual (si hay suficientes puntos)
                    CerrarPoligonoActual();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar punto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra el polígono actual y cambia al siguiente modo.
        /// </summary>
        private void CerrarPoligonoActual()
        {
            try
            {
                if (puntosTemporales == null || puntosTemporales.Count < 3)
                {
                    MessageBox.Show("Se necesitan al menos 3 puntos para formar un polígono.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dibujandoSujeto)
                {
                    poligonoSujeto = new List<PointF>(puntosTemporales);
                    dibujandoSujeto = false;
                    puntosTemporales.Clear();
                    MessageBox.Show("Polígono sujeto completado. Ahora dibuja el polígono de recorte.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    poligonoRecorte = new List<PointF>(puntosTemporales);
                    puntosTemporales.Clear();
                    AplicarRecorte();
                }

                RedibujarTodo();
                pictureBox1.Refresh();
                ActualizarInstrucciones();
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar polígono: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aplica el algoritmo de recorte seleccionado.
        /// </summary>
        private void AplicarRecorte()
        {
            try
            {
                // Validaciones básicas
                if (recortador == null)
                {
                    MessageBox.Show("La clase RecortarPoligono no está disponible.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    poligonoResultado = new List<PointF>();
                    return;
                }

                if (poligonoSujeto == null || poligonoRecorte == null)
                {
                    poligonoResultado = new List<PointF>();
                    return;
                }

                if (poligonoSujeto.Count < 3 || poligonoRecorte.Count < 3)
                {
                    poligonoResultado = new List<PointF>();
                    return;
                }

                switch (varianteSeleccionada)
                {
                    case 0:
                        poligonoResultado = recortador.SutherlandHodgman(poligonoSujeto, poligonoRecorte) ?? new List<PointF>();
                        break;
                    case 1:
                        poligonoResultado = recortador.WeilerAtherton(poligonoSujeto, poligonoRecorte) ?? new List<PointF>();
                        break;
                    case 2:
                        poligonoResultado = recortador.SutherlandHodgmanOptimizado(poligonoSujeto, poligonoRecorte) ?? new List<PointF>();
                        break;
                    default:
                        poligonoResultado = new List<PointF>();
                        break;
                }

                // Asegurar no nulos
                if (poligonoResultado == null) poligonoResultado = new List<PointF>();

                RedibujarTodo();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Error en parámetros: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                poligonoResultado = new List<PointF>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar recorte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                poligonoResultado = new List<PointF>();
            }
        }

        /// <summary>
        /// Redibuja todos los polígonos en el lienzo.
        /// </summary>
        private void RedibujarTodo()
        {
            if (g == null || bmp == null) return;

            g.Clear(Color.White);

            // Dibujar polígono sujeto (azul)
            if (poligonoSujeto != null && poligonoSujeto.Count >= 3)
            {
                try
                {
                    using (Pen penSujeto = new Pen(Color.Blue, 2))
                    using (Brush brushSujeto = new SolidBrush(Color.FromArgb(50, Color.Blue)))
                    {
                        g.FillPolygon(brushSujeto, poligonoSujeto.ToArray());
                        g.DrawPolygon(penSujeto, poligonoSujeto.ToArray());
                    }

                    // Dibujar vértices
                    using (Brush brushVertex = new SolidBrush(Color.Blue))
                    {
                        foreach (PointF p in poligonoSujeto)
                        {
                            g.FillEllipse(brushVertex, p.X - 3, p.Y - 3, 6, 6);
                        }
                    }
                }
                catch { /* dibujo seguro: si falla, se ignora para no bloquear UI */ }
            }

            // Dibujar polígono de recorte (verde)
            if (poligonoRecorte != null && poligonoRecorte.Count >= 3)
            {
                try
                {
                    using (Pen penRecorte = new Pen(Color.Green, 2))
                    {
                        penRecorte.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawPolygon(penRecorte, poligonoRecorte.ToArray());
                    }

                    // Dibujar vértices
                    using (Brush brushVertex = new SolidBrush(Color.Green))
                    {
                        foreach (PointF p in poligonoRecorte)
                        {
                            g.FillEllipse(brushVertex, p.X - 3, p.Y - 3, 6, 6);
                        }
                    }
                }
                catch { }
            }

            // Dibujar polígono resultado (rojo)
            if (poligonoResultado != null && poligonoResultado.Count >= 3)
            {
                try
                {
                    using (Pen penResultado = new Pen(Color.Red, 3))
                    using (Brush brushResultado = new SolidBrush(Color.FromArgb(100, Color.Red)))
                    {
                        g.FillPolygon(brushResultado, poligonoResultado.ToArray());
                        g.DrawPolygon(penResultado, poligonoResultado.ToArray());
                    }

                    // Dibujar vértices
                    using (Brush brushVertex = new SolidBrush(Color.DarkRed))
                    {
                        foreach (PointF p in poligonoResultado)
                        {
                            g.FillEllipse(brushVertex, p.X - 4, p.Y - 4, 8, 8);
                        }
                    }
                }
                catch { }
            }

            // Volcar imagen al pictureBox
            pictureBox1.Image = bmp;
        }

        /// <summary>
        /// Dibuja los puntos temporales mientras se construye un polígono.
        /// </summary>
        private void DibujarPuntosTemporales()
        {
            if (puntosTemporales == null || puntosTemporales.Count == 0) return;

            Color color = dibujandoSujeto ? Color.Blue : Color.Green;

            try
            {
                using (Pen pen = new Pen(color, 1))
                using (Brush brush = new SolidBrush(color))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                    // Dibujar líneas entre puntos temporales
                    for (int i = 0; i < puntosTemporales.Count - 1; i++)
                    {
                        g.DrawLine(pen, puntosTemporales[i], puntosTemporales[i + 1]);
                    }

                    // Dibujar puntos
                    foreach (PointF p in puntosTemporales)
                    {
                        g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
                    }
                }

                // Actualizar la imagen en pantalla
                pictureBox1.Image = bmp;
            }
            catch { }
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
                    algoritmo = "Sutherland-Hodgman";
                    break;
                case 1:
                    algoritmo = "Weiler-Atherton";
                    break;
                case 2:
                    algoritmo = "S-H Optimizado";
                    break;
            }

            string estado = dibujandoSujeto ? "Dibujando SUJETO (azul)" : "Dibujando RECORTE (verde)";

            lblInstruccion.Text = $"Algoritmo: {algoritmo} | {estado} | Click izq: Punto | Click der: Cerrar";
        }

        /// <summary>
        /// Actualiza el estado de los botones según el progreso.
        /// </summary>
        private void ActualizarEstadoBotones()
        {
            if (btnRecortar != null)
            {
                btnRecortar.Enabled = (poligonoSujeto != null && poligonoSujeto.Count >= 3)
                    && (poligonoRecorte != null && poligonoRecorte.Count >= 3);
            }
        }

        /// <summary>
        /// Limpia todo y reinicia.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                poligonoSujeto?.Clear();
                poligonoRecorte?.Clear();
                poligonoResultado?.Clear();
                puntosTemporales?.Clear();

                dibujandoSujeto = true;

                if (g != null && bmp != null)
                {
                    g.Clear(Color.White);
                    pictureBox1.Image = bmp;
                }

                ActualizarInstrucciones();
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aplica el recorte manualmente cuando ambos polígonos están definidos.
        /// </summary>
        private void btnRecortar_Click(object sender, EventArgs e)
        {
            AplicarRecorte();
            pictureBox1.Refresh();
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
                ActualizarInstrucciones();

                // Reaplicar recorte si ya hay polígonos definidos
                if (poligonoSujeto != null && poligonoSujeto.Count >= 3 &&
                    poligonoRecorte != null && poligonoRecorte.Count >= 3)
                {
                    AplicarRecorte();
                    pictureBox1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar variante: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra información sobre el algoritmo seleccionado.
        /// </summary>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            string info = "";

            switch (varianteSeleccionada)
            {
                case 0:
                    info = "Sutherland-Hodgman\n\n" +
                           "Descripcion: Algoritmo clasico para recorte de poligonos contra " +
                           "ventanas rectangulares o poligonos convexos. Procesa el poligono " +
                           "contra cada borde del area de recorte secuencialmente.\n\n" +
                           "Complejidad: O(n·m) donde n es vertices del sujeto " +
                           "y m es lados del recorte.\n\n" +
                           "Ventajas: Simple, eficiente, funciona bien para poligonos convexos.\n\n" +
                           "Limitaciones: Solo funciona correctamente con poligonos de recorte convexos.";
                    break;

                case 1:
                    info = "Weiler-Atherton (Simplificado)\n\n" +
                           "Descripcion: Algoritmo general que puede manejar poligonos concavos " +
                           "y casos mas complejos. Esta version simplificada identifica intersecciones " +
                           "y vertices dentro de cada poligono.\n\n" +
                           "Complejidad: O(n·m) pero con mayor constante que Sutherland-Hodgman.\n\n" +
                           "Ventajas: Maneja poligonos concavos, mas robusto para casos complejos.\n\n" +
                           "Limitaciones: Mas complejo de implementar, puede requerir post-procesamiento.";
                    break;

                case 2:
                    info = "Sutherland-Hodgman Optimizado\n\n" +
                           "Descripcion: Version mejorada del algoritmo clasico con eliminacion " +
                           "automatica de vertices duplicados y mejor manejo de errores de punto flotante.\n\n" +
                           "Complejidad: O(n·m) con optimizaciones para casos especiales.\n\n" +
                           "Ventajas: Mas robusto que la version basica, evita problemas numericos, " +
                           "produce poligonos mas limpios.\n\n" +
                           "Mejoras: Tolerancia epsilon, eliminacion de duplicados, mejor precision.";
                    break;
            }

            MessageBox.Show(info, "Informacion del Algoritmo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Libera recursos gráficos al cerrar.
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
        /// Maneja el cierre del formulario.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            LiberarRecursosGraficos();
            base.OnFormClosing(e);
        }

        private void FormRecortarPoligono_Load(object sender, EventArgs e)
        {

        }
    }
}
