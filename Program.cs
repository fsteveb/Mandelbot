using System;

class Program
{
    static void Main()
    {
        int width = 120;
        int height = 40;
        double xmin = -2.5, xmax = 1.0;
        double ymin = -1.0, ymax = 1.0;
        int maxIter = 1000;

        for (int y = 0; y < height; y++)
        {
            double cy = ymin + (y / (double)height) * (ymax - ymin);
            for (int x = 0; x < width; x++)
            {
                double cx = xmin + (x / (double)width) * (xmax - xmin);
                double zx = 0.0, zy = 0.0;
                int iter = 0;
                while (zx * zx + zy * zy <= 4.0 && iter < maxIter)
                {
                    double xt = zx * zx - zy * zy + cx;
                    zy = 2.0 * zx * zy + cy;
                    zx = xt;
                    iter++;
                }

                char c = MapChar(iter, maxIter);
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }

    static char MapChar(int iter, int max)
    {
        if (iter >= max) return ' ';
        string ramp = "@%#*+=-:. ";
        int idx = (int)(iter / (double)max * (ramp.Length - 1));
        return ramp[idx];
    }
}
