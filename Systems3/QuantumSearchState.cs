using System.Collections.Generic;
using System.Numerics; // For Complex numbers
using System;

public class QuantumSearchState
{
    public Complex[] Amplitudes { get; private set; }

    public QuantumSearchState(int size)
    {
        Amplitudes = new Complex[size];
        InitializeUniformState();
    }

    private void InitializeUniformState()
    {
        double normalizationFactor = 1.0 / Math.Sqrt(Amplitudes.Length);
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            Amplitudes[i] = new Complex(normalizationFactor, 0); // Initialize with real normalization factor
        }
    }

    public void ApplyOracle(Func<int, bool> oracle)
    {
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            if (oracle(i)) // Flip the amplitude for the target states
            {
                Amplitudes[i] = Amplitudes[i] * new Complex(-1, 0); // Multiply by -1 as a Complex
            }
        }
    }

    public void ApplyDiffusion()
    {
        Complex meanAmplitude = new Complex(0, 0);
        foreach (var amplitude in Amplitudes)
        {
            meanAmplitude += amplitude;
        }
        meanAmplitude /= new Complex(Amplitudes.Length, 0); // Divide by the size as a Complex

        for (int i = 0; i < Amplitudes.Length; i++)
        {
            Amplitudes[i] = (new Complex(2, 0) * meanAmplitude) - Amplitudes[i]; // Reflect around the mean
        }
    }

    public int Measure()
    {
        System.Random random = new System.Random();
        double randomValue = random.NextDouble();
        double cumulativeProbability = 0;

        for (int i = 0; i < Amplitudes.Length; i++)
        {
            cumulativeProbability += Math.Pow(Amplitudes[i].Magnitude, 2);
            if (randomValue <= cumulativeProbability)
            {
                return i; // Return the measured state index
            }
        }

        return Amplitudes.Length - 1; // Fallback
    }
}
