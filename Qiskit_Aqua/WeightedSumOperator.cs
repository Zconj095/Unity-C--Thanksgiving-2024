using System;
using System.Collections.Generic;

public class WeightedSumOperator
{
    private int numStateQubits;
    private List<int> weights;
    private int numSumQubits;
    private int numCarryQubits;
    private List<int> stateIndices;
    private List<int> sumIndices;

    public WeightedSumOperator(int numStateQubits, List<int> weights, List<int> stateIndices = null, List<int> sumIndices = null)
    {
        // Validate inputs
        if (weights == null || weights.Count != numStateQubits)
        {
            throw new ArgumentException("Weights must have the same length as the number of state qubits.");
        }

        this.numStateQubits = numStateQubits;
        this.weights = new List<int>(weights);

        foreach (var weight in weights)
        {
            if (weight < 0)
            {
                throw new ArgumentException("Weights must be non-negative integers.");
            }
        }

        // Calculate number of sum and carry qubits
        numSumQubits = GetRequiredSumQubits(weights);
        numCarryQubits = Math.Max(0, numSumQubits - 1);

        // Initialize state and sum indices
        this.stateIndices = stateIndices ?? CreateSequentialList(0, numStateQubits);
        this.sumIndices = sumIndices ?? CreateSequentialList(numStateQubits, numStateQubits + numSumQubits);

        if (this.sumIndices.Count != numSumQubits)
        {
            throw new ArgumentException($"Sum indices list must have {numSumQubits} elements.");
        }
    }

    public void BuildCircuit(QuantumCircuit circuit, QuantumRegister stateRegister, QuantumRegister sumRegister, QuantumRegister ancillaryRegister = null)
    {
        if (stateRegister.Count != numStateQubits)
        {
            throw new ArgumentException("State register size does not match the number of state qubits.");
        }

        if (sumRegister.Count < numSumQubits)
        {
            throw new ArgumentException("Sum register must have enough qubits to represent the sum.");
        }

        int requiredAncillas = RequiredAncillas();
        if (ancillaryRegister != null && ancillaryRegister.Count < requiredAncillas)
        {
            throw new ArgumentException($"Ancillary register must have at least {requiredAncillas} qubits.");
        }

        // Build the weighted sum logic using controlled additions
        for (int i = 0; i < numStateQubits; i++)
        {
            int weight = weights[i];
            if (weight > 0)
            {
                AddWeightedControlledAddition(circuit, stateRegister[i], sumRegister, ancillaryRegister, weight);
            }
        }
    }

    private void AddWeightedControlledAddition(QuantumCircuit circuit, QuantumQubit controlQubit, QuantumRegister sumRegister, QuantumRegister ancillaryRegister, int weight)
    {
        // Encode the logic to add the weighted value to the sum register controlled by the input qubit
        int numBits = (int)Math.Ceiling(Math.Log2(weight + 1));
        for (int i = 0; i < numBits; i++)
        {
            if ((weight & (1 << i)) != 0)
            {
                circuit.CX(controlQubit, sumRegister[i]);
            }
        }
    }

    public int RequiredAncillas()
    {
        return numCarryQubits > 2 ? numCarryQubits + 1 : numCarryQubits;
    }

    public static int GetRequiredSumQubits(List<int> weights)
    {
        return (int)Math.Floor(Math.Log2(weights.Sum())) + 1;
    }

    private List<int> CreateSequentialList(int start, int count)
    {
        var list = new List<int>();
        for (int i = start; i < start + count; i++)
        {
            list.Add(i);
        }
        return list;
    }
}
