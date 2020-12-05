using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        enum Color { White, Gray, Black };
        const int INF = 1000000;
        static int N;
        static int[,] M;
        const int N_MAX = 20;
        const int LENGTH_MAX = 100;
        static void Main(string[] args)
        {


            DirectoryInfo Dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] Files = Dir.GetFiles("*.txt");
            if (Files.Length == 0)
            {
                Console.WriteLine("В каталоге приложения не обнаружено текстовых файлов.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("В каталоге приложения обнаружены текстовые файлы:");
            for (int i = 0; i < Files.Length; i++)
            {
                Console.WriteLine((i + 1) + ":" + Files[i].Name);
            }
            Console.WriteLine("Номер файла с описанием графа(1.." + Files.Length + "): ");
            int r = 0;
            try
            {
                r = int.Parse(Console.ReadLine());
                if (r > Files.Length)
                {
                    Console.WriteLine("ОШИБКА: Неверный номер файла.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ОШИБКА: Неверный номер файла.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            bool ReadOK = ReadGraph(Files[r - 1].Name);
            if (!ReadOK)
            {
                Console.WriteLine("граф не удалось считать");
                Console.ReadKey();
                return;
            }
            int c;
            while (true)
            {
                Console.WriteLine("Операции над графом " + Files[r - 1] + ":");
                Console.WriteLine("1. Вывод матрицы смежности.");
                Console.WriteLine("2.Вывод списка рёбер.");
                Console.WriteLine("3.Вывод списков смежности.");
                Console.WriteLine("4.Определение свойств графа.");
                Console.WriteLine("5. Матрица кратчайших расстояний (алгоритм Флойда - Уоршелла).");
                Console.WriteLine("6. Кратчайшее расстояние от вершины до остальных вершин (алгоритм Дейкстры).");
                Console.WriteLine("7. Минимум переходов от вершины до остальных вершин (поиск в ширину).");
                Console.WriteLine("8.Связность графа и определение циклов(поиск в глубину).");
                Console.WriteLine("9. Выход из программы.");
                Console.WriteLine("Введите номер действия (1 .. 9): ");
                try
                {
                    c = int.Parse(Console.ReadLine());


                }
                catch (Exception)
                {
                    Console.WriteLine("введено не число ");
                    continue;
                }
                if (c < 1 || c > 9)
                {
                    Console.WriteLine(" ОШИБКА: Неверный номер действия.");
                    Console.ReadKey();
                    continue;
                }

                switch (c)
                {
                    case 1:
                        PrintAdjacencyMatrix();
                        break;
                    case 2:
                        PrintEdgesList();
                        break;
                    case 3:
                        PrintAdjacencyLists();
                        break;
                    case 4:
                        PrintGraphProperties();
                        break;
                    case 5:
                        FloydWarshall();
                        break;
                    case 6:
                        Dijkstra();
                        break;
                    case 7:
                        BFS();
                        break;
                    case 8:
                        DFS();
                        break;
                    case 9:
                        return;
                }


            }
        }

        static bool ReadGraph(string FileName)
        {
            StreamReader F = new StreamReader(FileName);
            N = int.Parse(F.ReadLine());
            M = new int[N, N];
            string[] Buf;
            string s;

            try
            {
                if (N > N_MAX)
                {
                    Console.WriteLine("\nВнимание! Коэффициент матрицы не должен привышать 20");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                for (int i = 0; i < N; i++)
                {
                    s = F.ReadLine();
                    Buf = s.Split(' ');
                    for (int j = 0; j < N; j++)
                    {
                        if (N > N_MAX)
                        {
                            Console.WriteLine("\nВнимание! Коэффициент матрицы не должен привышать 6");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                        if (int.Parse(Buf[j]) == 0)
                            M[i, j] = INF;
                        else
                            M[i, j] = int.Parse(Buf[j]);
                        if (int.Parse(Buf[j]) > LENGTH_MAX)
                        {
                            Console.WriteLine("\nЭлемент матрицы не должен превышать 100");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }

                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        static void PrintAdjacencyMatrix()
        {
            Console.WriteLine("МАТРИЦА СМЕЖНОСТИ");
            PrintMatrix(M);
        }
        static void PrintEdgesList()
        {
            Console.WriteLine("СПИСОК РЁБЕР");
            List<int[]> Edges = new List<int[]>();
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                    if (M[i, j] < INF && i > j)
                        Edges.Add(new int[] { i, j, M[i, j] });
            Console.WriteLine("Вершины: A-{0}", Convert.ToChar('A' + N - 1));
            foreach (int[] E in Edges)
            {
                if (IsWeightedGraph() && !IsDirectedGraph())

                    Console.WriteLine("{0}-{1} ({2})", Convert.ToChar('A' + E[0]), Convert.ToChar('A' + E[1]), E[2]);
                else if (IsWeightedGraph() && IsDirectedGraph())
                    Console.WriteLine("{0}->{1} ({2})", Convert.ToChar('A' + E[0]), Convert.ToChar('A' + E[1]), E[2]);
                else if (!IsWeightedGraph() && !IsDirectedGraph())
                    Console.WriteLine("{0}-{1}", Convert.ToChar('A' + E[0]), Convert.ToChar('A' + E[1]), E[2]);
                else if (!IsWeightedGraph() && IsDirectedGraph())
                    Console.WriteLine("{0}->{1} ({2})", Convert.ToChar('A' + E[0]), Convert.ToChar('A' + E[1]), E[2]);
            }
        }
        static void PrintAdjacencyLists()
        {
            Console.WriteLine("СПИСКИ СМЕЖНОСТИ");
            List<int[]>[] AdjacencyLists = new List<int[]>[N];
            for (int i = 0; i < N; ++i)
            {
                AdjacencyLists[i] = new List<int[]>();
                for (int j = 0; j < N; ++j)
                    if (M[i, j] < INF)
                        AdjacencyLists[i].Add(new int[] { j, M[i, j] });
            }
            for (int i = 0; i < AdjacencyLists.Length; ++i)
            {
                Console.Write("{0}: ", Convert.ToChar('A' + i));
                foreach (int[] E in AdjacencyLists[i])
                    if (IsWeightedGraph())
                        Console.Write("{0}({1}) ", Convert.ToChar('A' + E[0]), E[1]);
                    else
                        Console.Write("{0}", Convert.ToChar('A' + E[0]), E[1]);
                Console.WriteLine();
            }

        }
        static void PrintGraphProperties()
        {
            Console.WriteLine("СВОЙСТВА ГРАФА");
            bool Loops = false;
            for (int i = 0; i < N; ++i)
                if (M[i, i] < INF)
                {
                    if (!Loops)
                    {
                        Console.Write("В графе есть петли: ");
                        Loops = true;
                    }
                    Console.Write("{0}({1}) ", Convert.ToChar('A' + i), M[i, i]);
                }
            if (Loops)
                Console.WriteLine();
            else
                Console.WriteLine("В графе нет петель.");
            if (IsDirectedGraph())
                Console.WriteLine("ориентированный");
            else
                Console.WriteLine(" не ориентированный");
            if (IsWeightedGraph())
            {
                Console.WriteLine("Граф взвешенный");
            }
            else
                Console.WriteLine("Граф не взвешенный");
        }
        static bool IsDirectedGraph()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (M[i, j] != M[j, i])
                    {
                        return true;

                    }
                }
            }



            return false;
        }
        static bool IsWeightedGraph()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (M[i, j] != 1 && M[i, j] != INF)
                        return true;
                }
            }
            return false;

        }
        static void FloydWarshall()
        {

            Console.WriteLine("МАТРИЦА КРАТЧАЙШИХ РАССТОЯНИЙ" + " (АЛГОРИТМ ФЛОЙДА - УОРШЕЛЛА)");
            int[,] R = new int[N, N];
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                    R[i, j] = i == j ? 0 : M[i, j];
            for (int k = 0; k < N; ++k)
                for (int i = 0; i < N; ++i)
                    for (int j = 0; j < N; ++j)
                        R[i, j] = Math.Min(R[i, j], R[i, k] + R[k, j]);
            PrintMatrix(R);

        }
        static void Dijkstra()
        {
            Console.WriteLine("КРАТЧАЙШЕЕ РАССТОЯНИЕ ОТ ВЕРШИНЫ" + " ДО ОСТАЛЬНЫХ ВЕРШИН (АЛГОРИТМ ДЕЙКСТРЫ)");
            int S = GetVertex();
            if (S == -1)
            {
                return;
            }
            int[] Distance = new int[N];
            bool[] Visited = new bool[N];
            for (int i = 0; i < N; ++i)
            {
                Distance[i] = INF;
                Visited[i] = false;
            }
            Distance[S] = 0;
            int MinD;
            do
            {
                MinD = INF;
                int MinV = -1;
                for (int i = 0; i < N; ++i)
                    if (Distance[i] < MinD && !Visited[i])
                    {
                        MinD = Distance[i];
                        MinV = i;

                    }

                if (MinV == -1)
                    break;
                //обратботка соседних непосещенных вершин 
                for (int i = 0; i < N; ++i)
                    if (M[MinV, i] < INF && !Visited[i])
                        Distance[i] = Math.Min(Distance[i],
                        Distance[MinV] + M[MinV, i]);
                Visited[MinV] = true;

            }
            while (MinD < INF);
            Console.WriteLine();
            Console.WriteLine("Кратчайшие расстояния до вершин:");
            PrintByVertices(Distance);
            Console.WriteLine();
            Console.WriteLine("Кратчайшие пути:");
            for (int i = 0; i < N; ++i)
                if (Distance[i] > 0 && Distance[i] < INF)
                {
                    int T = i;
                    string R = "";
                    while (T != S)
                    {
                        for (int j = 0; j < N; ++j)
                            if (M[j, T] < INF && Distance[j] == Distance[T] - M[j, T])
                            {
                                T = j;
                                R = Convert.ToChar('A' + T) + "-" + R;
                                break;
                            }
                    }
                    Console.Write(R);
                    Console.WriteLine("{0:C}", Convert.ToChar('A' + i));
                }

        }
        static void PrintMatrix(int[,] T)
        {
            Console.Write(" ");
            for (int i = 0; i < N; ++i)
            {
                Console.Write("{0,6}", Convert.ToChar('A' + i));
                Console.Write(" ");
            }

            Console.WriteLine();
            for (int i = 0; i < N; i++)
            {
                Console.Write("{0,6}", Convert.ToChar('A' + i));
                Console.Write("\t");
                for (int j = 0; j < N; ++j)
                {
                    if (T[i, j] == INF)
                        Console.Write("-");
                    else
                        Console.Write(T[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

        }
        static int GetVertex()
        {
            char V, MaxLetter = Convert.ToChar('A' + N - 1);
            Console.Write("Введите имя исходной вершины (A-{0:C}): ", MaxLetter);
            try
            {
                V = char.ToUpper(char.Parse(Console.ReadLine()));
                for (int i = 0; i < N; i++)
                    if (V == Convert.ToChar('A' + i))
                        return V - 'A';
                    else
                    if (V == Convert.ToChar('0' + i))
                    {

                        Console.WriteLine("ОШИБКА: Неверно указана вершина.");
                        Console.ReadKey();
                        return -1;
                    }
            }
            catch (Exception)
            {
                Console.WriteLine("ОШИБКА: Неверно указана вершина.");
                Console.ReadKey();

            }

            return -1;


        }
        static void PrintByVertices(int[] D)
        {
           Console.Write(" ");
            for (int i = 0; i < N; ++i)
            {
                Console.Write("{0,6}", Convert.ToChar('A' + i));
                Console.Write("");
            }
            Console.WriteLine();
            Console.Write("      ");
            for (int i = 0; i < N; ++i)
            {
                if (D[i] == INF)
                    Console.Write("-");
                else
                    Console.Write(D[i]);
                Console.Write("     ");
            }

        }
        static void BFS()
        {
            Console.WriteLine("МИНИМУМ ПЕРЕХОДОВ ОТ ВЕРШИНЫ" + " ДО ОСТАЛЬНЫХ ВЕРШИН (ПОИСК В ШИРИНУ)");
            int S = GetVertex();
            if (S == -1)
            {
                return;
            }
            int[] D = new int[N];
            for (int i = 0; i < N; ++i)
                D[i] = INF;
            Queue<int> Q = new Queue<int>();
            int T = S;
            D[T] = 0;
            Q.Enqueue(T);
            Console.Write("+" + Convert.ToChar('A' + T) + "(" + Convert.ToString(D[T]) + ") ");

            while (Q.Count > 0)
            {

                T = Q.Dequeue();

                Console.Write("-" + Convert.ToChar('A' + T) + "(" + Convert.ToString(D[T]) + ") ");




                for (int i = 0; i < N; ++i)
                    if (M[T, i] < INF && D[i] == INF)
                    {
                        D[i] = D[T] + 1;
                        Q.Enqueue(i);
                        Console.Write("+" + Convert.ToChar('A' + i) + "(" + Convert.ToString(D[i]) + ") ");

                    }
            }
            Console.WriteLine();
            Console.WriteLine("Минимум переходов:");
            PrintByVertices(D);
            Console.WriteLine();
            Console.WriteLine("Кратчайшие пути:");
            for (int i = 0; i < N; ++i)
                if (D[i] > 0 && D[i] < INF)
                {
                    int K = i;
                    string R = "";
                    while (K != S)
                    {
                        for (int j = 0; j < N; ++j)
                            if (D[j] == D[K] - 1)
                            {
                                K = j;
                                R = Convert.ToChar('A' + K) + "-" + R;
                                break;
                            }
                    }
                    Console.Write(R);
                    Console.WriteLine("{0:C}", Convert.ToChar('A' + i));
                }
        }
        static void DFS()
        {
            bool f = false;
            List<int> Cycle = new List<int>();
            Console.WriteLine("СВЯЗНОСТЬ ГРАФА И ОПРЕДЕЛЕНИЕ ЦИКЛОВ" + " (ПОИСК В ГЛУБИНУ)");
            bool Directed = IsDirectedGraph();
            if (Directed)
                Console.WriteLine("Граф ориентированный." + " Связность не определяется.");
            int[] Components = new int[N];
            Stack<int> GrayPath = new Stack<int>();
            Color[] Colors = new Color[N];
            for (int i = 0; i < N; ++i)
            {
                Components[i] = 0;
                Colors[i] = Color.White;
            }
            int ComponentsCount = 0;
            for (int i = 0; i < N; ++i)
                if (Components[i] == 0)
                {
                    ComponentsCount++;
                    //int Prev = -1;
                    GrayPath.Push(i);
                    while (GrayPath.Count > 0)
                    {
                        int V = GrayPath.Peek();
                        if (Colors[V] == Color.White)
                        {
                            Colors[V] = Color.Gray;
                            Console.Write("(" + Convert.ToChar('A' + V) + " ");
                            Components[V] = ComponentsCount;
                        }
                        bool FoundWhite = false;
                        for (int j = 0; j < N; ++j)
                        {
                            if (M[V, j] < INF && Colors[j] == Color.Gray)
                            {
                                GrayPath.Pop();
                                int Prev = GrayPath.Count == 0 ? -1 : GrayPath.Peek();
                                GrayPath.Push(V);
                                if (Directed || !Directed && j != Prev)// && !f)
                                {
                                    Cycle.Clear();
                                    while (j != GrayPath.Peek())
                                        Cycle.Insert(0, GrayPath.Pop());
                                    foreach (int U in Cycle)
                                        GrayPath.Push(U);
                                    Cycle.Insert(0, j);
                                    //f = true;
                                }
                            }
                                if ((M[V, j] < INF) && (Colors[j] == Color.White))
                                {
                                    FoundWhite = true;
                                    // Prev = GrayPath.Peek();
                                    GrayPath.Push(j);

                                    break;
                                }
                            
                        }
                        if (!FoundWhite)
                        {
                            Console.Write(Convert.ToChar('A' + V) + ") ");
                            Colors[V] = Color.Black;
                            GrayPath.Pop();
                        }
                    }

                }
            Console.WriteLine();
            if (!Directed)
                if (ComponentsCount == 1)
                    Console.WriteLine("Граф связный.");
                else
                {
                    Console.WriteLine("Граф несвязный. Количество компонент: {0:D}", ComponentsCount);
                   
                    Console.WriteLine("Принадлежность к компонентам связности:");
                    for (int i = 1; i <= ComponentsCount; ++i)
                    {
                        Console.Write("{0:D}: ", i);
                        for (int j = 0; j < N; ++j)
                            if (Components[j] == i)
                                Console.Write("{0} ", Convert.ToChar('A' + j));
                        Console.WriteLine();
                    }
                }
            if (Cycle.Count == 0)
                Console.WriteLine("В графе нет циклов.");
            else
            {
                Console.Write("В графе есть цикл: ");
                foreach (int V in Cycle)
                    Console.Write("{0} ", Convert.ToChar('A' + V));
                Console.WriteLine();
            }
          

        }
        }
    }

