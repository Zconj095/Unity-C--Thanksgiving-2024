# RANSAC<TModel>

## Overview
The `RANSAC<TModel>` class implements a multipurpose RANSAC (Random Sample Consensus) algorithm for robust model fitting. This algorithm is particularly useful in scenarios where the dataset contains a significant amount of outliers. The RANSAC method iteratively selects random subsets of data points, fits a model to these points, and evaluates the model's quality based on a defined threshold. This class is designed to be flexible, allowing for different types of models and distance functions to be used, making it a valuable component of the EdgeLoreMachineLearning codebase.

## Variables
- **minSamples**: (int) The minimum number of samples required to fit a model.
- **threshold**: (double) The distance threshold used to determine inliers.
- **maxSamplings**: (int) The maximum number of attempts to find non-degenerate samples (default is 100).
- **maxEvaluations**: (int) The maximum number of trials allowed to find the best model (default is 1000).
- **probability**: (double) The probability of finding a good model (default is 0.99).
- **fittingFunction**: (Func<int[], TModel>) A function that takes an array of sample indices and returns a fitted model.
- **distanceFunction**: (Func<TModel, double, int[]>) A function that evaluates the distance of the fitted model and returns an array of inlier indices.
- **degeneracyCheck**: (Func<int[], bool>) A function that checks if a sample is degenerate (i.e., unsuitable for model fitting).
- **TrialsNeeded**: (int) The number of trials needed to find a good model, calculated during the execution of the algorithm.
- **TrialsPerformed**: (int) The number of trials that have been performed during the execution of the algorithm.

## Functions
- **RANSAC(int minSamples, double threshold, double probability = 0.99)**: 
  Initializes a new instance of the RANSAC class with specified minimum samples, distance threshold, and probability of finding a good model. Validates input parameters.

- **TModel Compute(int size, out int[] inliers)**: 
  Computes the best model using the RANSAC algorithm. It randomly samples data points, fits a model, evaluates the model, and keeps track of the best model found. Returns the best model and outputs an array of inlier indices.

- **int[] SampleRandomIndices(int dataSize, int sampleSize)**: 
  Randomly samples unique indices from the dataset. Ensures that the sampled indices are unique and returns them as an array. This function is private and used internally by the `Compute` method.