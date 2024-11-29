# SpringtimeNighttimePatternManager

## Overview
The `SpringtimeNighttimePatternManager` is a Unity script designed to manage and display various nighttime patterns associated with different entities during the spring season. This script organizes patterns based on several categories such as trees, berries, flowers, and more, and simulates the passage of time to check which patterns are currently active based on the in-game hour. It integrates seamlessly into the larger codebase, providing a way to enrich the game environment with dynamic nighttime behaviors.

## Variables
- `TreePatterns`: A list of `SpringNighttimePattern` objects representing patterns specific to trees.
- `BerryPatterns`: A list of `SpringNighttimePattern` objects representing patterns specific to berries.
- `FlowerPatterns`: A list of `SpringNighttimePattern` objects representing patterns specific to flowers.
- `ItemPatterns`: A list of `SpringNighttimePattern` objects representing patterns related to items.
- `MoonPhasePatterns`: A list of `SpringNighttimePattern` objects representing patterns based on moon phases.
- `WeatherPatterns`: A list of `SpringNighttimePattern` objects representing patterns influenced by weather conditions.
- `RoutePatterns`: A list of `SpringNighttimePattern` objects representing patterns related to routes or paths.
- `HabitatPatterns`: A list of `SpringNighttimePattern` objects representing patterns related to different habitats.
- `BiomePatterns`: A list of `SpringNighttimePattern` objects representing patterns associated with biomes.
- `EcosystemPatterns`: A list of `SpringNighttimePattern` objects representing patterns related to ecosystems.
- `RegionPatterns`: A list of `SpringNighttimePattern` objects representing patterns specific to certain regions.
- `CreaturePatterns`: A list of `SpringNighttimePattern` objects representing patterns related to creatures.
- `CurrentHour`: A float representing the current in-game hour, initialized to 22 (10 PM).

## Functions
- `Start()`: Initializes the script, populating the various pattern lists with example data and checking active patterns for trees and creatures at the start of the game.
  
- `Update()`: Called once per frame, this function simulates the passage of time and checks for active patterns in the flower and weather categories.

- `InitializePatterns()`: Populates the pattern lists with predefined `SpringNighttimePattern` instances, each representing a unique nighttime behavior for different entities.

- `SimulateTime()`: Advances the `CurrentHour` by simulating the passage of time; specifically, it increments the hour based on the frame time and wraps around to maintain a 24-hour format.

- `CheckActivePatterns(List<SpringNighttimePattern> patterns, string category)`: Logs the active patterns for a given category by checking each pattern in the provided list against the `CurrentHour` to determine if it is currently active.