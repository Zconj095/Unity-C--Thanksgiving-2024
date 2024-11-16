using System;
using System.Collections.Generic;
using System.Linq;

public class DepolarizingError
{
    public static QuantumError DepolarizingError(double param, int numQubits)
    {
        if (numQubits < 1)
        {
            throw new ArgumentException("numQubits must be a positive integer.");
        }

        int numTerms = (int)Math.Pow(4, numQubits);
        double maxParam = (double)numTerms / (numTerms - 1);

        if (param < 0 || param > maxParam)
        {
            throw new ArgumentException($"Depolarizing parameter must be between 0 and {maxParam}.");
        }

        double probIden = 1 - param / maxParam;
        double probPauli = param / numTerms;

        List<double> probs = new List<double> { probIden };
        probs.AddRange(Enumerable.Repeat(probPauli, numTerms - 1));

        List<Pauli> paulis = GeneratePauliTerms(numQubits);
        return new QuantumError(paulis.Zip(probs, (pauli, prob) => (pauli, prob)));
    }

    private static List<Pauli> GeneratePauliTerms(int numQubits)
    {
        char[] basis = { 'I', 'X', 'Y', 'Z' };
        List<Pauli> paulis = new List<Pauli>();

        foreach (var combination in CartesianProduct(Enumerable.Repeat(basis, numQubits).ToArray()))
        {
            paulis.Add(new Pauli(new string(combination.ToArray())));
        }

        return paulis;
    }

    private static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<T>[] sequences)
    {
        IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };

        foreach (var sequence in sequences)
        {
            result = result.SelectMany(_ => sequence, (accumulated, item) => accumulated.Concat(new[] { item }));
        }

        return result;
    }
}
