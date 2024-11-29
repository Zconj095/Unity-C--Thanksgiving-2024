# DecisionTreeLearningBase

## Overview
The `DecisionTreeLearningBase` class serves as a foundational component for implementing tree-inducing learning algorithms within a Unity environment. It provides the structure and functionality necessary to manage decision tree attributes, configure tree parameters, and calculate split information for decision-making processes. This class is designed to be extended by more specific decision tree implementations, facilitating the creation and manipulation of decision trees in machine learning applications.

## Variables
- **tree**: An instance of the `DecisionTree` class representing the decision tree model being learned.
- **attributes**: A list of `DecisionVariable` objects that represent the attributes used in the decision tree.
- **maxHeight**: An integer that specifies the maximum allowed height of the decision tree. Default is 0, indicating no height limit.
- **maxVariables**: An integer that specifies the maximum number of attributes that can be used in the tree. Default is 0, indicating no limit.
- **join**: An integer that indicates how many times an attribute can be used in a decision path. Default is 1, meaning an attribute can be used only once per path.
- **attributeUsageCount**: An array of integers that tracks how many times each attribute has been used in the decision-making process.

## Functions
- **MaxHeight**: Property that gets or sets the maximum allowed height of the decision tree. Throws an `ArgumentOutOfRangeException` if a negative value is set.
  
- **MaxVariables**: Property that gets or sets the maximum number of attributes that can be used in the tree. Throws an `ArgumentOutOfRangeException` if a negative value is set.
  
- **Attributes**: Property that gets or sets the list of attributes for the decision tree, converting the input list to a `List<DecisionVariable>`.
  
- **Join**: Property that gets or sets how many times an attribute can be used in a decision path. Throws an `ArgumentOutOfRangeException` if a negative value is set.
  
- **Model**: Property that gets or sets the decision tree being learned. It also initializes the attribute usage count when the tree is set.
  
- **DecisionTreeLearningBase()**: Constructor that initializes a new instance of the `DecisionTreeLearningBase` class with an empty list of attributes.
  
- **DecisionTreeLearningBase(IEnumerable<DecisionVariable> attributes)**: Constructor that initializes a new instance of the `DecisionTreeLearningBase` class with a specified set of attributes.
  
- **Add(DecisionVariable variable)**: Method that adds a new attribute to the decision tree's attribute list.
  
- **InitializeAttributeUsageCount()**: Private method that initializes the usage count for the attributes based on the decision tree's inputs. Throws an `InvalidOperationException` if the tree is not initialized with valid attributes.
  
- **GetEnumerator()**: Method that returns an enumerator for iterating over the attributes of the decision tree.
  
- **IEnumerable.GetEnumerator()**: Explicit interface implementation that returns the enumerator for the attributes.
  
- **SplitInformation(int samples, IList<int>[] partitions, List<int> missing = null)**: Static method that computes the split information measure for the given partitions of a dataset. It calculates the information gain based on the distribution of samples across the partitions and handles missing values if provided. Returns a double representing the split information.