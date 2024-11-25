using System.Collections.Generic;
using System.Linq;
using System;

public class VectorClassifier
{
    private List<HybridState> trainingData;
    private List<int> labels;

    public VectorClassifier()
    {
        trainingData = new List<HybridState>();
        labels = new List<int>();
    }

    public void Train(HybridState state, int label)
    {
        trainingData.Add(state);
        labels.Add(label);
    }

    public int Predict(HybridState state)
    {
        float bestScore = float.MinValue;
        int bestLabel = -1;

        for (int i = 0; i < trainingData.Count; i++)
        {
            float score = state.HyperState.Bind(trainingData[i].HyperState).Vector.Sum()
                          + state.LLMQuantumState2.Interfere(trainingData[i].LLMQuantumState2).Measure();

            if (score > bestScore)
            {
                bestScore = score;
                bestLabel = labels[i];
            }
        }

        return bestLabel;
    }
}
