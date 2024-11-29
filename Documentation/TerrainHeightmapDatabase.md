# TerrainHeightmapDatabase

## Overview
The `TerrainHeightmapDatabase` script is a Unity `ScriptableObject` designed to manage a collection of terrain heightmaps. This class allows for the addition, removal, and searching of heightmaps based on specific attributes, such as name, terrain type, and tags. It acts as a centralized repository for heightmap metadata, making it easier for developers to manage terrain data within a Unity project.

## Variables
- `heightmaps`: A `List<TerrainHeightmapMetadata>` that stores instances of terrain heightmap metadata. This list serves as the main data structure for holding all heightmaps managed by the `TerrainHeightmapDatabase`.

## Functions
- `AddHeightmap(TerrainHeightmapMetadata heightmap)`: Adds a `TerrainHeightmapMetadata` instance to the `heightmaps` list if it is not already present. This function ensures that duplicate heightmaps are not added to the database.

- `RemoveHeightmap(TerrainHeightmapMetadata heightmap)`: Removes a specified `TerrainHeightmapMetadata` instance from the `heightmaps` list if it exists. This function allows for the management of heightmaps by enabling their removal when they are no longer needed.

- `FindHeightmapByName(string name)`: Searches the `heightmaps` list for a `TerrainHeightmapMetadata` instance that matches the specified name. It returns the first matching heightmap, or `null` if no match is found. This function is useful for retrieving a specific heightmap by its name.

- `FindHeightmapsByTerrainType(string terrainType)`: Returns a list of `TerrainHeightmapMetadata` instances that match the specified terrain type. This function allows developers to filter heightmaps based on the type of terrain they represent.

- `FindHeightmapsByTag(string tag)`: Returns a list of `TerrainHeightmapMetadata` instances that contain the specified tag. This function helps in categorizing and retrieving heightmaps based on custom tags assigned to them.