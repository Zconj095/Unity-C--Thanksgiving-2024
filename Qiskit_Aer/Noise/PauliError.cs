using System;
using System.Collections.Generic;
using System.Linq;

public class PauliError : BaseQuantumError
{
    private readonly List<Pauli> _paulis;
    private readonly List<double> _probabilities;

    public PauliError(List<Pauli> paulis, List<double> probabilities) : base(paulis.First().NumQubits)
    {
        if (paulis.Count != probabilities.Count)
            throw new Exception("Input Paulis and probabilities must have the same length.");

        _paulis = new List<Pauli>(paulis);
        _probabilities = new List<double>(probabilities);
    }

    public override string ToString()
    {
        return $"{GetType().Name}({string.Join(", ", _paulis)}, {string.Join(", ", _probabilities)})";
    }

    public override bool Equals(object obj)
    {
        if (obj is not PauliError other) return false;
        if (!_paulis.SequenceEqual(other._paulis)) return false;

        return _probabilities.SequenceEqual(other._probabilities);
    }

    public override int GetHashCode()
    {
        return _paulis.GetHashCode() ^ _probabilities.GetHashCode();
    }

    public int Size => _paulis.Count;

    public List<Pauli> Paulis => new List<Pauli>(_paulis);

    public List<double> Probabilities => new List<double>(_probabilities);

    public override bool Ideal()
    {
        return IsCPTP() && _paulis.All(pauli => pauli.IsIdentity());
    }

    public bool IsCPTP(double atol = 1e-8, double rtol = 1e-5)
    {
        return IsCP(atol, rtol) && IsTP(atol, rtol);
    }

    public bool IsTP(double atol = 1e-8, double rtol = 1e-5)
    {
        return Math.Abs(_probabilities.Sum() - 1) <= atol + rtol * Math.Abs(1);
    }

    public bool IsCP(double atol = 1e-8, double rtol = 1e-5)
    {
        return _probabilities.All(p => p >= -atol);
    }

    public PauliError Tensor(PauliError other)
    {
        var tensorPaulis = _paulis.SelectMany(p => other.Paulis, (p1, p2) => p1.Tensor(p2)).ToList();
        var tensorProbs = _probabilities.SelectMany(p => other.Probabilities, (p1, p2) => p1 * p2).ToList();
        return new PauliError(tensorPaulis, tensorProbs);
    }

    public PauliError Expand(PauliError other)
    {
        return other.Tensor(this);
    }

    public PauliError Compose(PauliError other)
    {
        var composedPaulis = new List<Pauli>();
        var composedProbs = new List<double>();

        foreach (var pauli1 in _paulis)
        {
            foreach (var pauli2 in other.Paulis)
            {
                composedPaulis.Add(pauli1.Compose(pauli2));
                composedProbs.Add(_probabilities[_paulis.IndexOf(pauli1)] * other.Probabilities[other.Paulis.IndexOf(pauli2)]);
            }
        }

        return new PauliError(composedPaulis, composedProbs);
    }

    public PauliError Simplify(double atol = 1e-8, double rtol = 1e-5)
    {
        var simplifiedPaulis = new List<Pauli>();
        var simplifiedProbs = new List<double>();

        for (int i = 0; i < _paulis.Count; i++)
        {
            if (Math.Abs(_probabilities[i]) > atol)
            {
                simplifiedPaulis.Add(_paulis[i]);
                simplifiedProbs.Add(_probabilities[i]);
            }
        }

        return new PauliError(simplifiedPaulis, simplifiedProbs);
    }

    public Dictionary<string, object> ToDict()
    {
        return new Dictionary<string, object>
        {
            { "type", "qerror" },
            { "id", Id },
            { "operations", new List<string>() },
            { "instructions", _paulis.Select(p => new { name = "pauli", @params = new[] { p.ToLabel() } }).ToList() },
            { "probabilities", _probabilities }
        };
    }

    public static PauliError FromDict(Dictionary<string, object> errorDict)
    {
        if (!errorDict.ContainsKey("instructions") || !errorDict.ContainsKey("probabilities"))
            throw new Exception("Invalid PauliError dictionary.");

        var instructions = errorDict["instructions"] as List<Dictionary<string, object>>;
        var probabilities = errorDict["probabilities"] as List<double>;

        if (instructions.Count != probabilities.Count)
            throw new Exception("Instructions and probabilities count mismatch.");

        var paulis = instructions.Select(inst => Pauli.FromLabel(inst["params"].ToString())).ToList();
        return new PauliError(paulis, probabilities);
    }
}

public class Pauli
{
    // Placeholder implementation for Pauli class.
    public int NumQubits { get; private set; }

    public bool IsIdentity() => true; // Replace with actual logic.

    public string ToLabel() => "I"; // Replace with actual label.

    public Pauli Tensor(Pauli other) => new Pauli(); // Implement tensor logic.

    public Pauli Compose(Pauli other) => new Pauli(); // Implement compose logic.
}
