using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   t-SNE implementation for Unity.
    /// </summary>
    public class TSNE
    {
        private double perplexity = 50;
        private double theta = 0.5;

        /// <summary>
        ///   Gets or sets t-SNE's perplexity value. Default is 50.
        /// </summary>
        public double Perplexity
        {
            get => perplexity;
            set => perplexity = value;
        }

        /// <summary>
        ///   Gets or sets t-SNE's Theta value. Default is 0.5.
        /// </summary>
        public double Theta
        {
            get => theta;
            set => theta = value;
        }

        /// <summary>
        ///   Applies the t-SNE transformation to reduce dimensions of input data.
        /// </summary>
        /// <param name="input">Input high-dimensional data (N x D).</param>
        /// <returns>Low-dimensional data (N x 2).</returns>
        public Vector2[] Transform(Vector3[] input)
        {
            int N = input.Length;
            int outputDims = 2;

            // Convert input data to a double array for computation
            double[][] inputData = new double[N][];
            for (int i = 0; i < N; i++)
            {
                inputData[i] = new double[] { input[i].x, input[i].y, input[i].z };
            }

            // Prepare the output data container
            double[][] outputData = new double[N][];
            for (int i = 0; i < N; i++)
            {
                outputData[i] = new double[outputDims];
            }

            // Run the t-SNE algorithm
            RunTSNE(inputData, outputData, perplexity, theta);

            // Convert the result to Unity-compatible Vector2
            Vector2[] result = new Vector2[N];
            for (int i = 0; i < N; i++)
            {
                result[i] = new Vector2((float)outputData[i][0], (float)outputData[i][1]);
            }

            return result;
        }

        private void RunTSNE(double[][] input, double[][] output, double perplexity, double theta)
        {
            // Placeholder for the actual t-SNE implementation logic.
            // Adapt the existing t-SNE methods from Accord here,
            // replacing incompatible logic with Unity-compatible alternatives.
            //
            // For example:
            // - Use Unity's Debug.Log for logging.
            // - Replace file operations with Unity's ScriptableObjects or serialized assets.
            //
            // Input validation and dimensionality checks
            if (input.Length < 3 * perplexity)
            {
                throw new ArgumentException("Perplexity is too large for the number of data points.");
            }

            // Initialization, normalization, and gradient descent logic go here.

            // Final embedding is stored in `output`.
        }
    }
}
