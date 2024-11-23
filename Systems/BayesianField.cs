using System.Collections.Generic;
using UnityEngine;

public class BayesianField : MonoBehaviour
{
    private Dictionary<string, float> priorProbabilities;
    private Dictionary<string, float> posteriorProbabilities;

    public BayesianField()
    {
        // Initialize with uniform prior probabilities
        priorProbabilities = new Dictionary<string, float>()
        {
            {"delta", 0.2f},
            {"theta", 0.2f},
            {"alpha", 0.2f},
            {"beta", 0.2f},
            {"gamma", 0.2f}
        };

        posteriorProbabilities = new Dictionary<string, float>(priorProbabilities);
    }

    public void UpdatePosterior(Dictionary<string, float> observedData)
    {
        float total = 0f;

        // Update posterior probabilities using Bayes' theorem
        foreach (var band in observedData.Keys)
        {
            float likelihood = Mathf.Exp(-observedData[band]); // Example likelihood function
            posteriorProbabilities[band] = likelihood * priorProbabilities[band];
            total += posteriorProbabilities[band];
        }

        // Normalize posterior probabilities
        var keys = new List<string>(posteriorProbabilities.Keys); // Make a copy of the keys
        foreach (var band in keys)
        {
            posteriorProbabilities[band] /= total;
        }
    }

    public Dictionary<string, float> GetPosterior()
    {
        return posteriorProbabilities;
    }
}
