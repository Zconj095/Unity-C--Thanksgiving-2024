# Clause Class Documentation

## Overview
The `Clause` class represents a fuzzy clause, which is a linguistic expression that describes a relationship between a linguistic variable and a fuzzy set. Specifically, it follows the format "Variable IS Value". This class is part of the UnityFuzzy namespace and is designed to facilitate fuzzy logic operations within the codebase. It allows for the evaluation of fuzzy clauses and provides a string representation of the clause for easier understanding and debugging.

## Variables
- `LinguisticVariable variable`: This variable holds the linguistic variable associated with the clause. It represents the concept or property being evaluated.
- `FuzzySet label`: This variable contains the label of the clause, which is represented as a fuzzy set. The label defines the fuzzy value associated with the linguistic variable.

## Functions
- `public LinguisticVariable Variable { get; }`: This property retrieves the linguistic variable of the clause.

- `public FuzzySet Label { get; }`: This property retrieves the fuzzy set label of the clause.

- `public Clause(LinguisticVariable variable, FuzzySet label)`: This is the constructor for the `Clause` class. It initializes a new instance of the class with the provided linguistic variable and fuzzy set label. It also checks if the label exists within the linguistic variable using reflection. If the label is not found, it throws a `KeyNotFoundException`.

- `public float Evaluate()`: This method evaluates the fuzzy clause and returns the degree of membership as a float value between 0 and 1. It uses reflection to call the `GetMembership` method on the label to determine the membership degree based on the numeric input of the linguistic variable.

- `public override string ToString()`: This method returns a string representation of the fuzzy clause in the format "Variable IS Value", making it easier to understand the relationship defined by the clause.