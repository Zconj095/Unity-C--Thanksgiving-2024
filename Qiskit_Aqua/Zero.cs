using System;
using System.Collections.Generic;

public class Zero:
{
    private int _numQubits;

    /// <summary>
    /// Initializes a new instance of the <see cref="Zero"/> class.
    /// </summary>
    /// <param name="numQubits">Number of qubits, must be at least 1.</param>
    public Zero(int numQubits)
    {
        if (numQubits < 1)
        {
            throw new ArgumentException("Number of qubits must be at least 1.");
        }

        _numQubits = numQubits;
    }

    /// <summary>
    /// Constructs the quantum state as either a vector or circuit.
    /// </summary>
    /// <param name="mode">The mode: "vector" for state vector, "circuit" for quantum circuit.</param>
    /// <returns>State vector or quantum circuit depending on the mode.</returns>
    public object Construct(string mode = "circuit")
    {
        if (mode == "vector")
        {
            // Return the zero state vector
            int size = (int)Math.Pow(2, _numQubits);
            double[] stateVector = new double[size];
            stateVector[0] = 1.0; // |0...0> state
            return stateVector;
        }
        else if (mode == "circuit")
        {
            // Return an empty circuit representing the zero state
            QuantumCircuit circuit = new QuantumCircuit(_numQubits);
            return circuit;
        }
        else
        {
            throw new ArgumentException("Mode should be either 'vector' or 'circuit'.");
        }
    }

    /// <summary>
    /// Gets the number of qubits for this zero state.
    /// </summary>
    public int NumQubits => _numQubits;
}
