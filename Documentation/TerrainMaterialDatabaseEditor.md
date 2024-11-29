# TerrainMaterialDatabaseEditor

## Overview
The `TerrainMaterialDatabaseEditor` script is a custom Unity editor that provides an interface for managing a `TerrainMaterialDatabase`. This editor allows users to search for materials by name, add new material metadata, save the database, and display a list of stored materials. It enhances the usability of the `TerrainMaterialDatabase` by providing a graphical interface for developers to interact with the material data, making it easier to manage and organize terrain materials within a Unity project.

## Variables
- **searchFilter**: A string variable that holds the current search term entered by the user to filter materials by name.
- **searchTag**: (This variable is declared but not used in the script. It may have been intended for future functionality related to tagging materials.)

## Functions
- **OnInspectorGUI()**: This method overrides the default inspector GUI for the `TerrainMaterialDatabase`. It is responsible for rendering the custom editor interface, which includes:
  - A label for the Terrain Material Database.
  - An input field for searching materials by name.
  - A button that, when clicked, searches the materials in the database and logs any matches to the console.
  - A button to add new material metadata, which creates a new instance of `TerrainMaterialMetadata`, saves it as an asset, and adds it to the database.
  - A button to save the current state of the database.
  - A section that displays the names of stored materials, along with buttons to select or remove each material from the database.