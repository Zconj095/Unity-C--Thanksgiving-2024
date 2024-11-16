using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the S|0> reflection factory for quantum operations.
/// </summary>
public class S0Factory
{
    private readonly int numTargetQubits;

    /// <summary>
    /// Initializes the S0Factory with the specified number of target qubits.
    /// </summary>
    /// <param name="numTargetQubits">The number of target qubits.</param>
    public S0Factory(int numTargetQubits)
    {
        if (numTargetQubits < 1)
            throw new ArgumentException("Number of target qubits must be at least 1.", nameof(numTargetQubits));

        this.numTargetQubits = numTargetQubits;
    }

    /// <summary>
    /// Returns the number of ancillas required for the S|0> reflection.
    /// </summary>
    public int RequiredAncillas()
    {
        return numTargetQubits == 1 ? 0 : Math.Max(1, numTargetQubits - 1);
    }

    /// <summary>
    /// Returns the number of controlled ancillas required for the S|0> reflection.
    /// </summary>
    public int RequiredAncillasControlled()
    {
        return numTargetQubits == 1 ? 0 : numTargetQubits;
    }

    /// <summary>
    /// Builds the S|0> reflection circuit.
    /// </summary>
    /// <param name="qc">The quantum circuit to add the reflection to.</param>
    /// <param name="q">The array of target qubits.</param>
    /// <param name="qAncillas">The array of ancilla qubits.</param>
    public void Build(QuantumCircuit qc, Qubit[] q, Qubit[] qAncillas = null)
    {
        if (qc == null || q == null)
            throw new ArgumentNullException("QuantumCircuit or qubits cannot be null.");

        if (numTargetQubits == 1)
        {
            // Apply Z gate directly for a single qubit
            qc.AddGate("Z", q[0]);
        }
        else
        {
            // Apply X gates to all target qubits
            foreach (var qubit in q)
            {
                qc.AddGate("X", qubit);
            }

            // Apply multi-controlled Z using ancilla qubits
            if (qAncillas == null || qAncillas.Length < RequiredAncillas())
                throw new ArgumentException("Insufficient ancilla qubits provided.", nameof(qAncillas));

            // Prepare the first ancilla for the controlled operation
            qc.AddGate("X", qAncillas[0]);
            qc.AddGate("H", qAncillas[0]);

            // Apply the multi-controlled operation
            var controlQubits = new List<Qubit>(q);
            var ancillaList = new List<Qubit>(qAncillas);
            qc.AddMultiControlledGate("Z", controlQubits.ToArray(), qAncillas[0], ancillaList.GetRange(1, ancillaList.Count - 1).ToArray());

            // Revert the first ancilla back to its original state
            qc.AddGate("H", qAncillas[0]);
            qc.AddGate("X", qAncillas[0]);

            // Undo the X gates on all target qubits
            foreach (var qubit in q)
            {
                qc.AddGate("X", qubit);
            }
        }
    }
}
