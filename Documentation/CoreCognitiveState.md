# CoreCognitiveState

## Overview
The `CoreCognitiveState` class represents a cognitive state with a name, intensity, and associated color. This class is designed to be used within a Unity project, likely to manage and visualize different cognitive states in a game or simulation. It encapsulates the properties and behaviors associated with a cognitive state, allowing for easy adjustments and representation of these states.

## Variables
- `Name` (string): The name of the cognitive state, which serves as an identifier for that state.
- `Intensity` (float): A value representing the strength of the cognitive state, constrained between 0 and 1. This indicates how pronounced the state is.
- `StateColor` (Color): The color associated with the cognitive state, which can be used for visual representation in the game or application.

## Functions
- **CoreCognitiveState(string name, float intensity, Color color)**: Constructor that initializes a new instance of the `CoreCognitiveState` class. It takes a name, an intensity value (which is clamped between 0 and 1), and a color.
  
- **void AdjustIntensity(float delta)**: This method adjusts the intensity of the cognitive state by a specified delta value. The new intensity is clamped to ensure it remains within the range of 0 to 1.

- **override string ToString()**: This method overrides the default `ToString()` method to provide a string representation of the cognitive state, including its name and intensity. This is useful for debugging and logging purposes.