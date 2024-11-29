# EdgeLoreMachineLearning

## Overview
The `EdgeLoreMachineLearning` script defines two structures, `ClassPair` and `Decision`, which are used to represent pairs of class labels and the decisions made between them in a machine learning context. This script is essential for managing and processing classification decisions, particularly in scenarios where two classes are compared and a winner is determined. The structures facilitate clear representation and manipulation of class relationships, making them integral to the broader machine learning framework.

## Variables

### ClassPair Structure
- **class1**: An integer representing the first class in the pair.
- **class2**: An integer representing the second class in the pair.

### Decision Structure
- **pair**: An instance of `ClassPair` that holds the two adversarial classes being compared.
- **winner**: An integer representing the class label of the winning class from the pair.

## Functions

### ClassPair Functions
- **Class1**: A property that returns the first class in the pair.
- **Class2**: A property that returns the second class in the pair.
- **ClassPair(int i, int j)**: A constructor that initializes a new instance of `ClassPair` with the specified class labels `i` and `j`.
- **ToTuple()**: Converts the `ClassPair` instance into a tuple containing both class labels.
- **ToString()**: Returns a string representation of the `ClassPair` in the format "class1,class2".
- **Equals(ClassPair other)**: Determines if the current `ClassPair` instance is equal to another `ClassPair` instance.
- **Equals(object obj)**: Overrides the default equality check to compare with another object.
- **GetHashCode()**: Returns a hash code for the `ClassPair` instance, which is used for hashing algorithms and data structures.

### Decision Functions
- **Pair**: A property that returns the `ClassPair` instance representing the adversarial classes.
- **Winner**: A property that returns the class label of the winning class.
- **Decision(int i, int j, int winner)**: A constructor that initializes a new instance of `Decision` with the specified class labels `i`, `j`, and the winning class. It throws an exception if the winner is not one of the provided classes.
- **ToTuple()**: Converts the `Decision` instance into a tuple containing the two class labels and the winner.
- **ToString()**: Returns a string representation of the `Decision` in the format "class1,class2>winner".