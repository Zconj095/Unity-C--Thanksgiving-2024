# InnerParameters Class Documentation

## Overview
The `InnerParameters` class is designed to encapsulate the parameters required for learning a binary decision model within the EdgeLoreMachineLearning framework. It serves as a data structure that is utilized by learning algorithms such as `OneVsRestLearning` or `OneVsOneLearning` to configure how binary classifiers are created. By providing a clear organization of the model, inputs, outputs, and class pairs, this class simplifies the process of training binary classifiers.

## Variables
- **model** (`TBinary`): Represents the binary model that will be learned. This is the core model that the classifier will use.
- **inputs** (`TInput[]`): An array of input data that will be utilized during the training of the classifier. This data serves as the features or attributes for the model.
- **outputs** (`bool[]`): An array of boolean values representing the expected outputs for the corresponding inputs. These values indicate the true classifications for the training data.
- **pair** (`ClassPair`): An instance of the `ClassPair` class that holds the class labels relevant to the binary classification problem being addressed. This defines the specific classes that the classifier will learn to distinguish between.

## Functions
- **Model**: A property that retrieves the binary model to be learned.
- **Inputs**: A property that retrieves the input data used for training the classifier.
- **Outputs**: A property that retrieves the output data used for training the classifier.
- **Pair**: A property that retrieves the class pair that the classifier is designated to learn.
- **InnerParameters(TBinary model, TInput[] inputs, bool[] outputs, ClassPair pair)**: A constructor that initializes a new instance of the `InnerParameters` class with the specified model, inputs, outputs, and class pair. This constructor sets up the parameters necessary for training the binary classifier.