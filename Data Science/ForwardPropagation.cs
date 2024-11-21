using UnityEngine;

public class ForwardPropagation : MonoBehaviour
{
    [Header("Network Configuration")]
    [SerializeField] private int inputLayerSize = 3;   // Number of input features
    [SerializeField] private int hiddenLayerSize = 4;  // Number of neurons in hidden layer
    [SerializeField] private int outputLayerSize = 1;  // Number of outputs (e.g., binary classification)

    private float[,] inputToHiddenWeights;  // Weights between input and hidden layer
    private float[,] hiddenToOutputWeights; // Weights between hidden and output layer
    private float[] hiddenBiases;           // Biases for hidden layer
    private float[] outputBiases;           // Biases for output layer

    private void Start()
    {
        // Initialize weights and biases
        inputToHiddenWeights = InitializeWeights(inputLayerSize, hiddenLayerSize);
        hiddenToOutputWeights = InitializeWeights(hiddenLayerSize, outputLayerSize);
        hiddenBiases = InitializeBiases(hiddenLayerSize);
        outputBiases = InitializeBiases(outputLayerSize);

        // Example input
        float[] input = { 1.0f, 0.5f, -1.5f };

        // Perform forward propagation
        float[] output = ForwardPropagate(input);

        // Output the result
        Debug.Log($"Network Output: {output[0]}");
    }

    /// <summary>
    /// Sigmoid activation function.
    /// </summary>
    private float Sigmoid(float x)
    {
        return 1.0f / (1.0f + Mathf.Exp(-x));
    }

    /// <summary>
    /// Initializes weights with random values between -1 and 1.
    /// </summary>
    private float[,] InitializeWeights(int rows, int cols)
    {
        float[,] weights = new float[rows, cols];
        System.Random rand = new System.Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                weights[i, j] = (float)(rand.NextDouble() * 2 - 1); // Random values between -1 and 1
            }
        }

        return weights;
    }

    /// <summary>
    /// Initializes biases with random values between -1 and 1.
    /// </summary>
    private float[] InitializeBiases(int size)
    {
        float[] biases = new float[size];
        System.Random rand = new System.Random();

        for (int i = 0; i < size; i++)
        {
            biases[i] = (float)(rand.NextDouble() * 2 - 1); // Random values between -1 and 1
        }

        return biases;
    }

    /// <summary>
    /// Performs forward propagation through the network.
    /// </summary>
    private float[] ForwardPropagate(float[] input)
    {
        // Input to Hidden Layer
        float[] hiddenLayerInput = new float[hiddenLayerSize];
        for (int i = 0; i < hiddenLayerSize; i++)
        {
            hiddenLayerInput[i] = hiddenBiases[i];
            for (int j = 0; j < inputLayerSize; j++)
            {
                hiddenLayerInput[i] += input[j] * inputToHiddenWeights[j, i];
            }
            hiddenLayerInput[i] = Sigmoid(hiddenLayerInput[i]);
        }

        // Hidden to Output Layer
        float[] outputLayerInput = new float[outputLayerSize];
        for (int i = 0; i < outputLayerSize; i++)
        {
            outputLayerInput[i] = outputBiases[i];
            for (int j = 0; j < hiddenLayerSize; j++)
            {
                outputLayerInput[i] += hiddenLayerInput[j] * hiddenToOutputWeights[j, i];
            }
            outputLayerInput[i] = Sigmoid(outputLayerInput[i]);
        }

        return outputLayerInput;
    }
}
