# KNearestNeighbors

## Overview
The `KNearestNeighbors` class implements the K-Nearest Neighbors (kNN) algorithm, which is a simple and effective machine learning technique used for classification and regression tasks. This class is part of the `EdgeLoreMachineLearning` namespace and extends a base class `BaseKNearestNeighbors`. The main function of this script is to allow users to train a model with labeled data and then make predictions based on the distance to the nearest neighbors of a given input point. The kNN algorithm is particularly useful in scenarios where the decision boundary is complex and hard to model with traditional algorithms.

## Variables
- `data`: A private list that stores training data as tuples, where each tuple consists of a position (an array of doubles) and a label (an integer). This list is used to hold the training examples used for predicting the class of new inputs.

## Functions
- **KNearestNeighbors()**: Constructor that initializes a new instance of the `KNearestNeighbors` class and initializes the `data` list.

- **KNearestNeighbors(int k)**: Constructor that initializes a new instance with a specified number of neighbors (k) and sets the distance metric to the default Euclidean distance.

- **KNearestNeighbors(int k, Func<double[], double[], double> distance)**: Constructor that allows the user to specify both the number of neighbors (k) and a custom distance metric function.

- **double EuclideanDistance(double[] point1, double[] point2)**: A private method that calculates and returns the Euclidean distance between two points represented as arrays of doubles.

- **void AddTrainingData(double[][] inputs, int[] outputs)**: Public method to add training data to the model. It takes an array of input vectors and an array of corresponding output labels. It throws an exception if the lengths of inputs and outputs do not match.

- **double[] Scores(double[] input)**: Overrides the base method to compute and return an array of scores representing the association between the input point and each class based on the nearest neighbors.

- **double[][] GetNearestNeighbors(double[] input, out int[] labels)**: Overrides the base method to retrieve the k-nearest neighbors for a given input point. It returns an array of the nearest positions and outputs the corresponding labels.

- **KNearestNeighbors Learn(double[][] inputs, int[] outputs, double[] weights = null)**: Overrides the base method to train the kNN model with the provided dataset. It checks the validity of the arguments and adds the training data.

- **static KNearestNeighbors FromData(int k, Func<double[], double[], double> distance, double[][] inputs, int[] outputs)**: Static method that creates a new instance of `KNearestNeighbors` from existing training data. It initializes the instance with a specified number of neighbors and a custom distance function, then adds the provided training data.