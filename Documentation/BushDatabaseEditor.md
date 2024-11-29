# BushDatabaseEditor

## Overview
The `BushDatabaseEditor` script is a custom editor for the `BushDatabase` class in Unity. It enhances the Unity Inspector interface, allowing developers to manage a collection of bush metadata more efficiently. The main function of this script is to provide a user-friendly interface for searching, adding, displaying, and removing bush entries within the `BushDatabase`. This script is integral to the codebase as it simplifies interaction with bush data, making it easier for developers to maintain and update the database.

## Variables
- `searchFilter`: A string that holds the current text input for searching bushes by name.
- `searchTag`: A string that is declared but not utilized in the current implementation. It may be intended for future use or could be removed for clarity.

## Functions
- `OnInspectorGUI()`: This overridden method is responsible for rendering the custom GUI in the Unity Inspector for the `BushDatabase`. It includes:
  - A label for the database title.
  - A text field for searching bushes by their names.
  - A button to perform the search, which logs the names of bushes that match the search filter.
  - A button to add new bush metadata, creating a new instance of `BushMetadata` and saving it in the project.
  - A button to save the current state of the `BushDatabase`.
  - A section that lists all stored bushes, displaying each bush's name and associated file, along with buttons to select or remove the bush from the database. 

This structure ensures that users can easily navigate and manipulate the bush data directly from the Unity editor, enhancing productivity and usability.