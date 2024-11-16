using System;
using System.Collections.Generic;
using UnityEngine;

public class IterativeAmplitudeEstimation
{
    private float epsilon;
    private float alpha;
    private string confintMethod;
    private float minRatio;
    private List<float> aIntervals;
    private List<float> thetaIntervals;
    private int numOracleQueries;

    public IterativeAmplitudeEstimation(float epsilon, float alpha, string confintMethod = "beta", float minRatio = 2.0f)
    {
        if (epsilon <= 0 || epsilon >= 0.5f)
            throw new ArgumentException("Epsilon must be between 0 and 0.5");
        if (alpha <= 0 || alpha >= 1)
            throw new ArgumentException("Alpha must be between 0 and 1");
        if (confintMethod != "beta" && confintMethod != "chernoff")
            throw new ArgumentException("Invalid confidence interval method");

        this.epsilon = epsilon;
        this.alpha = alpha;
        this.confintMethod = confintMethod;
        this.minRatio = minRatio;
        this.aIntervals = new List<float> { 0.0f, 1.0f };
        this.thetaIntervals = new List<float> { 0.0f, 0.25f }; // initial interval [0, π/4]
    }

    public float Epsilon => this.epsilon;

    public float Alpha => this.alpha;

    public List<float> GetConfidenceInterval()
    {
        return new List<float> { this.aIntervals[0], this.aIntervals[this.aIntervals.Count - 1] };
    }

    public float FindNextK(int k, bool upperHalfCircle, Tuple<float, float> thetaInterval)
    {
        float thetaL = thetaInterval.Item1;
        float thetaU = thetaInterval.Item2;
        int scaling = (int)(1 / (2 * (thetaU - thetaL)));
        int nextK = scaling / 4;

        if (nextK <= k)
            nextK = k; // Ensure progress

        return nextK;
    }

    public float Estimate()
    {
        // Simulate estimation process
        this.numOracleQueries = 0; // Mock implementation
        Debug.Log($"Confidence Interval: {this.aIntervals[0]} - {this.aIntervals[1]}");
        return (this.aIntervals[0] + this.aIntervals[1]) / 2; // Midpoint estimate
    }

    private Tuple<float, float> ComputeConfidenceInterval(float value, int shots)
    {
        // Mock computation of confidence interval
        float margin = Mathf.Sqrt(1f / shots);
        return new Tuple<float, float>(value - margin, value + margin);
    }
}
