using System;

namespace lab2
{
    public class Matrix
    {
        private static Random rnd = new Random();
        public float[,] matrix;
        public Matrix(int rows, int cols)
        {
            matrix = new float[rows, cols];
        }

        private float[,] MultiplyMatrixByVector(float[,] vector)
        {
            if (matrix.GetLength(1) != vector.GetLength(0))
            {
                throw new ArgumentException("Matrix columns must be equal to vector rows");
            }

            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            float[,] result = new float[m, 1];

            for (int i = 0; i < m; i++)
            {
                result[i, 0] = 0;
                for (int j = 0; j < n; j++)
                {
                    result[i, 0] += matrix[i, j] * vector[j, 0];
                }
            }

            return result;
        }

        public static float[,] operator *(Matrix m, float[,] vector)
        {
            return m.MultiplyMatrixByVector(vector);
        }

        private float[,] MultiplyMatrix(float[,] b)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            int p = b.GetLength(0);
            int q = b.GetLength(1);

            if (n != p)
            {
                throw new ArgumentException("Invalid matrix dimensions for multiplication");
            }

            float[,] result = new float[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        result[i, j] += matrix[i, k] * b[k, j];
                    }
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            float[,] resultMatrix = a.MultiplyMatrix(b.matrix);
            Matrix result = new Matrix(resultMatrix.GetLength(0), resultMatrix.GetLength(1));
            result.matrix = resultMatrix;
            return result;
        }

        private float[,] MultiplyMatrixAndNumber(float number)
        {
            float[,] result = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j] * number;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix a, float number)
        {
            float[,] resultMatrix = a.MultiplyMatrixAndNumber(number);
            Matrix result = new Matrix(resultMatrix.GetLength(0), resultMatrix.GetLength(1));
            result.matrix = resultMatrix;
            return result;
        }

        private float[,] AddMatrix(float[,] b)
        {
            float[,] result = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j] + b[i, j];
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            float[,] resultMatrix = a.AddMatrix(b.matrix);
            Matrix result = new Matrix(resultMatrix.GetLength(0), resultMatrix.GetLength(1));
            result.matrix = resultMatrix;
            return result;
        }

        private float[,] SubtractMatrix(float[,] b)
        {
            float[,] result = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j] - b[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            float[,] resultMatrix = a.SubtractMatrix(b.matrix);
            Matrix result = new Matrix(resultMatrix.GetLength(0), resultMatrix.GetLength(1));
            result.matrix = resultMatrix;
            return result;
        }

        public Matrix Transpose()
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            float[,] resultMatrix = new float[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    resultMatrix[j, i] = matrix[i, j];
                }
            }
            Matrix result = new Matrix(resultMatrix.GetLength(0), resultMatrix.GetLength(1));
            result.matrix = resultMatrix;
            return result;
        }
        public void PrintMatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }
    }
}
