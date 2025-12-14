using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lineas
{
    /// <summary>
    /// Clase que implementa algoritmos para dibujar curvas B-Spline.
    /// Variante 1: B-Spline Uniforme
    /// Variante 2: B-Spline No Uniforme
    /// Variante 3: B-Spline Cúbico (Cox-de Boor)
    /// </summary>
    public class DibujarBSpline
    {
        #region B-Spline Uniforme (Variante 1)

        /// <summary>
        /// Genera una curva B-Spline uniforme de grado 3 (cúbica).
        /// Un B-Spline uniforme usa espaciado igual en el vector de nodos.
        /// La curva NO pasa por los puntos de control (excepto posiblemente en los extremos).
        /// </summary>
        /// <param name="puntosControl">Lista de puntos de control (mínimo 4 para cúbica)</param>
        /// <param name="numPuntos">Número de puntos a generar en la curva</param>
        /// <returns>Lista de puntos que forman la curva B-Spline</returns>
        public List<PointF> BSplineUniforme(List<PointF> puntosControl, int numPuntos = 100)
        {
            if (puntosControl == null || puntosControl.Count < 4)
                throw new ArgumentException("Se requieren al menos 4 puntos de control para B-Spline cúbico.");

            if (numPuntos < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos.");

            List<PointF> puntosCurva = new List<PointF>();
            int n = puntosControl.Count;
            int grado = 3; // Grado 3 = cúbico

            // Generar vector de nodos uniforme
            List<float> nodos = GenerarNodosUniformes(n, grado);

            // Generar puntos a lo largo de la curva
            float tInicio = nodos[grado];
            float tFin = nodos[n];

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = tInicio + (tFin - tInicio) * i / numPuntos;
                PointF punto = EvaluarBSpline(puntosControl, grado, nodos, t);
                puntosCurva.Add(punto);
            }

            return puntosCurva;
        }

        /// <summary>
        /// Genera un vector de nodos uniforme para B-Spline.
        /// </summary>
        private List<float> GenerarNodosUniformes(int numPuntosControl, int grado)
        {
            List<float> nodos = new List<float>();
            int numNodos = numPuntosControl + grado + 1;

            for (int i = 0; i < numNodos; i++)
            {
                nodos.Add(i);
            }

            return nodos;
        }

        #endregion

        #region B-Spline No Uniforme (Variante 2)

        /// <summary>
        /// Genera una curva B-Spline no uniforme de grado 3.
        /// Permite espaciado no uniforme en el vector de nodos, dando mayor control.
        /// Útil cuando se necesita más densidad de curva en ciertas regiones.
        /// </summary>
        /// <param name="puntosControl">Lista de puntos de control</param>
        /// <param name="vectorNodos">Vector de nodos personalizado (opcional)</param>
        /// <param name="numPuntos">Número de puntos a generar</param>
        /// <returns>Lista de puntos de la curva</returns>
        public List<PointF> BSplineNoUniforme(List<PointF> puntosControl, List<float> vectorNodos = null, int numPuntos = 100)
        {
            if (puntosControl == null || puntosControl.Count < 4)
                throw new ArgumentException("Se requieren al menos 4 puntos de control.");

            if (numPuntos < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos.");

            int n = puntosControl.Count;
            int grado = 3;

            // Si no se proporciona vector de nodos, usar uno con mayor densidad en los extremos
            List<float> nodos = vectorNodos ?? GenerarNodosNoUniformes(n, grado);

            List<PointF> puntosCurva = new List<PointF>();

            float tInicio = nodos[grado];
            float tFin = nodos[n];

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = tInicio + (tFin - tInicio) * i / numPuntos;
                PointF punto = EvaluarBSpline(puntosControl, grado, nodos, t);
                puntosCurva.Add(punto);
            }

            return puntosCurva;
        }

        /// <summary>
        /// Genera un vector de nodos no uniforme con mayor densidad en los extremos.
        /// </summary>
        private List<float> GenerarNodosNoUniformes(int numPuntosControl, int grado)
        {
            List<float> nodos = new List<float>();
            int numNodos = numPuntosControl + grado + 1;

            // Nodos múltiples al inicio (para que la curva pase por el primer punto de control)
            for (int i = 0; i <= grado; i++)
            {
                nodos.Add(0);
            }

            // Nodos intermedios espaciados uniformemente
            for (int i = 1; i < numPuntosControl - grado; i++)
            {
                nodos.Add(i);
            }

            // Nodos múltiples al final (para que la curva pase por el último punto de control)
            for (int i = 0; i <= grado; i++)
            {
                nodos.Add(numPuntosControl - grado);
            }

            return nodos;
        }

        #endregion

        #region B-Spline Cúbico Cox-de Boor (Variante 3)

        /// <summary>
        /// Implementación del algoritmo de Cox-de Boor para evaluar B-Splines cúbicos.
        /// Este es el método estándar y más eficiente para calcular B-Splines.
        /// Utiliza una fórmula recursiva para las funciones base.
        /// </summary>
        /// <param name="puntosControl">Puntos de control</param>
        /// <param name="numPuntos">Número de puntos a generar</param>
        /// <returns>Curva B-Spline calculada con Cox-de Boor</returns>
        public List<PointF> BSplineCoxDeBoor(List<PointF> puntosControl, int numPuntos = 100)
        {
            if (puntosControl == null || puntosControl.Count < 4)
                throw new ArgumentException("Se requieren al menos 4 puntos de control.");

            if (numPuntos < 2)
                throw new ArgumentException("Se requieren al menos 2 puntos.");

            int n = puntosControl.Count;
            int grado = 3;

            // Vector de nodos abierto (curva pasa por puntos extremos)
            List<float> nodos = GenerarNodosAbiertos(n, grado);

            List<PointF> puntosCurva = new List<PointF>();

            float tInicio = nodos[grado];
            float tFin = nodos[n];

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = tInicio + (tFin - tInicio) * i / numPuntos;

                // Asegurar que t no exceda el rango válido
                t = Math.Max(tInicio, Math.Min(t, tFin - 0.0001f));

                PointF punto = CoxDeBoorAlgoritmo(puntosControl, grado, nodos, t);
                puntosCurva.Add(punto);
            }

            return puntosCurva;
        }

        /// <summary>
        /// Genera un vector de nodos abierto (open uniform) para B-Spline.
        /// Este tipo de vector hace que la curva pase por el primer y último punto de control.
        /// </summary>
        private List<float> GenerarNodosAbiertos(int numPuntosControl, int grado)
        {
            List<float> nodos = new List<float>();
            int numNodos = numPuntosControl + grado + 1;

            // Repetir nodos al inicio
            for (int i = 0; i <= grado; i++)
            {
                nodos.Add(0);
            }

            // Nodos internos
            int numNodosInternos = numPuntosControl - grado;
            for (int i = 1; i < numNodosInternos; i++)
            {
                nodos.Add(i);
            }

            // Repetir nodos al final
            for (int i = 0; i <= grado; i++)
            {
                nodos.Add(numNodosInternos);
            }

            return nodos;
        }

        /// <summary>
        /// Algoritmo de Cox-de Boor para evaluar un punto en una curva B-Spline.
        /// Versión optimizada que calcula directamente el punto sin recursión excesiva.
        /// </summary>
        private PointF CoxDeBoorAlgoritmo(List<PointF> puntosControl, int grado, List<float> nodos, float t)
        {
            int n = puntosControl.Count;

            // Encontrar el intervalo de nodos que contiene t
            int k = EncontrarIntervaloNodo(nodos, n, grado, t);

            // Tabla de puntos temporales para el algoritmo de De Boor
            PointF[,] d = new PointF[grado + 1, grado + 1];

            // Inicializar con los puntos de control relevantes
            for (int j = 0; j <= grado; j++)
            {
                d[0, j] = puntosControl[k - grado + j];
            }

            // Aplicar el algoritmo de De Boor
            for (int r = 1; r <= grado; r++)
            {
                for (int j = grado; j >= r; j--)
                {
                    int i = k - grado + j;
                    float alfa = (t - nodos[i]) / (nodos[i + grado - r + 1] - nodos[i]);

                    d[r, j] = new PointF(
                        (1 - alfa) * d[r - 1, j - 1].X + alfa * d[r - 1, j].X,
                        (1 - alfa) * d[r - 1, j - 1].Y + alfa * d[r - 1, j].Y
                    );
                }
            }

            return d[grado, grado];
        }

        /// <summary>
        /// Encuentra el intervalo de nodos donde se encuentra el parámetro t.
        /// </summary>
        private int EncontrarIntervaloNodo(List<float> nodos, int n, int grado, float t)
        {
            // Si t está en el final, devolver el último intervalo válido
            if (t >= nodos[n])
                return n - 1;

            // Búsqueda del intervalo
            for (int i = grado; i < n; i++)
            {
                if (t >= nodos[i] && t < nodos[i + 1])
                    return i;
            }

            return grado;
        }

        #endregion

        #region Evaluación General de B-Spline

        /// <summary>
        /// Evalúa un punto en una curva B-Spline usando funciones base recursivas.
        /// Implementación de la definición matemática estándar de B-Spline.
        /// </summary>
        private PointF EvaluarBSpline(List<PointF> puntosControl, int grado, List<float> nodos, float t)
        {
            float x = 0, y = 0;
            int n = puntosControl.Count;

            for (int i = 0; i < n; i++)
            {
                float base_i = FuncionBase(i, grado, nodos, t);
                x += puntosControl[i].X * base_i;
                y += puntosControl[i].Y * base_i;
            }

            return new PointF(x, y);
        }

        /// <summary>
        /// Calcula la función base B-Spline recursivamente (fórmula de Cox-de Boor).
        /// N_{i,p}(t) = ((t - t_i) / (t_{i+p} - t_i)) * N_{i,p-1}(t) + 
        ///              ((t_{i+p+1} - t) / (t_{i+p+1} - t_{i+1})) * N_{i+1,p-1}(t)
        /// </summary>
        private float FuncionBase(int i, int p, List<float> nodos, float t)
        {
            // Caso base: grado 0
            if (p == 0)
            {
                return (t >= nodos[i] && t < nodos[i + 1]) ? 1.0f : 0.0f;
            }

            // Términos recursivos
            float izq = 0.0f, der = 0.0f;

            // Primer término
            float denominador1 = nodos[i + p] - nodos[i];
            if (Math.Abs(denominador1) > 1e-6f)
            {
                izq = ((t - nodos[i]) / denominador1) * FuncionBase(i, p - 1, nodos, t);
            }

            // Segundo término
            float denominador2 = nodos[i + p + 1] - nodos[i + 1];
            if (Math.Abs(denominador2) > 1e-6f)
            {
                der = ((nodos[i + p + 1] - t) / denominador2) * FuncionBase(i + 1, p - 1, nodos, t);
            }

            return izq + der;
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Calcula la longitud aproximada de una curva.
        /// </summary>
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

        /// <summary>
        /// Obtiene los puntos de control efectivos para un segmento específico de la curva.
        /// Útil para visualizar qué puntos de control afectan cada parte de la curva.
        /// </summary>
        /// <param name="puntosControl">Todos los puntos de control</param>
        /// <param name="grado">Grado de la curva</param>
        /// <param name="indiceSegmento">Índice del segmento</param>
        /// <returns>Lista de puntos de control que afectan este segmento</returns>
        public List<int> ObtenerPuntosControlActivos(int numPuntosControl, int grado, int indiceSegmento)
        {
            List<int> indicesActivos = new List<int>();

            int inicio = Math.Max(0, indiceSegmento - grado);
            int fin = Math.Min(numPuntosControl - 1, indiceSegmento);

            for (int i = inicio; i <= fin; i++)
            {
                indicesActivos.Add(i);
            }

            return indicesActivos;
        }

        #endregion
    }
}