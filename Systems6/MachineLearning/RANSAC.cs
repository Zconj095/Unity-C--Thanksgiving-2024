using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Multipurpose RANSAC algorithm for robust model fitting.
    /// </summary>
    public class RANSAC<TModel> where TModel : class
    {
        private int minSamples;         // Minimum number of samples
        private double threshold;       // Distance threshold for inliers
        private int maxSamplings = 100; // Max attempts to find non-degenerate samples
        private int maxEvaluations = 1000; // Max trials to find the best model
        private double probability = 0.99; // Probability of finding a good model

        private Func<int[], TModel> fittingFunction;   // Model fitting function
        private Func<TModel, double, int[]> distanceFunction; // Distance evaluation function
        private Func<int[], bool> degeneracyCheck;     // Degeneracy check function

        /// <summary>
        /// Distance threshold for inlier determination.
        /// </summary>
        public double Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        /// <summary>
        /// Minimum number of samples required to fit a model.
        /// </summary>
        public int MinSamples
        {
            get => minSamples;
            set => minSamples = value;
        }

        /// <summary>
        /// Maximum number of attempts to find non-degenerate samples.
        /// </summary>
        public int MaxSamplings
        {
            get => maxSamplings;
            set => maxSamplings = value;
        }

        /// <summary>
        /// Maximum number of evaluations allowed.
        /// </summary>
        public int MaxEvaluations
        {
            get => maxEvaluations;
            set => maxEvaluations = value;
        }

        /// <summary>
        /// Probability of finding a good model.
        /// </summary>
        public double Probability
        {
            get => probability;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentException("Probability must be between 0 and 1.");
                probability = value;
            }
        }

        public Func<int[], TModel> FittingFunction
        {
            get => fittingFunction;
            set => fittingFunction = value;
        }

        public Func<TModel, double, int[]> DistanceFunction
        {
            get => distanceFunction;
            set => distanceFunction = value;
        }

        public Func<int[], bool> DegeneracyCheck
        {
            get => degeneracyCheck;
            set => degeneracyCheck = value;
        }

        public int TrialsNeeded { get; private set; }
        public int TrialsPerformed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the RANSAC class.
        /// </summary>
        public RANSAC(int minSamples, double threshold, double probability = 0.99)
        {
            if (minSamples <= 0)
                throw new ArgumentOutOfRangeException(nameof(minSamples), "MinSamples must be greater than zero.");
            if (threshold < 0)
                throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be non-negative.");
            if (probability <= 0 || probability > 1)
                throw new ArgumentException("Probability must be between 0 and 1.", nameof(probability));

            this.minSamples = minSamples;
            this.threshold = threshold;
            this.probability = probability;
        }

        /// <summary>
        /// Computes the best model using the RANSAC algorithm.
        /// </summary>
        public TModel Compute(int size, out int[] inliers)
        {
            if (fittingFunction == null || distanceFunction == null)
                throw new InvalidOperationException("Fitting and Distance functions must be defined.");

            TModel bestModel = null;
            int[] bestInliers = null;
            int maxInliers = 0;

            TrialsPerformed = 0;
            TrialsNeeded = maxEvaluations;

            while (TrialsPerformed < TrialsNeeded && TrialsPerformed < maxEvaluations)
            {
                int[] sample = null;
                TModel model = null;

                int samplings = 0;
                while (samplings < maxSamplings)
                {
                    sample = SampleRandomIndices(size, minSamples);

                    if (degeneracyCheck == null || !degeneracyCheck(sample))
                    {
                        model = fittingFunction(sample);
                        break;
                    }

                    samplings++;
                }

                if (model == null)
                    throw new Exception("Could not fit a valid model.");

                int[] currentInliers = distanceFunction(model, threshold);

                if (currentInliers.Length > maxInliers)
                {
                    maxInliers = currentInliers.Length;
                    bestModel = model;
                    bestInliers = currentInliers;

                    double inlierRatio = (double)currentInliers.Length / size;
                    double pNoOutliers = 1.0 - Math.Pow(inlierRatio, minSamples);

                    if (pNoOutliers == 0)
                        TrialsNeeded = maxEvaluations;
                    else
                        TrialsNeeded = (int)(Math.Log(1 - probability) / Math.Log(pNoOutliers));
                }

                TrialsPerformed++;
            }

            inliers = bestInliers;
            return bestModel;
        }

        /// <summary>
        /// Randomly samples unique indices.
        /// </summary>
        private int[] SampleRandomIndices(int dataSize, int sampleSize)
        {
            var indices = new HashSet<int>();
            while (indices.Count < sampleSize)
            {
                indices.Add(UnityEngine.Random.Range(0, dataSize));
            }

            return new List<int>(indices).ToArray();
        }
    }
}
