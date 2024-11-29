# CrystalDatabaseEditor

## Overview
The `CrystalDatabaseEditor` script is a custom editor for the `CrystalDatabase` class in Unity. It provides a user-friendly interface in the Unity Inspector that allows developers to manage a collection of crystal metadata. The main functions of this script include searching for crystals by name, adding new crystal metadata, saving the database, and displaying the list of stored crystals with options to select or remove them. This script enhances the usability of the `CrystalDatabase` class by providing a visual interface for interacting with crystal data.

## Variables
- **searchFilter**: A string variable that holds the current input value for searching crystals by name.
- **searchTag**: A string variable intended for future use, but currently not utilized within this script.

## Functions
- **OnInspectorGUI()**: This method overrides the default inspector GUI to create a custom layout for the `CrystalDatabase`. It includes:
  - A label for the database title.
  - A text field for entering the search filter.
  - A button that, when clicked, searches the `crystals` list in the `CrystalDatabase` for any crystal whose name contains the search filter and logs the result.
  - A button that allows the user to add new crystal metadata by creating a new instance of `CrystalMetadata` and saving it as an asset.
  - A button to save changes made to the `CrystalDatabase`.
  - A section that displays all stored crystals, with each crystal having buttons to select or remove it from the database.