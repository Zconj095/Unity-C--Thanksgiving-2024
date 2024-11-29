# TerrainMaterialDatabase

## Overview
The `TerrainMaterialDatabase` script is a Unity `ScriptableObject` that serves as a centralized repository for managing terrain materials in a game or simulation. It allows developers to add, remove, and search for terrain materials based on various criteria such as name, terrain type, or tags. This script fits within the larger codebase by providing a structured way to organize and access terrain materials, which can be utilized by other components of the game, such as terrain generation or rendering systems.

## Variables
- `public List<TerrainMaterialMetadata> materials`: A list that stores instances of `TerrainMaterialMetadata`. Each instance contains information about a specific terrain material, such as its name, type, and associated tags.

## Functions
- `public void AddMaterial(TerrainMaterialMetadata materialData)`: This function adds a new terrain material to the `materials` list if it is not already present. It ensures that there are no duplicates in the database.

- `public void RemoveMaterial(TerrainMaterialMetadata materialData)`: This function removes a specified terrain material from the `materials` list if it exists. It helps maintain the integrity of the database by allowing the removal of materials that are no longer needed.

- `public TerrainMaterialMetadata FindMaterialByName(string name)`: This function searches for a terrain material in the `materials` list by its name. If a matching material is found, it returns the corresponding `TerrainMaterialMetadata` instance; otherwise, it returns null.

- `public List<TerrainMaterialMetadata> FindMaterialsByTerrainType(string terrainType)`: This function retrieves a list of terrain materials that match a specified terrain type. It returns all materials that have the given terrain type, enabling categorization based on terrain characteristics.

- `public List<TerrainMaterialMetadata> FindMaterialsByTag(string tag)`: This function finds and returns a list of terrain materials that have a specific tag. It uses an array search to check if the tag exists within the tags of each material, allowing for flexible filtering based on user-defined criteria.