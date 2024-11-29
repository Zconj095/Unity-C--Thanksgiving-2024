# HumanAnimationDatabaseEditor

## Overview
The `HumanAnimationDatabaseEditor` script is a custom editor for the `HumanAnimationDatabase` class in Unity. It provides a user interface within the Unity Editor that allows developers to manage animations stored in a `HumanAnimationDatabase`. This script enables searching for animations by name, adding new animation metadata, saving the database, and displaying the list of stored animations with options to select or remove them. It enhances the workflow of managing animations by providing a more intuitive interface.

## Variables
- `searchFilter`: A string variable that holds the current input for searching animations by name.
- `searchTag`: A string variable intended for future use, potentially for tagging animations (currently not used in the script).

## Functions
- `OnInspectorGUI()`: This is the main function that draws the custom inspector GUI for the `HumanAnimationDatabase`. It includes:
  - A label for the database.
  - A search field for filtering animations by name.
  - A button to execute the search, which logs found animations to the console.
  - A button to add new animation metadata, creating a new instance of `HumanAnimationMetadata` and adding it to the database.
  - A button to save the database, marking it as dirty to ensure changes are saved.
  - A section that displays all stored animations, providing buttons to select or remove each animation from the database.