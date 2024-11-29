# SubproblemEventArgsV2

## Overview
The `SubproblemEventArgsV2` class is designed to encapsulate information related to the progress of a subproblem within the context of machine learning tasks in the `EdgeLoreMachineLearning` namespace. This class inherits from `EventArgs`, making it suitable for use in event-driven programming, where it can be used to pass data to event handlers about the progress of a subproblem. It provides properties to access the classes involved in the subproblem, the current progress, and the maximum progress value.

## Variables
- **class1**: An integer representing one of the classes associated with the subproblem.
- **class2**: An integer representing the other class associated with the subproblem.
- **progress**: An integer indicating the current progress of the subproblem, which can be updated.
- **maximum**: An integer representing the maximum value for the current progress, which can also be updated.

## Functions
- **SubproblemEventArgsV2(int class1, int class2)**: Constructor that initializes a new instance of the `SubproblemEventArgsV2` class with the specified classes. It takes two integer parameters, `class1` and `class2`, which represent the classes involved in the subproblem.

- **Class1**: A public property that returns the value of `class1`, representing one of the classes in the subproblem.

- **Class2**: A public property that returns the value of `class2`, representing the other class in the subproblem.

- **Progress**: A public property that gets or sets the current progress of the subproblem. It is an integer value that ranges from zero to the maximum value.

- **Maximum**: A public property that gets or sets the maximum value for the current progress. It allows the definition of the upper limit for the progress tracking.