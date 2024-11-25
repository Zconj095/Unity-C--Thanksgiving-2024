using System;
using System.Numerics; // For complex numbers
using System.Collections.Generic;
using System.Linq;

public class QuantumMeasurement
{
    public static int Measure(LLMQuantumState state)
    {
        Random random = new Random();
        double cumulativeProbability = 0;
        double randomValue = random.NextDouble();

        for (int i = 0; i < state.Amplitudes.Length; i++)
        {
            cumulativeProbability += Math.Pow(state.Amplitudes[i].Magnitude, 2);
            if (randomValue <= cumulativeProbability)
            {
                return i; // Return the measured state index
            }
        }

        return state.Amplitudes.Length - 1; // Fallback to the last state
    }
}
