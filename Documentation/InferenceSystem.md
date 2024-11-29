# InferenceSystem

## Overview
The `InferenceSystem` class represents a Fuzzy Inference System (FIS) that is capable of executing fuzzy computations. It serves as a central component in the UnityFuzzy codebase, allowing for the creation of fuzzy rules, setting inputs, and evaluating outputs based on fuzzy logic principles. This class interacts with various components such as a database of variables, a defuzzifier, and a rulebase to perform its functions.

## Variables
- `database`: An object representing the database that stores the fuzzy variables used in the inference process.
- `defuzzifier`: An object responsible for converting fuzzy output into a crisp value.
- `normOperator`: An object that defines the normalization operation used within the fuzzy inference.
- `conormOperator`: An object that defines the conorm operation used in the fuzzy inference.
- `rulebase`: An object that contains a collection of fuzzy rules for the inference system.

## Functions
- **InferenceSystem(object database, object defuzzifier)**: Constructor that initializes the `InferenceSystem` with a database and a defuzzifier. It also creates instances of the default normalization and conorm operators (MinimumNorm and MaximumCoNorm).
  
- **InferenceSystem(object database, object defuzzifier, object normOperator, object conormOperator)**: Constructor that allows the initialization of the `InferenceSystem` with specific normalization and conorm operators, in addition to the database and defuzzifier.

- **object NewRule(string name, string rule)**: Creates a new fuzzy rule with the specified name and rule string, adds it to the rulebase, and returns the rule instance.

- **void SetInput(string variableName, float value)**: Sets the numeric input value for a specified variable in the database.

- **object GetLinguisticVariable(string variableName)**: Retrieves a linguistic variable from the database based on its name.

- **object GetRule(string ruleName)**: Retrieves a specific rule from the rulebase using its name.

- **float Evaluate(string variableName)**: Evaluates the fuzzy output for a specified variable name and returns a defuzzified crisp value.

- **object ExecuteInference(string variableName)**: Executes the inference process for a specified variable name, evaluates the firing strength of the rules, and returns the fuzzy output based on the applied rules.