# EnviroPatterns

## Overview
The `EnviroPatterns` script is designed to manage and simulate celestial patterns, specifically lunar and solar events, within a Unity environment. It provides a structured way to define different celestial patterns, their effects on the environment, and the times during which they are active. This script is crucial for any game or simulation that requires dynamic environmental changes based on celestial events.

## Variables

- **PatternType**: An enumeration that defines two types of patterns: `Lunar` and `Solar`.
  
- **CelestialPattern**: A serializable class that holds details about each celestial pattern, which includes:
  - `Type` (PatternType): Indicates whether the pattern is lunar or solar.
  - `Name` (string): The name of the celestial pattern.
  - `Description` (string): A brief description of the pattern's effects.
  - `StartHour` (float): The hour at which the pattern begins (in 24-hour format).
  - `EndHour` (float): The hour at which the pattern ends (in 24-hour format).
  - `Effects` (string): Describes the environmental effects of the pattern (e.g., light level, temperature).

- **celestialPatterns** (List<CelestialPattern>): A list that stores multiple celestial patterns for easy access and manipulation.

## Functions

- **Start()**: This method is called when the script is initialized. It defines several celestial patterns (both lunar and solar) and adds them to the `celestialPatterns` list. It also displays the details of each defined pattern in the debug log.

- **SimulatePatterns(float currentHour)**: This public method simulates the effects of celestial patterns based on the current hour. It logs which patterns are active at that time and their associated effects.

- **IsPatternActive(CelestialPattern pattern, float currentHour)**: A private helper function that checks if a given celestial pattern is active based on the current hour. It accounts for patterns that may span across midnight, ensuring correct activation checks.