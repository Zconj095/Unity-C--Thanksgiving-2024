# Bayes Class Documentation

## Overview
The `Bayes` class implements a Bayesian model for machine learning, allowing for the management and evaluation of multiple distributions associated with different classes. It is designed to be generic, accepting any distribution type and input type, which makes it flexible for various applications. The class is part of the `EdgeLoreMachineLearning` namespace and is used to compute the log-likelihood of inputs given a specific class, leveraging the prior probabilities of each class.

## Variables

- **distributions**: An array of type `TDistribution` that holds the different distributions for each class. This array is initialized based on the number of classes specified during instantiation.
  
- **priors**: An array of type `double` that contains the prior probabilities for each class. This array is initialized with equal probabilities based on the number of classes.

- **NumberOfClasses**: An integer representing the total number of classes in the model. This value is set during initialization and cannot be modified afterward.

- **NumberOfInputs**: An integer representing the total number of input features the model expects. This value is also set during initialization and cannot be modified afterward.

## Functions

- **Bayes(int classes, int inputs, Func<TDistribution> initializer)**: Constructor that initializes the `Bayes` class with a specified number of classes and inputs. It uses a provided initializer function to create the distribution for each class.

- **Bayes(int classes, int inputs, Func<int, TDistribution> initializer)**: Overloaded constructor that initializes the `Bayes` class similarly but allows for a different initializer function that takes the class index as a parameter for creating the distribution.

- **Initialize(int classes, int inputs)**: A private method that sets up the class by validating the number of classes and inputs. It initializes the `distributions` and `priors` arrays based on the specified parameters.

- **LogLikelihood(TInput input, int classIndex)**: This method computes the log-likelihood of a given input belonging to a specified class. It retrieves the corresponding distribution, checks if it is defined, and invokes the `LogProbabilityFunction` method from the distribution to calculate the log probability. The result combines this log probability with the log of the prior probability for the class.

Overall, the `Bayes` class provides a structured way to implement Bayesian inference with custom distributions and is essential for probabilistic modeling in the codebase.