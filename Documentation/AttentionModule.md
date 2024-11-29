# AttentionModule

## Overview
The `AttentionModule` class is a component in Unity that computes an attention score based on the relevance of a given input string to a specified context string. This score can be useful in various applications, such as natural language processing or game AI, where understanding the importance of information is crucial. The `ComputeAttentionScore` method evaluates the input against the context and returns a normalized score reflecting their relevance.

## Variables
- **None**: This script does not contain any instance variables. All data is handled within the method.

## Functions
- **ComputeAttentionScore(string input, string context)**: 
  - This method takes two strings, `input` and `context`, as parameters. It calculates a score based on how many keywords from the `context` are found within the `input`. The score is normalized by dividing the total matches by the number of keywords. The method returns a floating-point value representing the attention score, where a higher score indicates greater relevance of the input to the context.