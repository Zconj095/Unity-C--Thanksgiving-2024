# AveragedStochasticGradientDescent

## Overview
The `AveragedStochasticGradientDescent` class implements a machine learning algorithm based on the stochastic gradient descent (SGD) method with averaging. It is designed to optimize a model's parameters using a specified kernel and loss function. This class fits into the broader codebase by providing a mechanism for training models, making predictions, and managing model persistence through saving and loading. The use of generics allows it to be flexible and applicable to various models, kernels, inputs, and loss functions.

## Variables
- `private TKernel kernel`: Stores the kernel used for the model.
- `private double lambda`: Regularization parameter to prevent overfitting (default is `1e-3`).
- `private double eta0`: Initial learning rate (default is `0.01`).
- `private double mu0`: A parameter related to the weight averaging (default is `1.0`).
- `private double tstart`: Not used in the provided code but could represent a starting time for training.
- `private double[] weights`: Array holding the current weights of the model.
- `private double[] averagedWeights`: Array holding the averaged weights for the model.
- `private double weightDivisor`: Used for weight normalization (default is `1.0`).
- `private double weightBias`: Stores the bias term for the weights.
- `private double averagedWeightBias`: Stores the averaged bias term.

## Functions
- **`public TKernel Kernel { get; set; }`**: Property to get or set the kernel used in the model.

- **`public double LearningRate { get; set; }`**: Property to get or set the learning rate for the model.

- **`public double Lambda { get; set; }`**: Property to get or set the regularization parameter.

- **`public void Train(IEnumerable<TInput> inputs, IEnumerable<bool> outputs)`**: Trains the model using the provided input-output pairs. It retrieves the `Score` method from the model type and iteratively updates the weights based on the inputs and outputs.

- **`private void TrainOne(TInput input, bool output, double eta, double mu)`**: Performs a single update of the model's weights and biases based on one input-output pair. It adjusts the weights using the specified learning rate and regularization.

- **`private double Predict(TInput input)`**: Predicts the output for a given input based on the current weights and bias. Throws an exception if the model has not been trained.

- **`private double EvaluateLoss(IEnumerable<TInput> inputs, IEnumerable<bool> outputs)`**: Evaluates the loss of the model by comparing predictions against actual outputs. It calculates the mean squared error over the provided data.

- **`private IEnumerable<(TInput, bool)> PairInputsOutputs(IEnumerable<TInput> inputs, IEnumerable<bool> outputs)`**: Pairs inputs with their corresponding outputs for training, yielding tuples of input-output pairs.

- **`public void SaveModel(string path)`**: Serializes the model's weights, averaged weights, and biases to a JSON file at the specified path.

- **`public void LoadModel(string path)`**: Deserializes the model's parameters from a JSON file at the specified path, restoring the model's state. 

- **`[Serializable] private class ModelData`**: A private class used for serializing the model's data, including weights and biases. The kernel is commented out but can be included if needed.