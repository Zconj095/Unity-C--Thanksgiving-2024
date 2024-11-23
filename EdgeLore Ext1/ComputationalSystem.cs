using UnityEngine;
using System;
using System.Reflection;

public class ComputationalSystem : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private QuantumHiddenMarkovModel quantumHiddenMarkovModel;
    [SerializeField] private KMeansClustering kMeansCluster;
    [SerializeField] private GroversHopfieldNetwork groversHopfieldNetwork;
    [SerializeField] private GroversAlgorithm groversAlgorithm;

    private QuantumState quantumState;

    [Header("Quantum State Configuration")]
    [SerializeField] private int numQubits = 2;

    private int[] observations;
    private int[] mostLikelyStates;

    private MethodInfo performClusteringMethod;

    public event Action OnHMMProcessingCompleted;

    void Awake()
    {
        // Auto-assign components if not set in the Inspector
        ValidateOrAssignComponent(ref quantumHiddenMarkovModel, "QuantumHiddenMarkovModel");
        ValidateOrAssignComponent(ref kMeansCluster, "KMeansClustering");
        ValidateOrAssignComponent(ref groversHopfieldNetwork, "GroversHopfieldNetwork");
        ValidateOrAssignComponent(ref groversAlgorithm, "GroversAlgorithm");

        try
        {
            quantumState = new QuantumState(numQubits);
            Debug.Log($"QuantumState instantiated with numQubits = {numQubits}.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to instantiate QuantumState: {ex.Message}");
        }
    }

    void Start()
    {
        if (!ValidateComponents())
        {
            Debug.LogError("Some components are not properly assigned. Aborting Computational System initialization.");
            return;
        }

        InitializeSystem();
        ConnectComponents();
        GenerateOutput();
    }

    /// <summary>
    /// Validates that all required components are assigned.
    /// </summary>
    /// <returns>True if all components are valid; otherwise, false.</returns>
    private bool ValidateComponents()
    {
        bool isValid = true;

        if (quantumHiddenMarkovModel == null)
        {
            Debug.LogError("Quantum Hidden Markov Model is not assigned.");
            isValid = false;
        }

        if (quantumState == null)
        {
            Debug.LogError("Quantum State is not assigned.");
            isValid = false;
        }

        if (groversHopfieldNetwork == null)
        {
            Debug.LogError("Grovers Hopfield Network is not assigned.");
            isValid = false;
        }

        if (groversAlgorithm == null)
        {
            Debug.LogError("Grovers Algorithm is not assigned.");
            isValid = false;
        }

        return isValid;
    }

    /// <summary>
    /// Initializes the computational system.
    /// </summary>
    private void InitializeSystem()
    {
        Debug.Log("Initializing Computational System...");
        // Add any additional initialization logic here
        Debug.Log("Initialization Complete.");
    }

    /// <summary>
    /// Connects various computational components and processes data.
    /// </summary>
    private void ConnectComponents()
    {
        Debug.Log("Connecting Computational Components...");

        try
        {
            // Generate a sequence of random observations
            observations = quantumHiddenMarkovModel.GenerateRandomObservations(10);
            ValidateArray(observations, "Generated Observations");

            // Decode the most likely states using the Viterbi algorithm
            mostLikelyStates = quantumHiddenMarkovModel.Viterbi(observations);
            ValidateArray(mostLikelyStates, "Most Likely States");

            if (mostLikelyStates.Length != observations.Length)
            {
                Debug.LogError($"Mismatch between observations count ({observations.Length}) and mostLikelyStates count ({mostLikelyStates.Length}).");
                return;
            }

            OnHMMProcessingCompleted?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in Quantum Hidden Markov Model processing: {ex.Message}\n{ex.StackTrace}");
            return;
        }

        try
        {
            // Execute Grover's Algorithm
            Vector3 groverResult = groversAlgorithm.RunGrover();
            Debug.Log($"Grover's Algorithm Result: {groverResult}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in Grover's Algorithm: {ex.Message}\n{ex.StackTrace}");
            return;
        }

        try
        {
            // Train and retrieve patterns from Grover's Hopfield Network
            groversHopfieldNetwork.TrainNetwork();
            groversHopfieldNetwork.RetrievePattern();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in Grover's Hopfield Network processing: {ex.Message}\n{ex.StackTrace}");
            return;
        }

        if (kMeansCluster != null && performClusteringMethod != null)
        {
            PerformKMeansClustering();
        }
        else
        {
            Debug.LogWarning("K-Means Clustering is not assigned or method unavailable. Skipping clustering step.");
        }
    }

    /// <summary>
    /// Generates output based on processed data.
    /// </summary>
    private void GenerateOutput()
    {
        Debug.Log("Generating Output...");
        // Implement output generation logic here
        Debug.Log("Output Generation Complete.");
    }

    /// <summary>
    /// Validates or assigns a component reference.
    /// </summary>
    /// <typeparam name="T">Type of the component.</typeparam>
    /// <param name="component">Reference to the component.</param>
    /// <param name="componentName">Name of the component for logging.</param>
    private void ValidateOrAssignComponent<T>(ref T component, string componentName) where T : Component
    {
        if (component == null)
        {
            component = GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"{componentName} is not assigned or found on the GameObject.");
            }
            else
            {
                Debug.Log($"{componentName} auto-assigned via GetComponent.");
            }
        }
    }

    /// <summary>
    /// Validates that an array is not null or empty.
    /// </summary>
    /// <typeparam name="T">Type of the array elements.</typeparam>
    /// <param name="array">The array to validate.</param>
    /// <param name="arrayName">Name of the array for logging.</param>
    private void ValidateArray<T>(T[] array, string arrayName)
    {
        if (array == null || array.Length == 0)
        {
            throw new InvalidOperationException($"{arrayName} is null or empty.");
        }

        Debug.Log($"{arrayName} (Count: {array.Length}): {string.Join(", ", array)}");
    }

    /// <summary>
    /// Performs K-Means Clustering using reflection.
    /// </summary>
    private void PerformKMeansClustering()
    {
        try
        {
            float[] dataPoints = PrepareDataPoints(observations);
            ValidateArray(dataPoints, "Data Points for Clustering");

            PerformKMeansClusteringViaReflection(dataPoints, 3);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in K-Means Clustering: {ex.Message}\n{ex.StackTrace}");
        }
    }

    /// <summary>
    /// Prepares data points for clustering by converting integer observations to floats.
    /// </summary>
    /// <param name="observations">Array of integer observations.</param>
    /// <returns>Array of float data points.</returns>
    private float[] PrepareDataPoints(int[] observations)
    {
        float[] dataPoints = new float[observations.Length];
        for (int i = 0; i < observations.Length; i++)
        {
            dataPoints[i] = (float)observations[i];
        }
        return dataPoints;
    }

    /// <summary>
    /// Invokes the PerformKMeansClustering method via reflection.
    /// </summary>
    /// <param name="dataPoints">Data points for clustering.</param>
    /// <param name="k">Number of clusters.</param>
    private void PerformKMeansClusteringViaReflection(float[] dataPoints, int k)
    {
        if (kMeansCluster == null)
        {
            Debug.LogError("KMeansClustering component is not assigned.");
            return;
        }

        // Get the PerformKMeansClustering method if not already cached
        if (performClusteringMethod == null)
        {
            performClusteringMethod = typeof(KMeansClustering).GetMethod("PerformKMeansClustering", BindingFlags.Public | BindingFlags.Instance);
            if (performClusteringMethod == null)
            {
                Debug.LogError("PerformKMeansClustering method not found in KMeansClustering class.");
                return;
            }
        }

        if (dataPoints.Length < 2)
        {
            Debug.LogError("Not enough data points for K-Means Clustering.");
            return;
        }

        try
        {
            performClusteringMethod.Invoke(kMeansCluster, new object[] { dataPoints, k });
            Debug.Log("K-Means Clustering completed.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error invoking PerformKMeansClustering: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
