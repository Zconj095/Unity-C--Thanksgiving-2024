# TerrainDatabase

## Overview
The `TerrainDatabase` script is a Unity ScriptableObject that serves as a centralized repository for managing terrain metadata within a game project. It allows developers to store, add, remove, and search for terrain data based on various criteria such as name, biome type, and tags. This functionality is crucial for organizing and accessing terrain information efficiently, enhancing the overall game development process by providing a structured way to handle terrain assets.

## Variables
- `public List<TerrainMetadata> terrains`: A list that holds `TerrainMetadata` objects. Each object represents a unique terrain and contains relevant information about that terrain.

## Functions
- `public void AddTerrain(TerrainMetadata terrainData)`: This function adds a new `TerrainMetadata` object to the `terrains` list if it does not already exist in the list. This helps to prevent duplicates and maintain a clean database of terrains.

- `public void RemoveTerrain(TerrainMetadata terrainData)`: This function removes a specified `TerrainMetadata` object from the `terrains` list if it exists. This allows for easy management of the terrain database by enabling the deletion of unwanted terrain data.

- `public TerrainMetadata FindTerrainByName(string name)`: This function searches for a `TerrainMetadata` object in the `terrains` list by its name. It returns the corresponding terrain object if found, or `null` if no match is found. This is useful for quickly retrieving terrain information based on its name.

- `public List<TerrainMetadata> FindTerrainsByBiome(string biomeType)`: This function searches for all `TerrainMetadata` objects in the `terrains` list that match a specified biome type. It returns a list of matching terrain objects, providing a way to filter terrains based on their biome characteristics.

- `public List<TerrainMetadata> FindTerrainsByTag(string tag)`: This function finds all `TerrainMetadata` objects in the `terrains` list that contain a specified tag. It returns a list of terrains that match the given tag, allowing for flexible categorization and retrieval of terrains based on custom tags.