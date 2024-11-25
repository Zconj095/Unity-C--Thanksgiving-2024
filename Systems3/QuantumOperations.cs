using System;
using System.Numerics; // For complex numbers

public class QuantumOperations
{
    public static LLMQuantumState ApplySuperposition(LLMQuantumState inputState)
    {
        int size = inputState.Amplitudes.Length;
        LLMQuantumState outputState = new LLMQuantumState(size);

        for (int i = 0; i < size; i++)
        {
            // Use a Complex number for division
            outputState.Amplitudes[i] = inputState.Amplitudes[i] / new Complex(Math.Sqrt(2), 0);
        }

        return outputState;
    }
}
