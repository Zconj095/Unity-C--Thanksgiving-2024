using System;
using System.Collections.Generic;
using UnityEngine;

public class HyperfiniteReasoning : MonoBehaviour
{
    // Represents a logical node with premises, conclusions, and subjections
    public class LogicalNode
    {
        public string Premise { get; set; }
        public string Conclusion { get; set; }
        public float TruthValue { get; set; } // Hyperfinite truth value (0.0 - 1.0)
        public List<LogicalNode> Subjections { get; set; }

        public LogicalNode(string premise, string conclusion, float truthValue)
        {
            Premise = premise;
            Conclusion = conclusion;
            TruthValue = truthValue;
            Subjections = new List<LogicalNode>();
        }
    }

    // Evaluate the logical structure recursively
    public static float EvaluateLogicalNode(LogicalNode node)
    {
        if (node.Subjections.Count == 0)
            return node.TruthValue;

        float combinedTruthValue = node.TruthValue;

        foreach (var subjection in node.Subjections)
        {
            float subjectionTruthValue = EvaluateLogicalNode(subjection);
            combinedTruthValue *= subjectionTruthValue; // Logical conjunction (AND)
        }

        return combinedTruthValue;
    }

    // Generate hyperfinite reasoning
    public static LogicalNode BuildHyperfiniteReasoning()
    {
        // Root logical node
        LogicalNode root = new LogicalNode("Premise: A", "Conclusion: B", 0.9f);

        // Add subjections
        LogicalNode sub1 = new LogicalNode("Premise: B1", "Conclusion: B2", 0.8f);
        LogicalNode sub2 = new LogicalNode("Premise: C1", "Conclusion: C2", 0.7f);

        // Add nested subjections
        LogicalNode subSub1 = new LogicalNode("Premise: D1", "Conclusion: D2", 0.6f);
        sub1.Subjections.Add(subSub1);

        // Attach subjections to the root
        root.Subjections.Add(sub1);
        root.Subjections.Add(sub2);

        return root;
    }

    void Start()
    {
        // Build hyperfinite reasoning structure
        var reasoning = HyperfiniteReasoning.BuildHyperfiniteReasoning();

        // Evaluate the reasoning structure
        float truthValue = HyperfiniteReasoning.EvaluateLogicalNode(reasoning);

        // Output the result
        Debug.Log($"Hyperfinite reasoning evaluation result: {truthValue}");
    }

}
