# Rulebase Class Documentation

## Overview
The `Rulebase` class represents a collection of fuzzy rules used in a Fuzzy Inference System. It serves as a central repository for managing these rules, allowing for the addition, removal, and retrieval of fuzzy rules. This class is essential for the functionality of the fuzzy inference system, as it enables the system to operate based on a predefined set of rules.

## Variables
- `rules`: A `Dictionary<string, object>` that stores the fuzzy rules. The key is the name of the rule, and the value is the rule object itself.

## Functions
- **Rulebase()**
  - **Description**: Constructor that initializes a new instance of the `Rulebase` class. It sets up the `rules` dictionary with an initial capacity of 20.

- **void AddRule(object rule)**
  - **Description**: Adds a fuzzy rule to the rulebase. It checks if the rule has a `Name` property and ensures that the rule name does not already exist in the rulebase. If the name exists, it throws an `ArgumentException`.

- **void ClearRules()**
  - **Description**: Removes all fuzzy rules from the rulebase, effectively clearing the collection.

- **object GetRule(string ruleName)**
  - **Description**: Retrieves a fuzzy rule from the rulebase by its name. If the rule is not found, it throws a `KeyNotFoundException`.

- **object[] GetRules()**
  - **Description**: Returns an array containing all the rules currently in the rulebase. This allows for easy access to all stored rules.