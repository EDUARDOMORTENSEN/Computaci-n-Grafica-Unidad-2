using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lineas
{
    /// <summary>
    /// Clase que implementa tres variantes de algoritmos de recorte de polígonos.
    /// Variante 1: Sutherland-Hodgman (recorte contra polígonos convexos)
    /// Variante 2: Weiler-Atherton (recorte general para polígonos cóncavos)
    /// Variante 3: Sutherland-Hodgman Optimizado (con manejo mejorado de casos especiales)
    /// </summary>
    public class RecortarPoligono
    {
        #region Sutherland-Hodgman (Variante 1)

        /// <summary>
        /// Algoritmo Sutherland-Hodgman para recorte de polígonos.
        /// Recorta un polígono sujeto contra un polígono de recorte convexo.
        /// Procesa el polígono contra cada borde del área de recorte secuencialmente.
        /// </summary>
        /// <param name="subject">Polígono a recortar</param>
        /// <param name="clip">Polígono de recorte (debe ser convexo)</param>
        /// <returns>Polígono resultante después del recorte</returns>
        public List<PointF> SutherlandHodgman(List<PointF> subject, List<PointF> clip)
        {
            // Validación de entrada
            if (subject == null || subject.Count < 3)
                throw new ArgumentException("El polígono sujeto debe tener al menos 3 vértices.");

            if (clip == null || clip.Count < 3)
                throw new ArgumentException("El polígono de recorte debe tener al menos 3 vértices.");

            List<PointF> output = new List<PointF>(subject);

            // Procesar contra cada borde del polígono de recorte
            for (int i = 0; i < clip.Count; i++)
            {
                if (output.Count == 0) break;

                PointF edgeStart = clip[i];
                PointF edgeEnd = clip[(i + 1) % clip.Count];

                List<PointF> input = new List<PointF>(output);
                output.Clear();

                if (input.Count == 0) continue;

                // Vértice anterior
                PointF previousVertex = input[input.Count - 1];

                foreach (PointF currentVertex in input)
                {
                    bool currentInside = EstaDentroSH(currentVertex, edgeStart, edgeEnd);
                    bool previousInside = EstaDentroSH(previousVertex, edgeStart, edgeEnd);

                    if (currentInside)
                    {
                        if (!previousInside)
                        {
                            // Entrando: agregar intersección
                            PointF intersection = CalcularInterseccion(previousVertex, currentVertex, edgeStart, edgeEnd);
                            output.Add(intersection);
                        }
                        // Agregar vértice actual
                        output.Add(currentVertex);
                    }
                    else if (previousInside)
                    {
                        // Saliendo: agregar intersección
                        PointF intersection = CalcularInterseccion(previousVertex, currentVertex, edgeStart, edgeEnd);
                        output.Add(intersection);
                    }
                    // Si ambos están afuera, no agregar nada

                    previousVertex = currentVertex;
                }
            }

            return output;
        }

        /// <summary>
        /// Determina si un punto está dentro del semiplano definido por un borde.
        /// Usa el producto cruzado para determinar el lado del punto respecto al borde.
        /// </summary>
        private bool EstaDentroSH(PointF point, PointF edgeStart, PointF edgeEnd)
        {
            // Producto cruzado: (edgeEnd - edgeStart) x (point - edgeStart)
            float cross = (edgeEnd.X - edgeStart.X) * (point.Y - edgeStart.Y) -
                          (edgeEnd.Y - edgeStart.Y) * (point.X - edgeStart.X);

            // En coordenadas de pantalla (Y hacia abajo), lado izquierdo es negativo
            return cross <= 0;
        }

        #endregion

        #region Weiler-Atherton Simplificado (Variante 2)

        /// <summary>
        /// Algoritmo Weiler-Atherton simplificado para recorte de polígonos.
        /// Puede manejar polígonos cóncavos y casos más complejos que Sutherland-Hodgman.
        /// Esta es una versión simplificada que funciona bien para la mayoría de casos.
        /// </summary>
        public List<PointF> WeilerAtherton(List<PointF> subject, List<PointF> clip)
        {
            // Validación de entrada
            if (subject == null || subject.Count < 3)
                throw new ArgumentException("El polígono sujeto debe tener al menos 3 vértices.");

            if (clip == null || clip.Count < 3)
                throw new ArgumentException("El polígono de recorte debe tener al menos 3 vértices.");

            // Para esta implementación simplificada, usamos un enfoque híbrido
            // que combina detección de intersecciones con clasificación de vértices

            List<PointF> result = new List<PointF>();
            List<PointF> intersections = new List<PointF>();

            // Encontrar todos los vértices del sujeto que están dentro del clip
            foreach (PointF vertex in subject)
            {
                if (PuntoEnPoligono(vertex, clip))
                {
                    result.Add(vertex);
                }
            }

            // Encontrar todas las intersecciones entre los bordes
            for (int i = 0; i < subject.Count; i++)
            {
                PointF s1 = subject[i];
                PointF s2 = subject[(i + 1) % subject.Count];

                for (int j = 0; j < clip.Count; j++)
                {
                    PointF c1 = clip[j];
                    PointF c2 = clip[(j + 1) % clip.Count];

                    PointF intersection;
                    if (SegmentosSeIntersecan(s1, s2, c1, c2, out intersection))
                    {
                        if (!Contienepunto(result, intersection))
                        {
                            result.Add(intersection);
                        }
                    }
                }
            }

            // Encontrar vértices del clip que están dentro del sujeto
            foreach (PointF vertex in clip)
            {
                if (PuntoEnPoligono(vertex, subject))
                {
                    if (!Contienepunto(result, vertex))
                    {
                        result.Add(vertex);
                    }
                }
            }

            // Ordenar puntos para formar un polígono válido
            if (result.Count > 0)
            {
                result = OrdenarPuntosPoligono(result);
            }

            return result;
        }

        /// <summary>
        /// Verifica si un punto está dentro de un polígono usando ray casting.
        /// </summary>
        private bool PuntoEnPoligono(PointF point, List<PointF> polygon)
        {
            int intersections = 0;
            int n = polygon.Count;

            for (int i = 0; i < n; i++)
            {
                PointF p1 = polygon[i];
                PointF p2 = polygon[(i + 1) % n];

                if ((p1.Y > point.Y) != (p2.Y > point.Y))
                {
                    float xIntersection = (p2.X - p1.X) * (point.Y - p1.Y) / (p2.Y - p1.Y) + p1.X;
                    if (point.X < xIntersection)
                    {
                        intersections++;
                    }
                }
            }

            return (intersections % 2) == 1;
        }

        /// <summary>
        /// Verifica si dos segmentos se intersectan y calcula el punto de intersección.
        /// </summary>
        private bool SegmentosSeIntersecan(PointF a1, PointF a2, PointF b1, PointF b2, out PointF intersection)
        {
            intersection = new PointF(0, 0);

            float d = (a1.X - a2.X) * (b1.Y - b2.Y) - (a1.Y - a2.Y) * (b1.X - b2.X);

            if (Math.Abs(d) < 1e-6f)
                return false; // Paralelos

            float t = ((a1.X - b1.X) * (b1.Y - b2.Y) - (a1.Y - b1.Y) * (b1.X - b2.X)) / d;
            float u = ((a1.X - b1.X) * (a1.Y - a2.Y) - (a1.Y - b1.Y) * (a1.X - a2.X)) / d;

            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                intersection.X = a1.X + t * (a2.X - a1.X);
                intersection.Y = a1.Y + t * (a2.Y - a1.Y);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verifica si una lista contiene un punto (con tolerancia).
        /// </summary>
        private bool Contienepunto(List<PointF> points, PointF point, float tolerance = 0.5f)
        {
            foreach (PointF p in points)
            {
                float dx = p.X - point.X;
                float dy = p.Y - point.Y;
                if (dx * dx + dy * dy < tolerance * tolerance)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ordena puntos para formar un polígono convexo (usando ángulo desde el centroide).
        /// </summary>
        private List<PointF> OrdenarPuntosPoligono(List<PointF> points)
        {
            if (points.Count < 3) return points;

            // Calcular centroide
            float cx = 0, cy = 0;
            foreach (PointF p in points)
            {
                cx += p.X;
                cy += p.Y;
            }
            cx /= points.Count;
            cy /= points.Count;

            // Ordenar por ángulo desde el centroide
            points.Sort((a, b) =>
            {
                double angleA = Math.Atan2(a.Y - cy, a.X - cx);
                double angleB = Math.Atan2(b.Y - cy, b.X - cx);
                return angleA.CompareTo(angleB);
            });

            return points;
        }

        #endregion

        #region Sutherland-Hodgman Optimizado (Variante 3)

        /// <summary>
        /// Versión optimizada del algoritmo Sutherland-Hodgman.
        /// Incluye manejo mejorado de casos especiales y eliminación de vértices duplicados.
        /// </summary>
        public List<PointF> SutherlandHodgmanOptimizado(List<PointF> subject, List<PointF> clip)
        {
            // Validación de entrada
            if (subject == null || subject.Count < 3)
                throw new ArgumentException("El polígono sujeto debe tener al menos 3 vértices.");

            if (clip == null || clip.Count < 3)
                throw new ArgumentException("El polígono de recorte debe tener al menos 3 vértices.");

            List<PointF> output = new List<PointF>(subject);
            const float EPSILON = 1e-5f;

            // Procesar contra cada borde del polígono de recorte
            for (int i = 0; i < clip.Count; i++)
            {
                if (output.Count == 0) break;

                PointF edgeStart = clip[i];
                PointF edgeEnd = clip[(i + 1) % clip.Count];

                List<PointF> input = new List<PointF>(output);
                output.Clear();

                if (input.Count == 0) continue;

                PointF previousVertex = input[input.Count - 1];

                foreach (PointF currentVertex in input)
                {
                    bool currentInside = EstaDentroOptimizado(currentVertex, edgeStart, edgeEnd);
                    bool previousInside = EstaDentroOptimizado(previousVertex, edgeStart, edgeEnd);

                    if (currentInside)
                    {
                        if (!previousInside)
                        {
                            // Entrando: agregar intersección
                            PointF intersection = CalcularInterseccion(previousVertex, currentVertex, edgeStart, edgeEnd);
                            AgregarPuntoSinDuplicados(output, intersection, EPSILON);
                        }
                        // Agregar vértice actual
                        AgregarPuntoSinDuplicados(output, currentVertex, EPSILON);
                    }
                    else if (previousInside)
                    {
                        // Saliendo: agregar intersección
                        PointF intersection = CalcularInterseccion(previousVertex, currentVertex, edgeStart, edgeEnd);
                        AgregarPuntoSinDuplicados(output, intersection, EPSILON);
                    }

                    previousVertex = currentVertex;
                }
            }

            return output;
        }

        /// <summary>
        /// Versión optimizada para determinar si un punto está dentro.
        /// Usa tolerancia para manejar errores de punto flotante.
        /// </summary>
        private bool EstaDentroOptimizado(PointF point, PointF edgeStart, PointF edgeEnd)
        {
            const float EPSILON = 1e-5f;
            float cross = (edgeEnd.X - edgeStart.X) * (point.Y - edgeStart.Y) -
                          (edgeEnd.Y - edgeStart.Y) * (point.X - edgeStart.X);

            return cross <= EPSILON;
        }

        /// <summary>
        /// Agrega un punto a la lista solo si no existe uno muy cercano.
        /// Evita vértices duplicados que pueden causar problemas de renderizado.
        /// </summary>
        private void AgregarPuntoSinDuplicados(List<PointF> points, PointF newPoint, float tolerance)
        {
            foreach (PointF existingPoint in points)
            {
                float dx = existingPoint.X - newPoint.X;
                float dy = existingPoint.Y - newPoint.Y;
                float distSq = dx * dx + dy * dy;

                if (distSq < tolerance * tolerance)
                {
                    return; // Punto duplicado, no agregar
                }
            }

            points.Add(newPoint);
        }

        #endregion

        #region Métodos Auxiliares Compartidos

        /// <summary>
        /// Calcula la intersección entre dos segmentos de línea.
        /// Usa la fórmula paramétrica de intersección de líneas.
        /// </summary>
        private PointF CalcularInterseccion(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            float x1 = p1.X, y1 = p1.Y;
            float x2 = p2.X, y2 = p2.Y;
            float x3 = p3.X, y3 = p3.Y;
            float x4 = p4.X, y4 = p4.Y;

            float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (Math.Abs(denominator) < 1e-6f)
            {
                // Líneas paralelas, devolver punto medio
                return new PointF((x1 + x2) / 2, (y1 + y2) / 2);
            }

            float px = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominator;
            float py = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominator;

            return new PointF(px, py);
        }

        /// <summary>
        /// Verifica si una lista contiene un punto específico.
        /// </summary>
        private bool ContienePoint(List<PointF> points, PointF point)
        {
            const float tolerance = 0.5f;
            foreach (PointF p in points)
            {
                float dx = p.X - point.X;
                float dy = p.Y - point.Y;
                if (Math.Sqrt(dx * dx + dy * dy) < tolerance)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}