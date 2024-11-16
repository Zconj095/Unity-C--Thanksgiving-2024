using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaximumLikelihoodAmplitudeEstimation
{
    private int numOracleCircuits;
    private int[] evaluationSchedule;
    private int likelihoodEvals;
    private Dictionary<string, object> results;

    public MaximumLikelihoodAmplitudeEstimation(int numOracleCircuits, int? likelihoodEvals = null)
    {
        if (numOracleCircuits < 1)
        {
            throw new ArgumentException("numOracleCircuits must be at least 1.");
        }

        this.numOracleCircuits = numOracleCircuits;
        this.evaluationSchedule = GenerateEvaluationSchedule(numOracleCircuits);
        this.likelihoodEvals = likelihoodEvals ?? Mathf.Max(10000, Mathf.CeilToInt((float)(Math.PI / 2 * 1000 * Math.Pow(2, numOracleCircuits))));
        this.results = new Dictionary<string, object>();
    }

    private int[] GenerateEvaluationSchedule(int numOracleCircuits)
    {
        List<int> schedule = new List<int> { 0 };
        for (int j = 0; j < numOracleCircuits; j++)
        {
            schedule.Add((int)Math.Pow(2, j));
        }
        return schedule.ToArray();
    }

    public List<string> ConstructCircuits()
    {
        List<string> circuits = evaluationSchedule.Select(k => $"Circuit_{k}").ToList();
        Debug.Log($"Constructed circuits: {string.Join(", ", circuits)}");
        return circuits;
    }

    private List<float> EvaluateStatevectors(List<List<Complex>> statevectors)
    {
        List<float> probabilities = new List<float>();
        foreach (var statevector in statevectors)
        {
            float probability = statevector.Sum(sv => (float)(sv.Magnitude * sv.Magnitude));
            probabilities.Add(probability);
        }
        Debug.Log($"Evaluated statevector probabilities: {string.Join(", ", probabilities)}");
        return probabilities;
    }

    private float ComputeMLE(List<float> oneHits, List<float> allHits)
    {
        float[] searchRange = Enumerable.Range(0, likelihoodEvals)
            .Select(i => (float)(i * (Math.PI / 2) / (likelihoodEvals - 1)))
            .ToArray();

        float LogLikelihood(float theta)
        {
            float loglik = 0;
            for (int i = 0; i < evaluationSchedule.Length; i++)
            {
                int k = evaluationSchedule[i];
                loglik += (float)(Math.Log(Math.Pow(Math.Sin((2 * k + 1) * theta), 2)) * oneHits[i]);
                loglik += (float)(Math.Log(Math.Pow(Math.Cos((2 * k + 1) * theta), 2)) * (allHits[i] - oneHits[i]));
            }
            return -loglik;
        }

        float[] likelihoods = searchRange.Select(theta => LogLikelihood(theta)).ToArray();
        int maxIndex = Array.IndexOf(likelihoods, likelihoods.Min());
        Debug.Log($"MLE computed: {searchRange[maxIndex]}");
        return searchRange[maxIndex];
    }

    public Dictionary<string, object> Run()
    {
        // Simulate statevector probabilities
        List<List<Complex>> statevectors = evaluationSchedule
            .Select(n => Enumerable.Repeat(new Complex(0.1, 0.9), (int)Math.Pow(2, n)).ToList())
            .ToList();

        List<float> probabilities = EvaluateStatevectors(statevectors);
        List<float> oneHits = probabilities;
        List<float> allHits = Enumerable.Repeat(1f, probabilities.Count).ToList(); // Simulated perfect shots

        // Compute the MLE
        float thetaMLE = ComputeMLE(oneHits, allHits);
        float amplitude = (float)Math.Pow(Math.Sin(thetaMLE), 2);

        // Store results
        results["theta"] = thetaMLE;
        results["amplitude"] = amplitude;
        results["confidence_interval"] = new float[] { amplitude - 0.05f, amplitude + 0.05f }; // Example interval

        return results;
    }
}
