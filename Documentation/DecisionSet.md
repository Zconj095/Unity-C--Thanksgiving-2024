# DecisionSet Class Documentation

## Overview
The `DecisionSet` class represents a collection of decision rules used in decision trees. It allows for the management of these rules, including adding, removing, and clearing them. The class also provides functionality to compute outputs based on input vectors, making it a crucial component in machine learning tasks, specifically in classification problems. This class integrates seamlessly with other components of the EdgeLoreMachineLearning codebase, particularly with the `DecisionTree` class, allowing for the conversion of decision trees into a set of actionable rules.

## Variables
- `rules`: A private `HashSet<DecisionRule>` that stores the unique decision rules within the `DecisionSet`.
- `NumberOfClasses`: An integer property that indicates the number of distinct classes available in the decision set.
- `NumberOfOutputs`: An integer property that specifies the number of possible outputs that can be generated from the decision rules.

## Functions
- **Constructor: `DecisionSet()`**
  - Initializes a new instance of the `DecisionSet` class with an empty set of decision rules.

- **Constructor: `DecisionSet(IEnumerable<DecisionRule> rules)`**
  - Initializes a new instance of the `DecisionSet` class with a specified collection of decision rules.

- **Method: `static DecisionSet FromDecisionTree(DecisionTree tree)`**
  - Creates a `DecisionSet` from a given decision tree. It extracts leaf nodes that have outputs and generates a corresponding set of decision rules.

- **Method: `void Add(DecisionRule rule)`**
  - Adds a single decision rule to the decision set.

- **Method: `void AddRange(IEnumerable<DecisionRule> rulesToAdd)`**
  - Adds multiple decision rules to the decision set in one operation.

- **Method: `bool Remove(DecisionRule rule)`**
  - Removes a specified decision rule from the decision set. Returns true if the rule was successfully removed; otherwise, false.

- **Method: `void Clear()`**
  - Clears all decision rules from the decision set, leaving it empty.

- **Property: `int Count`**
  - Gets the number of decision rules currently stored in the decision set.

- **Method: `int Decide(double[] input)`**
  - Computes the output for a given input vector. It checks if the input contains any NaN values and processes the rules to determine the most likely output class.

- **Method: `override string ToString()`**
  - Converts the decision set to a string representation using the current culture.

- **Method: `string ToString(CultureInfo culture)`**
  - Converts the decision set to a string representation formatted according to a specified culture.

- **Method: `IEnumerator<DecisionRule> GetEnumerator()`**
  - Returns an enumerator that iterates through the decision rules in the set.

- **Method: `IEnumerator IEnumerable.GetEnumerator()`**
  - Returns a non-generic enumerator that iterates through the decision rules in the set.