using System.Collections.Generic;
using System.Linq;
using System;

public class OptimizationInterface
{
    public static void TriggerOptimization(
        OriginState[] origins,
        HopfieldNetworkIntegration hopfield,
        LLMBayesianNetwork bayesian,
        float learningRate)
    {
        // Generate global feedback (example logic)
        float[] globalFeedback = new float[origins[0].StateVector.PrimaryState.Length];
        Random random = new Random();
        for (int i = 0; i < globalFeedback.Length; i++)
        {
            globalFeedback[i] = (float)(random.NextDouble() * 2 - 1); // Random feedback
        }

        // Propagate feedback through the system
        SystemWideOptimizer.PropagateFeedback(origins, globalFeedback, learningRate, hopfield, bayesian);
    }
}
