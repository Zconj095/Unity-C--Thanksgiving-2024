using UnityEngine;
using System.Collections.Generic;

public class QuantumDataLake : MonoBehaviour
{
    /// <summary>
    /// Represents a quantum state with probabilities for different outcomes.
    /// </summary>
    [System.Serializable]
    public class QuantumData
    {
        public string Name; // Name of the quantum state
        public float[] StateProbabilities; // Probabilities of each state

        /// <summary>
        /// Constructor to initialize a quantum state with the given name and number of states.
        /// </summary>
        /// <param name="name">Name of the quantum data.</param>
        /// <param name="numStates">Number of possible states.</param>
        public QuantumData(string name, int numStates)
        {
            Name = name;
            StateProbabilities = new float[numStates];
            InitializeQuantumState();
        }

        /// <summary>
        /// Initializes the quantum state with random probabilities and normalizes them.
        /// </summary>
        private void InitializeQuantumState()
        {
            float total = 0f;
            for (int i = 0; i < StateProbabilities.Length; i++)
            {
                StateProbabilities[i] = Random.Range(0f, 1f);
                total += StateProbabilities[i];
            }

            // Normalize probabilities to ensure they sum to 1
            for (int i = 0; i < StateProbabilities.Length; i++)
            {
                StateProbabilities[i] /= total;
            }
        }

        /// <summary>
        /// Simulates a quantum measurement by collapsing the state based on probabilities.
        /// </summary>
        /// <returns>The index of the measured state.</returns>
        public int Measure()
        {
            float rand = Random.Range(0f, 1f);
            float cumulativeProbability = 0f;

            for (int i = 0; i < StateProbabilities.Length; i++)
            {
                cumulativeProbability += StateProbabilities[i];
                if (rand <= cumulativeProbability)
                {
                    return i; // Return the index of the collapsed state
                }
            }

            return StateProbabilities.Length - 1; // Fallback, should never reach here
        }
    }

    [Header("Quantum Data Lake")]
    [SerializeField]
    private List<QuantumData> quantumDataLake = new List<QuantumData>();

    private void Start()
    {
        // Example quantum data initialization
        AddQuantumData("QuantumState1", 4);
        AddQuantumData("QuantumState2", 3);

        // Perform initial quantum measurements
        PerformQuantumMeasurements();
    }

    /// <summary>
    /// Adds a new quantum state to the data lake.
    /// </summary>
    /// <param name="name">Name of the quantum state.</param>
    /// <param name="numStates">Number of possible states.</param>
    public void AddQuantumData(string name, int numStates)
    {
        quantumDataLake.Add(new QuantumData(name, numStates));
        Debug.Log($"Added Quantum Data: {name} with {numStates} states.");
    }

    /// <summary>
    /// Performs measurements on all quantum states in the data lake.
    /// </summary>
    private void PerformQuantumMeasurements()
    {
        foreach (var quantumData in quantumDataLake)
        {
            int measuredState = quantumData.Measure();
            Debug.Log($"{quantumData.Name} collapsed to state: {measuredState}");
        }
    }

    private void Update()
    {
        // Optional: Add real-time interactions or dynamic updates to the quantum states here
    }
}
