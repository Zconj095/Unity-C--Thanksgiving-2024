# TrainValTestSplit<T>

## Overview
The `TrainValTestSplit<T>` class is designed to represent a structured division of data into three distinct parts: training, validation, and testing. This class is particularly useful in machine learning contexts where datasets are commonly split into these segments to train models, validate their performance during training, and finally test their efficacy on unseen data. By encapsulating these splits within a single class, it simplifies the management and access of these subsets throughout the codebase.

## Variables
- **Training**: This property holds the training data split, which is used to train the machine learning model.
- **Validation**: This property contains the validation data split, which is used to tune the model's parameters and avoid overfitting during training.
- **Testing**: This property represents the testing data split, which is employed to evaluate the model's performance after it has been trained and validated.

## Functions
- **TrainValTestSplit()**: This is the default constructor that initializes a new instance of the `TrainValTestSplit<T>` class without any predefined splits.
  
- **TrainValTestSplit(T training, T validation, T testing)**: This constructor allows the instantiation of the `TrainValTestSplit<T>` class with specified training, validation, and testing splits. It initializes the respective properties with the provided data.

- **IEnumerator<T> GetEnumerator()**: This function provides an enumerator that allows iteration through the splits (Training, Validation, Testing) in the collection. It yields each split in turn.

- **IEnumerator IEnumerable.GetEnumerator()**: This is an explicit interface implementation that returns a non-generic enumerator for the collection, enabling compatibility with non-generic collection interfaces. It calls the generic `GetEnumerator()` method to perform the actual iteration.