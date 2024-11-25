using System.Collections.Generic;
using System;

public class DenseLayer2
{
    private float[,] weights;
    private float[] biases;

    public DenseLayer2(int inputSize, int outputSize)
    {
        weights = new float[inputSize, outputSize];
        biases = new float[outputSize];
        InitializeWeights();
    }

    private void InitializeWeights()
    {
        Random random = new Random();
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] = (float)random.NextDouble() * 2 - 1;
            }
        }
        for (int i = 0; i < biases.Length; i++)
        {
            biases[i] = 0f;
        }
    }

    public float[] Forward(float[] input)
    {
        float[] output = new float[weights.GetLength(1)];
        for (int j = 0; j < output.Length; j++)
        {
            output[j] = biases[j];
            for (int i = 0; i < input.Length; i++)
            {
                output[j] += input[i] * weights[i, j];
            }
            output[j] = (float)Math.Tanh(output[j]); // Use System.Math.Tanh
        }
        return output;
    }
}
