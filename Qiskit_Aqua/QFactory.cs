using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the QFactory responsible for constructing the Grover operator Q,
/// which includes specific reflection gates and the problem unitary A.
/// </summary>
public class QFactory
{
    private readonly AFactory aFactory;
    private readonly int iObjective;
    private readonly SPsi0Factory sPsi0ReflectionFactory;
    private readonly S0Factory s0ReflectionFactory;
    private readonly int numAncillas;
    private readonly int numAncillasControlled;
    private readonly int numQubits;
    private readonly int numQubitsControlled;

    /// <summary>
    /// Initializes a new QFactory instance.
    /// </summary>
    public QFactory(AFactory aFactory, int iObjective)
    {
        if (aFactory == null)
            throw new ArgumentNullException(nameof(aFactory));

        this.aFactory = aFactory;
        this.iObjective = iObjective;

        // Initialize reflection factories
        sPsi0ReflectionFactory = new SPsi0Factory(aFactory.NumTargetQubits, iObjective);
        s0ReflectionFactory = new S0Factory(aFactory.NumTargetQubits);

        // Calculate ancilla requirements
        numAncillas = Math.Max(aFactory.RequiredAncillas(),
            Math.Max(sPsi0ReflectionFactory.RequiredAncillas(), s0ReflectionFactory.RequiredAncillas()));

        numAncillasControlled = Math.Max(aFactory.RequiredAncillas(),
            Math.Max(sPsi0ReflectionFactory.RequiredAncillasControlled(), s0ReflectionFactory.RequiredAncillasControlled()));

        // Calculate total qubits
        numQubits = aFactory.NumTargetQubits + numAncillas;
        numQubitsControlled = aFactory.NumTargetQubits + numAncillasControlled;
    }

    /// <summary>
    /// Returns the number of ancilla qubits required.
    /// </summary>
    public int RequiredAncillas()
    {
        return numAncillas;
    }

    /// <summary>
    /// Returns the number of controlled ancilla qubits required.
    /// </summary>
    public int RequiredAncillasControlled()
    {
        return numAncillasControlled;
    }

    /// <summary>
    /// Builds the Grover operator Q circuit.
    /// </summary>
    public void Build(QuantumCircuit qc, Qubit[] q, Qubit[] qAncillas = null)
    {
        if (qc == null || q == null)
            throw new ArgumentNullException("QuantumCircuit or qubits cannot be null.");

        sPsi0ReflectionFactory.Build(qc, q, qAncillas);
        aFactory.BuildInverse(qc, q, qAncillas);
        s0ReflectionFactory.Build(qc, q, qAncillas);
        aFactory.Build(qc, q, qAncillas);
    }

    /// <summary>
    /// Builds the controlled version of the Grover operator Q circuit.
    /// </summary>
    public void BuildControlled(QuantumCircuit qc, Qubit[] q, Qubit[] qControl, Qubit[] qAncillas = null)
    {
        if (qc == null || q == null || qControl == null)
            throw new ArgumentNullException("QuantumCircuit, qubits, or control qubits cannot be null.");

        sPsi0ReflectionFactory.BuildControlled(qc, q, qControl, qAncillas);
        aFactory.BuildInverse(qc, q, qAncillas);
        s0ReflectionFactory.BuildControlled(qc, q, qControl, qAncillas);
        aFactory.Build(qc, q, qAncillas);
    }
}

/// <summary>
/// Represents the AFactory responsible for the problem unitary.
/// </summary>
public class AFactory
{
    public int NumTargetQubits { get; private set; }

    public AFactory(int numTargetQubits)
    {
        NumTargetQubits = numTargetQubits;
    }

    public int RequiredAncillas()
    {
        // Example: No additional ancillas needed for the A operation.
        return 0;
    }

    public void Build(QuantumCircuit qc, Qubit[] q, Qubit[] qAncillas)
    {
        if (qc == null || q == null)
            throw new ArgumentNullException("QuantumCircuit or qubits cannot be null.");

        // Apply A operation (e.g., Hadamard gates to all qubits as a simple example)
        foreach (var qubit in q)
        {
            qc.AddGate("H", qubit);
        }
    }

    public void BuildInverse(QuantumCircuit qc, Qubit[] q, Qubit[] qAncillas)
    {
        if (qc == null || q == null)
            throw new ArgumentNullException("QuantumCircuit or qubits cannot be null.");

        // Apply A† (inverse of A). For this simple example, Hadamard is its own inverse.
        foreach (var qubit in q)
        {
            qc.AddGate("H", qubit);
        }
    }
}
