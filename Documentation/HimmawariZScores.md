# HimmawariZScores

## Overview
The `HimmawariZScores` script is designed to calculate Z-scores for multi-dimensional data points in a Unity environment. It processes a list of data points, computes their means and standard deviations, calculates weights based on the influence of each dimension, and finally computes a relevance score for each data point based on the Z-scores. This functionality is essential for statistical analysis and can be integrated into larger data processing or machine learning systems within the codebase.

## Variables
- `dataPoints`: A `List<float[]>` that holds multi-dimensional data points for which the Z-scores will be calculated.
- `means`: A `float[]` that stores the mean values for each dimension of the data points.
- `stdDevs`: A `float[]` that contains the standard deviation values for each dimension of the data points.
- `weights`: A `float[]` that holds the calculated weights for each dimension based on their influence.

## Functions
- `Start()`: This is a Unity lifecycle method that is called before the first frame update. It initializes the calculations by calling the methods to compute means, standard deviations, weights, and Z-scores.

- `CalculateMeansAndStdDevs()`: This method calculates the mean and standard deviation for each dimension of the data points. It iterates through each dimension, sums the values and their squares, and then computes the mean and standard deviation.

- `CalculateWeights()`: This method computes the weights for each dimension based on their influence. It calculates the total influence of each dimension and normalizes the weights so that they sum to one.

- `CalculateZScores()`: This method calculates the Z-scores for each data point using the previously computed means, standard deviations, and weights. It computes a relevance score for each point and logs it to the console.

This documentation provides a clear understanding of the script's purpose, its variables, and its functions, making it easier for developers to engage with and utilize the code effectively.