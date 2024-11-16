using System;
using System.Collections.Generic;
using System.Linq;

public class PauliLindbladError : BaseQuantumError
{
    private readonly List<Pauli> _generators;
    private readonly List<double> _rates;

    public PauliLindbladError(List<Pauli> generators, List<double> rates) : base(generators.First().NumQubits)
    {
        if (generators.Count != rates.Count)
            throw new ArgumentException("Input generators and rates must have the same length.");

        _generators = new List<Pauli>(generators);
        _rates = new List<double>(rates);
    }

    public override string ToString()
    {
        return $"{GetType().Name}({string.Join(", ", _generators)}, {string.Join(", ", _rates)})";
    }

    public override bool Equals(object obj)
    {
        if (obj is not PauliLindbladError other) return false;
        if (!_generators.SequenceEqual(other._generators)) return false;
        return _rates.SequenceEqual(other._rates);
    }

    public override int GetHashCode()
    {
        return _generators.GetHashCode() ^ _rates.GetHashCode();
    }

    public int Size => _generators.Count;

    public List<Pauli> Generators => new List<Pauli>(_generators);

    public List<double> Rates => new List<double>(_rates);

    public override bool Ideal()
    {
        return _rates.All(rate => Math.Abs(rate) < 1e-8) || _generators.All(gen => gen.IsIdentity());
    }

    public bool IsCPTP(double atol = 1e-8)
    {
        return IsCP(atol);
    }

    public bool IsTP()
    {
        return true; // Always trace-preserving by construction
    }

    public bool IsCP(double atol = 1e-8)
    {
        return _rates.All(rate => rate >= -atol);
    }

    public PauliLindbladError Tensor(PauliLindbladError other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));

        var generators = _generators.SelectMany(g => other.Generators, (g1, g2) => g1.Tensor(g2)).ToList();
        var rates = _rates.Concat(other.Rates).ToList();
        return new PauliLindbladError(generators, rates);
    }

    public PauliLindbladError Expand(PauliLindbladError other)
    {
        return other.Tensor(this);
    }

    public PauliLindbladError Compose(PauliLindbladError other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));

        var generators = new List<Pauli>(_generators.Concat(other.Generators));
        var rates = new List<double>(_rates.Concat(other.Rates));
        return new PauliLindbladError(generators, rates);
    }

    public PauliLindbladError Power(double exponent)
    {
        var rates = _rates.Select(rate => rate * exponent).ToList();
        return new PauliLindbladError(_generators, rates);
    }

    public PauliLindbladError Inverse()
    {
        var rates = _rates.Select(rate => -rate).ToList();
        return new PauliLindbladError(_generators, rates);
    }

    public PauliLindbladError Simplify(double atol = 1e-8)
    {
        var nonZeroIndices = _rates
            .Select((rate, index) => new { rate, index })
            .Where(item => Math.Abs(item.rate) > atol)
            .Select(item => item.index)
            .ToList();

        var generators = nonZeroIndices.Select(index => _generators[index]).ToList();
        var rates = nonZeroIndices.Select(index => _rates[index]).ToList();

        return new PauliLindbladError(generators, rates);
    }

    public PauliError ToPauliError(bool simplify = true)
    {
        var paulis = new List<Pauli>();
        var probabilities = new List<double> { 1.0 };

        foreach (var (gen, rate) in _generators.Zip(_rates, (gen, rate) => (gen, rate)))
        {
            var prob = 0.5 - 0.5 * Math.Exp(-2 * rate);
            var identityProbabilities = probabilities.Select(p => (1 - prob) * p).ToList();
            var genProbabilities = probabilities.Select(p => prob * p).ToList();

            probabilities = identityProbabilities.Concat(genProbabilities).ToList();
            paulis = paulis.Concat(paulis.Select(p => p.Compose(gen))).ToList();
        }

        var error = new PauliError(paulis, probabilities);
        return simplify ? error.Simplify() : error;
    }

    public Dictionary<string, object> ToDict()
    {
        var instructions = _generators
            .Select(gen => new Dictionary<string, object>
            {
                { "name", "pauli" },
                { "params", new[] { gen.ToLabel() } }
            })
            .ToList();

        return new Dictionary<string, object>
        {
            { "type", "plerror" },
            { "id", Id },
            { "operations", new List<object>() },
            { "instructions", instructions },
            { "rates", _rates }
        };
    }

    public static PauliLindbladError FromDict(Dictionary<string, object> errorDict)
    {
        if (!errorDict.ContainsKey("instructions") || !errorDict.ContainsKey("rates"))
            throw new Exception("Invalid PauliLindbladError dictionary.");

        var instructions = errorDict["instructions"] as List<Dictionary<string, object>>;
        var rates = (errorDict["rates"] as List<object>).Select(Convert.ToDouble).ToList();

        if (instructions.Count != rates.Count)
            throw new Exception("Instructions and rates count mismatch.");

        var generators = instructions.Select(inst => Pauli.FromLabel(inst["params"].ToString())).ToList();
        return new PauliLindbladError(generators, rates);
    }
}
