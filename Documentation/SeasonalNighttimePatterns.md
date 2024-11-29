# SeasonalNighttimePatterns

## Overview
The `SeasonalNighttimePatterns` script is designed to manage and represent various nighttime patterns associated with different entities in a seasonal context within a Unity game. It provides a structured way to define when specific entities (like trees, berries, flowers, etc.) are active during the night, based on the current season and time. This script fits into the larger codebase by enabling dynamic environmental interactions and gameplay elements that change according to the season and time of day.

## Variables

- **Season**
  - An enumeration that defines the four seasons: Spring, Summer, Autumn, and Winter.

- **NighttimePattern**
  - A class that represents a specific nighttime behavior for an entity.
  - **EntityName**: A string that holds the name of the entity (e.g., Tree, Berry).
  - **ActiveSeason**: An enum value indicating the season in which this pattern is active.
  - **StartHour**: A float representing the start time of the pattern during nighttime.
  - **EndHour**: A float representing the end time of the pattern during nighttime.
  - **Description**: A string providing additional details about the pattern.

- **TreePatterns**
  - A list that holds `NighttimePattern` objects specific to trees.

- **BerryPatterns**
  - A list that holds `NighttimePattern` objects specific to berries.

- **FlowerPatterns**
  - A list that holds `NighttimePattern` objects specific to flowers.

- **ItemPatterns**
  - A list that holds `NighttimePattern` objects specific to items.

- **MoonPhasePatterns**
  - A list that holds `NighttimePattern` objects that are influenced by moon phases.

- **WeatherPatterns**
  - A list that holds `NighttimePattern` objects that are influenced by weather conditions.

- **RoutePatterns**
  - A list that holds `NighttimePattern` objects related to specific routes.

- **HabitatPatterns**
  - A list that holds `NighttimePattern` objects associated with different habitats.

- **BiomePatterns**
  - A list that holds `NighttimePattern` objects specific to various biomes.

- **EcosystemPatterns**
  - A list that holds `NighttimePattern` objects that affect the ecosystem.

- **RegionPatterns**
  - A list that holds `NighttimePattern` objects that are specific to different regions.

## Functions

- **NighttimePattern(string name, Season season, float startHour, float endHour, string description)**
  - Constructor for creating a new `NighttimePattern` instance with specified entity name, active season, start and end hours, and description.

- **DisplayDetails()**
  - A method that logs the details of the `NighttimePattern` to the console, including entity name, active season, active hours, and description.

- **IsActive(Season currentSeason, float currentHour)**
  - A method that checks if the `NighttimePattern` is currently active based on the provided current season and hour. It accounts for patterns that span midnight by checking if the current hour falls within the defined start and end hours, returning a boolean value.