using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lineas
{
    /// <summary>
    /// Clase que implementa algoritmos para dibujar curvas de Bézier.
    /// Variante 1: Algoritmo de De Casteljau
    /// Variante 2: Curvas Bézier Cuadráticas (3 puntos de control)
    /// Variante 3: Curvas Bézier Cúbicas (4 puntos de control)
    /// </summary>
    public class DibujarCurva
    {
        #region Algoritmo de De Casteljau (Variante 1)

        /// <summary>
        /// Algoritmo de De Casteljau para calcular un punto en una curva de Bézier.
        /// Este es el método fundamental que funciona para cualquier número de puntos de control.
        /// Utiliza interpolación lineal recursiva para encontrar el punto en el parámetro t.
        /// </summary>
        /// <param name="puntos">Lista de puntos de control</param>
        /// <param name="t">Parámetro de la curva (0.0 a 1.0)</param>
        /// <returns>Punto calculado en la curva para el parámetro t</returns>
        public PointF DeCasteljau(List<PointF> puntos, float t)
        {
            if (puntos == null || puntos.Count == 0)
                throw new ArgumentException("Se requiere al menos un punto de control.");

            if (t < 0 || t > 1)
                throw new ArgumentOutOfRangeException("t", "El parámetro t debe estar entre 0 y 1.");

            // Caso base: un solo punto
            if (puntos.Count == 1)
                return puntos[0];

            // Lista temporal para los puntos interpolados
            List<PointF> puntosInterpolados = new List<PointF>();

            // Interpolar linealmente entre cada par de puntos consecutivos
            for (int i = 0; i < puntos.Count - 1; i++)
            {
                float x = (1 - t) * puntos[i].X + t * puntos[i + 1].X;
                float y = (1 - t) * puntos[i].Y + t * puntos[i + 1].Y;
                puntosInterpolados.Add(new PointF(x, y));
            }

            // Recursión: repetir el proceso con los puntos interpolados
            return DeCasteljau(puntosInterpolados, t);
        }

        /// <summary>
        /// Obtiene todos los puntos intermedios generados durante el algoritmo de De Casteljau.
        /// Útil para visualizar la construcción paso a paso de la curva.
        /// </summary>
        /// <param name="puntos">Puntos de control</param>
        /// <param name="t">Parámetro de la curva</param>
        /// <returns>Lista de listas, donde cada sublista contiene los puntos de un nivel de recursión</returns>
        public List<List<PointF>> DeCasteljauConPasos(List<PointF> puntos, float t)
        {
            if (puntos == null || puntos.Count == 0)
                throw new ArgumentException("Se requiere al menos un punto de control.");

            List<List<PointF>> todosLosPasos = new List<List<PointF>>();
            List<PointF> puntosActuales = new List<PointF>(puntos);
            todosLosPasos.Add(new List<PointF>(puntosActuales));

            while (puntosActuales.Count > 1)
            {
                List<PointF> puntosInterpolados = new List<PointF>();

                for (int i = 0; i < puntosActuales.Count - 1; i++)
                {
                    float x = (1 - t) * puntosActuales[i].X + t * puntosActuales[i + 1].X;
                    float y = (1 - t) * puntosActuales[i].Y + t * puntosActuales[i + 1].Y;
                    puntosInterpolados.Add(new PointF(x, y));
                }

                todosLosPasos.Add(new List<PointF>(puntosInterpolados));
                puntosActuales = puntosInterpolados;
            }

            return todosLosPasos;
        }

        #endregion

        #region Curva Bézier Cuadrática (Variante 2)

        /// <summary>
        /// Calcula un punto en una curva de Bézier cuadrática (3 puntos de control).
        /// Utiliza la fórmula explícita: B(t) = (1-t)²P₀ + 2(1-t)tP₁ + t²P₂
        /// </summary>
        /// <param name="p0">Punto inicial</param>
        /// <param name="p1">Punto de control</param>
        /// <param name="p2">Punto final</param>
        /// <param name="t">Parámetro de la curva (0.0 a 1.0)</param>
        /// <returns>Punto calculado en la curva</returns>
        public PointF BezierCuadratica(PointF p0, PointF p1, PointF p2, float t)
        {
            if (t < 0 || t > 1)
                throw new ArgumentOutOfRangeException("t", "El parámetro t debe estar entre 0 y 1.");

            // Coeficientes de Bernstein para n=2
            float b0 = (1 - t) * (1 - t);           // (1-t)²
            float b1 = 2 * (1 - t) * t;             // 2(1-t)t
            float b2 = t * t;                        // t²

            // Calcular coordenadas
            float x = b0 * p0.X + b1 * p1.X + b2 * p2.X;
            float y = b0 * p0.Y + b1 * p1.Y + b2 * p2.Y;

            return new PointF(x, y);
        }

        /// <summary>
        /// Genera todos los puntos de una curva de Bézier cuadrática.
        /// </summary>
        /// <param name="p0">Punto inicial</param>
        /// <param name="p1">Punto de control</param>
        /// <param name="p2">Punto final</param>
        /// <param name="numPuntos">Número de puntos a generar (mayor = más suave)</param>
        /// <returns>Lista de puntos que forman la curva</returns>
        public List<PointF> GenerarCurvaCuadratica(PointF p0, PointF p1, PointF p2, int numPuntos = 50)
        {
            if (numPuntos < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos.");

            List<PointF> puntosCurva = new List<PointF>();

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = i / (float)numPuntos;
                puntosCurva.Add(BezierCuadratica(p0, p1, p2, t));
            }

            return puntosCurva;
        }

        #endregion

        #region Curva Bézier Cúbica (Variante 3)

        /// <summary>
        /// Calcula un punto en una curva de Bézier cúbica (4 puntos de control).
        /// Utiliza la fórmula: B(t) = (1-t)³P₀ + 3(1-t)²tP₁ + 3(1-t)t²P₂ + t³P₃
        /// </summary>
        /// <param name="p0">Punto inicial</param>
        /// <param name="p1">Primer punto de control</param>
        /// <param name="p2">Segundo punto de control</param>
        /// <param name="p3">Punto final</param>
        /// <param name="t">Parámetro de la curva (0.0 a 1.0)</param>
        /// <returns>Punto calculado en la curva</returns>
        public PointF BezierCubica(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            if (t < 0 || t > 1)
                throw new ArgumentOutOfRangeException("t", "El parámetro t debe estar entre 0 y 1.");

            // Coeficientes de Bernstein para n=3
            float b0 = (1 - t) * (1 - t) * (1 - t);         // (1-t)³
            float b1 = 3 * (1 - t) * (1 - t) * t;           // 3(1-t)²t
            float b2 = 3 * (1 - t) * t * t;                 // 3(1-t)t²
            float b3 = t * t * t;                            // t³

            // Calcular coordenadas
            float x = b0 * p0.X + b1 * p1.X + b2 * p2.X + b3 * p3.X;
            float y = b0 * p0.Y + b1 * p1.Y + b2 * p2.Y + b3 * p3.Y;

            return new PointF(x, y);
        }

        /// <summary>
        /// Genera todos los puntos de una curva de Bézier cúbica.
        /// </summary>
        /// <param name="p0">Punto inicial</param>
        /// <param name="p1">Primer punto de control</param>
        /// <param name="p2">Segundo punto de control</param>
        /// <param name="p3">Punto final</param>
        /// <param name="numPuntos">Número de puntos a generar</param>
        /// <returns>Lista de puntos que forman la curva</returns>
        public List<PointF> GenerarCurvaCubica(PointF p0, PointF p1, PointF p2, PointF p3, int numPuntos = 50)
        {
            if (numPuntos < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos.");

            List<PointF> puntosCurva = new List<PointF>();

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = i / (float)numPuntos;
                puntosCurva.Add(BezierCubica(p0, p1, p2, p3, t));
            }

            return puntosCurva;
        }

        #endregion

        #region Métodos Generales

        /// <summary>
        /// Genera una curva de Bézier genérica usando De Casteljau para cualquier número de puntos.
        /// </summary>
        /// <param name="puntosControl">Lista de puntos de control (mínimo 2)</param>
        /// <param name="numPuntos">Número de puntos a generar en la curva</param>
        /// <returns>Lista de puntos que forman la curva</returns>
        public List<PointF> GenerarCurvaBezier(List<PointF> puntosControl, int numPuntos = 50)
        {
            if (puntosControl == null || puntosControl.Count < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos de control.");

            if (numPuntos < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos para la curva.");

            List<PointF> puntosCurva = new List<PointF>();

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = i / (float)numPuntos;
                puntosCurva.Add(DeCasteljau(puntosControl, t));
            }

            return puntosCurva;
        }

        /// <summary>
        /// Calcula la longitud aproximada de una curva de Bézier.
        /// </summary>
        /// <param name="puntosCurva">Puntos que forman la curva</param>
        /// <returns>Longitud aproximada de la curva</returns>
        public float CalcularLongitudCurva(List<PointF> puntosCurva)
        {
            if (puntosCurva == null || puntosCurva.Count < 2)
                return 0;

            float longitud = 0;

            for (int i = 0; i < puntosCurva.Count - 1; i++)
            {
                float dx = puntosCurva[i + 1].X - puntosCurva[i].X;
                float dy = puntosCurva[i + 1].Y - puntosCurva[i].Y;
                longitud += (float)Math.Sqrt(dx * dx + dy * dy);
            }

            return longitud;
        }

        #endregion
    }
}