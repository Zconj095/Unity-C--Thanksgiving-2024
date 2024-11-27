using System;
using UnityEngine;

public class NeuralNetworkRegressor : MonoBehaviour
{
    private int _inputSize;
    private int _hiddenSize;
    private int _outputSize;
    private float[,] _weights1;
    private float[,] _weights2;
    private float[] _bias1;
    private float[] _bias2;
    private float _learningRate;
    private float[,] _X; // Inputs
    private float[,] _y; // Targets

    public NeuralNetworkRegressor(int inputSize, int hiddenSize, int outputSize, float learningRate = 0.01f)
    {
        _inputSize = inputSize;
        _hiddenSize = hiddenSize;
        _outputSize = outputSize;
        _learningRate = learningRate;

        // Initialize weights and biases
        _weights1 = new float[inputSize, hiddenSize];
        _weights2 = new float[hiddenSize, outputSize];
        _bias1 = new float[hiddenSize];
        _bias2 = new float[outputSize];

        InitializeWeights();
    }

    // Initialize weights and biases with small random values
    private void InitializeWeights()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < _inputSize; i++)
        {
            for (int j = 0; j < _hiddenSize; j++)
            {
                _weights1[i, j] = (float)(rand.NextDouble() * 0.01); // Small random values
            }
        }

        for (int i = 0; i < _hiddenSize; i++)
        {
            for (int j = 0; j < _outputSize; j++)
            {
                _weights2[i, j] = (float)(rand.NextDouble() * 0.01); // Small random values
            }
        }

        for (int i = 0; i < _hiddenSize; i++)
        {
            _bias1[i] = 0f; // Biases are initialized to 0
        }

        for (int i = 0; i < _outputSize; i++)
        {
            _bias2[i] = 0f; // Biases are initialized to 0
        }
    }

    // Sigmoid activation function
    private float Sigmoid(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    // Sigmoid derivative (for backpropagation)
    private float SigmoidDerivative(float x)
    {
        return x * (1f - x);
    }

    // Forward pass
    public float[,] Forward(float[,] X)
    {
        int m = X.GetLength(0); // Number of samples
        int n = X.GetLength(1); // Number of features

        // Create storage for hidden layer activations and output
        float[,] hiddenLayer = new float[m, _hiddenSize];
        float[,] outputLayer = new float[m, _outputSize];

        // Calculate hidden layer activations (Z1 -> A1)
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < _hiddenSize; j++)
            {
                hiddenLayer[i, j] = 0;
                for (int k = 0; k < n; k++)
                {
                    hiddenLayer[i, j] += X[i, k] * _weights1[k, j];
                }
                hiddenLayer[i, j] += _bias1[j];
                hiddenLayer[i, j] = Sigmoid(hiddenLayer[i, j]); // Activation function
            }
        }

        // Calculate output layer activations (Z2 -> A2)
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < _outputSize; j++)
            {
                outputLayer[i, j] = 0;
                for (int k = 0; k < _hiddenSize; k++)
                {
                    outputLayer[i, j] += hiddenLayer[i, k] * _weights2[k, j];
                }
                outputLayer[i, j] += _bias2[j];
                outputLayer[i, j] = Sigmoid(outputLayer[i, j]); // Activation function
            }
        }

        return outputLayer;
    }

    // Mean Squared Error Loss Function
    private float ComputeLoss(float[,] predictions, float[,] y)
    {
        int m = y.GetLength(0);
        float loss = 0;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < _outputSize; j++)
            {
                loss += Mathf.Pow(predictions[i, j] - y[i, j], 2);
            }
        }

        return loss / m;
    }

    // Backpropagation to calculate gradients
    private void Backward(float[,] X, float[,] y, float[,] predictions)
    {
        int m = y.GetLength(0);
        float[,] dZ2 = new float[m, _outputSize];
        float[,] dW2 = new float[_hiddenSize, _outputSize];
        float[] dB2 = new float[_outputSize];

        float[,] dZ1 = new float[m, _hiddenSize];
        float[,] dW1 = new float[_inputSize, _hiddenSize];
        float[] dB1 = new float[_hiddenSize];

        // Compute gradients for the output layer
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < _outputSize; j++)
            {
                dZ2[i, j] = predictions[i, j] - y[i, j]; // Derivative of MSE
                dW2[j, i] = dZ2[i, j] * predictions[i, j] * (1 - predictions[i, j]);
                dB2[j] = dZ2[i, j] * predictions[i, j] * (1 - predictions[i, j]);
            }
        }

        // Compute gradients for the hidden layer
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < _hiddenSize; j++)
            {
                dZ1[i, j] = dZ2[i, j] * SigmoidDerivative(predictions[i, j]); 
                dW1[j, i] = dZ1[i, j] * X[i, j];
                dB1[j] = dZ1[i, j] ;
            }
        }

        // Update weights and biases using gradient descent
        for (int i = 0; i < _hiddenSize; i++)
        {
            for (int j = 0; j < _outputSize; j++)
            {
                _weights2[i, j] -= _learningRate * dW2[i, j];
            }
        }

        for (int i = 0; i < _outputSize; i++)
        {
            _bias2[i] -= _learningRate * dB2[i];
        }
    }
    
    // Fit the model
    public void Fit(float[,] X, float[,] y, int epochs = 1000)
    {
        _X = X;
        _y = y;

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            float[,] predictions = Forward(X);
            float loss = ComputeLoss(predictions, y);
            Debug.Log("Epoch " + epoch + " Loss: " + loss);
            Backward(X, y, predictions);
        }
    }
}

