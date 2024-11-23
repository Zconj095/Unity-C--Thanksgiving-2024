using UnityEngine;
using System;

public class MEMORYRECALLLSTM
{
    private int inputSize;
    private int hiddenSize;

    private float[,] Wf, Wi, Wo, Wc; // Weight matrices for gates
    private float[] bf, bi, bo, bc; // Bias terms
    private float[] cellState, hiddenState; // Cell state and hidden state

    public MEMORYRECALLLSTM(int inputSize, int hiddenSize)
    {
        this.inputSize = inputSize;
        this.hiddenSize = hiddenSize;

        InitializeWeights();
    }

    private void InitializeWeights()
    {
        Wf = RandomMatrix(hiddenSize, inputSize + hiddenSize);
        Wi = RandomMatrix(hiddenSize, inputSize + hiddenSize);
        Wo = RandomMatrix(hiddenSize, inputSize + hiddenSize);
        Wc = RandomMatrix(hiddenSize, inputSize + hiddenSize);

        bf = RandomVector(hiddenSize);
        bi = RandomVector(hiddenSize);
        bo = RandomVector(hiddenSize);
        bc = RandomVector(hiddenSize);

        cellState = new float[hiddenSize];
        hiddenState = new float[hiddenSize];
    }

    private float[,] RandomMatrix(int rows, int cols)
    {
        float[,] matrix = new float[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = UnityEngine.Random.Range(-1f, 1f);
            }
        }
        return matrix;
    }

    private float[] RandomVector(int size)
    {
        float[] vector = new float[size];
        for (int i = 0; i < size; i++)
        {
            vector[i] = UnityEngine.Random.Range(-1f, 1f);
        }
        return vector;
    }

    public float[] Forward(float[] input)
    {
        float[] combinedInput = CombineArrays(input, hiddenState);

        float[] forgetGate = Sigmoid(Add(Dot(Wf, combinedInput), bf));
        float[] inputGate = Sigmoid(Add(Dot(Wi, combinedInput), bi));
        float[] outputGate = Sigmoid(Add(Dot(Wo, combinedInput), bo));
        float[] cellInput = Tanh(Add(Dot(Wc, combinedInput), bc));

        for (int i = 0; i < cellState.Length; i++)
        {
            cellState[i] = forgetGate[i] * cellState[i] + inputGate[i] * cellInput[i];
            hiddenState[i] = outputGate[i] * Tanh(cellState[i]);
        }

        return hiddenState;
    }

    private float[] CombineArrays(float[] a, float[] b)
    {
        float[] combined = new float[a.Length + b.Length];
        a.CopyTo(combined, 0);
        b.CopyTo(combined, a.Length);
        return combined;
    }

    private float[] Dot(float[,] matrix, float[] vector)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        float[] result = new float[rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i] += matrix[i, j] * vector[j];
            }
        }
        return result;
    }

    private float[] Add(float[] a, float[] b)
    {
        float[] result = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] + b[i];
        }
        return result;
    }

    private float[] Sigmoid(float[] x)
    {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            result[i] = 1 / (1 + Mathf.Exp(-x[i]));
        }
        return result;
    }

    private float Tanh(float x)
    {
        return (float)Math.Tanh(x); // Ensure System.Math is used for Tanh
    }

    private float[] Tanh(float[] x)
    {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            result[i] = Tanh(x[i]);
        }
        return result;
    }
}
