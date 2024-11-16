using System;
using System.Collections.Generic;

public class QuantumChannel
{
    public List<double[,]> Data { get; set; }
    public int NumQubits { get; set; }
    public OpShape OpShape { get; set; }

    public QuantumChannel(object data, int numQubits = 0, OpShape opShape = null)
    {
        // Initialize quantum channel
        if (data is List<object> dataList)
        {
            Data = new List<double[,]>();
            foreach (var item in dataList)
            {
                if (item is double[,] matrix)
                {
                    Data.Add(matrix);
                }
                else
                {
                    throw new ArgumentException("Invalid data format.");
                }
            }
        }
        else if (data is double[,])
        {
            var matrix = (double[,])data;
            Data = new List<double[,]>{ matrix };
        }
        else
        {
            throw new ArgumentException("Invalid data type.");
        }

        this.NumQubits = numQubits;
        this.OpShape = opShape ?? new OpShape();
    }

    // Matrix operations
    private static double[,] Transpose(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] transposed = new double[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposed[j, i] = matrix[i, j];
            }
        }
        return transposed;
    }

    private static double[,] Multiply(double[,] A, double[,] B)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int rowsB = B.GetLength(0);
        int colsB = B.GetLength(1);

        if (colsA != rowsB)
        {
            throw new ArgumentException("Matrix dimensions must agree for multiplication.");
        }

        double[,] result = new double[rowsA, colsB];
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += A[i, k] * B[k, j];
                }
            }
        }
        return result;
    }

    // Matrix power
    public QuantumChannel Power(int n)
    {
        var superOp = this.ToSuperOp();
        var superOpData = superOp.Data[0]; // Just work with the first matrix
        var poweredData = MatrixPower(superOpData, n);
        return new QuantumChannel(poweredData, this.NumQubits, this.OpShape);
    }

    private static double[,] MatrixPower(double[,] matrix, int n)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (rows != cols)
        {
            throw new ArgumentException("Matrix must be square to compute power.");
        }

        var result = new double[rows, cols];
        for (int i = 0; i < rows; i++)
            result[i, i] = 1; // Identity matrix

        for (int i = 0; i < n; i++)
        {
            result = Multiply(result, matrix);
        }
        return result;
    }

    // Matrix addition
    public QuantumChannel Add(QuantumChannel other)
    {
        if (this.NumQubits != other.NumQubits)
        {
            throw new ArgumentException("Cannot add channels with different number of qubits.");
        }

        List<double[,]> resultData = new List<double[,]>();
        for (int i = 0; i < this.Data.Count; i++)
        {
            double[,] matrixA = this.Data[i];
            double[,] matrixB = other.Data[i];
            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            double[,] sumMatrix = new double[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    sumMatrix[r, c] = matrixA[r, c] + matrixB[r, c];
                }
            }

            resultData.Add(sumMatrix);
        }
        return new QuantumChannel(resultData, this.NumQubits, this.OpShape);
    }

    // Conjugate (complex conjugate handled manually if needed)
    public QuantumChannel Conjugate()
    {
        var conjugatedData = new List<double[,]>();
        foreach (var matrix in this.Data)
        {
            var conjugatedMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    conjugatedMatrix[i, j] = -matrix[i, j]; // Assuming real matrices for simplicity
                }
            }
            conjugatedData.Add(conjugatedMatrix);
        }
        return new QuantumChannel(conjugatedData, this.NumQubits, this.OpShape);
    }

    public QuantumChannel Transpose()
    {
        var transposedData = new List<double[,]>();
        foreach (var matrix in this.Data)
        {
            transposedData.Add(Transpose(matrix));
        }
        return new QuantumChannel(transposedData, this.NumQubits, this.OpShape);
    }

    // Adjoint (conjugate + transpose)
    public QuantumChannel Adjoint()
    {
        var adjointData = new List<double[,]>();
        foreach (var matrix in this.Data)
        {
            adjointData.Add(Transpose(matrix));
        }
        return new QuantumChannel(adjointData, this.NumQubits, this.OpShape);
    }

    public QuantumChannel Compose(QuantumChannel other, bool front = false)
    {
        if (this.NumQubits != other.NumQubits)
        {
            throw new ArgumentException("Cannot compose channels with different numbers of qubits.");
        }

        List<double[,]> composedData = new List<double[,]>();
        foreach (var matrixA in this.Data)
        {
            foreach (var matrixB in other.Data)
            {
                double[,] composedMatrix = front ? Multiply(matrixB, matrixA) : Multiply(matrixA, matrixB);
                composedData.Add(composedMatrix);
            }
        }
        return new QuantumChannel(composedData, this.NumQubits, this.OpShape);
    }

    // Helper methods for quantum channel operations
    public SuperOp ToSuperOp()
    {
        // Conversion of channel to SuperOp form
        return new SuperOp(this.Data, this.NumQubits, this.OpShape);
    }
}

