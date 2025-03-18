using Xunit;
using System.Collections.Generic;

class TestMatrix
{
    [Fact]
    public void TestMatrixMultiplication()
    {
        Matrix m1 = new Matrix(2, 3);
        Matrix m2 = new Matrix(3, 2);
        m1.matrix = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
        m2.matrix = new float[,] { { 7, 8 }, { 9, 10 }, { 11, 12 } };
        float[,] result = m1 * m2.matrix;
        Assert.Equal(new float[,] { { 58, 64 }, { 139, 154 } }, result);
    }

    [Fact]
    public void TestMatrixVectorMultiplication()
    {
        Matrix m = new Matrix(2, 3);
        m.matrix = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
        float[,] vector = new float[,] { { 7 }, { 8 }, { 9 } };
        float[,] result = m * vector;
        Assert.Equal(new float[,] { { 50 }, { 122 } }, result);
    }
}