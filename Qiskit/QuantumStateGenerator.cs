using System;
using System.Linq;
using UnityEngine;

public static class QuantumStateGenerator
{
    // Generate a random statevector using the Hilbert-Schmidt method
    public static Statevector RandomStatevector(int[] dims, int? seed = null)
    {
        System.Random rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();

        // Flatten dimensions and generate random complex vector
        int dim = dims.Aggregate((a, b) => a * b);  // Product of dimensions
        var realPart = new double[dim];
        var imaginaryPart = new double[dim];

        // Generate random complex vector
        for (int i = 0; i < dim; i++)
        {
            realPart[i] = rng.NextDouble();
            imaginaryPart[i] = rng.NextDouble();
        }

        // Normalize to get a unit vector
        double norm = Math.Sqrt(realPart.Sum(r => r * r) + imaginaryPart.Sum(i => i * i));
        for (int i = 0; i < dim; i++)
        {
            realPart[i] /= norm;
            imaginaryPart[i] /= norm;
        }

        // Convert to complex array
        var complexData = new Complex[dim];
        for (int i = 0; i < dim; i++)
        {
            complexData[i] = new Complex(realPart[i], imaginaryPart[i]);
        }

        return new Statevector(complexData, dims);
    }

    // Generate a random density matrix using Hilbert-Schmidt or Bures method
    public static DensityMatrix RandomDensityMatrix(int[] dims, int? rank = null, string method = "Hilbert-Schmidt", int? seed = null)
    {
        int dim = dims.Aggregate((a, b) => a * b);  // Product of dimensions
        rank ??= dim;  // Default to full rank

        if (method == "Hilbert-Schmidt")
        {
            var rho = RandomDensityHilbertSchmidt(dim, rank.Value, seed);
            return new DensityMatrix(rho, dims);
        }
        else if (method == "Bures")
        {
            var rho = RandomDensityBures(dim, rank.Value, seed);
            return new DensityMatrix(rho, dims);
        }
        else
        {
            throw new ArgumentException($"Unrecognized method: {method}");
        }
    }

    // Generate a Ginibre matrix (normally distributed complex matrix)
    private static Complex[,] GinibreMatrix(int nrow, int ncol, int? seed = null)
    {
        System.Random rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();

        var matrix = new Complex[nrow, ncol];
        for (int i = 0; i < nrow; i++)
        {
            for (int j = 0; j < ncol; j++)
            {
                double realPart = rng.NextDouble();
                double imaginaryPart = rng.NextDouble();
                matrix[i, j] = new Complex(realPart, imaginaryPart);
            }
        }
        return matrix;
    }

    // Generate a random density matrix using the Hilbert-Schmidt metric
    private static Complex[,] RandomDensityHilbertSchmidt(int dim, int rank, int? seed = null)
    {
        var mat = GinibreMatrix(dim, rank, seed);
        var result = mat.Multiply(mat.ConjugateTranspose());
        return NormalizeDensityMatrix(result);
    }

    // Generate a random density matrix using the Bures metric
    private static Complex[,] RandomDensityBures(int dim, int rank, int? seed = null)
    {
        var identity = Complex.IdentityMatrix(dim);
        var randomUnitary = RandomUnitary(dim, seed);
        var mat = identity.Multiply(randomUnitary.Data);
        mat = mat.Multiply(GinibreMatrix(dim, rank, seed).ConjugateTranspose());
        return NormalizeDensityMatrix(mat);
    }

    // Normalize a density matrix (ensure Tr(rho) = 1)
    private static Complex[,] NormalizeDensityMatrix(Complex[,] matrix)
    {
        double trace = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            trace += matrix[i, i].Real;
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] /= trace;
            }
        }

        return matrix;
    }

    // Generate a random unitary matrix using the Haar measure (simplified for the example)
    private static Unitary RandomUnitary(int dim, int? seed = null)
    {
        System.Random rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();

        var matrix = new Complex[dim, dim];
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                matrix[i, j] = new Complex(rng.NextDouble(), rng.NextDouble());
            }
        }

        // Convert to unitary (simplified approach)
        return new Unitary(matrix);
    }
}
