using System;
using System.Collections.Generic;
public class EstimatorPub
{
    public List<EstimatorPubLike> Estimators { get; set; }
    public string EstimationMethod { get; set; }

    public EstimatorPub(string method)
    {
        Estimators = new List<EstimatorPubLike>();
        EstimationMethod = method;
    }

    // Add an EstimatorPubLike to the list
    public void AddEstimator(EstimatorPubLike estimator)
    {
        Estimators.Add(estimator);
    }

    // Perform the estimation process
    public double PerformEstimation()
    {
        double totalEstimate = 0;

        foreach (var estimator in Estimators)
        {
            totalEstimate += estimator.Estimate(); // Placeholder logic for summing estimates
        }

        return totalEstimate;
    }

    // Get the status of estimators
    public string GetStatus()
    {
        return Estimators.Count > 0 ? "Estimations in progress" : "No estimations available";
    }
}
