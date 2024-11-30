# GridSearchParameter and GridSearchParameterCollection

## Overview
The `GridSearchParameter` and `GridSearchParameterCollection` classes are designed to handle parameters used in grid search for machine learning models. The `GridSearchParameter` structure represents a single parameter with a name and value, while the `GridSearchParameterCollection` class provides a collection of these parameters, allowing for easy management and retrieval based on parameter names. This functionality is crucial for optimizing machine learning models by systematically exploring a range of parameter values.

## Variables

### GridSearchParameter
- `name`: A string that stores the name of the parameter.
- `value`: A double that holds the value of the parameter.

### GridSearchParameterCollection
- Inherits from `KeyedCollection<string, GridSearchParameter>`, which manages a collection of `GridSearchParameter` objects using their names as keys.

## Functions

### GridSearchParameter
- **GridSearchParameter(string name, double value)**: Constructor that initializes a new instance of the `GridSearchParameter` with a specified name and value.
- **Equals(object obj)**: Overrides the default equality check to determine if another object is equal to the current `GridSearchParameter` instance based on its name and value.
- **GetHashCode()**: Provides a hash code for the `GridSearchParameter`, combining the hash codes of the name and value.
- **operator ==(GridSearchParameter parameter1, GridSearchParameter parameter2)**: Defines equality comparison between two `GridSearchParameter` instances.
- **operator !=(GridSearchParameter parameter1, GridSearchParameter parameter2)**: Defines inequality comparison between two `GridSearchParameter` instances.
- **ToString()**: Returns a string representation of the `GridSearchParameter`, formatted as "name: value".
- **implicit operator double(GridSearchParameter param)**: Allows for implicit conversion from a `GridSearchParameter` to its value as a double.

### GridSearchParameterCollection
- **GridSearchParameterCollection(params GridSearchParameter[] parameters)**: Constructor that initializes a new collection of `GridSearchParameter` objects from an array of parameters.
- **GridSearchParameterCollection(IEnumerable<GridSearchParameter> parameters)**: Constructor that initializes a new collection of `GridSearchParameter` objects from an enumerable collection of parameters.
- **GetKeyForItem(GridSearchParameter item)**: Overrides the method to return the identifying key (name) for a `GridSearchParameter` in the collection.