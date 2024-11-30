# AssociationRule3<T>

## Overview
The `AssociationRule3<T>` class is designed for itemset mining within the Unity environment. It represents an association rule that connects a set of items (antecedents) to another set of items (consequents). This class is useful in identifying relationships between items in a dataset, where the presence of certain items can predict the presence of others. The main functionalities of this class include evaluating whether a given input matches the antecedents of the rule and providing a string representation of the rule with its support and confidence metrics.

## Variables

- **X**: `HashSet<T>`  
  The antecedent of the rule, representing the items that trigger this rule. It is a collection of items that must be present for the rule to be applicable.

- **Y**: `HashSet<T>`  
  The consequent of the rule, representing the items that are likely to appear if the antecedent (X) is present. This collection indicates the predicted items based on the presence of X.

- **Support**: `double`  
  The support value of the rule, which reflects the frequency of occurrence of the rule in the dataset. It quantifies how often the items in X and Y appear together in the data.

- **Confidence**: `double`  
  The confidence of the rule, indicating the likelihood that the items in Y will appear when the items in X are present. It measures the strength of the implication from X to Y.

## Functions

- **Matches(HashSet<T> input)**:  
  Evaluates whether the rule applies to a given input set. It returns `true` if all items in the antecedent (X) are present in the input set; otherwise, it returns `false`.

- **Matches(T[] input)**:  
  This function checks if the rule applies to a given input array. It converts the input array to a `HashSet<T>` and calls the `Matches` method to determine applicability.

- **ToString()**:  
  Provides a string representation of the association rule. It formats the output to include the items in the antecedent and consequent, along with the support and confidence values, making it easier to understand the rule at a glance.