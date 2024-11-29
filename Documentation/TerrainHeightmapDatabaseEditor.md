# TerrainHeightmapDatabaseEditor

## Overview
The `TerrainHeightmapDatabaseEditor` script is a custom editor for the `TerrainHeightmapDatabase` class in Unity. It provides a user interface in the Unity Editor that allows developers to manage heightmap data effectively. This editor enables users to search for heightmaps by name, add new heightmap metadata, save the database, and display the currently stored heightmaps with options to select or remove them. The script enhances the usability of the `TerrainHeightmapDatabase` by providing a clear and interactive way to manage heightmap assets.

## Variables
- `searchFilter`: A string that holds the user's input for searching heightmaps by name.
- `searchTag`: A string that is defined but not used in the current implementation; it could be intended for future filtering functionality.

## Functions
- `OnInspectorGUI()`: This is the main function that draws the custom inspector GUI in the Unity Editor. It includes:
  - A label for the "Terrain Heightmap Database".
  - A text field for entering the search term for heightmaps.
  - A button to trigger the search, which logs the names of heightmaps that contain the search term.
  - A button to add new heightmap metadata, which creates a new instance of `TerrainHeightmapMetadata`, saves it as an asset, and adds it to the database.
  - A button to save the database, marking it as dirty to ensure changes are saved.
  - A section that lists all stored heightmaps, providing buttons to select or remove each heightmap from the database.