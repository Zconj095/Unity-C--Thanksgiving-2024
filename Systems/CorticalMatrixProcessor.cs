using System;
using UnityEngine;

public class CorticalMatrixProcessor : MonoBehaviour
{
    // Function to compute cortical means (row-wise or column-wise)
    public static float[] ComputeCorticalMeans(float[,] matrix, bool rowWise = true)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        float[] means;

        if (rowWise)
        {
            means = new float[rows];
            for (int i = 0; i < rows; i++)
            {
                float sum = 0f;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j];
                }
                means[i] = sum / cols;
            }
        }
        else
        {
            means = new float[cols];
            for (int j = 0; j < cols; j++)
            {
                float sum = 0f;
                for (int i = 0; i < rows; i++)
                {
                    sum += matrix[i, j];
                }
                means[j] = sum / rows;
            }
        }

        return means;
    }

    // Function to compute cortical means differentiation (difference across rows or columns)
    public static float[] ComputeCorticalMeansDifferentiation(float[,] matrix, bool rowWise = true)
    {
        float[] means = ComputeCorticalMeans(matrix, rowWise);

        float[] differences = new float[means.Length - 1];
        for (int i = 0; i < means.Length - 1; i++)
        {
            differences[i] = means[i + 1] - means[i];
        }

        return differences;
    }

    // Example usage
    void Start()
    {
        float[,] matrix = new float[,]
        {
            { 1.0f, 2.0f, 3.0f },
            { 4.0f, 5.0f, 6.0f },
            { 7.0f, 8.0f, 9.0f }
        };

        // Compute row-wise cortical means
        float[] rowMeans = ComputeCorticalMeans(matrix, true);
        Debug.Log("Row-wise cortical means: " + string.Join(", ", rowMeans));

        // Compute row-wise cortical means differentiation
        float[] rowDiffs = ComputeCorticalMeansDifferentiation(matrix, true);
        Debug.Log("Row-wise cortical means differentiation: " + string.Join(", ", rowDiffs));

        // Compute column-wise cortical means
        float[] colMeans = ComputeCorticalMeans(matrix, false);
        Debug.Log("Column-wise cortical means: " + string.Join(", ", colMeans));

        // Compute column-wise cortical means differentiation
        float[] colDiffs = ComputeCorticalMeansDifferentiation(matrix, false);
        Debug.Log("Column-wise cortical means differentiation: " + string.Join(", ", colDiffs));
    }
}
