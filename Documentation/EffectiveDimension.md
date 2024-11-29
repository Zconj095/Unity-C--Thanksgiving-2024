# EffectiveDimension

## Overview
The `EffectiveDimension` class is designed to facilitate the execution of Monte Carlo simulations for a given model. It manages the generation and handling of weight and input samples, which are critical for the performance of the model during the simulation. The class provides methods to set these samples, ensuring they adhere to the model's specifications, and it runs the Monte Carlo simulation by invoking the model's forward and backward methods. This class fits within a larger codebase that likely includes models requiring probabilistic analysis or optimization techniques.

## Variables

- `_model`: An object representing the model for which the Monte Carlo simulations are performed. This model must expose specific properties and methods.
  
- `_weightSamples`: A 2-dimensional array of floats that holds the weight samples generated for the model.
  
- `_inputSamples`: A 2-dimensional array of floats that contains the input samples used in the simulations.
  
- `_numWeightSamples`: An integer that stores the number of weight samples generated.
  
- `_numInputSamples`: An integer that keeps track of the number of input samples.

## Functions

- **Constructor: `EffectiveDimension(object model, int weightSampleCount = 1, int inputSampleCount = 1)`**
  - Initializes an instance of the `EffectiveDimension` class. It requires a model object and optionally accepts counts for weight and input samples. It throws an exception if the model is null and sets the weight and input samples based on the provided counts.

- **Property: `WeightSamples`**
  - Gets or sets the weight samples. When setting, it checks that the dimension of the provided samples matches the model's weight dimension. If they do not match, it throws an exception.

- **Property: `InputSamples`**
  - Gets or sets the input samples. Similar to `WeightSamples`, it verifies that the dimension of the provided samples matches the model's input dimension, throwing an exception if they do not.

- **Method: `RunMonteCarlo()`**
  - Executes the Monte Carlo simulation by iterating through the weight and input samples. It invokes the model's forward and backward methods, storing the results in the `gradients` and `outputs` arrays, which are then returned as a tuple.

- **Private Method: `SetWeightSamples(int count)`**
  - Generates random weight samples based on the specified count and the model's weight dimension.

- **Private Method: `SetInputSamples(int count)`**
  - Generates random input samples based on the specified count and the model's input dimension.

- **Private Static Method: `GetProperty<T>(object obj, string propertyName)`**
  - Retrieves a property value from the specified object using reflection. It throws an exception if the property is not found.

- **Private Static Method: `InvokeMethod<T>(object obj, string methodName, params object[] args)`**
  - Invokes a method on the specified object using reflection and returns the result. It throws an exception if the method is not found.

- **Private Static Method: `GenerateRandomArray(int rows, int cols)`**
  - Creates a 2-dimensional array filled with random float values.

- **Private Static Method: `GetRow(float[,] matrix, int rowIndex)`**
  - Extracts a specific row from a 2-dimensional array and returns it as a 1-dimensional array.

- **Private Static Method: `SetRow(float[,] matrix, int rowIndex, float[] row)`**
  - Sets a specific row in a 2-dimensional array using values from a 1-dimensional array.

- **Private Static Method: `GetSlice(float[,,] array, int index)`**
  - Extracts a 2-dimensional slice from a 3-dimensional array based on the specified index.

- **Private Static Method: `SetSlice(float[,,] array, int index, float[,] slice)`**
  - Sets a specific slice in a 3-dimensional array using values from a 2-dimensional array.