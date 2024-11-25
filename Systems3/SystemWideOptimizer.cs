using System.Collections.Generic;
using System.Linq;
using System;

public class SystemWideOptimizer
{
    public static void PropagateFeedback(
        OriginState[] origins,
        float[] globalFeedback,
        float learningRate,
        HopfieldNetworkIntegration hopfield,
        LLMBayesianNetwork bayesian)
    {
        // Optimize origin states
        foreach (var origin in origins)
        {
            origin.StateVector.Optimize(globalFeedback, learningRate);
        }

        // Update Hopfield network
        foreach (var origin in origins)
        {
            hopfield.Train(origin.StateVector.PrimaryState);
        }

        // Update Bayesian beliefs
        bayesian.UpdateBelief("Stability", "true", globalFeedback.Average());
    }
}
