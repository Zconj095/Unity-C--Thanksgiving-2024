using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeuralNetworkClassifier : MonoBehaviour
{
    private float[,] weights;
    private string lossFunction;
    private bool oneHotEncoding;
    private bool isFitted = false;
    private int? numClasses = null;

    public void Initialize(int inputSize, int outputSize, string lossFunction = "squared_error", bool oneHotEncoding = false)
    {
        this.lossFunction = lossFunction;
        this.oneHotEncoding = oneHotEncoding;
        weights = new float[inputSize, outputSize];
        InitializeWeights();
    }

    private void InitializeWeights()
    {
        // Randomly initialize weights
        System.Random random = new System.Random();
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] = (float)(random.NextDouble() * 2 - 1); // Random values between -1 and 1
            }
        }
    }

    public void Train(float[,] X, float[,] y, int epochs, float learningRate)
    {
        if (X.GetLength(0) != y.GetLength(0))
            throw new ArgumentException("The number of samples in X and y must match.");

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            for (int i = 0; i < X.GetLength(0); i++)
            {
                float[] input = GetRow(X, i);
                float[] target = GetRow(y, i);
                float[] output = PredictSingle(input);

                // Compute the error
                float[] error = new float[target.Length];
                for (int j = 0; j < target.Length; j++)
                {
                    error[j] = target[j] - output[j];
                }

                // Update weights (gradient descent)
                for (int j = 0; j < weights.GetLength(0); j++)
                {
                    for (int k = 0; k < weights.GetLength(1); k++)
                    {
                        weights[j, k] += learningRate * error[k] * input[j];
                    }
                }
            }
        }

        numClasses = oneHotEncoding ? y.GetLength(1) : y.Cast<float>().Distinct().Count();
        isFitted = true;
    }

    public float[] PredictSingle(float[] input)
    {
        float[] output = new float[weights.GetLength(1)];

        for (int j = 0; j < weights.GetLength(1); j++)
        {
            output[j] = 0;
            for (int i = 0; i < input.Length; i++)
            {
                output[j] += input[i] * weights[i, j];
            }

            // Apply activation function (e.g., sigmoid for classification)
            output[j] = Sigmoid(output[j]);
        }

        return oneHotEncoding ? OneHotEncode(output) : output;
    }

    public float[,] Predict(float[,] X)
    {
        int samples = X.GetLength(0);
        int outputs = weights.GetLength(1);
        float[,] predictions = new float[samples, outputs];

        for (int i = 0; i < samples; i++)
        {
            float[] input = GetRow(X, i);
            float[] output = PredictSingle(input);

            for (int j = 0; j < outputs; j++)
            {
                predictions[i, j] = output[j];
            }
        }

        return predictions;
    }

    private float[] OneHotEncode(float[] probabilities)
    {
        int maxIndex = Array.IndexOf(probabilities, probabilities.Max());
        float[] oneHot = new float[probabilities.Length];
        oneHot[maxIndex] = 1;
        return oneHot;
    }

    private float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }

    private float[] GetRow(float[,] matrix, int row)
    {
        int cols = matrix.GetLength(1);
        float[] rowData = new float[cols];
        for (int i = 0; i < cols; i++)
        {
            rowData[i] = matrix[row, i];
        }
        return rowData;
    }

    public int? NumClasses => numClasses;

    public bool IsFitted => isFitted;
}
