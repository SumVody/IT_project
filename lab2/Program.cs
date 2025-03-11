using System;
using System.Threading;

namespace lab2
{
    class Program
    {
#pragma warning disable CS8618, CS8600, CS8604

        static int N = 3;

        static Matrix A, B, C;

        static Matrix b, y;

        static Matrix finalValue;

        static Random rnd = new Random();

        static void InitializeMatrices()
        {
            A = new Matrix(N, N);
            B = new Matrix(N, N);
            C = new Matrix(N, N);

            b = new Matrix(N, 1);
            y = new Matrix(N, 1);

            finalValue = new Matrix(N, 1);
        }

        static void GenerateMatrices()
        {
            RandomMatrix(A);
            RandomMatrix(B);
            RandomMatrix(C);
            RandomMatrix(b);
        }

        static void ReadMatrices()
        {
            ReadMatrix(A, "A");
            ReadMatrix(B, "B");
            ReadMatrix(C, "C");
            ReadMatrix(b, "b");
        }

        public static void RandomMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.matrix.GetLength(1); j++)
                {
                    matrix.matrix[i, j] = rnd.Next(1000) / 500f;
                }
            }
        }

        public static void ReadMatrix(Matrix matrix, string matrixName)
        {
            for (int i = 0; i < matrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrixName}[{i}][{j}]= ");
                    string input = Console.ReadLine();
                    matrix.matrix[i, j] = float.Parse(input);
                }
            }
        }

        public static void FindY(Matrix matrix)
        {
            int N = matrix.matrix.GetLength(0);

            for (int i = 0; i < N; i++)
            {
                matrix.matrix[i, 0] = 21 / ((i + 1) * (i + 1));
            }
        }

        static void Main(string[] args)
        {
            Thread thr2, thr3, thr4, thr5;

            Console.Write("Enter N: ");
            string nInput = Console.ReadLine();
            if (nInput != null)
            {
                N = Int32.Parse(nInput);
            }

            InitializeMatrices();

            Console.Write("Generate random values: y/n - ");
            string rndInput = Console.ReadLine();
            if (rndInput == "n")
            {
                thr2 = new Thread(() => { ReadMatrix(A, "A"); });
                thr3 = new Thread(() => { thr2.Join(); ReadMatrix(B, "B"); });
                thr4 = new Thread(() => { thr3.Join(); ReadMatrix(C, "C"); });
                thr5 = new Thread(() => { thr4.Join(); ReadMatrix(b, "b"); });
            }
            else if (rndInput == "y")
            {
                thr2 = new Thread(() => RandomMatrix(A));
                thr3 = new Thread(() => RandomMatrix(B));
                thr4 = new Thread(() => RandomMatrix(C));
                thr5 = new Thread(() => RandomMatrix(b));
            }
            else
            {
                throw new Exception("Invalid input");
            }

            thr2.Start();
            thr3.Start();
            thr4.Start();
            thr5.Start();

            GenerateMatrices();

            DateTime startTime = DateTime.Now;
            Thread thr1 = new Thread(() => { FindY(y); });
            thr1.Start();

            Thread thr6 = new Thread(() => { thr1.Join(); thr2.Join(); finalValue = A * b + B * y + C; });
            thr6.Start();

            thr6.Join();

            DateTime finalTime = DateTime.Now;

            Console.WriteLine("Final");
            finalValue.PrintMatrix();
            Console.WriteLine($"Time: {(finalTime - startTime).TotalSeconds}");
        }
    }
}
