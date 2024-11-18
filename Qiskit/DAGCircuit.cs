using System;
using System.Collections.Generic;
using System.Linq;

public class DAGCircuit
{
    // Stores the nodes (operations) in the circuit
    public Dictionary<int, DAGOpNode> Nodes { get; set; } = new Dictionary<int, DAGOpNode>();

    // A list to store the topological order of the nodes
    public List<int> TopologicalOrder { get; set; } = new List<int>(); 

    // Add an operation node to the circuit
    public void AddOpNode(DAGOpNode node)
    {
        if (!Nodes.ContainsKey(node.NodeId))
        {
            Nodes[node.NodeId] = node;
            TopologicalOrder.Add(node.NodeId);
        }
    }

    // Get a node by its ID
    public DAGOpNode GetNode(int nodeId)
    {
        if (Nodes.ContainsKey(nodeId))
        {
            return Nodes[nodeId];
        }
        else
        {
            throw new Exception("Node not found.");
        }
    }

    // Remove a node by its ID
    public void RemoveNode(int nodeId)
    {
        if (Nodes.ContainsKey(nodeId))
        {
            Nodes.Remove(nodeId);
            TopologicalOrder.Remove(nodeId);
        }
        else
        {
            throw new Exception("Node not found.");
        }
    }

    // Add an edge between two nodes to represent a dependency
    public void AddEdge(int srcNodeId, int destNodeId)
    {
        if (Nodes.ContainsKey(srcNodeId) && Nodes.ContainsKey(destNodeId))
        {
            Nodes[srcNodeId].Successors.Add(Nodes[destNodeId]);
            Nodes[destNodeId].Predecessors.Add(Nodes[srcNodeId]);
        }
        else
        {
            throw new Exception("Invalid node IDs.");
        }
    }

    // Get all nodes in topologically sorted order
    public List<DAGOpNode> GetTopologicallySortedNodes()
    {
        return TopologicalOrder.Select(id => Nodes[id]).ToList();
    }
}

// A basic class representing an operation node in the DAG (Quantum Operation)
public class DAGOpNode
{
    public int NodeId { get; set; }
    public string Name { get; set; }
    public List<DAGOpNode> Predecessors { get; set; } = new List<DAGOpNode>();
    public List<DAGOpNode> Successors { get; set; } = new List<DAGOpNode>();

    public DAGOpNode(int nodeId, string name)
    {
        NodeId = nodeId;
        Name = name;
    }
}
