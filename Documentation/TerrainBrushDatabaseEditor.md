# TerrainBrushDatabaseEditor

## Overview
The `TerrainBrushDatabaseEditor` script is a custom editor for the `TerrainBrushDatabase` class in Unity. It provides a user interface within the Unity Editor that allows developers to manage terrain brushes effectively. This script enables users to search for brushes by name, add new brush metadata, save the database, and display the list of stored brushes. It enhances the usability of the `TerrainBrushDatabase` by providing a streamlined way to interact with brush assets.

## Variables

- `searchFilter`: A string variable that holds the input for the brush name search filter. It is used to filter the displayed brushes based on user input.
- `searchTag`: A string variable that is declared but not utilized in the current implementation. It could potentially be used for tagging brushes in future enhancements.

## Functions

- `OnInspectorGUI()`: This is an overridden method from the `Editor` class that is responsible for rendering the custom inspector GUI for the `TerrainBrushDatabase`. It includes:
  - A label for the database.
  - A search field for filtering brushes by name.
  - A button to execute the search, which logs the names of brushes that match the search filter.
  - A button to add new brush metadata, which creates a new instance of `TerrainBrushMetadata`, saves it as an asset, and adds it to the database.
  - A button to save changes made to the database, marking it as dirty to ensure the changes are persisted.
  - A section that displays all stored brushes with options to select or remove each brush from the database. Each brush is displayed with its name and buttons to interact with it.