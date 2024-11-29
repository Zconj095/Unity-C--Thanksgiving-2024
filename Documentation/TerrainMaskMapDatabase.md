# TerrainMaskMapDatabase

## Overview
The `TerrainMaskMapDatabase` script is a custom Unity `ScriptableObject` that serves as a database for managing a collection of terrain mask maps. This script allows developers to easily add, remove, and search for terrain mask maps based on various criteria, such as name, terrain type, and tags. It acts as a centralized storage for terrain-related metadata, facilitating the organization and retrieval of mask maps within the game development process.

## Variables

- **maskMaps**: 
  - Type: `List<TerrainMaskMapMetadata>`
  - Description: A list that holds instances of `TerrainMaskMapMetadata`, representing the terrain mask maps in the database. This collection allows for easy management and retrieval of mask map data.

## Functions

- **AddMaskMap(TerrainMaskMapMetadata maskMap)**:
  - Description: Adds a new `TerrainMaskMapMetadata` instance to the `maskMaps` list if it is not already present. This function ensures that duplicate entries are avoided.

- **RemoveMaskMap(TerrainMaskMapMetadata maskMap)**:
  - Description: Removes an existing `TerrainMaskMapMetadata` instance from the `maskMaps` list if it exists. This function helps maintain the integrity of the database by allowing for the removal of unwanted or outdated mask maps.

- **FindMaskMapByName(string name)**:
  - Description: Searches for a `TerrainMaskMapMetadata` instance in the `maskMaps` list by its name. It returns the first matching mask map found or `null` if no match is found. This function is useful for quickly locating a specific mask map.

- **FindMaskMapsByTerrainType(string terrainType)**:
  - Description: Retrieves a list of `TerrainMaskMapMetadata` instances that match the specified terrain type. This function returns all mask maps that correspond to the given terrain type, allowing for efficient filtering based on terrain characteristics.

- **FindMaskMapsByTag(string tag)**:
  - Description: Searches for all `TerrainMaskMapMetadata` instances in the `maskMaps` list that contain the specified tag. It returns a list of matching mask maps, enabling developers to categorize and retrieve mask maps based on associated tags.