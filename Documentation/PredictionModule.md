# PredictionModule

## Overview
The `PredictionModule` script is designed to provide simple predictions based on user input. It analyzes the given input string and returns a corresponding prediction related to weather or scheduling. This module can be integrated into larger systems, such as chatbots or user interfaces, to enhance user interaction by offering predictive responses.

## Variables
- **input**: A string parameter that represents the user's query or statement. It is used to determine which prediction to return based on its content.

## Functions
- **PredictOutcome(string input)**: This function takes a string as an argument and checks its content for specific keywords. 
  - If the input contains the word "weather", it returns the prediction "It might rain tomorrow."
  - If the input contains the word "schedule", it returns the prediction "Your meeting is likely to run late."
  - If the input does not match any of the specified keywords, it returns "No prediction available." 

This function serves as the core logic of the module, allowing it to provide relevant feedback based on user input.