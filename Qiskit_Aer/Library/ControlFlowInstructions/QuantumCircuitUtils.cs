using System;
using System.Collections.Generic;

public static class QuantumCircuitUtils
{
    /// <summary>
    /// Helper method to return a list of qubits.
    /// </summary>
    /// <param name="circuit">A quantum circuit.</param>
    /// <param name="qubits">Optional list of qubits. If null, all qubits in the circuit are returned.</param>
    /// <returns>List of qubits.</returns>
    /// <exception cref="ArgumentException">Thrown if the circuit has no qubits and qubits parameter is null.</exception>
    public static List<Qubit> DefaultQubits(QuantumCircuit circuit, List<Qubit> qubits = null)
    {
        // If no qubits are specified, use all qubits in the circuit
        if (qubits == null)
        {
            qubits = circuit.GetQubits();
            if (qubits.Count == 0)
            {
                throw new ArgumentException("No qubits available for snapshot.");
            }
        }

        return qubits;
    }
}
