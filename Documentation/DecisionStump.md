# DecisionStump Class

## Overview
The `DecisionStump` class implements a simple classifier that operates based on decision thresholds for one-dimensional features. It is part of the `EdgeLoreMachineLearning.Boosting.Learners` namespace and is designed to be used in machine learning scenarios where classification tasks require evaluating input features against specified thresholds. This class can be utilized within a larger machine learning framework to make decisions based on the learned thresholds and features.

## Variables

- `private double threshold;`
  - This variable stores the decision threshold value that will be used to classify the input data.

- `private int featureIndex;`
  - This variable represents the index of the feature that is being used for making the classification decision.

- `private ComparisonKind comparison;`
  - This variable indicates the type of comparison that is performed (e.g., greater than, equal to) when evaluating the feature against the threshold.

## Functions

- `public DecisionStump()`
  - Constructor that initializes a new instance of the `DecisionStump` class.

- `public double Threshold { get; set; }`
  - Property that gets or sets the decision threshold for the classifier.

- `public int Index { get; set; }`
  - Property that gets or sets the index of the feature used for the decision.

- `public ComparisonKind Comparison { get; set; }`
  - Property that gets or sets the type of comparison that will be performed.

- `public bool Decide(double[] input)`
  - This method takes an input vector and determines the classification result based on the feature index, threshold, and comparison type. It returns `true` if the input satisfies the threshold condition; otherwise, it returns `false`.

- `public void Learn(double[][] inputs, int[] outputs, double[] weights = null)`
  - This method trains the decision stump using provided input vectors, expected output labels, and optional weights. It evaluates various thresholds and comparison types to minimize classification error.

- `private double EvaluateError(double[][] inputs, int[] outputs, double[] weights, int feature, double threshold, ComparisonKind comparisonType)`
  - This method calculates the classification error for a given feature, threshold, and comparison type. It sums the weights of the incorrectly classified inputs.

- `private bool Compare(double value, double threshold, ComparisonKind comparisonType)`
  - This method performs the actual comparison between a value and the threshold based on the specified comparison type. It returns `true` or `false` depending on the result of the comparison.

## Enum

- `public enum ComparisonKind`
  - This enumeration defines the types of comparisons that can be performed by the classifier:
    - `Equal`
    - `NotEqual`
    - `GreaterThan`
    - `GreaterThanOrEqual`
    - `LessThan`
    - `LessThanOrEqual`