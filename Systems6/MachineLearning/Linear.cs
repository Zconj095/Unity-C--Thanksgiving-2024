using UnityEngine;
using System;
namespace EdgeLoreMachineLearning
{
    public class Linear
    {
        // Example kernel function for a linear SVM
        public double Compute(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have the same length.");

            double result = 0;
            for (int i = 0; i < a.Length; i++)
                result += a[i] * b[i];

            return result;
        }

        public int GetLength(double[][] inputs)
        {
            return inputs.Length > 0 ? inputs[0].Length : 0;
        }
    }
}
