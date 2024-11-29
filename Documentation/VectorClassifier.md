# VectorClassifier

## Overview
The `VectorClassifier` class is a Unity component designed to classify input states based on training data. It allows for the training of a model using labeled data and provides a method to predict the label of new input states. This functionality is essential for scenarios where classification of hybrid states is required, such as in machine learning or artificial intelligence applications within a Unity project.

## Variables

- `trainingData`: A private list that stores instances of `HybridState` used for training the classifier.
- `labels`: A private list that holds integer labels corresponding to each `HybridState` in the `trainingData`. These labels represent the classifications of the training data.

## Functions

- `VectorClassifier()`: Constructor that initializes the `trainingData` and `labels` lists when a new instance of `VectorClassifier` is created.

- `void Train(HybridState state, int label)`: This method adds a new `HybridState` and its corresponding label to the training data. It is used to train the classifier with new examples.

- `int Predict(HybridState state)`: This method predicts the label for a given `HybridState`. It calculates a score for each training state by combining the results of binding and interference operations, and returns the label of the training state that produces the highest score. This is the core functionality of the classifier, enabling it to make predictions based on learned data.