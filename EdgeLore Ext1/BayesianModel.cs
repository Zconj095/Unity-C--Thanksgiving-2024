using System.Collections.Generic;

public class BayesianModel
{
    private Dictionary<string, float> probabilities = new Dictionary<string, float>();

    public void AddPrior(string state, float probability)
    {
        probabilities[state] = probability;
    }

    public float UpdatePosterior(string state, float likelihood)
    {
        if (!probabilities.ContainsKey(state)) return 0;

        probabilities[state] *= likelihood;
        float total = 0;

        foreach (var key in probabilities.Keys)
        {
            total += probabilities[key];
        }

        probabilities[state] /= total;

        return probabilities[state];
    }
}
