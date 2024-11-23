using System.Collections.Generic;
using UnityEngine;

public class EvolutionaryLayer : MonoBehaviour
{
    public NeuroplasticSystem NeuralSystem;
    public int Generation = 0;

    public void Evolve()
    {
        Generation++;
        for (int i = 0; i < NeuralSystem.Nodes.Count; i++)
        {
            for (int j = i + 1; j < NeuralSystem.Nodes.Count; j++)
            {
                var connectionKey = (i, j);
                if (NeuralSystem.Connections.ContainsKey(connectionKey))
                {
                    // Introduce mutations or weight adjustments
                    NeuralSystem.Connections[connectionKey] *= Random.Range(0.9f, 1.1f);
                }
            }
        }
    }
}
