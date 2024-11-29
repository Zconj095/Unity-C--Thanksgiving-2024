# RandomForest

## Overview
The `RandomForest` class is an implementation of the Random Forest algorithm specifically designed for use within the Unity game engine. It serves as a collection of decision trees that work together to make predictions based on input data. The main function of this script is to provide a structured way to manage and utilize multiple decision trees for classification tasks, enhancing the performance and accuracy of predictions by leveraging the collective decision-making of the trees.

## Variables

- **trees**: A private list that holds the collection of `DecisionTree` instances that make up the random forest.
- **NumberOfOutputs**: A public property that indicates the number of outputs produced by the decision trees in the forest.
- **NumberOfInputs**: A public property that indicates the number of input features that the decision trees expect.
- **NumberOfClasses**: A public property that indicates the total number of classes that the decision trees can predict.
- **Trees**: A public read-only property that exposes the collection of decision trees as a read-only enumerable.

## Functions

- **RandomForest()**: Constructor that initializes a new instance of the `RandomForest` class with an empty list of decision trees.

- **RandomForest(IEnumerable<DecisionTree> trees)**: Constructor that initializes a new random forest with a specified collection of decision trees. It also sets the parameters based on the first tree in the collection if it exists.

- **RandomForest(int treeCount, IList<DecisionVariable> attributes, int classCount)**: Constructor that initializes a random forest with a specified number of trees, attributes for each tree, and the number of output classes.

- **AddTree(DecisionTree tree)**: Method that adds a decision tree to the random forest. It checks for compatibility with existing trees and initializes parameters if the forest is empty.

- **Decide(double[] input)**: Method that takes an input vector and computes the most common decision among all trees in the forest, returning the index of the most frequent class decision.

- **Clear()**: Method that clears all decision trees from the forest, effectively resetting it.

- **TreeCount**: A public property that returns the number of decision trees currently in the forest.

- **InitializeParameters(DecisionTree sampleTree)**: A private method that initializes the number of inputs, outputs, and classes based on a sample decision tree.

- **ValidateTreeCompatibility(DecisionTree tree)**: A private method that checks if a new decision tree is compatible with the existing configuration of the random forest. It throws an exception if there are mismatches in the number of inputs, outputs, or classes.