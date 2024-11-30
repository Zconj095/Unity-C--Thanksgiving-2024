using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class LeastSquaresLearning<TKernel, TInput>
        where TKernel : struct
        where TInput : IList<float>
    {
        private TKernel kernel;
        private float tolerance = 1e-6f;
        private float[] diagonal;
        private float[] weights;
        private float threshold;

        public float Tolerance
        {
            get => tolerance;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Tolerance must be greater than zero.");
                tolerance = value;
            }
        }

        public void Train(TInput[] inputs, int[] outputs)
        {
            int length = inputs.Length;

            // Initialize the diagonal and kernel cache
            diagonal = new float[length];
            var kernelCache = new float[length, length];

            // Fill kernel cache and diagonal
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    kernelCache[i, j] = ComputeKernel(inputs[i], inputs[j]);
                }
                diagonal[i] = kernelCache[i, i] + 1.0f / length;
            }

            // Solve for eta and nu using conjugate gradient
            float[] eta = ConjugateGradient(VectorCreate(length, 1f), kernelCache, diagonal, length); // Fixed type
            float[] nu = ConjugateGradient(VectorCreate(length, 1f), kernelCache, diagonal, length);

            // Compute scaling factor
            float s = 0;
            for (int i = 0; i < length; i++)
                s += outputs[i] * eta[i];

            // Compute threshold (bias)
            threshold = 0;
            for (int i = 0; i < length; i++)
                threshold += eta[i];
            threshold /= s;

            // Compute weights
            weights = new float[length];
            for (int i = 0; i < length; i++)
                weights[i] = (nu[i] - eta[i] * threshold) * outputs[i];

            Debug.Log("Training completed successfully.");
        }

        private float[] ConjugateGradient(float[] target, float[,] kernelCache, float[] diagonal, int length)
        {
            float[] x = new float[length];
            float[] r = new float[length];
            float[] p = new float[length];
            float[] h = new float[length];

            Array.Copy(target, r, length);
            Array.Copy(target, p, length);

            float norm = DotProduct(r, r);
            float beta = 1;
            int iteration = 0;

            while (norm > tolerance && iteration < length)
            {
                iteration++;

                for (int i = 0; i < length; i++)
                    p[i] = r[i] + beta * p[i];

                for (int i = 0; i < length; i++)
                {
                    h[i] = 0;
                    for (int j = 0; j < length; j++)
                        h[i] += kernelCache[i, j] * p[j];
                    h[i] += diagonal[i] * p[i];
                }

                float alpha = norm / DotProduct(p, h);

                for (int i = 0; i < length; i++)
                {
                    x[i] += alpha * p[i];
                    r[i] -= alpha * h[i];
                }

                float newNorm = DotProduct(r, r);
                beta = newNorm / norm;
                norm = newNorm;
            }

            return x;
        }

        private float ComputeKernel(TInput x, TInput y)
        {
            // Example linear kernel: dot product of two vectors
            float sum = 0;
            for (int i = 0; i < x.Count; i++)
                sum += x[i] * y[i];
            return sum;
        }

        private static float[] VectorCreate(int length, float value)
        {
            float[] vector = new float[length];
            for (int i = 0; i < length; i++)
                vector[i] = value;
            return vector;
        }

        private static float DotProduct(float[] a, float[] b)
        {
            float sum = 0;
            for (int i = 0; i < a.Length; i++)
                sum += a[i] * b[i];
            return sum;
        }

        public float Predict(TInput input, TInput[] supportVectors)
        {
            float prediction = 0;

            for (int i = 0; i < supportVectors.Length; i++)
            {
                float kernelValue = ComputeKernel(input, supportVectors[i]);
                prediction += weights[i] * kernelValue;
            }

            return prediction + threshold;
        }
    }
}
