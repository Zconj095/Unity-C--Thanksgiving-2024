using UnityEngine;
using System.Collections.Generic;

public class QuantumSuperposition : MonoBehaviour
{
    /// <summary>
    /// Represents a quantum state with a name and a complex amplitude.
    /// </summary>
    public class QuantumState
    {
        public string StateName { get; private set; }
        public ComplexNumber Amplitude { get; private set; }

        public QuantumState(string name, ComplexNumber amplitude)
        {
            StateName = name;
            Amplitude = amplitude;
        }

        public void SetAmplitude(ComplexNumber newAmplitude)
        {
            Amplitude = newAmplitude;
        }
    }

    private List<QuantumState> states;
    private ComplexNumber[,] unitaryMatrix;
    private bool isCollapsed = false;
    private QuantumState collapsedState;

    void Start()
    {
        // Initialize quantum states
        states = new List<QuantumState>
        {
            new QuantumState("State A", ComplexNumber.Create(0.5, 0.5)),
            new QuantumState("State B", ComplexNumber.Create(0.5, -0.5)),
            new QuantumState("State C", ComplexNumber.Create(0.5, 0.5)),
            new QuantumState("State D", ComplexNumber.Create(0.5, -0.5))
        };

        NormalizeStateVector();

        // Define a unitary matrix (Hadamard-like transformation)
        InitializeUnitaryMatrix();
    }

    /// <summary>
    /// Initializes a 4x4 unitary transformation matrix.
    /// </summary>
    void InitializeUnitaryMatrix()
    {
        unitaryMatrix = new ComplexNumber[4, 4];

        unitaryMatrix[0, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[0, 1] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[0, 2] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[0, 3] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[1, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[1, 1] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[1, 2] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[1, 3] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[2, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[2, 1] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[2, 2] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[2, 3] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[3, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[3, 1] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[3, 2] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[3, 3] = ComplexNumber.Create(0.5, 0);
    }

    /// <summary>
    /// Normalizes the state vector so that the total probability sums to 1.
    /// </summary>
    void NormalizeStateVector()
    {
        double totalAmplitudeSquared = 0;

        foreach (var state in states)
        {
            totalAmplitudeSquared += state.Amplitude.GetMagnitude() * state.Amplitude.GetMagnitude();
        }

        double normalizationFactor = Mathf.Sqrt((float)totalAmplitudeSquared);
        foreach (var state in states)
        {
            ComplexNumber normalizedAmplitude = ComplexNumber.Divide(state.Amplitude, normalizationFactor);
            state.SetAmplitude(normalizedAmplitude);
        }
    }

    /// <summary>
    /// Applies a unitary transformation to the quantum states.
    /// </summary>
    void ApplyUnitaryTransformation()
    {
        List<ComplexNumber> newAmplitudes = new List<ComplexNumber>();

        for (int i = 0; i < states.Count; i++)
        {
            ComplexNumber newAmplitude = ComplexNumber.Create(0, 0);
            for (int j = 0; j < states.Count; j++)
            {
                ComplexNumber product = ComplexNumber.Multiply(unitaryMatrix[i, j], states[j].Amplitude);
                newAmplitude = ComplexNumber.Add(newAmplitude, product);
            }
            newAmplitudes.Add(newAmplitude);
        }

        for (int i = 0; i < states.Count; i++)
        {
            states[i].SetAmplitude(newAmplitudes[i]);
        }

        NormalizeStateVector();
        Debug.Log("Applied unitary transformation. New amplitudes have been calculated.");
    }

    /// <summary>
    /// Collapses the quantum state based on probabilities.
    /// </summary>
    void Collapse()
    {
        if (isCollapsed) return;

        double cumulativeProbability = 0;
        double randomValue = UnityEngine.Random.Range(0f, 1f);

        foreach (var state in states)
        {
            double probability = state.Amplitude.GetMagnitude() * state.Amplitude.GetMagnitude();
            cumulativeProbability += probability;

            if (randomValue <= cumulativeProbability)
            {
                collapsedState = state;
                isCollapsed = true;
                Debug.Log($"Collapsed to state: {collapsedState.StateName}");
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collapse();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyUnitaryTransformation();
        }
    }
}
