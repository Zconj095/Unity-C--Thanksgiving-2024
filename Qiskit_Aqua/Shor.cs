using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shor
{
    private int N;
    private int a;
    private QuantumCircuit circuit;
    private QuantumInstance quantumInstance;
    private List<int> factors;
    private int n;

    public Shor(int N, int a, QuantumInstance quantumInstance)
    {
        if (N < 3 || N % 2 == 0)
            throw new ArgumentException("N must be an odd integer greater than 2.");
        if (a >= N || GCD(a, N) != 1)
            throw new ArgumentException("a must satisfy 1 < a < N and GCD(a, N) = 1.");

        this.N = N;
        this.a = a;
        this.quantumInstance = quantumInstance;
        this.factors = new List<int>();
    }

    public List<int> Run()
    {
        if (IsPower(N, out int baseValue, out int power))
        {
            Debug.Log($"N is a power: {N} = {baseValue}^{power}");
            factors.Add(baseValue);
            return factors;
        }

        ConstructCircuit();
        ExecuteCircuit();

        return factors;
    }

    private void ConstructCircuit()
    {
        n = (int)Math.Ceiling(Math.Log(N, 2));
        var upRegister = new QuantumRegister(2 * n, "up");
        var downRegister = new QuantumRegister(n, "down");
        var auxRegister = new QuantumRegister(n + 2, "aux");
        circuit = new QuantumCircuit(upRegister, downRegister, auxRegister);

        // Apply Hadamard gates to the up register
        circuit.H(upRegister);

        // Initialize down register to |1⟩
        circuit.X(downRegister[0]);

        // Modular exponentiation
        for (int i = 0; i < 2 * n; i++)
        {
            int exp = (int)Math.Pow(a, Math.Pow(2, i));
            var controlledModMul = ControlledModularMultiplication(exp);
            circuit.Append(controlledModMul, new[] { upRegister[i] }.Concat(downRegister).Concat(auxRegister).ToArray());
        }

        // Apply inverse QFT
        circuit.Append(QFT.Inverse(2 * n), upRegister);
    }

    private QuantumGate ControlledModularMultiplication(int multiplier)
    {
        var modMulCircuit = new QuantumCircuit();
        // Create the modular multiplication circuit logic here...
        return modMulCircuit.ToGate();
    }

    private void ExecuteCircuit()
    {
        if (quantumInstance.IsStatevector)
        {
            var result = quantumInstance.Execute(circuit);
            var stateVector = result.GetStatevector();
            AnalyzeStateVector(stateVector);
        }
        else
        {
            circuit.AddMeasurement();
            var result = quantumInstance.Execute(circuit);
            AnalyzeMeasurement(result.GetCounts());
        }
    }

    private void AnalyzeMeasurement(Dictionary<string, int> counts)
    {
        foreach (var kvp in counts)
        {
            Debug.Log($"Measurement: {kvp.Key}, Count: {kvp.Value}");
            var factorsFromResult = GetFactors(kvp.Key);
            if (factorsFromResult != null)
                factors.AddRange(factorsFromResult);
        }
    }

    private void AnalyzeStateVector(Complex[] stateVector)
    {
        for (int i = 0; i < stateVector.Length; i++)
        {
            if (stateVector[i].Magnitude > 0)
            {
                var binaryResult = Convert.ToString(i, 2).PadLeft(2 * n, '0');
                var factorsFromResult = GetFactors(binaryResult);
                if (factorsFromResult != null)
                    factors.AddRange(factorsFromResult);
            }
        }
    }

    private List<int> GetFactors(string measurement)
    {
        int x = Convert.ToInt32(measurement, 2);
        int r = FindPeriod(x);
        if (r % 2 != 0) return null;

        int factor1 = GCD((int)Math.Pow(a, r / 2) - 1, N);
        int factor2 = GCD((int)Math.Pow(a, r / 2) + 1, N);

        if (factor1 != 1 && factor1 != N) return new List<int> { factor1, factor2 };
        return null;
    }

    private int FindPeriod(int x)
    {
        // Implement the continued fraction method to find the period (r)
        return 0;
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static bool IsPower(int N, out int baseValue, out int power)
    {
        for (int b = 2; b < Math.Sqrt(N) + 1; b++)
        {
            int p = 2;
            while (Math.Pow(b, p) <= N)
            {
                if (Math.Pow(b, p) == N)
                {
                    baseValue = b;
                    power = p;
                    return true;
                }
                p++;
            }
        }
        baseValue = power = 0;
        return false;
    }
}
