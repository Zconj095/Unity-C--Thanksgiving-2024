# OneVsRest Classifier

## Overview
The `OneVsRest` class serves as a base implementation for multi-class classification using a "one-vs-rest" approach. This method involves training multiple binary classifiers, each responsible for distinguishing one class from all others. The class is generic and can work with different types of binary classifiers and input data types. This functionality is essential for machine learning applications where multiple classes need to be classified based on input data.

## Variables
- **models**: A private list that holds instances of binary classifiers. Each classifier corresponds to one class in the multi-class classification problem.

## Functions
- **OneVsRest(int classes, Func<TModel> initializer)**: Constructor that initializes a new instance of the `OneVsRest` class. It takes the number of classes and a function to create the inner binary classifiers.

- **Initialize(int classes, Func<TModel> initializer)**: A private method that sets up the binary classifiers. It ensures that the number of classes is greater than one and populates the `models` list with newly created classifiers.

- **GetClassifierForClass(int classIndex)**: Returns the binary classifier associated with the specified class index.

- **Models**: A public property that provides access to the list of inner binary classifiers.

- **Decide(TInput input, int classIndex)**: Determines whether a given input vector belongs to the specified class by using the corresponding binary classifier.

- **Score(TInput input, int classIndex)**: Computes a numerical score that indicates the association between the input vector and the specified class. This method requires the binary classifier to support scoring.

- **GetEnumerator()**: Returns an enumerator that allows iteration through all binary classifiers, yielding key-value pairs of class indices and their corresponding classifiers.

## OneVsRest<TModel> Class
- **OneVsRest(int classes, Func<TModel> initializer)**: A simplified version of the `OneVsRest` class that specifically handles `double[]` inputs. It inherits from the generic `OneVsRest<TModel, double[]>` class, allowing for ease of use with the common input type.

## IBinaryScoreClassifier Interface
- **Score(TInput input, out bool decision)**: An interface method that allows classifiers to provide a numerical score along with a decision indicating if the input belongs to the class. This interface extends the basic classifier functionality and is used within the `Score` method of the `OneVsRest` class.