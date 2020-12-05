using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progC
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("                                             Программа для решения уравнений                                           ");
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("");
            int a = 0, b = 0, c = 0, am = 0, bm = 0, cm = 0, D = 0;
            double x1, x2, g;
        g:
            Console.WriteLine("Введите a: ");
            try
            {
                a = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("введено недопестимое значение");
                Console.ReadKey();
                Environment.Exit(0);
            }
            am = Math.Abs(a);
            if (am > 10000)
            {
                Console.WriteLine("числа, по модулю, превосходящие 10000 не обрабатываются");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("Введите b: ");
            try
            {
                b = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("введено недопестимое значение");
                Console.ReadKey();
                Environment.Exit(0);
            }
            bm = Math.Abs(b);
            if (bm > 10000)
            {
                Console.WriteLine("числа, по модулю, превосходящие 10000 не обрабатываются");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("Введите c: ");
            try
            {
                c = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("введено недопестимое значение");
                Console.ReadKey();
                Environment.Exit(0);
            }
            cm = Math.Abs(c);
            if (cm > 10000)
            {
                Console.WriteLine("числа, по модулю, превосходящие 10000 не обрабатываются");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (a == 0)
            {
                if ((b == 0) && (c == 0))
                {
                    Console.WriteLine("x - любое число");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                if (b == 0)
                {
                    Console.WriteLine("уравнение не имеет корней");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.Write("линейное уравнение: ");
                Console.Write("{0:F0}", b);
                Console.Write("*x+");
                Console.Write("{0:F0}", c);
                Console.WriteLine("=0");
                g = b;
                x1 = (-c / g);
                Console.Write("корень: ");
                Console.WriteLine("{0:F3}", x1);
                Console.ReadKey();
            }
            else
            {
                Console.Write("квадратное уравнение: ");
                Console.Write("{0:F0}", a);
                Console.Write("*x^2+");
                Console.Write("{0:F0}", b);
                Console.Write("*x+");
                Console.Write("{0:F0}", c);
                Console.WriteLine("=0");
                D = b * b - 4 * a * c;
                if (D > 0 || D == 0)
                {
                    g = b;
                    x1 = (-g + Math.Sqrt(D)) / (2 * a);
                    x2 = (-g - Math.Sqrt(D)) / (2 * a);
                    if (x1 == x2)
                    {
                        Console.Write("корень: ");
                        Console.WriteLine("{0:F3}", x1);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    Console.Write("Первый корень: ");
                    Console.WriteLine("{0:F3}", x1);
                    Console.Write("Второй корень: ");
                    Console.WriteLine("{0:F3}", x2);

                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Действительных корней нет");
                    Console.ReadKey();
                    goto g;

                }

            }

        }
    }

}
