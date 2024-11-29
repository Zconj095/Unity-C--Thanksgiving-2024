# AdaBoost Class

## Overview
The `AdaBoost` class implements the AdaBoost learning algorithm, which is designed to boost weak classifiers into a strong classifier. It achieves this by iteratively training weak classifiers, adjusting their weights based on their performance, and combining them into an ensemble that improves overall classification accuracy. This class is part of the `EdgeLoreMachineLearning` namespace and fits into a larger machine learning codebase where boosting techniques are needed to enhance model performance.

## Variables
- `ensemble`: A list that holds tuples of model weights and corresponding weak classifiers. This ensemble is used to combine the outputs of the weak classifiers to form a strong classifier.
- `threshold`: A double value set to 0.5, used as a threshold for classification error to determine when to stop training.
- `tolerance`: A double value initialized to 1e-6, which specifies the relative tolerance for convergence during the training process.
- `maxIterations`: An integer that sets the maximum number of iterations for training weak classifiers, defaulting to 100.

## Properties
- `Learner`: A function delegate that takes a weighted dataset and returns a trained weak classifier model. It is responsible for creating and training the model based on the provided inputs, outputs, and weights.
- `MaxIterations`: An integer property that allows getting or setting the maximum number of training iterations. It ensures the value is at least 1.
- `Tolerance`: A double property that allows getting or setting the relative tolerance for convergence. It ensures the value is at least 1e-9.

## Functions
- `Learn(double[][] inputs, int[] outputs, double[] weights = null)`: This method trains the AdaBoost model using the provided input samples, corresponding binary output labels, and optional sample weights. It returns a strong classifier composed of the weighted weak classifiers. It handles null inputs, checks for input-output length consistency, and performs the boosting process through multiple iterations.
  
- `Decide(double[] input)`: This method makes a classification decision based on the ensemble of weak classifiers. It calculates a score by summing the weighted decisions of each classifier and returns 1 if the score is non-negative and 0 otherwise.

- `Decide(TModel model, double[] input)`: A private method that makes a decision using a single weak classifier model. It uses reflection to invoke the `Decide` method of the model, which must be implemented in the model class. If the method does not exist, it throws an exception.

This class encapsulates the core logic of the AdaBoost algorithm, providing a flexible and reusable component for building strong classifiers from weak models in machine learning applications.