using System.Collections.Generic;
using System.Linq;
using System;

public class BayesianNode
{
    public string Name { get; private set; }
    public Dictionary<string, float> Probabilities { get; private set; }

    public BayesianNode(string name)
    {
        Name = name;
        Probabilities = new Dictionary<string, float>();
    }

    public void UpdateProbability(string state, float probability)
    {
        Probabilities[state] = probability;
    }

    public float GetProbability(string state)
    {
        return Probabilities.ContainsKey(state) ? Probabilities[state] : 0.0f;
    }
}
