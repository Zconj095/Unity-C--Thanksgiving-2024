# CorticalMatrixProcessor

## Overview
The `CorticalMatrixProcessor` class is designed to perform calculations on a two-dimensional array (matrix) of floating-point numbers. Its primary functions are to compute the means of the matrix either row-wise or column-wise, and to determine the differentiation of these means. This class is particularly useful in scenarios where data analysis of cortical structures is required, as it simplifies the process of obtaining mean values and their changes across specified dimensions. It integrates seamlessly into Unity's MonoBehaviour framework, allowing it to be utilized within Unity's game development environment.

## Variables
- **matrix**: A two-dimensional array of floats that represents the data to be processed. It is the input for the mean and differentiation calculations.
- **rowWise**: A boolean flag indicating whether to compute means row-wise (`true`) or column-wise (`false`). It determines the direction of the calculations.
- **rows**: An integer representing the number of rows in the input matrix.
- **cols**: An integer representing the number of columns in the input matrix.
- **means**: A one-dimensional array of floats that stores the computed mean values from the matrix.
- **sum**: A float variable used to accumulate the sum of the elements in a row or column during the mean calculation.
- **differences**: A one-dimensional array of floats that stores the computed differences between consecutive mean values.

## Functions
- **ComputeCorticalMeans(float[,] matrix, bool rowWise = true)**: 
  - This static function calculates the mean values of the input matrix. It can compute these means either row-wise or column-wise based on the `rowWise` parameter. The function returns an array of floats containing the computed means.

- **ComputeCorticalMeansDifferentiation(float[,] matrix, bool rowWise = true)**: 
  - This static function calculates the difference between consecutive mean values obtained from the `ComputeCorticalMeans` function. It returns an array of floats representing the differences, which can also be computed either row-wise or column-wise based on the `rowWise` parameter.

- **Start()**: 
  - This is a Unity-specific method that initializes the class. It creates a sample matrix and demonstrates the usage of the mean and differentiation functions by computing and logging the results for both row-wise and column-wise operations.