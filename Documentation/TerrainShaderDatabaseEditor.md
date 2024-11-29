# TerrainShaderDatabaseEditor

## Overview
The `TerrainShaderDatabaseEditor` script is a custom editor for the `TerrainShaderDatabase` class in Unity. It provides a user interface within the Unity Editor to manage terrain shader metadata. This script allows users to search for shaders by name, add new shader metadata, save the database, and display a list of stored shaders with options to select or remove them. It enhances the usability of the `TerrainShaderDatabase` by providing a graphical interface for shader management.

## Variables
- `searchFilter`: A string variable that holds the current search term entered by the user to filter shaders by name.

## Functions
- `OnInspectorGUI()`: This function overrides the default inspector GUI method. It defines the layout and functionality of the custom editor interface for the `TerrainShaderDatabase`. Within this function:
  - Displays the title "Terrain Shader Database".
  - Provides a text field for searching shaders by name.
  - Implements a button that triggers a search through the shaders in the database, logging the names of any shaders that match the search filter.
  - Adds a button to create a new instance of `TerrainShaderMetadata`, saves it as an asset, and adds it to the database.
  - Includes a button to save changes to the database.
  - Lists all stored shaders, providing buttons to select or remove each shader from the database.