# CrossValidationStatistics

## Overview
The `CrossValidationStatistics` class is designed to encapsulate and compute summary statistics for a cross-validation trial within machine learning projects, specifically in the context of Unity applications. This class computes essential performance metrics such as mean, variance, and standard deviation for the results obtained from different folds during cross-validation, thereby providing insights into the model's performance and stability.

## Variables

- **Values**: `double[]`
  - An array that stores the performance statistics (typically errors) acquired during the cross-validation process for each fold.

- **Variances**: `double[]`
  - An array that contains the variance of the performance statistics for each fold, if available.

- **Sizes**: `int[]`
  - An array that holds the number of samples used to compute the variance for each corresponding value in the `Values` array.

- **Mean**: `double`
  - The average of the performance statistics computed from the `Values` array.

- **Variance**: `double`
  - The variance of the performance statistics calculated from the `Values` array.

- **StandardDeviation**: `double`
  - The standard deviation of the performance statistics, derived from the `Variance`.

- **PooledVariance**: `double`
  - The pooled variance of the performance statistics, which combines the variances from different folds.

- **PooledStandardDeviation**: `double`
  - The standard deviation derived from the `PooledVariance`.

- **Tag**: `object`
  - A user-defined property that can hold any additional information related to the instance of the class.

## Functions

- **CrossValidationStatistics(int[] sizes, double[] values, double[] variances = null)**
  - Constructor that initializes a new instance of the `CrossValidationStatistics` class. It takes in the sizes of samples, the performance statistics, and optionally the variances. It computes the mean and variance of the provided values and calculates the pooled variance if variances are provided.

- **static double ComputeMean(double[] values)**
  - A private static function that computes and returns the mean of the provided array of values.

- **static double ComputeVariance(double[] values, double mean)**
  - A private static function that calculates and returns the variance of the provided values based on the mean.

- **static double ComputePooledVariance(int[] sizes, double[] variances)**
  - A private static function that computes and returns the pooled variance based on the sizes and variances provided for each fold. This is useful for understanding the overall variability across multiple samples.