# Rule

## Overview
The `Rule` class represents a fuzzy rule, which is a linguistic expression that drives the behavior of a Fuzzy Inference System (FIS). This class is integral to the UnityFuzzy codebase, as it enables the definition and evaluation of fuzzy rules that govern the inference process. The `Rule` class parses a rule string, converts it into Reverse Polish Notation (RPN) for evaluation, and provides methods to assess the strength of the rule's firing based on its components.

## Variables
- `name`: A string representing the name of the rule.
- `rule`: A string containing the fuzzy rule's expression.
- `rpnTokenList`: A list of objects that holds the tokens of the rule in Reverse Polish Notation.
- `database`: An object representing the database from which variables are retrieved.
- `normOperator`: An object representing the normalization operator used in fuzzy logic evaluations.
- `conormOperator`: An object representing the conorm operator used in fuzzy logic evaluations.
- `notOperator`: An object representing the NOT operator used in fuzzy logic evaluations.
- `unaryOperators`: A string containing the unary operators, specifically "NOT" and "VERY".
- `output`: An object that represents the consequent of the rule.

## Functions
- `Rule(object database, string name, string rule, object normOperator, object conormOperator)`: Constructor that initializes a new instance of the `Rule` class with a specified database, name, rule expression, normalization operator, and conorm operator. It also calls the `ParseRule` method to tokenize the rule.
  
- `Rule(object database, string name, string rule)`: Overloaded constructor that initializes the `Rule` using default normalization and conorm operators (MinimumNorm and MaximumCoNorm).

- `string GetRPNExpression()`: Returns the RPN expression of the rule as a string by concatenating the tokens in `rpnTokenList`.

- `int Priority(string operatorToken)`: Determines the precedence of the given operator token, returning an integer value that indicates its priority in the context of fuzzy logic operations.

- `void ParseRule()`: Parses the rule string into tokens, converting it into RPN format and populating the `rpnTokenList`. It handles operators, variables, and the consequent of the rule.

- `string[] GetRuleTokens(string rule)`: Splits the rule string into individual tokens, accounting for parentheses, and returns them as an array.

- `float EvaluateFiringStrength()`: Evaluates the firing strength of the rule based on its RPN tokens, using the specified operators to compute the final result. It returns a float value representing the strength of the rule's firing.