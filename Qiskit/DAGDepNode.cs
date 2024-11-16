using System;
using System.Collections.Generic;

public class DAGDepNode
{
    public string Type { get; set; }
    private object _op;
    public string Name { get; set; }
    private Tuple<Qubit>[] _qargs;
    public Tuple<Clbit>[] Cargs { get; set; }
    public string SortKey { get; set; }
    public int NodeId { get; set; }
    public List<int> Successors { get; set; }
    public List<int> Predecessors { get; set; }
    public object Reachable { get; set; }
    public List<int> MatchedWith { get; set; }
    public bool IsBlocked { get; set; }
    public List<int> SuccessorsToVisit { get; set; }
    public List<int> Qindices { get; set; }
    public List<int> Cindices { get; set; }

    public DAGDepNode(
        string type = null,
        object op = null,
        string name = null,
        Tuple<Qubit>[] qargs = null,
        Tuple<Clbit>[] cargs = null,
        List<int> successors = null,
        List<int> predecessors = null,
        object reachable = null,
        List<int> matchedwith = null,
        List<int> successorstovisit = null,
        bool? isblocked = null,
        List<int> qindices = null,
        List<int> cindices = null,
        int nid = -1
    )
    {
        Type = type;
        _op = op;
        Name = name;
        _qargs = qargs ?? new Tuple<Qubit>[0];
        Cargs = cargs ?? new Tuple<Clbit>[0];
        NodeId = nid;
        SortKey = string.Join(",", _qargs);
        Successors = successors ?? new List<int>();
        Predecessors = predecessors ?? new List<int>();
        Reachable = reachable;
        MatchedWith = matchedwith ?? new List<int>();
        IsBlocked = isblocked ?? false;
        SuccessorsToVisit = successorstovisit ?? new List<int>();
        Qindices = qindices ?? new List<int>();
        Cindices = cindices ?? new List<int>();
    }

    public object Op
    {
        get
        {
            if (Type != "op")
                throw new Exception($"The node {this} is not an op node.");
            return _op;
        }
        set
        {
            _op = value;
        }
    }

    public Tuple<Qubit>[] Qargs
    {
        get { return _qargs; }
        set
        {
            _qargs = value;
            SortKey = string.Join(",", value);
        }
    }

    public static bool SemanticEq(DAGDepNode node1, DAGDepNode node2)
    {
        // For barriers, qarg order is not significant so compare as sets
        if (node1.Name == "barrier" && node2.Name == "barrier")
        {
            return new HashSet<Tuple<Qubit>>(node1._qargs).SetEquals(new HashSet<Tuple<Qubit>>(node2._qargs));
        }

        if (node1.Type == node2.Type)
        {
            if (Equals(node1.Op, node2.Op))
            {
                if (node1.Name == node2.Name)
                {
                    if (EqualityComparer<Tuple<Qubit>[]>.Default.Equals(node1._qargs, node2._qargs))
                    {
                        if (EqualityComparer<Tuple<Clbit>[]>.Default.Equals(node1.Cargs, node2.Cargs))
                        {
                            if (node1.Type == "op")
                            {
                                var condition1 = node1.Op.GetType().GetProperty("Condition")?.GetValue(node1.Op, null);
                                var condition2 = node2.Op.GetType().GetProperty("Condition")?.GetValue(node2.Op, null);
                                return Equals(condition1, condition2);
                            }
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public DAGDepNode Copy()
    {
        return new DAGDepNode(
            Type,
            Op,
            Name,
            _qargs,
            Cargs,
            new List<int>(Successors),
            new List<int>(Predecessors),
            Reachable,
            new List<int>(MatchedWith),
            new List<int>(SuccessorsToVisit),
            IsBlocked,
            new List<int>(Qindices),
            new List<int>(Cindices),
            NodeId
        );
    }
}
