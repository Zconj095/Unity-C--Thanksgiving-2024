using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PromptDistributor
{
    private List<PromptCache> agents = new List<PromptCache>();

    public void DistributePrompt(string prompt, float[] embedding)
    {
        int selectedAgent = UnityEngine.Random.Range(0, agents.Count);
        agents[selectedAgent].AddPrompt(prompt, embedding);
        Debug.Log($"Prompt distributed to Agent {selectedAgent}");
    }
}
