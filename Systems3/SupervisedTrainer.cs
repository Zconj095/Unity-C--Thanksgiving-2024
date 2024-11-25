using System.Collections.Generic;
using System.Linq;
using System;

public class SupervisedTrainer
{
    public static void TrainHopfield(HopfieldNetworkIntegration network, float[][] patterns)
    {
        foreach (var pattern in patterns)
        {
            network.Train(pattern);
        }
    }

    public static void TrainBayesian(LLMBayesianNetwork network, Dictionary<string, float>[] data)
    {
        foreach (var entry in data)
        {
            foreach (var key in entry.Keys)
            {
                network.UpdateBelief(key, "true", entry[key]);
            }
        }
    }
}
