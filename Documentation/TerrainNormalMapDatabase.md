# TerrainNormalMapDatabase

## Overview
The `TerrainNormalMapDatabase` script is a Unity `ScriptableObject` that serves as a centralized storage system for managing terrain normal maps. This database allows developers to add, remove, and search for normal maps based on their names, terrain types, or associated tags. It is designed to simplify the management of terrain normal maps within a Unity project, making it easier to handle graphics and textures associated with various terrains.

## Variables
- `normalMaps`: A list of `TerrainNormalMapMetadata` objects that holds all the normal maps in the database. Each entry represents a different terrain normal map with its associated metadata.

## Functions
- `AddNormalMap(TerrainNormalMapMetadata normalMap)`: This function adds a new normal map to the `normalMaps` list if it is not already present. It ensures that duplicate entries are avoided.

- `RemoveNormalMap(TerrainNormalMapMetadata normalMap)`: This function removes a specified normal map from the `normalMaps` list if it exists. It helps in managing the database by allowing the removal of unused or unnecessary normal maps.

- `FindNormalMapByName(string name)`: This function searches for a normal map in the `normalMaps` list by its name. It returns the corresponding `TerrainNormalMapMetadata` object if found, or `null` if no match is found.

- `FindNormalMapsByTerrainType(string terrainType)`: This function retrieves all normal maps that match a specified terrain type. It returns a list of `TerrainNormalMapMetadata` objects that correspond to the given terrain type.

- `FindNormalMapsByTag(string tag)`: This function searches for normal maps that contain a specific tag. It returns a list of `TerrainNormalMapMetadata` objects that include the specified tag among their associated tags.