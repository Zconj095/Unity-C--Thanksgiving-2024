using System;
using UnityEngine;

public class ShorsAlgorithm : QuantumAlgorithm
{
    private int numberToFactor;
    private int maxAttempts;

    public ShorsAlgorithm(int number, int maxAttempts = 10) : base("Shor's Algorithm")
    {
        numberToFactor = number;
        this.maxAttempts = maxAttempts;
    }

    public override void Execute(QuantumCircuit circuit, QuantumSimulator simulator)
    {
        Debug.Log($"Executing Shor's Algorithm to factor {numberToFactor}...");

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int guess = UnityEngine.Random.Range(2, numberToFactor); // Random guess
            int gcd = GreatestCommonDivisor(guess, numberToFactor);

            if (gcd > 1)
            {
                Debug.Log($"Found factor: {gcd}");
                return;
            }

            // Quantum steps
            int period = FindPeriod(circuit, simulator, guess);
            if (period > 0 && period % 2 == 0)
            {
                int factor1 = (int)Math.Pow(guess, period / 2) - 1;
                int factor2 = (int)Math.Pow(guess, period / 2) + 1;

                gcd = GreatestCommonDivisor(factor1, numberToFactor);
                if (gcd > 1)
                {
                    Debug.Log($"Found factors: {gcd} and {numberToFactor / gcd}");
                    return;
                }
            }
        }

        Debug.Log("Failed to find factors.");
    }

    private int FindPeriod(QuantumCircuit circuit, QuantumSimulator simulator, int guess)
    {
        // Placeholder: simulate finding the period via QFT
        Debug.Log($"Simulating period finding for guess {guess}...");
        return UnityEngine.Random.Range(2, 10); // Random period for demonstration
    }

    private int GreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
