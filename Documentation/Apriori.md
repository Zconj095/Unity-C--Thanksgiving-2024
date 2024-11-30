# Apriori Class Documentation

## Overview
The `Apriori` class implements the A-priori algorithm for association rule mining within the Unity framework. It is designed to analyze a dataset of transactions to find frequent itemsets and generate association rules based on specified support and confidence thresholds. This class plays a crucial role in the EdgeLoreMachineLearning codebase by enabling the discovery of relationships between items in a dataset, which can be useful for various machine learning applications.

## Variables
- `supportMin`: An integer representing the minimum support threshold required for an itemset to be considered frequent.
- `confidence`: A double value that defines the minimum confidence threshold for the association rules generated from the frequent itemsets.
- `frequentItemsets`: A dictionary that maps frequent itemsets (as `HashSet<T>`) to their respective counts (as integers).

## Functions
- **FrequentItemsets**: 
  - A public property that returns the `frequentItemsets` dictionary, allowing access to the most frequent itemsets found during the mining process.

- **Apriori(int minSupport, double minConfidence)**: 
  - A constructor that initializes a new instance of the `Apriori` class with specified minimum support and confidence values.

- **Learn(List<HashSet<T>> transactions)**: 
  - This public method accepts a list of transactions (each represented as a `HashSet<T>`) and learns association rules from the dataset. It generates frequent itemsets and returns a list of association rules derived from those itemsets.

- **GenerateRules(List<HashSet<T>> transactions)**: 
  - A private method that generates association rules from the frequent itemsets. It calculates support and confidence for each potential rule and returns a list of valid association rules.

- **GetSupport(HashSet<T> set, List<HashSet<T>> transactions)**: 
  - A private method that calculates the support of a given itemset within the provided transactions. It returns the support value as a double.

- **GetSubsets(HashSet<T> set)**: 
  - A private method that generates all non-empty subsets of a given set. It returns a list of subsets represented as `HashSet<T>`.

- **SetComparer**: 
  - A private nested class that implements `IEqualityComparer<HashSet<T>>`. It provides custom equality comparison and hash code generation for `HashSet<T>` to facilitate their use as keys in dictionaries.

## AssociationRule Class
- **AssociationRule<T>**: 
  - A class that represents an association rule, consisting of:
    - `X`: A `HashSet<T>` representing the antecedent of the rule.
    - `Y`: A `HashSet<T>` representing the consequent of the rule.
    - `Confidence`: A double value indicating the confidence of the rule.
    - `Support`: A double value indicating the support of the rule.