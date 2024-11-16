using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the S|Psi0> reflection factory for quantum operations.
/// </summary>
public class SPsi0Factory
{
    private readonly int numTargetQubits;
    private readonly int iObjective;

    /// <summary>
    /// Initializes the SPsi0Factory with the specified number of target qubits and the objective index.
    /// </summary>
    /// <param name="numTargetQubits">The number of target qubits.</param>
    /// <param name="iObjective">The index of the objective qubit.</param>
    public SPsi0Factory(int numTargetQubits, int iObjective)
    {
        if (numTargetQubits < 1)
            throw new ArgumentException("Number of target qubits must be at least 1.", nameof(numTargetQubits));

        if (iObjective < 0 || iObjective >= numTargetQubits)
            throw new ArgumentException("Objective index must be within the range of target qubits.", nameof(iObjective));

        this.numTargetQubits = numTargetQubits;
        this.iObjective = iObjective;
    }

    /// <summary>
    /// Returns the number of ancillas required for the S|Psi0> reflection.
    /// </summary>
    public int RequiredAncillas()
    {
        return numTargetQubits == 1 ? 0 : 1;
    }

    /// <summary>
    /// Returns the number of controlled ancillas required for the S|Psi0> reflection.
    /// </summary>
    public int RequiredAncillasControlled()
    {
        return numTargetQubits == 1 ? 0 : 1;
    }

    /// <summary>
    /// Builds the S|Psi0> reflection circuit.
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
            if (qAncillas == null || qAncillas.Length < RequiredAncillas())
                throw new ArgumentException("Insufficient ancilla qubits provided.", nameof(qAncillas));

            Qubit ancilla = qAncillas[0];

            // Prepare ancilla in superposition and apply reflection logic
            qc.AddGate("X", ancilla);
            qc.AddGate("H", ancilla);

            // Apply X to the objective qubit, perform CNOT to the ancilla, and undo X
            qc.AddGate("X", q[iObjective]);
            qc.AddGate("CX", q[iObjective], ancilla);
            qc.AddGate("X", q[iObjective]);

            // Undo the superposition on the ancilla
            qc.AddGate("H", ancilla);
            qc.AddGate("X", ancilla);
        }
    }
}
