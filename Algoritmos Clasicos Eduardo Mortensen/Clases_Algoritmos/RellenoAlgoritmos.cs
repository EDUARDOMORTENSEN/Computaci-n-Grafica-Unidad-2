using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lineas
{
    public static class RellenoAlgoritmos
    {
        // ================================================================
        //  VARIANTE 1: FLOOD FILL BFS (COLA)
        //  - Recorre por amplitud
        //  - Muy estable, ideal para formas cerradas
        //  - Más lento que Scanline pero fácil de entender
        // ================================================================
        public static void FloodFillBFS(Bitmap bmp, int x, int y, Color nuevoColor)
        {
            if (!Dentro(bmp, x, y)) return;

            Color original = bmp.GetPixel(x, y);
            if (original.ToArgb() == nuevoColor.ToArgb()) return;

            Queue<Point> cola = new Queue<Point>();
            cola.Enqueue(new Point(x, y));

            while (cola.Count > 0)
            {
                Point p = cola.Dequeue();
                int px = p.X;
                int py = p.Y;

                if (!Dentro(bmp, px, py)) continue;

                if (bmp.GetPixel(px, py).ToArgb() != original.ToArgb())
                    continue;

                bmp.SetPixel(px, py, nuevoColor);

                cola.Enqueue(new Point(px + 1, py));
                cola.Enqueue(new Point(px - 1, py));
                cola.Enqueue(new Point(px, py + 1));
                cola.Enqueue(new Point(px, py - 1));
            }
        }

        // ================================================================
        //  VARIANTE 2: FLOOD FILL DFS ITERATIVO (PILA)
        //  - Evita StackOverflow (el recursivo falla con imágenes grandes)
        //  - Explora en profundidad
        //  - Rellena áreas grandes más rápido que BFS
        // ================================================================
        public static void FloodFillDFS(Bitmap bmp, int x, int y, Color nuevoColor)
        {
            if (!Dentro(bmp, x, y)) return;

            Color original = bmp.GetPixel(x, y);
            if (original.ToArgb() == nuevoColor.ToArgb()) return;

            Stack<Point> pila = new Stack<Point>();
            pila.Push(new Point(x, y));

            while (pila.Count > 0)
            {
                Point p = pila.Pop();
                int px = p.X;
                int py = p.Y;

                if (!Dentro(bmp, px, py)) continue;

                if (bmp.GetPixel(px, py).ToArgb() != original.ToArgb())
                    continue;

                bmp.SetPixel(px, py, nuevoColor);

                pila.Push(new Point(px + 1, py));
                pila.Push(new Point(px - 1, py));
                pila.Push(new Point(px, py + 1));
                pila.Push(new Point(px, py - 1));
            }
        }

        // ================================================================
        //  VARIANTE 3: FLOOD FILL SCANLINE (LA MÁS EFICIENTE)
        //  - Rellena por líneas horizontales completas
        //  - Mucho más veloz que BFS y DFS
        //  - Ideal para áreas muy grandes
        // ================================================================
        public static void FloodFillScanline(Bitmap bmp, int x, int y, Color nuevoColor)
        {
            if (!Dentro(bmp, x, y)) return;

            Color original = bmp.GetPixel(x, y);
            if (original.ToArgb() == nuevoColor.ToArgb()) return;

            Stack<Point> pila = new Stack<Point>();
            pila.Push(new Point(x, y));

            while (pila.Count > 0)
            {
                Point p = pila.Pop();
                int px = p.X;
                int py = p.Y;

                if (!Dentro(bmp, px, py)) continue;

                // Buscar izquierda
                int left = px;
                while (left >= 0 && bmp.GetPixel(left, py).ToArgb() == original.ToArgb())
                {
                    bmp.SetPixel(left, py, nuevoColor);
                    left--;
                }
                left++;

                // Buscar derecha
                int right = px;
                while (right < bmp.Width && bmp.GetPixel(right, py).ToArgb() == original.ToArgb())
                {
                    bmp.SetPixel(right, py, nuevoColor);
                    right++;
                }
                right--;

                // Revisar líneas superior e inferior
                for (int i = left; i <= right; i++)
                {
                    if (py > 0 &&
                        bmp.GetPixel(i, py - 1).ToArgb() == original.ToArgb())
                        pila.Push(new Point(i, py - 1));

                    if (py < bmp.Height - 1 &&
                        bmp.GetPixel(i, py + 1).ToArgb() == original.ToArgb())
                        pila.Push(new Point(i, py + 1));
                }
            }
        }

        // ================================================================
        //  Función auxiliar: verifica si el punto está dentro del bitmap
        // ================================================================
        private static bool Dentro(Bitmap bmp, int x, int y)
        {
            return x >= 0 && y >= 0 && x < bmp.Width && y < bmp.Height;
        }
    }
}
