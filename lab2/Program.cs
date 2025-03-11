using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace lab2
{
    class Program
    {
#pragma warning disable CS8618, CS8600, CS8604

        static int N = 3;

        static Matrix A, A1, A2, B2, Y3, C2;

        static Matrix b, b1, c1, y1, y2;

        static Matrix v3;
        static Matrix Y3_pow2;
        static Matrix v1;
        static Matrix v2;

        static Matrix finalValue;

        static Random rnd = new Random();

        static void InitializeMatriсes()
        {
            A = new Matrix(N, N);
            A1 = new Matrix(N, N);
            A2 = new Matrix(N, N);
            B2 = new Matrix(N, N);
            Y3 = new Matrix(N, N);
            C2 = new Matrix(N, N);

            b = new Matrix(N, 1);
            b1 = new Matrix(N, 1);
            c1 = new Matrix(N, 1);
            y1 = new Matrix(N, 1);
            y2 = new Matrix(N, 1);

            v3 = new Matrix(N, 1);
            Y3_pow2 = new Matrix(N, N);

            v1 = new Matrix(N, 1);
            v2 = new Matrix(N, 1);

            finalValue = new Matrix(N, 1);
        }

        static void GenerateMatrices()
        {
            RandomMatrix(A);
            RandomMatrix(A1);
            RandomMatrix(A2);
            RandomMatrix(B2);
            RandomMatrix(b1);
            RandomMatrix(c1);
        }

        static void ReadMatrices()
        {
            ReadMatrix(A, "A");
            ReadMatrix(A1, "A1");
            ReadMatrix(A2, "A2");
            ReadMatrix(B2, "B2");
            ReadMatrix(b1, "b1");
            ReadMatrix(c1, "c1");
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

        public static void FindB(Matrix matrix)
        {
            int N = matrix.matrix.GetLength(0);

            if (N <= 1)
            {
                throw new ArgumentException("Matrix size N must be greater than 1");
            }

            for (int i = 1; i < N; i++)
            {
                float denominator = i * i * i * i;
                if (denominator == 0) // Safeguard against division by zero
                {
                    matrix.matrix[i - 1, 0] = 21 / Int32.MaxValue;
                }
                else
                {
                    matrix.matrix[i - 1, 0] = 21 / denominator;
                }
            }
        }


        public static void FindC2(Matrix matrix)
        {
            int N = matrix.matrix.GetLength(0);
            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < N; j++)
                {
                    //if (((i*i) - 2 * j) == 0){;}

                    matrix.matrix[i - 1, j - 1] = 21 / ((i * i) + 2 * j);
                }
            }
        }

        static void Main(string[] args)
        {
            Thread thr2, thr3, thr4, thr5, thr6, thr7;

            Console.Write("Enter N: ");
            string nInput = Console.ReadLine();
            if (nInput != null)
            {
                N = Int32.Parse(nInput);
            }

            InitializeMatriсes();

            Console.Write("Generate random values: y/n - ");
            string rndInput = Console.ReadLine();
            if (rndInput == "n")
            {
                thr2 = new Thread(() => { ReadMatrix(A, "A"); });
                thr3 = new Thread(() => { thr2.Join(); ReadMatrix(A1, "A1"); });
                thr4 = new Thread(() => { thr3.Join(); ReadMatrix(A2, "A2"); });
                thr5 = new Thread(() => { thr4.Join(); ReadMatrix(B2, "B2"); });
                thr6 = new Thread(() => { thr5.Join(); ReadMatrix(b1, "b1"); });
                thr7 = new Thread(() => { thr6.Join(); ReadMatrix(c1, "c1"); });
            }
            else if (rndInput == "y")
            {
                thr2 = new Thread(() => RandomMatrix(A));
                thr3 = new Thread(() => RandomMatrix(A1));
                thr4 = new Thread(() => RandomMatrix(A2));
                thr5 = new Thread(() => RandomMatrix(B2));
                thr6 = new Thread(() => RandomMatrix(b1));
                thr7 = new Thread(() => RandomMatrix(c1));
            }
            else
            {
                throw new Exception("Invalid input");
            }

            thr2.Start();
            thr3.Start();
            thr4.Start();
            thr5.Start();
            thr6.Start();
            thr7.Start();

            GenerateMatrices();

            DateTime startTime = DateTime.Now;
            Thread thr1 = new Thread(() => { FindB(b); });
            thr1.Start();

            Thread thr8 = new Thread(() => { FindC2(C2); });
            thr8.Start();

            Thread thr9 = new Thread(() => { thr1.Join(); thr2.Join(); y1 = A * b; });
            thr9.Start();

            Thread thr10 = new Thread(() => { thr3.Join(); thr6.Join(); thr7.Join(); y2 = A1 * (b1 + c1); });
            thr10.Start();

            Thread thr11 = new Thread(() => { thr4.Join(); thr5.Join(); thr8.Join(); Y3 = A2 * B2 - C2; });
            thr11.Start();

            Thread thr12 = new Thread(() => { thr9.Join(); thr10.Join(); thr11.Join(); v3 = Y3 * y1 * y2.Transpose() * y2; });
            thr12.Start();

            Thread thr13 = new Thread(() => { thr11.Join(); Y3_pow2 = Y3 * Y3; });
            thr13.Start();

            Thread thr14 = new Thread(() => { thr10.Join(); thr11.Join(); thr13.Join(); v1 = Y3_pow2 * y2; });
            thr14.Start();

            Thread thr15 = new Thread(() => { thr12.Join(); thr11.Join(); v2 = Y3 * y1; });
            thr15.Start();

            Thread thrFinal = new Thread(() => { thr12.Join(); thr14.Join(); thr15.Join(); finalValue = (v1 + v2 + v3).Transpose() * Y3_pow2; });
            thrFinal.Start();

            thrFinal.Join();

            DateTime finalTime = DateTime.Now;

            Console.WriteLine("Final");
            finalValue.PrintMatrix();
            Console.WriteLine($"Time: {(finalTime - startTime).TotalSeconds}");
        }
    }
}
