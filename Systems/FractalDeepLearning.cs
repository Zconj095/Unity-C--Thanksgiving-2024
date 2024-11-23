using System;
using System.Collections.Generic;
using UnityEngine;

public class FractalDeepLearning : MonoBehaviour
{
    // Represents a single layer of the neural network
    public class Layer
    {
        public float[,] Weights;
        public float[] Biases;

        public Layer(int inputSize, int outputSize)
        {
            Weights = GenerateFractalWeights(inputSize, outputSize);
            Biases = new float[outputSize];
        }

        private float[,] GenerateFractalWeights(int inputSize, int outputSize)
        {
            float[,] weights = new float[inputSize, outputSize];
            for (int i = 0; i < inputSize; i++)
            {
                for (int j = 0; j < outputSize; j++)
                {
                    weights[i, j] = FractalFunction(i, j);
                }
            }
            return weights;
        }

        private float FractalFunction(int i, int j)
        {
            // Simple fractal weight function (e.g., Mandelbrot set approximation)
            return (float)(Math.Sin(i * 0.1) * Math.Cos(j * 0.1));
        }
    }

    private List<Layer> layers;

    public FractalDeepLearning(int[] layerSizes)
    {
        layers = new List<Layer>();
        for (int l = 0; l < layerSizes.Length - 1; l++)
        {
            layers.Add(new Layer(layerSizes[l], layerSizes[l + 1]));
        }
    }

    public float[] Forward(float[] input)
    {
        float[] activation = input;
        foreach (var layer in layers)
        {
            activation = ActivateLayer(activation, layer);
        }
        return activation;
    }

    private float[] ActivateLayer(float[] input, Layer layer)
    {
        int outputSize = layer.Biases.Length;
        float[] output = new float[outputSize];

        for (int j = 0; j < outputSize; j++)
        {
            float sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i] * layer.Weights[i, j];
            }
            output[j] = MathF.Tanh(sum + layer.Biases[j]); // Hyperbolic tangent activation
        }

        return output;
    }
}
