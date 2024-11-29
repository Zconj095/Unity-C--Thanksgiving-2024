# NighttimeSpawningManager

## Overview
The `NighttimeSpawningManager` class is responsible for managing and simulating the spawning of various entities during nighttime in a game environment based on different seasons and time. It handles the initialization of spawning patterns for different categories (like trees, berries, etc.) and checks which patterns are active based on the current in-game hour and season. This functionality allows for dynamic interactions within the game world, enhancing the gameplay experience by introducing variability in entity availability.

## Variables

- **Season**: An enumeration that defines the four seasons: Spring, Summer, Autumn, and Winter.
  
- **NighttimeSpawnPattern**: A serializable class that represents a specific spawning pattern for an entity.
  - **EntityName**: A string representing the name of the entity (e.g., "Tree", "Berry").
  - **ActiveSeason**: The season during which the spawning pattern is active.
  - **StartHour**: The starting hour of the spawning timeframe (in hours).
  - **EndHour**: The ending hour of the spawning timeframe (in hours).
  - **Description**: A string that describes the spawning pattern.

- **TreePatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for trees.
- **BerryPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for berries.
- **FlowerPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for flowers.
- **ItemPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for items.
- **MoonPhasePatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns based on moon phases.
- **WeatherPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns influenced by weather.
- **RoutePatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for routes.
- **HabitatPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for habitats.
- **BiomePatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for biomes.
- **EcosystemPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for ecosystems.
- **RegionPatterns**: A list of `NighttimeSpawnPattern` objects representing spawning patterns for regions.
- **CurrentSeason**: A variable of type `Season` that indicates the current season in the simulation (default is Spring).
- **CurrentHour**: A float representing the current hour in the simulation (default is 22, which corresponds to 10 PM).

## Functions

- **Start()**: Initializes the spawning patterns and checks for active patterns for trees, berries, and moon phases at the start of the simulation.

- **Update()**: Called once per frame to simulate the progression of time and check for active spawning patterns for trees and flowers.

- **InitializePatterns()**: Populates the lists of `NighttimeSpawnPattern` with predefined patterns for trees, berries, moon phases, and weather.

- **SimulateTime()**: Increments the `CurrentHour` variable to simulate the passage of time, wrapping around to 0 when it exceeds 24 hours.

- **CheckActivePatterns(List<NighttimeSpawnPattern> patterns, string category)**: Checks which patterns in the provided list are active based on the `CurrentHour` and `CurrentSeason`, logging the active patterns to the console.