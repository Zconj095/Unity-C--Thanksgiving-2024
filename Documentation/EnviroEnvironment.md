# EnviroEnvironment

## Overview
The `EnviroEnvironment` script is designed to model various environmental elements such as biomes, habitats, and ecosystems within a Unity game. It provides a structured way to represent these elements, allowing for the creation of complex environments that can be easily managed and displayed. The script includes classes for `Biome`, `Habitat`, and `Ecosystem`, each of which has properties and methods to define their characteristics and display their details in the console.

## Variables
- **Biome**: A class representing a specific biome within the environment.
  - `Name`: The name of the biome.
  - `Plants`: A list of plant species found in the biome.
  - `Animals`: A list of animal species found in the biome.
  - `Description`: A brief description of the biome.

- **Habitat**: A class representing a habitat that exists within a biome.
  - `Name`: The name of the habitat.
  - `BiomeType`: The type of biome the habitat is associated with.
  - `Requirements`: The minimum environmental requirements for the habitat to exist (e.g., water, shade).
  - `Description`: A brief description of the habitat.

- **Ecosystem**: A class representing a collection of biomes and their interactions.
  - `Name`: The name of the ecosystem.
  - `Biomes`: A list of biomes that make up the ecosystem.
  - `WeatherPattern`: The weather and seasonal impacts on the ecosystem.
  - `TerrainType`: A description of the terrain within the ecosystem.
  - `Description`: A brief description of the ecosystem.

## Functions
- **Biome.DisplayDetails()**: This method logs the details of the biome to the console, including its name, list of plants, list of animals, and description.

- **Habitat.DisplayDetails()**: This method logs the details of the habitat to the console, including its name, associated biome type, requirements, and description.

- **Ecosystem.DisplayDetails()**: This method logs the details of the ecosystem to the console, including its name, weather pattern, terrain type, description, and a list of biomes it contains.

- **Start()**: This Unity lifecycle method is called before the first frame update. It creates sample instances of `Biome`, `Habitat`, and `Ecosystem`, and calls their respective `DisplayDetails()` methods to show their information in the console.