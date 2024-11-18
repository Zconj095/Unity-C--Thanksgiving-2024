using System;

public class Matrix
{
    public double[,] Data { get; private set; }

    // Constructor to create a matrix from a 2D array
    public Matrix(double[,] data)
    {
        Data = data;
    }

    // Get number of rows
    public int RowCount => Data.GetLength(0);

    // Get number of columns
    public int ColumnCount => Data.GetLength(1);

    // Matrix multiplication
    public static Matrix Multiply(Matrix m1, Matrix m2)
    {
        if (m1.ColumnCount != m2.RowCount)
        {
            throw new ArgumentException("Matrix dimensions do not match for multiplication.");
        }

        int rows = m1.RowCount;
        int cols = m2.ColumnCount;
        int common = m1.ColumnCount;

        double[,] result = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < common; k++)
                {
                    result[i, j] += m1.Data[i, k] * m2.Data[k, j];
                }
            }
        }

        return new Matrix(result);
    }

    // Transpose the matrix
    public Matrix Transpose()
    {
        int rows = RowCount;
        int cols = ColumnCount;
        double[,] result = new double[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = Data[i, j];
            }
        }

        return new Matrix(result);
    }

    // Print matrix to console (for debugging)
    public void Print()
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                Console.Write(Data[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Example usage
    public static void Main()
    {
        // Example matrix multiplication
        double[,] m1Data = {
            { 1, 2 },
            { 3, 4 }
        };

        double[,] m2Data = {
            { 5, 6 },
            { 7, 8 }
        };

        Matrix m1 = new Matrix(m1Data);
        Matrix m2 = new Matrix(m2Data);

        Matrix result = Matrix.Multiply(m1, m2);
        result.Print(); // Output the result of the matrix multiplication
    }
}
