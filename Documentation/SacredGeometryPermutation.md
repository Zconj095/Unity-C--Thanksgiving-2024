# SacredGeometryPermutation

## Overview
The `SacredGeometryPermutation` script is a Unity MonoBehaviour that generates sacred geometric patterns, computes permutations of a list of integers, and performs a twin splice operation on two vectors. This script serves as a utility for creating visual patterns and mathematical operations that can be useful in various applications, such as game development or mathematical visualizations.

## Variables

- **TwinSplice**: A private nested class that represents a combination of two vectors (`VectorA` and `VectorB`) and generates a combined vector from them.
  
- **VectorA**: A property of the `TwinSplice` class that holds the first vector as an array of floats.

- **VectorB**: A property of the `TwinSplice` class that holds the second vector as an array of floats.

- **CombinedVector**: A property of the `TwinSplice` class that stores the resulting combined vector after performing the twin splice operation.

## Functions

### `GenerateSacredPattern(int numPoints, float radius, string shape = "circle")`
This private method generates a list of 2D points that form a sacred geometric pattern based on the specified shape. It can create points for either a circle or a spiral.

- **Parameters**:
  - `numPoints`: The number of points to generate in the pattern.
  - `radius`: The radius of the shape.
  - `shape`: A string that determines the shape type ("circle" or "spiral").

- **Returns**: A list of `Vector2` points representing the generated pattern.

### `GeneratePermutations(List<int> elements)`
This private method generates all possible permutations of a given list of integers.

- **Parameters**:
  - `elements`: A list of integers for which to generate permutations.

- **Returns**: A list of lists, where each inner list represents a unique permutation of the input list.

### `Permute(List<int> elements, int start, List<List<int>> permutations)`
This private recursive method is responsible for generating permutations by swapping elements in the list.

- **Parameters**:
  - `elements`: The list of integers to permute.
  - `start`: The current index to start the permutation from.
  - `permutations`: A list to store all generated permutations.

### `Swap(List<int> elements, int a, int b)`
This private method swaps two elements in a list.

- **Parameters**:
  - `elements`: The list of integers in which to swap elements.
  - `a`: The index of the first element to swap.
  - `b`: The index of the second element to swap.

### `Start()`
This Unity lifecycle method is called before the first frame update. It serves as an example of how to use the functionalities provided by the class.

- It creates an instance of `TwinSplice` using two example vectors and logs the combined vector.
- It generates a sacred circle pattern with a specified number of points and radius, then logs the generated points.
- It generates permutations for a sample list of integers and logs each permutation.