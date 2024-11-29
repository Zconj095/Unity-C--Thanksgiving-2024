# NaiveBayes

## Overview
The `NaiveBayes` class implements a Naive Bayes classifier, which is a probabilistic machine learning model used for classification tasks. This class is designed to handle multiple classes and can be trained on input data to predict the class of new instances based on learned distributions and prior probabilities. It fits within the `EdgeLoreMachineLearning` namespace, which likely contains other machine learning algorithms and utilities.

## Variables
- `symbols`: An array of integers representing the number of possible symbols (features) for each input variable.
- `priors`: An array of doubles that holds the prior probabilities for each class, initialized to uniform distribution.
- `distributions`: A three-dimensional array that holds the conditional probability distributions for each class and each symbol.

## Functions
- **NaiveBayes(int classes, params int[] symbols)**: Constructor that initializes the Naive Bayes model. It takes the number of classes and an array of symbols as parameters. It validates inputs and initializes the priors and distributions.

- **int[] NumberOfSymbols**: Property that returns the array of symbols.

- **double[] Priors**: Property to get or set the priors. It throws an exception if the provided array does not match the expected size.

- **double[,][] Distributions**: Property that returns the distributions array.

- **static NaiveBayes CreateNormal(int classes, int inputs)**: Static method that creates a Naive Bayes instance with a normal configuration, initializing each input with two symbols.

- **static NaiveBayes CreateNormal(int classes, int inputs, double[] classPriors)**: Static method that creates a Naive Bayes instance with specified class priors, initializing each input with two symbols.

- **double ComputeLikelihood(int[] input, int classIndex)**: Computes the log likelihood of an input belonging to a specified class. It validates the class index and input symbols, ensuring they are in the correct range.

- **int Predict(int[] input)**: Predicts the class of a given input based on the computed likelihoods for each class, returning the class with the highest likelihood.

- **void Train(int[][] inputs, int[] outputs)**: Trains the Naive Bayes model using provided input-output pairs. It counts the frequencies of symbols for each class, transforms these frequencies into probabilities, and updates the prior probabilities based on the training data. It ensures that the lengths of inputs and outputs match before proceeding.