# AssociationRule2Matcher

## Overview
The `AssociationRule2Matcher` class is designed for classification and transformation of data within the Unity environment using association rules. It utilizes a collection of rules to evaluate input datasets and make predictions based on the strength of the associations defined by these rules. This class fits into a larger codebase focused on machine learning techniques, specifically association rule learning, and serves as a tool for applying learned rules to classify and transform new data.

## Variables
- `items`: An integer representing the number of distinct items in the dataset.
- `rules`: An array of `AssociationRule2<T>` objects that define the association rules for classification.
- `threshold`: A double value that sets the confidence threshold for applying the rules.

## Functions
- `AssociationRule2Matcher(int items, AssociationRule2<T>[] rules)`: Constructor that initializes a new instance of the `AssociationRule2Matcher` class with a specified number of items and an array of association rules.
  
- `NumberOfInputs`: Property that returns the number of items seen by the model during training.

- `NumberOfOutputs`: Property that returns the number of rules in the model.

- `Rules`: Property to get or set the association rules in the model.

- `Threshold`: Property to get or set the confidence threshold for rule application.

- `Scores(HashSet<T> input, ref List<HashSet<T>> decision)`: Method that computes scores and corresponding decisions for a given input set. It evaluates the input against the rules and returns an array of scores representing the strength of the associations.

- `Decide(HashSet<T> input)`: Method that predicts a class decision for a given input set by calling the `Scores` method and returning the decisions.

- `Decide(HashSet<T>[] inputs)`: Overloaded method that computes decisions for multiple input sets, returning a list of decisions for each input set.

- `Transform(HashSet<T> input)`: Method that applies the transformation to an input, producing a list of decisions by calling the `Decide` method.

- `Transform(HashSet<T>[] inputs)`: Overloaded method that applies the transformation to multiple inputs, producing a list of decisions for each input set.

## AssociationRule2 Class
### Overview
The `AssociationRule2` class represents an individual association rule used in the matching process. It defines the conditions under which a rule is considered to match a given input set.

### Variables
- `X`: A `HashSet<T>` representing the antecedent of the rule (conditions).
- `Y`: A `HashSet<T>` representing the consequent of the rule (outcomes).
- `Confidence`: A double value indicating the confidence level of the rule.
- `Support`: A double value representing the support level of the rule.

### Functions
- `Matches(HashSet<T> input)`: Method that checks if the rule matches the given input set by determining if the antecedent `X` is a subset of the input.

## SetComparer Class
### Overview
The `SetComparer` class provides a custom comparer for `HashSet<T>` to enable its use as a key in dictionaries. This is essential for maintaining unique keys based on the contents of the sets.

### Functions
- `Equals(HashSet<T> x, HashSet<T> y)`: Method that determines if two `HashSet<T>` instances are equal by checking if they contain the same elements.

- `GetHashCode(HashSet<T> obj)`: Method that generates a hash code for a `HashSet<T>` by combining the hash codes of its elements.