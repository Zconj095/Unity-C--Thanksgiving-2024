# NaiveBayesLearning

## Overview
The `NaiveBayesLearning` class is a specialized implementation of a Naive Bayes learning algorithm designed for machine learning tasks. It extends the `NaiveBayesLearningBase` class, providing functionality to create a Naive Bayes model, learn from input data, and estimate probabilities for classification. This class fits within the larger `EdgeLoreMachineLearning` namespace, which likely contains other machine learning algorithms and tools.

## Variables
- `Options`: An object intended to hold configuration options for the Naive Bayes learning process. It is initialized to a new object in the constructor but should be replaced with a more specific options class if necessary.
- `Model`: Inherited from `NaiveBayesLearningBase`, this variable holds the model instance that is being learned or utilized.

## Functions

### Constructor
- `NaiveBayesLearning()`
  - Initializes a new instance of the `NaiveBayesLearning` class and sets the `Options` variable.

### Protected Methods
- `Create(int[][] x, int y)`
  - Creates an instance of the Naive Bayes model using the provided input data (`x`) and output label (`y`). It calculates input values, finds the Naive Bayes type, and invokes its constructor.

### Public Methods
- `Learn(int[][] x, int[] y, double[] weight = null)`
  - Learns a model that maps input data (`x`) to output labels (`y`). It validates the input arguments and, if the model is not already created, initializes it. It then iterates through the unique output classes to perform learning on each class.

### Private Methods
- `InnerLearn(int[][] x, int[] y, int classIndex)`
  - Learns from the input data specific to a class index. It determines which samples belong to the class and estimates probabilities for each input feature.

- `InnerEstimate(int classIndex, int inputIndex, int[][] values)`
  - Estimates the probabilities of input values for a specific class and input feature. It counts the occurrences of each symbol in the input data.

- `TransformToProbabilities(double[] frequencies, object distribution)`
  - Converts frequency counts into probabilities and sets these probabilities in the model's distribution object using reflection.

- `ValidateArguments(int[][] x, int[] y, double[] weight)`
  - Validates the input arguments to ensure they are not null and that weights, if provided, match the number of samples.

- `GetOutputCount()`
  - Retrieves the number of output classes defined in the model.

- `SetModelPrior(int classIndex, double value)`
  - Sets the prior probability for a specified class in the model.

- `GetSymbolCount(int inputIndex)`
  - Retrieves the number of symbols (or unique values) for a given input feature.

- `GetDistribution(int classIndex, int inputIndex)`
  - Retrieves the distribution object for a specific class and input feature from the model.