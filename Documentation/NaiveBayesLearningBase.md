# NaiveBayesLearningBase

## Overview
The `NaiveBayesLearningBase` class is an abstract base class designed for implementing Naive Bayes learning algorithms in a machine learning context. This class is generic, allowing for flexibility in the types of models, distributions, inputs, and options that can be used. It provides a foundational structure for learning from labeled data, managing the learning process, and fitting statistical distributions to the data. The class is intended to be extended by concrete implementations that specify the model and distribution behaviors.

## Variables
- `private bool optimized`: A flag indicating whether the model has been optimized through fitting.
- `public TModel Model { get; set; }`: The model being trained and used for predictions.
- `public bool Empirical { get; set; }`: A flag that determines if empirical priors should be used in the model.
- `public TOptions Options { get; set; }`: Options for configuring the learning process, created as a new instance of `TOptions`.
- `public Func<int, int, TDistribution> Distribution { get; set; }`: A function that defines how to create instances of the distribution type `TDistribution`.
- `public CancellationToken Token { get; set; }`: A token for managing cancellation of asynchronous operations.

## Functions
- `protected abstract TModel Create(TInput[][] x, int y)`: An abstract method that must be implemented by derived classes to create a model based on the input data and labels.

- `public virtual TModel Learn(TInput[][] x, int[] y, double[] weight = null)`: This method initiates the learning process. It validates the input arguments, creates the model if it does not exist, and then performs parallel learning for each output class.

- `private void InnerLearn(TInput[][] x, int[] y, double[] weight, int classIndex)`: A private method that processes the learning for a specific class index. It gathers samples belonging to that class, optionally sets the model prior, and fits the model to the samples.

- `protected virtual void Fit(int i, TInput[][] values, double[] weights, bool transposed)`: A method that fits the model to the provided values and weights using the specified distribution's `Fit` method. It checks for the existence of the `Fit` method and invokes it, setting the optimized flag to true.

- `private void ValidateArguments(TInput[][] x, int[] y, double[] weight)`: A private method that checks the validity of the input data and labels, ensuring they are not null and that weights, if provided, match the number of samples.

- `private int GetOutputCount()`: A private method that retrieves the number of outputs from the model by accessing the `NumberOfOutputs` property. It throws an exception if the property is not defined.

- `private void SetModelPrior(int classIndex, double value)`: A private method that sets the prior probability for a specific class in the model by accessing the `Priors` property. It checks for the existence of the property and ensures it is configured correctly.