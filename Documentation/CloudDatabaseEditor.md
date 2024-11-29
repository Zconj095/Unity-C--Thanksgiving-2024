# CloudDatabaseEditor

## Overview
The `CloudDatabaseEditor` script is a custom editor for the `CloudDatabase` class in Unity, allowing developers to manage cloud metadata directly from the Unity Inspector. This script enhances the usability of the `CloudDatabase` by providing a user-friendly interface for searching, adding, saving, and displaying cloud entries. It integrates seamlessly into the Unity Editor, enabling developers to efficiently manage cloud assets.

## Variables
- `searchFilter`: A string that holds the current input for filtering clouds by their names. This allows users to search for specific clouds in the database.
- `searchTag`: A string intended for future use, possibly to filter clouds by tags (currently not utilized in the script).

## Functions
- `OnInspectorGUI()`: This overridden method is responsible for rendering the custom inspector GUI for the `CloudDatabase`. It includes:
  - A label for the database title.
  - A text field for inputting a search term to find clouds by name.
  - A button that, when clicked, searches through the `clouds` collection in the `CloudDatabase` and logs any matches found.
  - A button to create and add new cloud metadata to the database, which creates a new instance of `CloudMetadata` and saves it as an asset.
  - A button to save any changes made to the `CloudDatabase`, ensuring that all modifications are persisted.
  - A section that displays all stored clouds, with options to select or remove each cloud from the database. Each cloud's name and associated file (if available) are shown, along with buttons for interaction.