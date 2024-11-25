using System.Collections.Generic;
using System.Linq;
using System;
public class LLMQuantumState2
{
    public float[] Amplitudes { get; private set; }

    public LLMQuantumState2(int dimensions)
    {
        Amplitudes = new float[dimensions];
        Randomize();
    }

    public void Randomize()
    {
        Random random = new Random();
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            Amplitudes[i] = (float)(random.NextDouble()); // Random amplitudes between 0 and 1
        }
        Normalize();
    }

    private void Normalize()
    {
        float norm = (float)Math.Sqrt(Amplitudes.Sum(a => a * a));
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            Amplitudes[i] /= norm;
        }
    }

    public LLMQuantumState2 Interfere(LLMQuantumState2 other)
    {
        float[] result = new float[Amplitudes.Length];
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            result[i] = (Amplitudes[i] + other.Amplitudes[i]) / 2; // Quantum interference
        }
        return new LLMQuantumState2(result);
    }

    public float Measure()
    {
        // Quantum measurement returns the norm of the state
        return (float)Math.Sqrt(Amplitudes.Sum(a => a * a));
    }

    private LLMQuantumState2(float[] amplitudes)
    {
        Amplitudes = amplitudes;
    }
}
