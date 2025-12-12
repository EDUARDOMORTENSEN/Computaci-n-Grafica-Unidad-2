using System;
using System.Drawing;

namespace Lineas
{
    public class CirculoAlgoritmo
    {
        // 1) Bresenham (tu versión original)
        public void DibujarCirculoBresenham(Graphics g, int cx, int cy, int r, int pixelSize, Color color)
        {
            int x = 0;
            int y = r;
            int p = 1 - r;

            using (Brush b = new SolidBrush(color))
            {
                while (x <= y)
                {
                    PintarPixel(g, cx + x, cy + y, pixelSize, b);
                    PintarPixel(g, cx + y, cy + x, pixelSize, b);
                    PintarPixel(g, cx - x, cy + y, pixelSize, b);
                    PintarPixel(g, cx - y, cy + x, pixelSize, b);
                    PintarPixel(g, cx + x, cy - y, pixelSize, b);
                    PintarPixel(g, cx + y, cy - x, pixelSize, b);
                    PintarPixel(g, cx - x, cy - y, pixelSize, b);
                    PintarPixel(g, cx - y, cy - x, pixelSize, b);

                    x++;
                    if (p < 0)
                    {
                        p += 2 * x + 1;
                    }
                    else
                    {
                        y--;
                        p += 2 * (x - y) + 1;
                    }
                }
            }
        }

        // 2) Algoritmo del punto medio (midpoint circle) - muy similar a Bresenham pero con otro cálculo
        public void DibujarCirculoPuntoMedio(Graphics g, int cx, int cy, int r, int pixelSize, Color color)
        {
            int x = r;
            int y = 0;
            int d = 1 - r;

            using (Brush b = new SolidBrush(color))
            {
                while (x >= y)
                {
                    // ocho octantes
                    PintarPixel(g, cx + x, cy + y, pixelSize, b);
                    PintarPixel(g, cx + y, cy + x, pixelSize, b);
                    PintarPixel(g, cx - y, cy + x, pixelSize, b);
                    PintarPixel(g, cx - x, cy + y, pixelSize, b);
                    PintarPixel(g, cx - x, cy - y, pixelSize, b);
                    PintarPixel(g, cx - y, cy - x, pixelSize, b);
                    PintarPixel(g, cx + y, cy - x, pixelSize, b);
                    PintarPixel(g, cx + x, cy - y, pixelSize, b);

                    y++;

                    if (d <= 0)
                    {
                        d = d + 2 * y + 1;
                    }
                    else
                    {
                        x--;
                        d = d + 2 * (y - x) + 1;
                    }
                }
            }
        }

        // 3) Paramétrico (uso de ángulos, útil para dibujar con antialiasing o con líneas continuas)
        public void DibujarCirculoParametrico(Graphics g, int cx, int cy, int r, int pixelSize, Color color)
        {
            // si pixelSize > 1, dibujamos con rectángulos; si =1, dibujamos puntos
            using (Brush b = new SolidBrush(color))
            {
                // recorrer ángulo 0..2pi con paso según radio para cubrir sin huecos
                double paso = 1.0 / r; // aproximación: paso pequeño para radios grandes
                if (paso <= 0) paso = 0.01;
                for (double theta = 0; theta < Math.PI * 2; theta += paso)
                {
                    int x = cx + (int)Math.Round(r * Math.Cos(theta));
                    int y = cy + (int)Math.Round(r * Math.Sin(theta));
                    PintarPixel(g, x, y, pixelSize, b);
                }
            }
        }

        private void PintarPixel(Graphics g, int x, int y, int size, Brush color)
        {
            g.FillRectangle(color, x, y, size, size);
        }
    }
}
