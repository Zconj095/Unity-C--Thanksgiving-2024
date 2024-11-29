# CognitiveEngine

## Overview
The `CognitiveEngine` class is responsible for making decisions based on the emotional state of an entity in a Unity game environment. It interacts with an `EmotionalEngine` to retrieve the current emotion and its intensity, which influence the decision-making process. This class serves as a key component of the codebase, allowing characters or entities to react dynamically to different situations based on their emotional state.

## Variables
- `public EmotionalEngine emotionalEngine;`
  - This variable holds a reference to the `EmotionalEngine` instance, which is used to obtain the current emotion and its intensity. It allows the `CognitiveEngine` to access emotional data necessary for decision-making.

## Functions
- `public string MakeDecision(string situation)`
  - This function takes a `situation` as a parameter and returns a string representing the decision made based on the current emotion and its intensity. The decision is determined by a switch statement that evaluates the current emotion retrieved from the `EmotionalEngine`. Depending on the emotion and its intensity, the function provides different responses, such as "Take a risky action" for high courage or "Trust the process" for faith. If the emotion does not match any predefined states, it defaults to "Neutral decision."