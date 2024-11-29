# DaytimeSpawningManager

## Overview
The `DaytimeSpawningManager` script is responsible for managing the spawning of various entities in a simulation based on the current time of day and the season. It defines different spawning patterns for entities such as trees, berries, flowers, and creatures, allowing these entities to appear or become active during specific hours of the day and under specific seasonal conditions. This script plays a crucial role in the overall game mechanics, enabling dynamic interactions with the environment based on time and season.

## Variables

- **Season**: An enumeration representing the four seasons: Spring, Summer, Autumn, and Winter.
  
- **DaytimeSpawnPattern**: A class representing the spawning pattern for entities, containing the following fields:
  - `EntityName`: A string representing the name of the entity (e.g., Tree, Berry).
  - `ActiveSeason`: A Season value indicating when the pattern is active.
  - `StartHour`: A float representing the start time (in hours) for when the pattern is effective.
  - `EndHour`: A float representing the end time (in hours) for when the pattern is effective.
  - `Description`: A string providing a description of the spawning pattern.

- **TreePatterns**: A list of `DaytimeSpawnPattern` objects specifically for tree spawning patterns.
- **BerryPatterns**: A list of `DaytimeSpawnPattern` objects for berry spawning patterns.
- **FlowerPatterns**: A list of `DaytimeSpawnPattern` objects for flower spawning patterns.
- **ItemPatterns**: A list of `DaytimeSpawnPattern` objects for item spawning patterns.
- **MoonPhasePatterns**: A list of `DaytimeSpawnPattern` objects for moon phase-related patterns.
- **WeatherPatterns**: A list of `DaytimeSpawnPattern` objects for weather-related patterns.
- **RoutePatterns**: A list of `DaytimeSpawnPattern` objects for route-related patterns.
- **HabitatPatterns**: A list of `DaytimeSpawnPattern` objects for habitat-related patterns.
- **BiomePatterns**: A list of `DaytimeSpawnPattern` objects for biome-related patterns.
- **EcosystemPatterns**: A list of `DaytimeSpawnPattern` objects for ecosystem-related patterns.
- **RegionPatterns**: A list of `DaytimeSpawnPattern` objects for region-related patterns.
- **CreaturePatterns**: A list of `DaytimeSpawnPattern` objects for creature spawning patterns.
- **CurrentSeason**: A Season variable representing the current season in the simulation (default is Spring).
- **CurrentHour**: A float representing the current hour in the simulation (default is 10 AM).

## Functions

- **Start()**: Initializes the spawning patterns and checks for active spawn patterns for trees and berries when the simulation starts.

- **Update()**: Called once per frame, simulates time progression and checks for active spawning patterns for flowers and creatures.

- **InitializePatterns()**: Populates the various lists of `DaytimeSpawnPattern` with predefined patterns for trees, berries, flowers, and creatures.

- **SimulateTime()**: Increments the `CurrentHour` to simulate the passage of time, wrapping around to 0 if it exceeds 24 hours.

- **CheckActivePatterns(List<DaytimeSpawnPattern> patterns, string category)**: Checks the provided list of spawning patterns for active patterns based on the current hour and season, logging active patterns to the console.