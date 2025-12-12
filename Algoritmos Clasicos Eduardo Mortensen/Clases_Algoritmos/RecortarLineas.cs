using System;
using System.Drawing;

namespace Lineas
{
    /// <summary>
    /// Clase que implementa tres variantes de algoritmos de recorte de líneas.
    /// Variante 1: Cohen-Sutherland (estándar, basado en códigos de región)
    /// Variante 2: Liang-Barsky (basado en ecuaciones paramétricas)
    /// Variante 3: Cyrus-Beck (generalizado para polígonos convexos, aquí aplicado a rectángulos)
    /// </summary>
    public class RecortarLineas
    {
        #region Cohen-Sutherland (Variante 1)

        // Códigos de región para Cohen-Sutherland
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

        /// <summary>
        /// Calcula el código de región de un punto respecto al rectángulo de recorte.
        /// </summary>
        private int ComputeOutCode(double x, double y, Rectangle rect)
        {
            int code = INSIDE;

            if (x < rect.Left)
                code |= LEFT;
            else if (x > rect.Right)
                code |= RIGHT;

            if (y < rect.Top)
                code |= TOP;
            else if (y > rect.Bottom)
                code |= BOTTOM;

            return code;
        }

        /// <summary>
        /// Algoritmo Cohen-Sutherland para recorte de líneas.
        /// Utiliza códigos de región para identificar rápidamente líneas completamente
        /// dentro o fuera del área de recorte, y calcula intersecciones iterativamente.
        /// </summary>
        public bool CohenSutherland(Rectangle rect, ref double x0, ref double y0, ref double x1, ref double y1)
        {
            // Validación de entrada
            if (rect.Width <= 0 || rect.Height <= 0)
                throw new ArgumentException("El rectángulo de recorte debe tener dimensiones positivas.");

            int outcode0 = ComputeOutCode(x0, y0, rect);
            int outcode1 = ComputeOutCode(x1, y1, rect);
            bool accept = false;

            while (true)
            {
                if ((outcode0 | outcode1) == 0)
                {
                    // Ambos puntos dentro
                    accept = true;
                    break;
                }
                else if ((outcode0 & outcode1) != 0)
                {
                    // Ambos puntos en la misma región externa - rechazo trivial
                    break;
                }
                else
                {
                    // Línea cruza el área - calcular intersección
                    double x = 0, y = 0;
                    int outcodeOut = (outcode0 != 0) ? outcode0 : outcode1;

                    // Calcular intersección con cada borde
                    if ((outcodeOut & TOP) != 0)
                    {
                        x = x0 + (x1 - x0) * (rect.Top - y0) / (y1 - y0);
                        y = rect.Top;
                    }
                    else if ((outcodeOut & BOTTOM) != 0)
                    {
                        x = x0 + (x1 - x0) * (rect.Bottom - y0) / (y1 - y0);
                        y = rect.Bottom;
                    }
                    else if ((outcodeOut & RIGHT) != 0)
                    {
                        y = y0 + (y1 - y0) * (rect.Right - x0) / (x1 - x0);
                        x = rect.Right;
                    }
                    else if ((outcodeOut & LEFT) != 0)
                    {
                        y = y0 + (y1 - y0) * (rect.Left - x0) / (x1 - x0);
                        x = rect.Left;
                    }

                    // Reemplazar punto exterior con el punto de intersección
                    if (outcodeOut == outcode0)
                    {
                        x0 = x;
                        y0 = y;
                        outcode0 = ComputeOutCode(x0, y0, rect);
                    }
                    else
                    {
                        x1 = x;
                        y1 = y;
                        outcode1 = ComputeOutCode(x1, y1, rect);
                    }
                }
            }

            return accept;
        }

        #endregion

        #region Liang-Barsky (Variante 2)

