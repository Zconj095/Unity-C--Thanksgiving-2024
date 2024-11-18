using UnityEngine;

public class HebbianLearning : MonoBehaviour
{
    [Header("Network Configuration")]
    [SerializeField] private int numNeurons = 5;  // Number of neurons
    [SerializeField] private float learningRate = 0.1f; // Learning rate (η)

    private float[,] weights;  // Weights matrix for neuron-to-neuron connections
    private int[] neuronActivations;  // Array of neuron activations (binary)

    private void Start()
    {
        // Initialize neuron activations and weights
        neuronActivations = new int[numNeurons];
        weights = new float[numNeurons, numNeurons];

        // Initialize weights to small random values
        InitializeWeights();
    }

    private void Update()
    {
        // Generate random neuron activations (replace this with actual data if needed)
        GenerateRandomActivations();

        // Apply Hebbian learning rule to update the weights
        ApplyHebbianLearning();

        // Debug: Print the activations and weights
        PrintNeuronsAndWeights();
    }

    /// <summary>
    /// Initializes the weights with small random values, avoiding self-connections.
    /// </summary>
    private void InitializeWeights()
    {
        for (int i = 0; i < numNeurons; i++)
        {
            for (int j = 0; j < numNeurons; j++)
            {
                if (i != j) // No self-connections
                {
                    weights[i, j] = Random.Range(-0.1f, 0.1f); // Small random initial weights
                }
            }
        }
    }

    /// <summary>
    /// Generates random binary activations for the neurons.
    /// </summary>
    private void GenerateRandomActivations()
    {
        for (int i = 0; i < numNeurons; i++)
        {
            neuronActivations[i] = Random.Range(0, 2); // Binary activation (0 or 1)
        }
    }

    /// <summary>
    /// Applies the Hebbian learning rule to update the weights.
    /// </summary>
    private void ApplyHebbianLearning()
    {
        for (int i = 0; i < numNeurons; i++)
        {
            for (int j = 0; j < numNeurons; j++)
            {
                if (i != j) // No self-connections
                {
                    // Hebbian learning rule: Δw_ij = η * x_i * x_j
                    weights[i, j] += learningRate * neuronActivations[i] * neuronActivations[j];
                }
            }
        }
    }

    /// <summary>
    /// Prints the neuron activations and the weights matrix for debugging.
    /// </summary>
    private void PrintNeuronsAndWeights()
    {
        string activationsStr = "Neuron Activations: ";
        for (int i = 0; i < numNeurons; i++)
        {
            activationsStr += neuronActivations[i] + " ";
        }
        Debug.Log(activationsStr);

        string weightsStr = "Weights Matrix:\n";
        for (int i = 0; i < numNeurons; i++)
        {
            for (int j = 0; j < numNeurons; j++)
            {
                weightsStr += $"{weights[i, j]:F2} "; // Format to 2 decimal places
            }
            weightsStr += "\n";
        }
        Debug.Log(weightsStr);
    }
}
