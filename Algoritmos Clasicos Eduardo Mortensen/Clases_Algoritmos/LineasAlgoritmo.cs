using System;
using System.Drawing;

namespace LineDrawingAlgorithms
{
    public static class LineasAlgoritmo
    {
        // ================================================================
        //  ALGORITMO DDA (Digital Differential Analyzer)
        //  - Usa incrementos fraccionarios.
        //  - Produce líneas suaves porque usa punto flotante.
        //  - Adecuado para explicaciones iniciales.
        // ================================================================
        public static void DrawDDA(Graphics g, Pen p, float x0, float y0, float x1, float y1)
        {
            // Diferencias en X y Y
            float dx = x1 - x0;
            float dy = y1 - y0;

            // Se determina cuántos pasos se necesitan
            float steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            if (steps == 0)
            {
                // Caso especial: un solo punto
                g.DrawRectangle(p, x0, y0, 1, 1);
                return;
            }

            // Incrementos por paso
            float xInc = dx / steps;
            float yInc = dy / steps;

            float x = x0;
            float y = y0;

            // Trazado de los pixeles
            for (int i = 0; i <= steps; i++)
            {
                g.DrawRectangle(p, (int)Math.Round(x), (int)Math.Round(y), 1, 1);
                x += xInc;
                y += yInc;
            }
        }

        // ================================================================
        //  ALGORITMO DE BRESENHAM
        //  - Usa solamente enteros.
        //  - Es eficiente y rápido (sin decimales).
        //  - Funciona para cualquier pendiente.
        // ================================================================
        public static void DrawBresenham(Graphics g, Pen p, int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);

            int sx = x0 < x1 ? 1 : -1; // dirección en X
            int sy = y0 < y1 ? 1 : -1; // dirección en Y

            int err = dx - dy; // error inicial

            while (true)
            {
                g.DrawRectangle(p, x0, y0, 1, 1);

                // Si ya llegamos al punto final, terminamos
                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;

                // Ajuste horizontal
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                // Ajuste vertical
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        // ================================================================
        //  ALGORITMO MIDPOINT (Punto Medio)
        //  - Usa un criterio geométrico para decidir el siguiente pixel.
        //  - Más simple que Bresenham pero similar precisión.
        //  - Debe implementarse correctamente para pendientes negativas.
        // ================================================================
        public static void DrawMidPoint(Graphics g, Pen p, int x0, int y0, int x1, int y1)
        {
            // Normalizar para que siempre dibuje de izquierda a derecha
            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            int dx = x1 - x0;
            int dy = y1 - y0;

            int sx = dy >= 0 ? 1 : -1; // dirección vertical
            dy = Math.Abs(dy);

            int d = 2 * dy - dx;   // decisión inicial
            int incE = 2 * dy;     // mover Este
            int incNE = 2 * (dy - dx); // mover Noreste

            int x = x0;
            int y = y0;

            while (x <= x1)
            {
                g.DrawRectangle(p, x, y, 1, 1);

                if (d <= 0)
                {
                    d += incE;
                }
                else
                {
                    d += incNE;
                    y += sx;
                }

                x++;
            }
        }
    }
}