        /// <summary>
        /// Algoritmo Liang-Barsky para recorte de líneas.
        /// Utiliza ecuaciones paramétricas de la línea y calcula intersecciones
        /// con los bordes del rectángulo mediante parámetros t en el rango [0,1].
        /// Es más eficiente que Cohen-Sutherland para múltiples líneas.
        /// </summary>
        public bool LiangBarsky(Rectangle rect, ref double x0, ref double y0, ref double x1, ref double y1)
        {
            // Validación de entrada
            if (rect.Width <= 0 || rect.Height <= 0)
                throw new ArgumentException("El rectángulo de recorte debe tener dimensiones positivas.");

            // Guardar coordenadas originales
            double x0Original = x0;
            double y0Original = y0;

            double t0 = 0.0, t1 = 1.0;
            double dx = x1 - x0;
            double dy = y1 - y0;

            // Comprobar los cuatro bordes del rectángulo
            // p y q representan la dirección y distancia a cada borde
            double[] p = { -dx, dx, -dy, dy };
            double[] q = { x0 - rect.Left, rect.Right - x0, y0 - rect.Top, rect.Bottom - y0 };

            for (int i = 0; i < 4; i++)
            {
                if (Math.Abs(p[i]) < 1e-10)
                {
                    // Línea paralela al borde
                    if (q[i] < 0)
                        return false; // Línea completamente fuera
                }
                else
                {
                    double t = q[i] / p[i];

                    if (p[i] < 0)
                    {
                        // Línea entrando al área de recorte
                        if (t > t1)
                            return false;
                        if (t > t0)
                            t0 = t;
                    }
                    else
                    {
                        // Línea saliendo del área de recorte
                        if (t < t0)
                            return false;
                        if (t < t1)
                            t1 = t;
                    }
                }
            }

            // Calcular los nuevos puntos basados en los parámetros t
            x0 = x0Original + t0 * dx;
            y0 = y0Original + t0 * dy;
            x1 = x0Original + t1 * dx;
            y1 = y0Original + t1 * dy;

            return true;
        }

        #endregion

        #region Cyrus-Beck (Variante 3)

        /// <summary>
        /// Algoritmo Cyrus-Beck para recorte de líneas.
        /// Generalización del Liang-Barsky que funciona con polígonos convexos.
        /// Aquí se aplica específicamente a rectángulos usando normales a los bordes.
        /// </summary>
        public bool CyrusBeck(Rectangle rect, ref double x0, ref double y0, ref double x1, ref double y1)
        {
            // Validación de entrada
            if (rect.Width <= 0 || rect.Height <= 0)
                throw new ArgumentException("El rectángulo de recorte debe tener dimensiones positivas.");

            // Guardar coordenadas originales
            double x0Original = x0;
            double y0Original = y0;
            double x1Original = x1;
            double y1Original = y1;

            // Definir los bordes del rectángulo con sus normales
            // Normal apunta hacia el interior del rectángulo
            PointF[] vertices = {
                new PointF(rect.Left, rect.Top),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Right, rect.Bottom),
                new PointF(rect.Left, rect.Bottom)
            };

            PointF[] normals = {
                new PointF(0, 1),   // Borde superior (normal hacia abajo)
                new PointF(-1, 0),  // Borde derecho (normal hacia izquierda)
                new PointF(0, -1),  // Borde inferior (normal hacia arriba)
                new PointF(1, 0)    // Borde izquierdo (normal hacia derecha)
            };

            double tE = 0.0; // Parámetro de entrada
            double tL = 1.0; // Parámetro de salida

            double dx = x1Original - x0Original;
            double dy = y1Original - y0Original;

            for (int i = 0; i < 4; i++)
            {
                PointF n = normals[i];
                PointF w = new PointF((float)(x0Original - vertices[i].X), (float)(y0Original - vertices[i].Y));

                double numerator = -(n.X * w.X + n.Y * w.Y);
                double denominator = n.X * dx + n.Y * dy;

                if (Math.Abs(denominator) < 1e-10)
                {
                    // Línea paralela al borde
                    if (numerator < 0)
                        return false; // Línea completamente fuera
                }
                else
                {
                    double t = numerator / denominator;

                    if (denominator < 0)
                    {
                        // Línea entrando
                        tE = Math.Max(tE, t);
                    }
                    else
                    {
                        // Línea saliendo
                        tL = Math.Min(tL, t);
                    }

                    if (tE > tL)
                        return false;
                }
            }

            // Calcular los nuevos puntos usando las coordenadas originales
            x0 = x0Original + tE * dx;
            y0 = y0Original + tE * dy;
            x1 = x0Original + tL * dx;
            y1 = y0Original + tL * dy;

            return true;
        }

        #endregion
    }
}