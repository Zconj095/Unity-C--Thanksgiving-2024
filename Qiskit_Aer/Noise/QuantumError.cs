using System;
using System.Collections.Generic;
using System.Linq;

public class QuantumError
{
    private List<QuantumCircuit> _circuits;
    private List<double> _probabilities;

    public QuantumError(IEnumerable<(QuantumCircuit Circuit, double Probability)> noiseOps)
    {
        if (noiseOps == null || !noiseOps.Any())
        {
            throw new ArgumentException("noiseOps must contain at least one operator with non-zero probability.");
        }

        var ops = noiseOps.Where(op => op.Probability > 0).ToList();

        if (!ops.Any())
        {
            throw new ArgumentException("noiseOps must contain at least one operator with non-zero probability.");
        }

        double totalProb = ops.Sum(op => op.Probability);
        if (Math.Abs(totalProb - 1) > 1e-8)
        {
            throw new ArgumentException("Probabilities are not normalized.");
        }

        _probabilities = ops.Select(op => op.Probability / totalProb).ToList();
        _circuits = ops.Select(op => EnsureValidCircuit(op.Circuit)).ToList();

        int maxQubits = _circuits.Max(c => c.NumQubits);
        _circuits = _circuits.Select(c => EnlargeCircuit(c, maxQubits)).ToList();
    }

    public int Size => _circuits.Count;
    public List<QuantumCircuit> Circuits => _circuits;
    public List<double> Probabilities => _probabilities;

    private static QuantumCircuit EnsureValidCircuit(QuantumCircuit circuit)
    {
        if (circuit.ClBits.Any())
        {
            throw new ArgumentException("Circuit with classical register cannot be a channel.");
        }

        return circuit;
    }

    private static QuantumCircuit EnlargeCircuit(QuantumCircuit circuit, int numQubits)
    {
        if (circuit.NumQubits < numQubits)
        {
            var enlarged = new QuantumCircuit(numQubits);
            enlarged.Compose(circuit);
            return enlarged;
        }

        return circuit;
    }

    public QuantumError Compose(QuantumError other, bool front = false)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        var composedCircuits = new List<QuantumCircuit>();
        var composedProbabilities = new List<double>();

        foreach (var lCircuit in _circuits)
        {
            foreach (var rCircuit in other._circuits)
            {
                composedCircuits.Add(front ? rCircuit.Compose(lCircuit) : lCircuit.Compose(rCircuit));
                composedProbabilities.Add(_probabilities[_circuits.IndexOf(lCircuit)] * other._probabilities[other._circuits.IndexOf(rCircuit)]);
            }
        }

        return new QuantumError(composedCircuits.Zip(composedProbabilities, (circ, prob) => (circ, prob)));
    }

    public QuantumError Tensor(QuantumError other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        var tensorCircuits = new List<QuantumCircuit>();
        var tensorProbabilities = new List<double>();

        foreach (var lCircuit in _circuits)
        {
            foreach (var rCircuit in other._circuits)
            {
                tensorCircuits.Add(lCircuit.Tensor(rCircuit));
                tensorProbabilities.Add(_probabilities[_circuits.IndexOf(lCircuit)] * other._probabilities[other._circuits.IndexOf(rCircuit)]);
            }
        }

        return new QuantumError(tensorCircuits.Zip(tensorProbabilities, (circ, prob) => (circ, prob)));
    }

    public QuantumError Expand(QuantumError other)
    {
        return other.Tensor(this);
    }

    public bool IsIdeal()
    {
        foreach (var circuit in _circuits)
        {
            if (!circuit.IsIdentity())
            {
                return false;
            }
        }

        return true;
    }

    public Dictionary<string, object> ToDict()
    {
        var instructions = _circuits.Select(circuit =>
            circuit.Instructions.Select(inst => inst.ToDict()).ToList()).ToList();

        return new Dictionary<string, object>
        {
            { "type", "qerror" },
            { "instructions", instructions },
            { "probabilities", _probabilities }
        };
    }

    public static QuantumError FromDict(Dictionary<string, object> dict)
    {
        if (!dict.ContainsKey("instructions") || !dict.ContainsKey("probabilities"))
        {
            throw new ArgumentException("Invalid error dictionary.");
        }

        var instructions = (List<List<Dictionary<string, object>>>)dict["instructions"];
        var probabilities = ((List<object>)dict["probabilities"]).Select(Convert.ToDouble).ToList();

        if (instructions.Count != probabilities.Count)
        {
            throw new ArgumentException("Mismatch between instructions and probabilities.");
        }

        var noiseOps = new List<(QuantumCircuit, double)>();

        for (int i = 0; i < instructions.Count; i++)
        {
            var circuit = QuantumCircuit.FromInstructions(instructions[i]);
            noiseOps.Add((circuit, probabilities[i]));
        }

        return new QuantumError(noiseOps);
    }

    public override string ToString()
    {
        var output = $"QuantumError on {_circuits.First().NumQubits} qubits:";
        for (int i = 0; i < _circuits.Count; i++)
        {
            output += $"\nP({i}) = {_probabilities[i]}, Circuit = {_circuits[i]}";
        }
        return output;
    }
}
