using System;
using System.Collections.Generic;
using System.Linq;

public class DAGDependency
{
    public string Name { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
    private PyDAG _multiGraph;
    public Dictionary<string, QuantumRegister> Qregs { get; set; }
    public Dictionary<string, ClassicalRegister> Cregs { get; set; }
    public List<Qubit> Qubits { get; set; }
    public List<Clbit> Clbits { get; set; }
    private double _globalPhase;
    private Dictionary<string, Dictionary<CalibrationKey, Schedule>> _calibrations;

    public double Duration { get; set; }
    public string Unit { get; set; }
    public CommutationChecker CommChecker { get; set; }

    public DAGDependency()
    {
        Name = null;
        Metadata = new Dictionary<string, object>();
        _multiGraph = new PyDAG();
        Qregs = new Dictionary<string, QuantumRegister>();
        Cregs = new Dictionary<string, ClassicalRegister>();
        Qubits = new List<Qubit>();
        Clbits = new List<Clbit>();
        _globalPhase = 0.0;
        _calibrations = new Dictionary<string, Dictionary<CalibrationKey, Schedule>>(); // Using CalibrationKey
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

    public Dictionary<string, Dictionary<CalibrationKey, Schedule>> Calibrations
    {
        get => _calibrations;
        set => _calibrations = new Dictionary<string, Dictionary<CalibrationKey, Schedule>>(value);
    }

    public PyDAG ToRetworkx()
    {
        return _multiGraph;
    }

    public int Size()
    {
        return _multiGraph.Size();
    }

    public int Depth()
    {
        int depth = _multiGraph.DagLongestPathLength();
        return depth >= 0 ? depth : 0;
    }

    public void AddQubits(IEnumerable<Qubit> qubits)
    {
        foreach (var qubit in qubits)
        {
            if (Qubits.Contains(qubit)) throw new Exception("Duplicate qubits detected.");
            Qubits.Add(qubit);
        }
    }

    public void AddClbits(IEnumerable<Clbit> clbits)
    {
        foreach (var clbit in clbits)
        {
            if (Clbits.Contains(clbit)) throw new Exception("Duplicate clbits detected.");
            Clbits.Add(clbit);
        }
    }

    public void AddQreg(QuantumRegister qreg)
    {
        if (Qregs.ContainsKey(qreg.Name)) throw new Exception("Duplicate register detected.");
        Qregs[qreg.Name] = qreg;
        foreach (var qubit in qreg.Qubits)
        {
            if (!Qubits.Contains(qubit)) Qubits.Add(qubit);
        }
    }

    public void AddCreg(ClassicalRegister creg)
    {
        if (Cregs.ContainsKey(creg.Name)) throw new Exception("Duplicate register detected.");
        Cregs[creg.Name] = creg;
        foreach (var clbit in creg.Clbits)
        {
            if (!Clbits.Contains(clbit)) Clbits.Add(clbit);
        }
    }

    public int AddMultiGraphNode(DAGDepNode node)
    {
        int nodeId = _multiGraph.AddNode(node);
        node.NodeId = nodeId;
        return nodeId;
    }

    public IEnumerable<DAGDepNode> GetNodes()
    {
        return _multiGraph.Nodes;
    }

    public DAGDepNode GetNode(int nodeId)
    {
        return _multiGraph.GetNodeData(nodeId);
    }

    public void AddMultiGraphEdge(int srcId, int destId, object data)
    {
        _multiGraph.AddEdge(srcId, destId, data);
    }

    public List<Tuple<int, int, object>> GetAllEdges()
    {
        return _multiGraph.GetAllEdges().Select(edge => Tuple.Create(edge.Item1, edge.Item2, edge.Item3)).ToList();
    }

    public List<int> GetInEdges(int nodeId)
    {
        return _multiGraph.InEdges(nodeId);
    }

    public List<int> GetOutEdges(int nodeId)
    {
        return _multiGraph.OutEdges(nodeId);
    }

    public List<int> DirectSuccessors(int nodeId)
    {
        return _multiGraph.DirectSuccessors(nodeId).OrderBy(id => id).ToList();
    }

    public List<int> DirectPredecessors(int nodeId)
    {
        return _multiGraph.DirectPredecessors(nodeId).OrderBy(id => id).ToList();
    }

    public List<int> Successors(int nodeId)
    {
        return _multiGraph.GetNodeData(nodeId).Successors;
    }

    public List<int> Predecessors(int nodeId)
    {
        return _multiGraph.GetNodeData(nodeId).Predecessors;
    }

    public IEnumerable<DAGDepNode> TopologicalNodes()
    {
        return _multiGraph.TopologicalSort();
    }

    public DAGDepNode CreateOpNode(Operation operation, List<Qubit> qargs, List<Clbit> cargs)
    {
        var qindices = qargs.Select(q => Qubits.IndexOf(q)).ToList();
        var cindices = cargs.Select(c => Clbits.IndexOf(c)).ToList();

        return new DAGDepNode
        {
            Type = "op",
            Op = operation,
            Name = operation.Name,
            Qargs = qargs,
            Cargs = cargs,
            Successors = new List<DAGDepNode>(),
            Predecessors = new List<DAGDepNode>(),
            Qindices = qindices,
            Cindices = cindices
        };
    }

    public void AddOpNode(Operation operation, List<Qubit> qargs, List<Clbit> cargs)
    {
        var newNode = CreateOpNode(operation, qargs, cargs);
        AddMultiGraphNode(newNode);
        UpdateEdges();
    }

    public void UpdateEdges()
    {
        var maxNodeId = _multiGraph.Size() - 1;
        var maxNode = GetNode(maxNodeId);

        var reachable = new List<bool>(new bool[maxNodeId]);

        for (var prevNodeId = maxNodeId - 1; prevNodeId >= 0; prevNodeId--)
        {
            if (reachable[prevNodeId])
            {
                var prevNode = GetNode(prevNodeId);

                if (!CommChecker.Commute(prevNode.Op, prevNode.Qargs, prevNode.Cargs, maxNode.Op, maxNode.Qargs, maxNode.Cargs))
                {
                    _multiGraph.AddEdge(prevNodeId, maxNodeId, new { commute = false });

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

    public void AddSuccessors()
    {
        for (int nodeId = _multiGraph.Size() - 1; nodeId >= 0; nodeId--)
        {
            _multiGraph.GetNodeData(nodeId).Successors = _multiGraph.Descendants(nodeId).ToList();
        }
    }

    public void AddPredecessors()
    {
        for (int nodeId = 0; nodeId < _multiGraph.Size(); nodeId++)
        {
            _multiGraph.GetNodeData(nodeId).Predecessors = _multiGraph.Ancestors(nodeId).ToList();
        }
    }

    public DAGDependency Copy()
    {
        var dagCopy = new DAGDependency
        {
            Name = Name,
            Qregs = new Dictionary<string, QuantumRegister>(Qregs),
            Cregs = new Dictionary<string, ClassicalRegister>(Cregs)
        };

        foreach (var node in GetNodes())
        {
            dagCopy._multiGraph.AddNode(node.Copy());
        }

        foreach (var edge in GetAllEdges())
        {
            dagCopy._multiGraph.AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        return dagCopy;
    }

    public void Draw(double scale = 0.7, string filename = null, string style = "color")
    {
        // Drawing logic using PyGraph (or equivalent library in C#)
        Console.WriteLine("Drawing graph with style: " + style);
    }

    public void ReplaceBlockWithOp(List<DAGDepNode> nodeBlock, Operation op, Dictionary<Qubit, int> wirePosMap, bool cycleCheck = true)
    {
        var blockQargs = new HashSet<Qubit>();
        var blockCargs = new HashSet<Clbit>();
        var blockIds = nodeBlock.Select(node => node.NodeId).ToList();

        if (nodeBlock.Count == 0) throw new Exception("Can't replace an empty node block.");

        foreach (var node in nodeBlock)
        {
            blockQargs.UnionWith(node.Qargs);
            blockCargs.UnionWith(node.Cargs);
        }

        var newNode = CreateOpNode(op, blockQargs.OrderBy(qb => wirePosMap[qb]).ToList(), blockCargs.OrderBy(cb => wirePosMap[cb]).ToList());

        try
        {
            newNode.NodeId = _multiGraph.ContractNodes(blockIds, newNode, cycleCheck);
        }
        catch (Exception ex)
        {
            throw new Exception("Replacing the specified node block would introduce a cycle.", ex);
        }
    }
}

public class CalibrationKey
{
    public int First { get; set; }
    public int Second { get; set; }

    public CalibrationKey(int first, int second)
    {
        First = first;
        Second = second;
    }

    // Override Equals and GetHashCode for dictionary key comparison
    public override bool Equals(object obj)
    {
        if (obj is CalibrationKey other)
        {
            return First == other.First && Second == other.Second;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }
}

// Mock Clbit class
public class Clbit
{
    public int Index { get; set; }

    public Clbit(int index)
    {
        Index = index;
    }
}

// Mock ClassicalRegister class
public class ClassicalRegister
{
    public string Name { get; set; }
    public List<Clbit> Clbits { get; set; }

    public ClassicalRegister(string name)
    {
        Name = name;
        Clbits = new List<Clbit>();
    }

    public void AddClbit(Clbit clbit)
    {
        Clbits.Add(clbit);
    }
}


// Mock PyDAG class
public class PyDAG
{
    public List<DAGDepNode> Nodes { get; set; }

    public PyDAG()
    {
        Nodes = new List<DAGDepNode>();
    }

    public int AddNode(DAGDepNode node)
    {
        Nodes.Add(node);
        return Nodes.Count - 1;  // Returning index as node ID
    }

    public DAGDepNode GetNodeData(int nodeId)
    {
        return Nodes[nodeId];
    }

    public List<int> InEdges(int nodeId)
    {
        return Nodes[nodeId].Predecessors.Select(n => n.NodeId).ToList();
    }

    public List<int> OutEdges(int nodeId)
    {
        return Nodes[nodeId].Successors.Select(n => n.NodeId).ToList();
    }

    public List<int> DirectSuccessors(int nodeId)
    {
        return Nodes[nodeId].Successors.Select(n => n.NodeId).ToList();
    }

    public List<int> DirectPredecessors(int nodeId)
    {
        return Nodes[nodeId].Predecessors.Select(n => n.NodeId).ToList();
    }

    public IEnumerable<DAGDepNode> TopologicalSort()
    {
        return Nodes.OrderBy(n => n.NodeId);  // Placeholder for actual topological sort
    }

    public int Size()
    {
        return Nodes.Count;
    }
}

// Mock Operation class
public class Operation
{
    public string Name { get; set; }
    public List<Qubit> Qubits { get; set; }

    public Operation(string name)
    {
        Name = name;
        Qubits = new List<Qubit>();
    }

    public void AddQubit(Qubit qubit)
    {
        Qubits.Add(qubit);
    }
}
