# TerrainDatabaseEditor

## Overview
The `TerrainDatabaseEditor` script is a custom Unity editor designed to manage a `TerrainDatabase`. Its primary function is to provide a user-friendly interface within the Unity Editor for searching, adding, saving, and displaying terrain metadata. This script enhances the workflow for developers by allowing them to interact with terrain data directly from the Inspector window, thereby streamlining the process of managing terrain assets in the game.

## Variables
- **searchFilter**: A string that holds the user input for filtering terrains by their names. This allows users to search for specific terrains in the database.
- **searchBiome**: A string placeholder that is currently defined but not utilized in the script. It may be intended for future functionality related to biome searching.

## Functions
- **OnInspectorGUI()**: This is an overridden method from the `Editor` class that defines the custom GUI for the `TerrainDatabase` in the Inspector. It includes:
  - A label for the "Terrain Database".
  - A text field for users to input a name to search for terrains.
  - A button that, when clicked, searches through the `database.terrains` for any terrain whose name contains the search filter string.
  - A button for adding new terrain metadata, which creates a new instance of `TerrainMetadata`, saves it as an asset, and adds it to the terrain database.
  - A button to save the current state of the terrain database, marking it as dirty to ensure changes are recorded.
  - A section that displays the names of stored terrains, allowing users to select or remove them through corresponding buttons. Each terrain is displayed alongside "Select" and "Remove" buttons for easy management.