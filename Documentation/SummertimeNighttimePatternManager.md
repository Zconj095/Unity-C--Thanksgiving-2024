# SummertimeNighttimePatternManager

## Overview
The `SummertimeNighttimePatternManager` script is responsible for managing various patterns that occur during summer nights within a Unity game environment. It defines and tracks different nighttime patterns for various entities, such as creatures, trees, and weather phenomena. The script simulates the passage of time and checks which patterns are currently active based on the simulated hour, providing dynamic interactions that enhance the game's atmosphere.

## Variables

- **CreaturePatterns**: A list that holds instances of `SummerNighttimePattern` representing nighttime behavior or events related to creatures.
- **TreePatterns**: A list that holds instances of `SummerNighttimePattern` related to the behaviors or events of trees during summer nights.
- **BerryPatterns**: A list that holds instances of `SummerNighttimePattern` for berry-related nighttime patterns.
- **FlowerPatterns**: A list that holds instances of `SummerNighttimePattern` for flower-related nighttime patterns.
- **ItemPatterns**: A list that holds instances of `SummerNighttimePattern` for special items that appear at night.
- **MoonPhasePatterns**: A list that holds instances of `SummerNighttimePattern` related to moon phases and their effects.
- **WeatherPatterns**: A list that holds instances of `SummerNighttimePattern` that describe weather events occurring during summer nights.
- **RoutePatterns**: A list that holds instances of `SummerNighttimePattern` for paths illuminated by moonlight.
- **HabitatPatterns**: A list that holds instances of `SummerNighttimePattern` that describe nocturnal activities in various habitats.
- **BiomePatterns**: A list that holds instances of `SummerNighttimePattern` that describe activities in different biomes during summer nights.
- **EcosystemPatterns**: A list that holds instances of `SummerNighttimePattern` for ecological interactions at night.
- **RegionPatterns**: A list that holds instances of `SummerNighttimePattern` that describe regional phenomena visible at night.
- **CurrentHour**: A float representing the current simulated hour in the game, initialized to 22 (10 PM).

## Functions

- **Start()**: Initializes the nighttime patterns with example data and checks the active patterns for creatures and trees when the game starts.
  
- **Update()**: Simulates the progression of time within the game and dynamically checks which flower and weather patterns are active based on the current hour.

- **InitializePatterns()**: Populates the various pattern lists with predefined instances of `SummerNighttimePattern`, each representing a unique nighttime occurrence.

- **SimulateTime()**: Increments the `CurrentHour` variable to simulate the passage of time in the game. It wraps the hour back to 0 after reaching 24 to maintain a 24-hour cycle.

- **CheckActivePatterns(List<SummerNighttimePattern> patterns, string category)**: Logs to the console the active patterns from a specified category based on the current hour, indicating which patterns are currently taking place.