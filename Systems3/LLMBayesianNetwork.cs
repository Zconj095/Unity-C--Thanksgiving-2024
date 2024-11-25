using System.Collections.Generic;
using System.Linq;
using System;
public class LLMBayesianNetwork
{
    private List<BayesianNode> nodes;

    public LLMBayesianNetwork()
    {
        nodes = new List<BayesianNode>();
    }

    public void AddNode(BayesianNode node)
    {
        nodes.Add(node);
    }

    public void UpdateBelief(string nodeName, string state, float evidence)
    {
        BayesianNode node = nodes.Find(n => n.Name == nodeName);
        if (node != null)
        {
            float prior = node.GetProbability(state);
            float posterior = (prior * evidence) / ((prior * evidence) + (1 - prior) * (1 - evidence));
            node.UpdateProbability(state, posterior);
        }
    }

    public float GetBelief(string nodeName, string state)
    {
        BayesianNode node = nodes.Find(n => n.Name == nodeName);
        return node?.GetProbability(state) ?? 0.0f;
    }
}
