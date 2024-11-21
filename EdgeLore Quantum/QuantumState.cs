using System;
using UnityEngine;
using System.Numerics;

public class QuantumState
{
    public Complex[] StateVector { get; private set; }

    public QuantumState(int numQubits)
    {
        int stateSize = (int)Math.Pow(2, numQubits);
        StateVector = new Complex[stateSize];
        StateVector[0] = new Complex(1, 0);
    }

    public void ApplyGate(QuantumGate gate)
    {
        if (gate.Operation != null)
        {
            StateVector = gate.Operation(StateVector);
        }
        else
        {
            Debug.LogWarning($"Operation for {gate.Name} gate is not defined.");
        }
    }

    public override string ToString()
    {
        string result = "Quantum State:\n";
        for (int i = 0; i < StateVector.Length; i++)
        {
            result += $"|{Convert.ToString(i, 2).PadLeft((int)(Math.Log(StateVector.Length) / Math.Log(2)), '0')}⟩: {StateVector[i]}\n";
        }
        return result;
    }
}
