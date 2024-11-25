using System.Collections.Generic;
using UnityEngine;

public class SymbolicAI : MonoBehaviour
{
    public Dictionary<string, float> KnowledgeBase = new Dictionary<string, float>();

    public void AddKnowledge(string concept, float importance)
    {
        if (!KnowledgeBase.ContainsKey(concept))
        {
            KnowledgeBase[concept] = importance;
        }
        else
        {
            KnowledgeBase[concept] += importance;
        }
    }

    public string Query(string queryConcept)
    {
        if (KnowledgeBase.ContainsKey(queryConcept))
        {
            return $"Concept {queryConcept}: Importance {KnowledgeBase[queryConcept]}";
        }
        return $"Concept {queryConcept} not found.";
    }

    public void IntegrateNeuralInsights(string concept, float neuralWeight)
    {
        AddKnowledge(concept, neuralWeight);
    }
}
