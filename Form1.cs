using System;
using System.Drawing;
using System.Windows.Forms;

namespace MandelbrotViewer
{
    public class Form1 : Form
    {
        private Bitmap? _bitmap;
        private const int maxIter = 1000;

        public Form1()
        {
            Text = "Mandelbrot Viewer";
            ClientSize = new Size(800, 600);
            DoubleBuffered = true;
            Resize += (s, e) => { GenerateBitmap(); Invalidate(); };
            GenerateBitmap();
        }

    private void GenerateBitmap()
    {
        if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
            return;

        _bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        double xmin = -2.5, xmax = 1.0;
        double ymin = -1.0, ymax = 1.0;

        for (int y = 0; y < _bitmap.Height; y++)
        {
            double cy = ymin + (y / (double)_bitmap.Height) * (ymax - ymin);
            for (int x = 0; x < _bitmap.Width; x++)
            {
                double cx = xmin + (x / (double)_bitmap.Width) * (xmax - xmin);
                double zx = 0.0, zy = 0.0;
                int iter = 0;
                while (zx * zx + zy * zy <= 4.0 && iter < maxIter)
                {
                    double xt = zx * zx - zy * zy + cx;
                    zy = 2.0 * zx * zy + cy;
                    zx = xt;
                    iter++;
                }

                Color color = iter >= maxIter
                    ? Color.Black
                    : Color.FromArgb(255 - iter * 255 / maxIter,
                                     255 - iter * 255 / maxIter,
                                     255);

                _bitmap.SetPixel(x, y, color);
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (_bitmap != null)
        {
            e.Graphics.DrawImage(_bitmap, Point.Empty);
        }
    }
}
}