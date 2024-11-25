using System;
using System.Numerics; // For complex numbers

public class LLMQuantumState
{
    public Complex[] Amplitudes { get; private set; }

    public LLMQuantumState(int size)
    {
        Amplitudes = new Complex[size];
        InitializeRandomState();
    }

    private void InitializeRandomState()
    {
        System.Random random = new System.Random();
        double magnitudeSum = 0;

        for (int i = 0; i < Amplitudes.Length; i++)
        {
            double real = random.NextDouble();
            double imaginary = random.NextDouble();
            Amplitudes[i] = new Complex(real, imaginary);
            magnitudeSum += Math.Pow(real, 2) + Math.Pow(imaginary, 2);
        }

        // Normalize amplitudes to ensure total probability equals 1
        double normFactor = Math.Sqrt(magnitudeSum);
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            // Use a Complex number for division
            Amplitudes[i] /= new Complex(normFactor, 0);
        }
    }

    public void PrintState()
    {
        for (int i = 0; i < Amplitudes.Length; i++)
        {
            Console.WriteLine($"State {i}: {Amplitudes[i]}");
        }
    }
}
