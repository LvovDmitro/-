using System;

namespace Численные_методы_4_задание___итеарционные_методы
{
    class Program
    {
        static int n = 10;
        static double[] g = new double[n + 1];
        static double[] y = new double[n + 1];
        static double[] r = new double[n + 1];
        static double[] temp = new double[n + 1];
        static double[] f = new double[n + 1];
        static double[] a = new double[n + 1];
        static double[] alf = new double[n + 1];
        static double[] bet = new double[n + 1];
        static double[] y0 = new double[n + 1];
        static double eps = 0.000001;
        static double h = (double)1 / n;

        static void Solution()
        {
            alf[1] = 0; bet[1] = 0;
            for (int i = 1; i < n; i++)
            {
                alf[i + 1] = -a[i + 1] / (-(a[i + 1] + a[i] + g[i] * h * h) + a[i] * alf[i]);
                bet[i + 1] = (-a[i] * bet[i] - f[i] * h * h) / (-(a[i + 1] + a[i] + g[i] * h * h) + a[i] * alf[i]);
            }

            y0[n] = 0; y0[0] = 0;
            for (int i = n - 1; i > 0; i--)
            {
                y0[i] = y0[i + 1] * alf[i + 1] + bet[i + 1];
            }
        }
        static void Yakoby()
        {
            for (int i = 0; i < n + 1; i++)
            {
                y[i] = 0;
                temp[i] = 0;//начальное прибл
            }
            int k = 0;
            double max;
            do
            {
                for (int j = 0; j < n + 1; j++) temp[j] = y[j];
                max = -1;
                for (int i = 1; i < n; i++)
                {
                    y[i] = (a[i + 1] * temp[i + 1] + a[i] * temp[i - 1] + h * h * f[i]) / (a[i + 1] + a[i] + g[i] * h * h);
                    if (Math.Abs(y[i] - temp[i]) > max) max = Math.Abs(y[i] - temp[i]);
                }
                k++;
            }

            while (Math.Abs(max) > eps);
            Console.WriteLine("y[j] \t\t\t y0[j] \t\t\t |y[j] - y0[j]|");
            for (int j = 0; j < n + 1; j++)
                Console.WriteLine("{0}\t{1}\t{2}", y[j], y0[j], Math.Abs(y[j] - y0[j]));
            Console.WriteLine("Количество итераций {0}", k);
        }
        static void NRelax()
        {
            for (double w = 0.02; w < 1; w += 0.02)
            {
                for (int i = 0; i < n + 1; i++)
                {
                    y[i] = 0;
                    temp[i] = 0;//начальное прибл
                }
                int k = 0;
                double max;
                do
                {
                    for (int j = 0; j < n + 1; j++) temp[j] = y[j];
                    max = -1;
                    for (int i = 1; i < n; i++)
                    {
                        y[i] = (1 - w) * temp[i] + w * (a[i + 1] * temp[i + 1] + a[i] * y[i - 1] + h * h * f[i]) / (a[i + 1] + a[i] + g[i] * h * h);
                        if (Math.Abs(y[i] - temp[i]) > max) max = Math.Abs(y[i] - temp[i]);
                    }
                    k++;
                }
                while (Math.Abs(max) > eps);

                Console.WriteLine(w + " " + k);

            }
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("y[j] \t\t\t y0[j] \t\t\t |y[j] - y0[j]|");
            for (int j = 0; j < n + 1; j++) Console.WriteLine("{0}\t{1}\t{2}", y[j], y0[j], Math.Abs(y[j] - y0[j]));

        }

        static void NSpyska()
        {
            for (int i = 0; i < n + 1; i++)
            {
                y[i] = 0;
                temp[i] = 0;
            }
            int k = 0;
            double t1 = 0, t2 = 0;
            r[0] = 0;
            r[n] = 0;
            double max;
            do
            {
                t1 = 0; t2 = 0;
                for (int j = 0; j < n + 1; j++) temp[j] = y[j];
                for (int i = 1; i < n; i++)
                {
                    r[i] = (-a[i + 1] * temp[i + 1] - a[i] * temp[i - 1] - h * h * f[i] + (a[i + 1] + a[i] + g[i] * h * h) * temp[i]);
                    t1 += r[i] * r[i];
                }
                for (int i = 1; i < n; i++)
                    t2 += (-a[i + 1] * r[i + 1] - a[i] * r[i - 1] + (a[i + 1] + a[i] + g[i] * h * h) * r[i]) * r[i];
                max = -1;
                for (int i = 1; i < n; i++)
                {
                    y[i] = temp[i] - t1 * r[i] / t2;
                    if (Math.Abs(y[i] - temp[i]) > max) max = Math.Abs(y[i] - temp[i]);
                }
                k++;
            }
            while (Math.Abs(max) > eps);
            Console.WriteLine("y[j] \t\t\t y0[j] \t\t\t |y[j] - y0[j]|");
            for (int j = 0; j < n + 1; j++) Console.WriteLine("{0}\t{1}\t{2}", y[j], y0[j], Math.Abs(y[j] - y0[j]));
            Console.WriteLine("Количество итераций {0}", k);
        }
        public static double P(double x)
        {
            double rez = 1 + x;
            return rez;
        }
        public static double G(double x)
        {
            double rez = 1 + x;
            return rez;
        }
        public static double F(double x)
        {
            double rez = (Math.Pow(x, 5)) - (Math.Pow(x, 4)) - 17 * (Math.Pow(x, 3)) + 7 * (Math.Pow(x, 2)) + 8 * x - 2;
            return rez;
        }
        static void Main(string[] args)
        {
            

            for (int i = 0; i <= n; i++)
            {
                a[i] = P(i * h);
                g[i] = G(i * h);
                f[i] = F(i * h) * Math.Pow(h, 2);
            }
            Solution();
            Console.WriteLine("Якоби");
            Yakoby();

            Console.WriteLine("---------------------------------------------------------------------");

            Console.WriteLine("Нижней релаксации");
            NRelax();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Наискорейшего спуска");
            NSpyska();
        }
    }

}
