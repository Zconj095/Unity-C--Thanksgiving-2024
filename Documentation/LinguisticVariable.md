# LinguisticVariable

## Overview
The `LinguisticVariable` class represents a linguistic variable in a Fuzzy Inference System. It serves as a fundamental component that allows the definition of variables with fuzzy labels, which can be used to evaluate membership degrees in fuzzy logic applications. This class manages the range of values for the variable, stores linguistic labels (which represent fuzzy sets), and provides methods to manipulate and interact with these labels. It fits into the broader codebase by enabling the construction of fuzzy inference systems that can handle uncertainty and vagueness in data.

## Variables
- **name**: A `string` representing the name of the linguistic variable.
- **start**: A `float` indicating the left limit of the valid range for the variable.
- **end**: A `float` indicating the right limit of the valid range for the variable.
- **labels**: A `Dictionary<string, object>` that stores the linguistic labels associated with the variable, where the key is the label name and the value is the corresponding fuzzy set.
- **numericInput**: A `float` that holds the numerical value of the input for this linguistic variable.

## Functions
- **LinguisticVariable(string name, float start, float end)**: Constructor that initializes a new instance of the `LinguisticVariable` class with the specified name, start, and end values.

- **AddLabel(object label)**: Adds a linguistic label (fuzzy set) to the variable. It checks for duplicate label names and ensures that the label's limits are within the variable's range. Throws an `ArgumentException` if the label name already exists or if the label limits are outside the variable range.

- **ClearLabels()**: Removes all linguistic labels associated with the linguistic variable.

- **GetLabel(string labelName)**: Retrieves an existing label from the linguistic variable by its name. Throws a `KeyNotFoundException` if the label is not found.

- **GetLabelMembership(string labelName, float value)**: Calculates the degree of membership of a given value to a specified label (fuzzy set). It invokes the `GetMembership` method of the label object. Throws a `KeyNotFoundException` if the label is not found, and an `InvalidOperationException` if the label object is missing the `GetMembership` method.