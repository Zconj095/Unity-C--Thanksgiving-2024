# AdHocDataGenerator

## Overview
The `AdHocDataGenerator` class is designed to generate synthetic datasets for training and testing machine learning models. It creates data points based on quantum mechanics principles, utilizing various mathematical constructs such as Z matrices, parity operators, and unitary transformations. The generated data can be used for testing algorithms in quantum computing or machine learning frameworks. The main function, `GenerateData`, takes the size of training and test datasets along with other parameters to produce the datasets.

## Variables

- **trainingSize**: An integer that specifies the number of training samples to generate.
- **testSize**: An integer that specifies the number of test samples to generate.
- **n**: An integer that determines the dimensionality of the data. Supported values are 2 and 3.
- **gap**: A float that sets a threshold for determining the significance of the computed expectation values.
- **oneHot**: A boolean flag indicating whether to use one-hot encoding for the labels (default is true).
- **includeSampleTotal**: A boolean flag indicating whether to include the total number of samples in the output (default is false).

## Functions

### GenerateData
```csharp
public static (float[,], int[], float[,], int[]) GenerateData(int trainingSize, int testSize, int n, float gap, bool oneHot = true, bool includeSampleTotal = false)
```
Generates synthetic training and testing datasets based on the specified parameters. It creates Z matrices, a parity operator, and a random unitary matrix, then computes sample points and extracts the training and test samples.

### GenerateZMatrices
```csharp
private static List<float[,]> GenerateZMatrices(int n, float[,] z, float[,] identity)
```
Generates a list of Z matrices using the Kronecker product based on the input dimension `n`. It combines the Z matrix with identity matrices to create higher-dimensional representations.

### GenerateParityMatrix
```csharp
private static float[,] GenerateParityMatrix(int n)
```
Creates a parity matrix of size \(2^n \times 2^n\) where each diagonal element is set to 1 or -1 depending on the parity of the binary representation of the index.

### GenerateRandomUnitary
```csharp
private static float[,] GenerateRandomUnitary(int n)
```
Generates a random unitary matrix of size \(2^n \times 2^n\) by populating it with random float values and normalizing the matrix.

### NormalizeMatrix
```csharp
private static float[,] NormalizeMatrix(float[,] matrix)
```
Normalizes the given matrix by scaling its elements so that the sum of the squares of all elements equals 1.

### GenerateSamplePoints
```csharp
private static List<float> GenerateSamplePoints(float[] xvals, List<float[,]> zMatrices, float[,] parityMatrix, float[,] randomUnitary, int n, float gap)
```
Computes sample points based on the expectation values derived from the input values and the generated matrices. It adds a point to the list based on whether the expectation value exceeds the specified gap.

### ComputeExpectationValue
```csharp
private static float ComputeExpectationValue(float[] x, List<float[,]> zMatrices, float[,] parityMatrix, float[,] randomUnitary, int n)
```
Calculates the expectation value for a given input vector `x` by applying the feature map, followed by the random unitary transformation, and finally computing the expectation with the parity operator.

### ApplyFeatureMap
```csharp
private static float[,] ApplyFeatureMap(float[] x, List<float[,]> zMatrices, int n)
```
Applies a feature mapping to the input vector `x` using the first Z matrix in the list. This is a placeholder implementation and should be replaced with actual logic.

### ApplyRandomUnitary
```csharp
private static float[,] ApplyRandomUnitary(float[,] state, float[,] randomUnitary)
```
Applies the random unitary transformation to the input state. This is a placeholder implementation and should be replaced with actual logic.

### ExpectationValue
```csharp
private static float ExpectationValue(float[,] state, float[,] operatorMatrix)
```
Calculates the expectation value of the given state with respect to the provided operator matrix. This is a placeholder implementation and should be replaced with actual logic.

### ExtractSamples
```csharp
private static (float[,], int[]) ExtractSamples(List<float> sampleTotal, float[] xvals, int numSamples, int n)
```
Extracts a specified number of samples from the generated sample points and the x-values. It returns the samples as a matrix and their corresponding labels as an array.

### KroneckerProduct
```csharp
private static float[,] KroneckerProduct(params float[][,] matrices)
```
Computes the Kronecker product of the provided matrices. This is a placeholder implementation and should be replaced with actual logic.