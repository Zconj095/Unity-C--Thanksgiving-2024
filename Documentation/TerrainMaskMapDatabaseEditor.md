# TerrainMaskMapDatabaseEditor

## Overview
The `TerrainMaskMapDatabaseEditor` script is a custom editor for the `TerrainMaskMapDatabase` class in Unity. It provides a user interface within the Unity Editor that allows developers to manage terrain mask maps efficiently. This includes searching for existing mask maps, adding new mask map metadata, saving the database, and displaying the stored mask maps. The script enhances the usability of the `TerrainMaskMapDatabase` by providing an intuitive GUI for developers to interact with terrain mask maps.

## Variables
- `searchFilter`: A string variable that holds the current search term used to filter mask maps by name.
- `searchTag`: A string variable intended for future use, possibly to filter mask maps by tags (currently not utilized in the script).

## Functions
- `OnInspectorGUI()`: This is an overridden method from the `Editor` class that defines how the custom inspector GUI is rendered. It includes:
  - A label for the database.
  - A text field for searching mask maps by their name.
  - A button to execute the search, which logs found mask maps that match the search term.
  - A button to add new mask map metadata, creating a new instance of `TerrainMaskMapMetadata` and adding it to the database.
  - A button to save changes made to the database.
  - A section that displays all stored mask maps, with options to select or remove each mask map, updating the database accordingly.