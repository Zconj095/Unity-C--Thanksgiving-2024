# CreatureBehaviorManager

## Overview
The `CreatureBehaviorManager` script is responsible for managing and simulating the behavior patterns of various creatures in a game environment based on the time of day, season, and specific behavioral patterns. It defines when certain creatures are active and what behaviors they exhibit during those times. This script integrates with the rest of the codebase to provide a dynamic simulation of creature activities, allowing for a more immersive and realistic environment.

## Variables

- `DayPhase`: An enumeration representing the two phases of the day - `Daytime` and `Nighttime`.
  
- `Season`: An enumeration representing the four seasons - `Spring`, `Summer`, `Autumn`, and `Winter`.
  
- `CreaturePattern`: A serializable class that defines the behavior patterns of creatures, containing the following fields:
  - `CreatureName`: A string representing the name of the creature.
  - `Phase`: A `DayPhase` enum indicating whether the creature is active during the day or night.
  - `SeasonalCycle`: A `Season` enum indicating the season during which the behavior pattern is active.
  - `StartHour`: A float representing the hour (in 24-hour format) when the creature's behavior starts.
  - `EndHour`: A float representing the hour (in 24-hour format) when the creature's behavior ends.
  - `Behavior`: A string describing the specific behavior of the creature during the defined timeframe.

- `creaturePatterns`: A list of `CreaturePattern` objects that hold the various patterns of creature behavior.

- `currentSeason`: A `Season` variable indicating the current season in the simulation (default is Spring).

- `currentHour`: A float representing the current hour in the simulation (default is 22, which represents 10 PM).

- `currentPhase`: A `DayPhase` variable indicating the current phase of the day (default is Nighttime).

## Functions

- `Start()`: Initializes the script by defining example creature patterns and displaying their details in the console.

- `Update()`: Called every frame to check which creature patterns are active based on the current simulation time, season, and phase. It also simulates the passage of time.

- `SimulateTime()`: Advances the `currentHour` by simulating the passage of minutes. It resets the hour if it exceeds 24 and calls `UpdatePhase()` to adjust the day phase accordingly.

- `UpdatePhase()`: Updates the `currentPhase` based on the `currentHour`. It sets the phase to Daytime if the hour is between 6 AM and 6 PM, and to Nighttime otherwise.

- `CreaturePattern.DisplayDetails()`: A method within the `CreaturePattern` class that logs the details of the creature's behavior pattern to the console.

- `CreaturePattern.IsActive(float currentHour, Season currentSeason, DayPhase currentPhase)`: A method that checks if the creature's behavior is active based on the current hour, season, and phase. It handles patterns that may span midnight.