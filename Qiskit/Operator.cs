using System;
using System.Collections.Generic;
using System.Linq;

public class Operator
{
    public double[,] Data { get; private set; }
    public int InputDimensions { get; private set; }
    public int OutputDimensions { get; private set; }

    // Constructor to initialize Operator from a matrix
    public Operator(double[,] data)
    {
        Data = data;
        InputDimensions = data.GetLength(1); // columns of matrix
        OutputDimensions = data.GetLength(0); // rows of matrix
    }

    // Method to multiply this operator with a state vector (Matrix-vector multiplication)
    public double[] ApplyToState(double[] state)
    {
        if (state.Length != InputDimensions)
        {
            throw new ArgumentException("State vector size does not match input dimensions of operator.");
        }

        var result = new double[OutputDimensions];
        for (int i = 0; i < OutputDimensions; i++)
        {
            result[i] = 0;
            for (int j = 0; j < InputDimensions; j++)
            {
                result[i] += Data[i, j] * state[j];
            }
        }
        return result;
    }

    // Method to apply this operator to a density matrix (Left and right multiplication)
    public double[,] ApplyToDensityMatrix(double[,] densityMatrix)
    {
        if (densityMatrix.GetLength(0) != InputDimensions || densityMatrix.GetLength(1) != InputDimensions)
        {
            throw new ArgumentException("Density matrix size does not match operator input dimensions.");
        }

        var result = new double[OutputDimensions, OutputDimensions];
        // Apply left multiplication M * rho
        for (int i = 0; i < OutputDimensions; i++)
        {
            for (int j = 0; j < OutputDimensions; j++)
            {
                for (int k = 0; k < InputDimensions; k++)
                {
                    result[i, j] += Data[i, k] * densityMatrix[k, j];
                }
            }
        }

        var finalResult = new double[OutputDimensions, OutputDimensions];
        // Apply right multiplication M^dagger * result
        for (int i = 0; i < OutputDimensions; i++)
        {
            for (int j = 0; j < OutputDimensions; j++)
            {
                for (int k = 0; k < OutputDimensions; k++)
                {
                    finalResult[i, j] += result[i, k] * Data[j, k]; // Use conjugate transpose for dagger
                }
            }
        }

        return finalResult;
    }

    // Method to check if the operator is unitary (for quantum operators)
    public bool IsUnitary()
    {
        var identity = new double[InputDimensions, InputDimensions];
        for (int i = 0; i < InputDimensions; i++)
        {
            identity[i, i] = 1;
        }

        var adjoint = GetAdjoint();
        var product = MultiplyMatrices(this.Data, adjoint);

        // Check if the result is an identity matrix (within a tolerance)
        for (int i = 0; i < InputDimensions; i++)
        {
            for (int j = 0; j < InputDimensions; j++)
            {
                if (Math.Abs(product[i, j] - identity[i, j]) > 1e-10)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Get the adjoint (conjugate transpose) of the operator
    private double[,] GetAdjoint()
    {
        int rows = Data.GetLength(0);
        int cols = Data.GetLength(1);
        var adjoint = new double[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                adjoint[j, i] = Data[i, j]; // Taking conjugate transpose (real matrix here)
            }
        }

        return adjoint;
    }

    // Multiply two matrices
    private double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB)
    {
        int aRows = matrixA.GetLength(0);
        int aCols = matrixA.GetLength(1);
        int bRows = matrixB.GetLength(0);
        int bCols = matrixB.GetLength(1);

        if (aCols != bRows)
            throw new InvalidOperationException("Matrix dimensions do not match for multiplication.");

        var result = new double[aRows, bCols];

        for (int i = 0; i < aRows; i++)
        {
            for (int j = 0; j < bCols; j++)
            {
                for (int k = 0; k < aCols; k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }

        return result;
    }

    // Display the matrix
    public override string ToString()
    {
        var result = "";
        for (int i = 0; i < Data.GetLength(0); i++)
        {
            for (int j = 0; j < Data.GetLength(1); j++)
            {
                result += Data[i, j] + " ";
            }
            result += "\n";
        }
        return result;
    }
}
