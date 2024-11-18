using System;
using System.Collections.Generic;
using System.Linq;

public class DAGDependencyV2
{
    public string Name { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
    private PyDAG _multiGraph;
    public Dictionary<string, QuantumRegister> Qregs { get; set; }
    public Dictionary<string, ClassicalRegister> Cregs { get; set; }
    public List<Qubit> Qubits { get; set; }
    public List<Clbit> Clbits { get; set; }
    private Dictionary<Qubit, BitLocations> _qubitIndices;
    private Dictionary<Clbit, BitLocations> _clbitIndices;
    private double _globalPhase;
    private Dictionary<string, Dictionary<Tuple<int[], object[]>, Schedule>> _calibrations;  // Use Tuple<int[], object[]>
    private Dictionary<string, int> _opNames;
    public double Duration { get; set; }
    public string Unit { get; set; }
    public CommutationChecker CommChecker { get; set; }

    public DAGDependencyV2()
    {
        Name = null;
        Metadata = new Dictionary<string, object>();
        _multiGraph = new PyDAG();
        Qregs = new Dictionary<string, QuantumRegister>();
        Cregs = new Dictionary<string, ClassicalRegister>();
        Qubits = new List<Qubit>();
        Clbits = new List<Clbit>();
        _qubitIndices = new Dictionary<Qubit, BitLocations>();
        _clbitIndices = new Dictionary<Clbit, BitLocations>();
        _globalPhase = 0.0;
        _calibrations = new Dictionary<string, Dictionary<Tuple<int[], object[]>, Schedule>>();  // Update the type here
        _opNames = new Dictionary<string, int>();
        Duration = 0;
        Unit = "dt";
        CommChecker = new CommutationChecker();
    }

    public double GlobalPhase
    {
        get => _globalPhase;
        set
        {
            _globalPhase = (value % (2 * Math.PI));
        }
    }

    public Dictionary<string, Dictionary<Tuple<int[], object[]>, Schedule>> Calibrations
    {
        get => new Dictionary<string, Dictionary<Tuple<int[], object[]>, Schedule>>(_calibrations);
        set => _calibrations = new Dictionary<string, Dictionary<Tuple<int[], object[]>, Schedule>>(value);
    }

    public void AddCalibration(string gate, List<int> qubits, Schedule schedule, List<object> paramsList = null)
    {
        if (paramsList != null)
        {
            paramsList = paramsList.Select(p => p is float || p is int ? (object)Convert.ToDouble(p) : p).ToList();
        }
        else
        {
            paramsList = new List<object>();
        }

        // Update the key to Tuple<int[], object[]>
        _calibrations[gate] = new Dictionary<Tuple<int[], object[]>, Schedule>
        {
            { Tuple.Create(qubits.ToArray(), paramsList.ToArray()), schedule }
        };
    }

    public bool HasCalibrationFor(DAGOpNode node)
    {
        var qubits = node.Qargs.Select(q => Qubits.IndexOf(q)).ToArray();
        var paramsList = node.Op.Params.Select(p => p is ParameterExpression ? (object)Convert.ToDouble(p) : p).ToArray();
        return _calibrations.ContainsKey(node.Op.Name) && _calibrations[node.Op.Name].ContainsKey(Tuple.Create(qubits, paramsList));
    }



    public int Size()
    {
        return _multiGraph.Size();
    }

    public int Depth()
    {
        return _multiGraph.DagLongestPathLength();
    }

    public int Width()
    {
        return Qubits.Count + Clbits.Count;
    }

    public int NumQubits()
    {
        return Qubits.Count;
    }

    public int NumClbits()
    {
        return Clbits.Count;
    }

    public void AddQubits(IEnumerable<Qubit> qubits)
    {
        foreach (var qubit in qubits)
        {
            if (!Qubits.Contains(qubit))
            {
                Qubits.Add(qubit);
                _qubitIndices[qubit] = new BitLocations(Qubits.Count - 1, new List<Tuple<Register, int>>());
            }
        }
    }

    public void AddClbits(IEnumerable<Clbit> clbits)
    {
        foreach (var clbit in clbits)
        {
            if (!Clbits.Contains(clbit))
            {
                Clbits.Add(clbit);
                _clbitIndices[clbit] = new BitLocations(Clbits.Count - 1, new List<Tuple<Register, int>>());
            }
        }
    }

    public void AddQreg(QuantumRegister qreg)
    {
        if (Qregs.ContainsKey(qreg.Name))
            throw new Exception($"Duplicate register {qreg.Name}");

        Qregs[qreg.Name] = qreg;
        foreach (var qubit in qreg.Qubits)
        {
            if (!_qubitIndices.ContainsKey(qubit))
            {
                Qubits.Add(qubit);
                _qubitIndices[qubit] = new BitLocations(Qubits.Count - 1, new List<Tuple<QuantumRegister, int>> { Tuple.Create(qreg, Qubits.Count - 1) });
            }
        }
    }

    public void AddCreg(ClassicalRegister creg)
    {
        if (Cregs.ContainsKey(creg.Name))
            throw new Exception($"Duplicate register {creg.Name}");

        Cregs[creg.Name] = creg;
        foreach (var clbit in creg.Clbits)
        {
            if (!_clbitIndices.ContainsKey(clbit))
            {
                Clbits.Add(clbit);
                _clbitIndices[clbit] = new BitLocations(Clbits.Count - 1, new List<Tuple<ClassicalRegister, int>> { Tuple.Create(creg, Clbits.Count - 1) });
            }
        }
    }

    public BitLocations FindBit(Bit bit)
    {
        if (bit is Qubit qubit)
            return _qubitIndices[qubit];
        if (bit is Clbit clbit)
            return _clbitIndices[clbit];

        throw new Exception($"Could not locate bit of unknown type: {bit.GetType()}");
    }

    public void ApplyOperationBack(Operation operation, List<Qubit> qargs, List<Clbit> cargs)
    {
        var newNode = new DAGOpNode
        {
            Op = operation,
            Qargs = qargs,
            Cargs = cargs
        };
        newNode.NodeId = _multiGraph.AddNode(newNode);
        _UpdateEdges();
    }

    private void _UpdateEdges()
    {
        var maxNodeId = _multiGraph.Size() - 1;
        var maxNode = _multiGraph.GetNodeData(maxNodeId);

        var reachable = new List<bool>(new bool[maxNodeId]);

        for (int prevNodeId = maxNodeId - 1; prevNodeId >= 0; prevNodeId--)
        {
            if (reachable[prevNodeId])
            {
                var prevNode = _multiGraph.GetNodeData(prevNodeId);

                if (!CommChecker.Commute(prevNode.Op, prevNode.Qargs, prevNode.Cargs, maxNode.Op, maxNode.Qargs, maxNode.Cargs))
                {
                    _multiGraph.AddEdge(prevNodeId, maxNodeId, new { Commute = false });

                    foreach (var predecessorId in _multiGraph.GetPredecessorIndices(prevNodeId))
                    {
                        reachable[predecessorId] = false;
                    }
                }
            }
            else
            {
                foreach (var predecessorId in _multiGraph.GetPredecessorIndices(prevNodeId))
                {
                    reachable[predecessorId] = false;
                }
            }
        }
    }

    public void Draw(double scale = 0.7, string filename = null, string style = "color")
    {
        // You would need to implement drawing logic here, possibly using a C# graph library or exporting to an external tool
        Console.WriteLine("Drawing DAGDependencyV2 graph with style: " + style);
    }

    // Add more functions as needed for the rest of your DAG operations
}
