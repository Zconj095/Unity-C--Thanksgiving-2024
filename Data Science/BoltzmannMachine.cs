using UnityEngine;

public class BoltzmannMachine : MonoBehaviour
{
    [SerializeField] private int numVisible = 6; // Number of visible nodes
    [SerializeField] private int numHidden = 3; // Number of hidden nodes
    [SerializeField] private float learningRate = 0.1f; // Learning rate
    [SerializeField] private float temperature = 1.0f; // Temperature for probability

    private float[,] weights; // Weights between visible and hidden nodes
    private int[] visibleUnits; // Array representing visible units (input layer)
    private int[] hiddenUnits; // Array representing hidden units (latent layer)

    private void Start()
    {
        // Initialize visible and hidden units
        visibleUnits = new int[numVisible];
        hiddenUnits = new int[numHidden];

        // Initialize weights to small random values
        weights = new float[numVisible, numHidden];
        InitializeWeights();
    }

    private void Update()
    {
        // Generate random visible data (replace this with actual input data as needed)
        for (int i = 0; i < numVisible; i++)
        {
            visibleUnits[i] = Random.Range(0, 2); // Binary input, 0 or 1
        }

        // Perform one step of Boltzmann Machine training
        TrainBoltzmannMachine();

        // Debug: Print visible and hidden units
        PrintUnits();
    }

    // Initialize the weights with small random values
    private void InitializeWeights()
    {
        for (int i = 0; i < numVisible; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                weights[i, j] = Random.Range(-0.1f, 0.1f); // Small random values
            }
        }
    }

    // Perform one step of Boltzmann Machine training (Contrastive Divergence)
    private void TrainBoltzmannMachine()
    {
        // Gibbs sampling step: Update hidden units based on visible units
        UpdateHiddenUnits();

        // Update visible units based on the hidden units
        UpdateVisibleUnits();

        // Update weights based on the difference between the positive and negative phases
        UpdateWeights();
    }

    // Update the hidden units based on the visible units (forward pass)
    private void UpdateHiddenUnits()
    {
        for (int j = 0; j < numHidden; j++)
        {
            float activation = 0.0f;
            for (int i = 0; i < numVisible; i++)
            {
                activation += visibleUnits[i] * weights[i, j];
            }
            hiddenUnits[j] = Sigmoid(activation / temperature) > 0.5f ? 1 : 0;
        }
    }

    // Update the visible units based on the hidden units (reconstruction phase)
    private void UpdateVisibleUnits()
    {
        for (int i = 0; i < numVisible; i++)
        {
            float activation = 0.0f;
            for (int j = 0; j < numHidden; j++)
            {
                activation += hiddenUnits[j] * weights[i, j];
            }
            visibleUnits[i] = Sigmoid(activation / temperature) > 0.5f ? 1 : 0;
        }
    }

    // Update the weights using Contrastive Divergence (CD-1)
    private void UpdateWeights()
    {
        // Calculate the outer product of visible and hidden units (positive phase)
        float[,] positivePhase = new float[numVisible, numHidden];
        for (int i = 0; i < numVisible; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                positivePhase[i, j] = visibleUnits[i] * hiddenUnits[j];
            }
        }

        // Calculate the outer product of the reconstructed visible and hidden units (negative phase)
        int[] reconstructedVisible = new int[numVisible];
        int[] reconstructedHidden = new int[numHidden];
        System.Array.Copy(visibleUnits, reconstructedVisible, numVisible);
        System.Array.Copy(hiddenUnits, reconstructedHidden, numHidden);

        float[,] negativePhase = new float[numVisible, numHidden];
        for (int i = 0; i < numVisible; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                negativePhase[i, j] = reconstructedVisible[i] * reconstructedHidden[j];
            }
        }

        // Update weights based on the difference (positive - negative phase)
        for (int i = 0; i < numVisible; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                weights[i, j] += learningRate * (positivePhase[i, j] - negativePhase[i, j]);
            }
        }
    }

    // Sigmoid function
    private float Sigmoid(float x)
    {
        return 1.0f / (1.0f + Mathf.Exp(-x));
    }

    // Debug function to print the current state of the visible and hidden units
    private void PrintUnits()
    {
        string visibleStr = "Visible: ";
        foreach (int unit in visibleUnits) visibleStr += $"{unit} ";
        Debug.Log(visibleStr);

        string hiddenStr = "Hidden: ";
        foreach (int unit in hiddenUnits) hiddenStr += $"{unit} ";
        Debug.Log(hiddenStr);
    }
}
