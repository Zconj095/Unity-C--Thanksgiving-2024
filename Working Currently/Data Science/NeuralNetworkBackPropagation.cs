using UnityEngine;

public class NeuralNetworkBackPropagation : MonoBehaviour
{
    // Network architecture parameters
    [SerializeField] private int numInputNodes = 3;  // Number of input nodes (features)
    [SerializeField] private int numHiddenNodes = 4; // Number of hidden nodes
    [SerializeField] private int numOutputNodes = 1; // Number of output nodes (target)

    // Learning rate
    [SerializeField] private float learningRate = 0.1f;

    // Weights and biases
    private float[,] inputHiddenWeights;  // Weights between input and hidden layers
    private float[] hiddenBiases;         // Biases for hidden layer
    private float[,] hiddenOutputWeights; // Weights between hidden and output layers
    private float[] outputBiases;         // Biases for output layer

    // Activation function (Sigmoid)
    private float Sigmoid(float x) => 1f / (1f + Mathf.Exp(-x));

    // Derivative of Sigmoid
    private float SigmoidDerivative(float x) => x * (1f - x);

    private void Start()
    {
        // Initialize weights and biases
        InitializeNetwork();

        // Training loop (using dummy data)
        TrainNetwork();
    }

    // Initialize weights and biases with small random values
    private void InitializeNetwork()
    {
        inputHiddenWeights = new float[numInputNodes, numHiddenNodes];
        hiddenBiases = new float[numHiddenNodes];
        hiddenOutputWeights = new float[numHiddenNodes, numOutputNodes];
        outputBiases = new float[numOutputNodes];

        for (int i = 0; i < numInputNodes; i++)
        {
            for (int j = 0; j < numHiddenNodes; j++)
            {
                inputHiddenWeights[i, j] = Random.Range(-0.5f, 0.5f);
            }
        }

        for (int i = 0; i < numHiddenNodes; i++)
        {
            hiddenBiases[i] = Random.Range(-0.5f, 0.5f);
        }

        for (int i = 0; i < numHiddenNodes; i++)
        {
            for (int j = 0; j < numOutputNodes; j++)
            {
                hiddenOutputWeights[i, j] = Random.Range(-0.5f, 0.5f);
            }
        }

        for (int i = 0; i < numOutputNodes; i++)
        {
            outputBiases[i] = Random.Range(-0.5f, 0.5f);
        }
    }

    // Train the network with dummy data
    private void TrainNetwork()
    {
        // Dummy training data: inputs (3 features) and target outputs
        float[,] inputs = {
            { 0f, 0f, 1f },
            { 1f, 0f, 1f },
            { 0f, 1f, 1f },
            { 1f, 1f, 1f }
        };
        float[] targetOutputs = { 0f, 1f, 1f, 0f }; // XOR pattern

        int epochs = 10000; // Number of training iterations
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            for (int i = 0; i < inputs.GetLength(0); i++)
            {
                // Perform forward pass
                float[] inputLayer = new float[numInputNodes];
                for (int j = 0; j < numInputNodes; j++)
                {
                    inputLayer[j] = inputs[i, j];
                }

                float[] hiddenLayer = new float[numHiddenNodes];
                for (int j = 0; j < numHiddenNodes; j++)
                {
                    float sum = hiddenBiases[j];
                    for (int k = 0; k < numInputNodes; k++)
                    {
                        sum += inputLayer[k] * inputHiddenWeights[k, j];
                    }
                    hiddenLayer[j] = Sigmoid(sum);
                }

                float[] outputLayer = new float[numOutputNodes];
                for (int j = 0; j < numOutputNodes; j++)
                {
                    float sum = outputBiases[j];
                    for (int k = 0; k < numHiddenNodes; k++)
                    {
                        sum += hiddenLayer[k] * hiddenOutputWeights[k, j];
                    }
                    outputLayer[j] = Sigmoid(sum);
                }

                // Calculate error
                float[] outputErrors = new float[numOutputNodes];
                for (int j = 0; j < numOutputNodes; j++)
                {
                    outputErrors[j] = targetOutputs[i] - outputLayer[j];
                }

                // Backpropagation
                float[] outputLayerDeltas = new float[numOutputNodes];
                for (int j = 0; j < numOutputNodes; j++)
                {
                    outputLayerDeltas[j] = outputErrors[j] * SigmoidDerivative(outputLayer[j]);
                }

                float[] hiddenLayerDeltas = new float[numHiddenNodes];
                for (int j = 0; j < numHiddenNodes; j++)
                {
                    float errorSum = 0f;
                    for (int k = 0; k < numOutputNodes; k++)
                    {
                        errorSum += outputLayerDeltas[k] * hiddenOutputWeights[j, k];
                    }
                    hiddenLayerDeltas[j] = errorSum * SigmoidDerivative(hiddenLayer[j]);
                }

                // Update weights and biases
                for (int j = 0; j < numOutputNodes; j++)
                {
                    for (int k = 0; k < numHiddenNodes; k++)
                    {
                        hiddenOutputWeights[k, j] += learningRate * hiddenLayer[k] * outputLayerDeltas[j];
                    }
                    outputBiases[j] += learningRate * outputLayerDeltas[j];
                }

                for (int j = 0; j < numHiddenNodes; j++)
                {
                    for (int k = 0; k < numInputNodes; k++)
                    {
                        inputHiddenWeights[k, j] += learningRate * inputLayer[k] * hiddenLayerDeltas[j];
                    }
                    hiddenBiases[j] += learningRate * hiddenLayerDeltas[j];
                }
            }
        }

        Debug.Log("Training complete!");
    }
}
