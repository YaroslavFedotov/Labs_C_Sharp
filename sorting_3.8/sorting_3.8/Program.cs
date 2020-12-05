using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public static class GLOBAL
    {
        public const int nachznach = -321;
        public static int Koll { get; set; }
        public static int MIN { get; set; }
        public static int MAX { get; set; }
        public static int PRV { get; set; }
        public static int PRVVOZ { get; set; }
        public static int[] Mass = new int[50];
        public static int[] MassOld = new int[50];
    }
    class Program
    {
        public static void NEDOP()
        {
            Console.WriteLine("");
            Console.WriteLine("введено недопустимое значение");
            Console.WriteLine("");
            Console.ReadKey();
            return;
        }
        public static void NECHIS()
        {
            Console.WriteLine("");
            Console.WriteLine("введено не число");
            Console.WriteLine("");
            Console.ReadKey();
            return;
        }
        public static void ARROUT()
        {
            Console.WriteLine("");
            for (int i = 0; i < GLOBAL.Koll; i++)
            {
                Console.Write("{0, 4}", GLOBAL.Mass[i]);
            }
            Console.WriteLine("");
            return;
        }
        public static void MASSOLD()
        {
            for (int i = 0; i < GLOBAL.Koll; i++)
            {
                GLOBAL.MassOld[i] = GLOBAL.Mass[i];
            }
            return;
        }
        public static void MASSZERO()
        {
            for (int i = 0; i < GLOBAL.Koll; i++)
            {
                GLOBAL.MassOld[i] = 0;
            }
            return;
        }
        public static void F1()
        {
            GLOBAL.Koll = 0;
            GLOBAL.MIN = 0;
            GLOBAL.MAX = 0;
            if (GLOBAL.PRV == 1)
            {
                GLOBAL.PRV--;
            }
            const int MINDOP = -100, MAXDOP = 100;
            Console.WriteLine("");
            Console.Write("Введите количество элементов массива (1..50): ");
            try
            {
                GLOBAL.Koll = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((GLOBAL.Koll < 1) || (GLOBAL.Koll > 50))
            {
                NEDOP();
                return;
            }
            Console.WriteLine("");
            Console.Write("Введите наименьшее возможное значение (-100 .. 100): ");
            try
            {
                GLOBAL.MIN = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((GLOBAL.MIN < MINDOP) || (GLOBAL.MIN > MAXDOP))
            {
                NEDOP();
                return;
            }
            Console.WriteLine("");
            Console.Write("Введите наибольшее возможное значение (-100 .. 100): ");
            try
            {
                GLOBAL.MAX = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((GLOBAL.MAX < MINDOP) || (GLOBAL.MIN > MAXDOP) || (GLOBAL.MIN > GLOBAL.MAX))
            {
                if (GLOBAL.MIN > GLOBAL.MAX)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Минимальное значение превышает максимальное");
                    Console.WriteLine("");
                    return;
                }
                NEDOP();
                return;
            }
            Random rand = new Random();
            GLOBAL.MAX++;
            Console.WriteLine("");
            for (int i = 0; i < GLOBAL.Koll; ++i)
            {
                GLOBAL.Mass[i] = rand.Next(GLOBAL.MIN, GLOBAL.MAX);
                Console.Write(GLOBAL.Mass[i] + " ");
            }
            GLOBAL.PRV++;
            Console.WriteLine("");
            Console.WriteLine("");
        }
        public static void F2()
        {
            int nap = 0;
            if (GLOBAL.PRV == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Массив не задан");
                Console.WriteLine("Воспользуйтесь 1-вой операцией для заполненя массива");
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            Console.Write("Укажите направление сортировки (0 — убыв., 1 — возр.): ");
            try
            {
                nap = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((nap > 1) || (nap < 0))
            {
                NEDOP();
                return;
            }
            int M = GLOBAL.nachznach;
            for (int i = 0; i < GLOBAL.Koll; i++)
            {
                if (GLOBAL.MassOld.SequenceEqual(GLOBAL.Mass))
                {
                    Console.WriteLine("Mассив отсортирован");
                    Console.WriteLine("");
                    MASSZERO();
                    return;
                }
                ARROUT();
                MASSOLD();
                for (int j = i + 1; j < GLOBAL.Koll; j++)
                {
                    if (nap == 1)
                    {
                        if (GLOBAL.Mass[i] > GLOBAL.Mass[j])
                        {
                            M = GLOBAL.Mass[i];
                            GLOBAL.Mass[i] = GLOBAL.Mass[j];
                            GLOBAL.Mass[j] = M;
                        }
                    }
                    if (nap == 0)
                    {
                        if (GLOBAL.Mass[i] < GLOBAL.Mass[j])
                        {
                            M = GLOBAL.Mass[i];
                            GLOBAL.Mass[i] = GLOBAL.Mass[j];
                            GLOBAL.Mass[j] = M;
                        }
                    }
                }
            }
            if (nap == 1)
            {
                GLOBAL.PRVVOZ = 1;
            }
            if (nap == 0)
            {
                GLOBAL.PRVVOZ = 0;
            }
            Console.WriteLine("Mассив отсортирован");
            Console.WriteLine("");
            MASSZERO();
            return;
        }
        public static void F3()
        {
            int nap = 0;
            if (GLOBAL.PRV == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Массив не задан");
                Console.WriteLine("Воспользуйтесь 1-вой операцией для заполненя массива");
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            Console.Write("Укажите направление сортировки (0 — убыв., 1 — возр.): ");
            try
            {
                nap = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((nap > 1) || (nap < 0))
            {
                NEDOP();
                return;
            }
            int n = GLOBAL.Koll;
            for (int g = 0; g < n; ++g)
            {
                Console.Write(GLOBAL.Mass[g] + " ");
            }
            Console.WriteLine();
            if (nap == 1)
            {
                GLOBAL.PRVVOZ = 1;
                for (int i = 0; i < n - 1; i++)
                {
                    int iMin = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        iMin = GLOBAL.Mass[j] < GLOBAL.Mass[iMin] ? j : iMin;
                    }
                    int T = GLOBAL.Mass[i];
                    GLOBAL.Mass[i] = GLOBAL.Mass[iMin];
                    GLOBAL.Mass[iMin] = T;
                    ARROUT();
                }
            }
            else
            {
                for (int i = 0; i < n - 1; i++)
                {
                    int iMin = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        iMin = GLOBAL.Mass[j] > GLOBAL.Mass[iMin] ? j : iMin;
                    }
                    int T = GLOBAL.Mass[i];
                    GLOBAL.Mass[i] = GLOBAL.Mass[iMin];
                    GLOBAL.Mass[iMin] = T;
                    ARROUT();
                }

            }
        }
        public static void F4()
        {
            int nap = 0;
            if (GLOBAL.PRV == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Массив не задан");
                Console.WriteLine("Воспользуйтесь 1-вой операцией для заполненя массива");
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            Console.Write("Укажите направление сортировки (0 — убыв., 1 — возр.): ");
            try
            {
                nap = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((nap > 1) || (nap < 0))
            {
                NEDOP();
                return;
            }
            ARROUT();
            if (nap == 1)
            {
                GLOBAL.PRVVOZ = 1;
                for (int i = 1; i < GLOBAL.Koll; i++)
                {
                    int j = i;
                    while (GLOBAL.Mass[j] < GLOBAL.Mass[j - 1])
                    {
                        int T = GLOBAL.Mass[j];
                        GLOBAL.Mass[j] = GLOBAL.Mass[j - 1];
                        GLOBAL.Mass[j - 1] = T;
                        j--;
                        if (j == 0)
                            break;
                    }
                    ARROUT();
                }
            }
            else
            {
                for (int i = 1; i < GLOBAL.Koll; i++)
                {
                    int j = i;
                    while (GLOBAL.Mass[j] > GLOBAL.Mass[j - 1])
                    {
                        int T = GLOBAL.Mass[j];
                        GLOBAL.Mass[j] = GLOBAL.Mass[j - 1];
                        GLOBAL.Mass[j - 1] = T;
                        j--;
                        if (j == 0)
                            break;
                    }
                    ARROUT();
                }
            }
            Console.WriteLine("");
            return;
        }
        public static void F5()
        {
            int nap = 0;
            if (GLOBAL.PRV == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Массив не задан");
                Console.WriteLine("Воспользуйтесь 1-вой операцией для заполненя массива");
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            Console.Write("Укажите направление сортировки (0 — убыв., 1 — возр.): ");
            try
            {
                nap = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((nap > 1) || (nap < 0))
            {
                NEDOP();
                return;
            }
            Console.WriteLine("");
            Console.WriteLine("Массив до сортировки");
            ARROUT();
            Console.WriteLine("");
            if (nap == 1)
            {
                GLOBAL.PRVVOZ = 1;
                int[] S = new int[201];
                for (int i = 0; i < GLOBAL.Koll; i++)
                    S[GLOBAL.Mass[i] + 100]++;
                int j = 0;
                for (int i = -100; i < 101; i++)
                    for (int d = 0; d < S[i + 100]; d++)
                        GLOBAL.Mass[j++] = i;
            }
            else
            {
                int[] S = new int[201];
                for (int i = 0; i < GLOBAL.Koll; i++)
                    S[GLOBAL.Mass[i] + 100]++;
                int j = 0;
                for (int i = 199; i > 0; i--)
                    for (int d = 0; d < S[i]; d++)
                        GLOBAL.Mass[j++] = i - 100;
            }
            Console.WriteLine("После");
            ARROUT();
            Console.WriteLine("");
        }
        public static void F6()
        {
            int kl = 0;
            for (int i = 0; i < GLOBAL.Koll; i++)
            {
                int j = 0;
                int k = 1;
                if (GLOBAL.Mass[j] > GLOBAL.Mass[k])
                {
                    kl++;
                }
                k++;
                j++;
            }
            if (kl == 0)
            {
                GLOBAL.PRVVOZ = 1;
            }
            if ((GLOBAL.PRVVOZ == 0) || (GLOBAL.PRV == 0))
            {
                Console.WriteLine("");
                Console.WriteLine("Массив не отсортирован по возрастанию");
                Console.WriteLine("");
                Console.WriteLine("");
                return;
            }
            int na = 0;
            Console.WriteLine("");
            Console.WriteLine("Введите искомое значение: ");
            Console.WriteLine("");
            try
            {
                na = int.Parse((Console.ReadLine()));
            }
            catch (Exception)
            {
                NECHIS();
                return;
            }
            if ((na > 1000) || (na < -1000))
            {
                NEDOP();
                return;
            }
            Console.WriteLine("");
            ARROUT();
            int L = 0;
            int R = GLOBAL.Koll - 1;
            int Mid = (L + R) / 2;
            bool Found = false;
            while (L <= R)
            {
                Mid = (L + R) / 2;
                if (na < GLOBAL.Mass[Mid])
                    R = Mid - 1;
                if (na > GLOBAL.Mass[Mid])
                    L = Mid + 1;
                if (na == GLOBAL.Mass[Mid])
                {
                    Found = true;
                    break;
                }
            }
            Mid++;
            if (Found)
            {
                Console.WriteLine("");
                Console.WriteLine("значение " + na + " имеет элемент индексом: " + Mid);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("значение " + na + " Не найдено");
                Console.WriteLine("");
            }
            return;
        }
        static void Main()
        {
            for (; ; )
            {
                int p = 0;
                int c = 0;
                Console.WriteLine("ОПЕРАЦИИ: ");
                Console.WriteLine("1. Заполнение массива");
                Console.WriteLine("2. Пузырьковая сортировка");
                Console.WriteLine("3. Сортировка выбором");
                Console.WriteLine("4. Сортировка вставками");
                Console.WriteLine("5. Сортировка подсчётом");
                Console.WriteLine("6. Двоичный поиск значения");
                Console.WriteLine("7. Выход из программы");
                Console.WriteLine("Введите номер расчета(1..7): ");
                Console.WriteLine("");
                try
                {
                    c = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    NEDOP();
                    p++;
                }
                if (p == 0)
                {
                    if ((c < 1) || (c > 7))

                    {
                        NEDOP();
                    }
                }

                switch (c)
                {
                    case 1:
                        F1();
                        break;
                    case 2:
                        F2();
                        break;
                    case 3:
                        F3();
                        break;
                    case 4:
                        F4();
                        break;
                    case 5:
                        F5();
                        break;
                    case 6:
                        F6();
                        break;
                    case 7:
                        Console.WriteLine("Выход");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}